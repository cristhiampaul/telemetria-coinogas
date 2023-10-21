using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Data;
using System.Linq;
using System.Globalization;

namespace Telemetria
{
    public partial class main : Form
    {
        Scripts sc = new Scripts();
        String[] filas_en;
        String punto, escala, puerto, rata_modbus, timeout, id_modbus, tipo_request, cadena_request="";
        //1-dia 2-hora 3-acumulado
        //x-1 index x-2 valores
        int punto_id,frecuencia, contador_request, longitud;
        byte[] request_dia, request_hora, request_acumulado, request_modbus;
        //705-dia 706-hora 707-acumulado
        int registro_dia = 705, registro_hora = 706, registro_acumulado = 707;
        public main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            request_modbus = request_acumulado;
            temporizador.Enabled = true;
            temporizador.Start();
            contador_request = 1;
            tipo_request = "1-1";
            enviar_modbus(request_modbus, 18);
        }

        private void main_Load(object sender, EventArgs e)
        {
            puertoserial.DataReceived += new SerialDataReceivedEventHandler(DatareceivedHandler);
            estado1.Text = "";
            estado2.Text = "";
            estado3.Text = "";
            carga_remitentes();
            carga_puertos();
            read_config();
            carga_modbus();
            carga_datos();

            
        }

        private void carga_datos()
        {
            //0-fecha 1-hora 2-presion 3-temperatura 4-energia 5-volumen 6-masa 7-PC 8-Densidad 9-TempBatt 10-Rata   
            //Crear datatable para rellenar datagrid gasoductos
            DataTable table_dia = new DataTable("TablaDia");
            table_dia.Columns.Add(new DataColumn("Id", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Fecha", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Hora", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Presión", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Temp", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_dia.Columns.Add(new DataColumn("P. Cal.", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Densidad", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Voltaje", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Rata", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Aux1", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Aux2", typeof(string)));

            DataTable table_hora = new DataTable("TablaHora");
            table_hora.Columns.Add(new DataColumn("Id", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Fecha", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Hora", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Presión", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Temp", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_hora.Columns.Add(new DataColumn("P. Cal.", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Densidad", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Voltaje", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Rata", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Aux1", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Aux2", typeof(string)));

            DataTable table_acumulado = new DataTable("TablaAcumulado");
            table_acumulado.Columns.Add(new DataColumn("Id", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Fecha", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Hora", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Presión", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Temp", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("P. Cal.", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Densidad", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Voltaje", typeof(string))); 
            table_acumulado.Columns.Add(new DataColumn("Rata", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Aux1", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Aux2", typeof(string)));

            //Datos de los puntos
            String SqlStr_dia = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,aux1,aux2 FROM consumo3 Where punto=" + punto_id.ToString() + " AND tiporeg=1 Order by id Desc Limit 1";
            String SqlStr_hora = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,aux1,aux2 FROM consumo3 Where punto=" + punto_id.ToString() + " AND tiporeg=2 Order by id Desc Limit 1";
            String SqlStr_acumulado = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,aux1,aux2 FROM consumo3 Where punto=" + punto_id.ToString() + " AND tiporeg=3 Order by id Desc Limit 1";

            //DV para cada dg
            DataView dv_dia = sc.get_data(SqlStr_dia, 12, table_dia);
            DataView dv_hora = sc.get_data(SqlStr_hora, 12, table_hora);
            DataView dv_acumulado = sc.get_data(SqlStr_acumulado, 12, table_acumulado);

            dg_dia.DataSource = dv_dia;
            dg_hora.DataSource = dv_hora;
            dg_acumulado.DataSource = dv_acumulado;

            temporizador.Interval = int.Parse(timeout);
            //carga modbus

            string tipo_modbus = "03";
            string cantidad_modbus = "1";
            string[] indices = dg_modbus.Rows[0].Cells[0].Value.ToString().Split('-');
            string tipo = dg_modbus.Rows[0].Cells[1].Value.ToString();
            string hexa_id = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(id_modbus)), 1);
            string hexa_tipo = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(tipo_modbus)), 1);
            string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(cantidad_modbus)), 0);
            string hexa_dia = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[0])), 0);
            string hexa_hora = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[1])), 0);
            string hexa_acumulado = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[2])), 0);

            if (tipo == "1")
            {
                request_dia = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
                request_hora = sc.crear_request(hexa_id, hexa_tipo, hexa_hora, hexa_cantidad);
                request_acumulado = sc.crear_request(hexa_id, hexa_tipo, hexa_acumulado, hexa_cantidad);
            }
        }
        private void carga_remitentes()
        {
            String SqlStr_puntos = "SELECT id, nombre FROM puntos WHERE eliminado=0 order by id";
            filas_en = sc.traerD(SqlStr_puntos);
            for (int i = 0; i <= filas_en.Length - 2; i++)
            {
                String[] items = filas_en[i].Split('&');
                //llenar combobox op
                cb_puntos.Items.Add(items[0] + "- " + items[1]);
            }
        }

        private void carga_puertos()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                cb_puertos.Items.Add(s);
            }
        }


        private void carga_modbus()
        {
            //0-fecha 1-hora 2-presion 3-temperatura 4-energia 5-volumen 6-masa 7-PC 8-Densidad 9-TempBatt 10-Rata   
            //Crear datatable para rellenar datagrid gasoductos
            DataTable table_modbus = new DataTable("TablaModbus");
            table_modbus.Columns.Add(new DataColumn("Indice", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Tipo", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Fecha", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Hora", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Presion", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Temperatura", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("PC", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Densidad", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Rata", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("TempBat", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Aux1", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Aux2", typeof(string)));
            table_modbus.Columns.Add(new DataColumn("Ajuste", typeof(string)));

            //Datos de los puntos
            String SqlStr_modbus = "Select indice,tipo,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,rata,tempbat,aux1,aux2,ajuste FROM modbusindex Where punto=" + punto_id.ToString() + " Order by id ASC";

            //DV para cada dg
            DataView dv_ga = sc.get_data(SqlStr_modbus, 16, table_modbus);

            dg_modbus.DataSource = dv_ga;

        }

        private void bt_guardar_Click(object sender, EventArgs e)
        {
            write_config();
        }

        private void dg_modbus_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            //if user clicked Shift+Ins or Ctrl+V (paste from clipboard)
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
            {
                char[] rowSplitter = { '\r', '\n' };
                char[] columnSplitter = { '\t' };

                //get the text from clipboard
                IDataObject dataInClipboard = Clipboard.GetDataObject();
                string stringInClipboard = (string)dataInClipboard.GetData(DataFormats.Text);

                //split it into lines
                string[] rowsInClipboard = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries);

                //get the row and column of selected cell in grid
                int r = dg.SelectedCells[0].RowIndex;
                int c = dg.SelectedCells[0].ColumnIndex;

                // loop through the lines, split them into cells and place the values in the corresponding cell.
                for (int iRow = 0; iRow < rowsInClipboard.Length; iRow++)
                {
                    //split row into cell values
                    if (r + iRow < dg.Rows.Count)
                    {
                        string[] valuesInRow = rowsInClipboard[iRow].Split(columnSplitter);

                        //cycle through cell values
                        for (int iCol = 0; iCol < valuesInRow.Length; iCol++)
                        {
                            //assign cell value, only if it within columns of the grid

                            if (dg.ColumnCount - 1 >= c + iCol)
                            {
                                dg.Rows[r + iRow].Cells[c + iCol].Value = valuesInRow[iCol];
                            }
                        }
                    }
                }

            }
        }

        private void temporizador_Tick(object sender, EventArgs e)
        {
            
                estado2.Text += " -" + contador_request.ToString();
            if (contador_request >= 3)
            {
                temporizador.Enabled = false;
                temporizador.Stop();
                if (puertoserial.IsOpen)
                {
                    puertoserial.Close();
                    MessageBox.Show("Timeout");
                }
                estado2.Text = "Cerrado";
                estado3.Text = "";
            }
            else
            {
                contador_request += 1;
                estado2.Text += " -" + contador_request.ToString();
                enviar_modbus(request_modbus,longitud);
            }
        }
        private void cb_puntos_SelectedIndexChanged(object sender, EventArgs e)
        {
            punto = cb_puntos.Text;
            String[] tmp_punto = punto.Split('-');
            int.TryParse(tmp_punto[0], out punto_id);
            carga_modbus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //array de ajustes
            string ajuste = dg_modbus.Rows[0].Cells[15].Value.ToString();

            String[] factor = new string[15];
            if (ajuste != "")
            {
                String[] ajustes = ajuste.Split('-');
                foreach (string a in ajustes)
                {
                    String[] ajustes_factor = a.Split('$');
                    int tmp_ajuste = 0;
                    int.TryParse(ajustes_factor[0], out tmp_ajuste);
                    factor[tmp_ajuste] = ajustes_factor[1];
                }
            }

            string cadena = "01033047c7118044dae0005bf42642c92b45491bfb5949ec318a4461c3533d4609cdc309fdc0417ab88a4900000000000000000549";
            String[] datos_tmp = sc.obtener_datos(cadena);
            String[] datos = new string[15];
            int contador = 0;

            foreach (string x in datos_tmp)
            {
                contador += 1;

                list_logs.Items.Add(x);
            }
            int tmp = 0;
            string item = "";
            for (int i = 2; i < 14; i++)
            {
                item = dg_modbus.Rows[0].Cells[i].Value.ToString();
                //list_logs.Items.Add(dg_modbus.Rows[0].Cells[i].Value);
                if (item != "0")
                {
                    int.TryParse(item, out tmp);
                    datos[i] = datos_tmp[tmp - 1];
                    list_logs.Items.Add(datos[i].ToString());
                    if (factor[i-1] != null)
                    {
                        double tmp_factor = 0;
                        double tmp_valor = 0;
                        double.TryParse(factor[i-1], out tmp_factor);
                        double.TryParse(datos[i], out tmp_valor);
                        list_logs.Items.Add((tmp_factor*tmp_valor).ToString());
                    }
                }
            }
        }

        private void enviar_modbus(byte[] request_modbus,int longitud_respuesta)
        {
            longitud = longitud_respuesta;
            if (!puertoserial.IsOpen)
            {
                puertoserial.PortName = puerto;
                puertoserial.BaudRate = int.Parse(rata_modbus);
                try { puertoserial.Open(); }
                catch (Exception ex)
                {
                    estado1.Text = "No se pudo abrir el puerto: " + puerto;
                    //MessageBox.Show(ex.Message);
                    Console.Write(ex.Message);
                }
            }
           
            if (puertoserial.IsOpen)
            {
                this.puertoserial.Write(request_modbus, 0, request_modbus.Length);
                this.puertoserial.Encoding = System.Text.Encoding.GetEncoding(1252);
                estado1.Text = "Puerto: " + puerto;
                estado2.Text = " Intento: " + contador_request.ToString();
            }
        }
        private void DatareceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string cadena = "";
            try
            {
                this.puertoserial.Encoding = System.Text.Encoding.GetEncoding(1252);
                String indata = this.puertoserial.ReadExisting();
                System.Threading.Thread.Sleep(500);
                for (int i = 0; i < 3; i++)
                {
                    indata += this.puertoserial.ReadExisting();
                }
                Console.WriteLine("Data Received");
                cadena = sc.convertir_hexa(indata);
                Console.Write(cadena);
                //MessageBox.Show(cadena);
                estado1.Text = cadena.Length.ToString();
                list_logs.Items.Add(cadena);
                list_logs.Items.Add("long: " + cadena.Length.ToString());
                if (cadena.Length >= longitud)
                {
                    //MessageBox.Show(cadena);
                    if (puertoserial.IsOpen)
                    {
                        //this.temporizador.Enabled=false;
                        //this.temporizador.Stop();
                        puertoserial.Close();
                        estado1.Text = "Puerto Cerrado";
                    }
                    estado2.Text = "";
                    estado3.Text = "Respuesta: ok";
                    cadena_request = cadena;
                    this.Invoke(new EventHandler(procesar_modbus));
                    //procesar_modbus(cadena);
                }
                else
                {
                    estado3.Text = "Respuesta: corta";
                }
            }
            catch (Exception ex)
            {
                Console.Write("Error:");
                Console.Write(cadena);
                Console.Write(ex.Message);
            }
        }

        private void procesar_modbus(object s, EventArgs e) //(string cadena)
        {
            this.temporizador.Enabled = false;
            this.temporizador.Stop();
            string tipo_modbus = "03";
            string[] indices = dg_modbus.Rows[0].Cells[0].Value.ToString().Split('-');
            string tipo = dg_modbus.Rows[0].Cells[1].Value.ToString();
            string hexa_id = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(id_modbus)), 1);
            string hexa_tipo = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(tipo_modbus)), 1);
            int registro = 0;
            string cadena = cadena_request;

            if (tipo_request == "1-1")
            {
                registro = registro_dia;
                String hex = cadena.Substring(12, 2) + cadena.Substring(10, 2) + cadena.Substring(8, 2) + cadena.Substring(6, 2);
                var i = int.Parse(hex, NumberStyles.AllowHexSpecifier);
                var b = BitConverter.GetBytes(i);
                int index = int.Parse(BitConverter.ToSingle(b, 0).ToString());
                Console.Write("Index-dia: " + index.ToString());
                //request_dia = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
                string hexa_tipo_registro = sc.completar_hexa(string.Format("{0:x2}", registro), 0);
                string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", index), 0);
                request_modbus = sc.crear_request(hexa_id, hexa_tipo, hexa_tipo_registro, hexa_cantidad);
                estado2.Text = "";
                estado3.Text = "";

                contador_request = 0;
                temporizador.Interval = int.Parse(timeout);
                temporizador.Enabled = true;
                temporizador.Start();
                tipo_request = "1-2";
                enviar_modbus(request_modbus, 106);
                estado3.Text = tipo_request;
            }
            else if (tipo_request == "1-2")
            {
                estado3.Text = tipo_request;

                Console.Write("Datos-dia: " + cadena);
                MessageBox.Show(cadena);
                list_logs.Items.Add(cadena);
                tb_test.Text = cadena;
                String[] datos = sc.obtener_datos(cadena);
                foreach (string x in datos)
                {
                    list_logs.Items.Add(x);
                }

            }
        }

        //CONFIGURACION INICIAL DE LA APLICACION
        public void write_config()
        {
            string fileName = Application.StartupPath + "\\config.ini";

            punto = cb_puntos.Text;
            String[] tmp_punto = punto.Split('-');
            int.TryParse(tmp_punto[0],out punto_id);
            puerto = cb_puertos.Text;
            frecuencia = Convert.ToInt32(tb_frecuencia.Text);
            escala = cb_escala.Text;
            puertoserial.PortName = punto;
            rata_modbus = cb_rata.Text;
            timeout = tb_timeout.Text;
            id_modbus = tb_id.Text;

            StreamWriter writer = File.CreateText(fileName);
            writer.WriteLine("Telemetria - COINOGAS SA ESP - Coinotel" + writer.NewLine);
            writer.WriteLine("## Se debe definir punto, puerto de comunicaciones, frecuencia y escala de tiempo segun la siguiente instrucción: ##" + writer.NewLine +
                "## punto= id y nombre del punto como esta registrado en Balgas (ej. 2-Cusianagas)##" + writer.NewLine +
                "## punto= id del punto como esta registrado en Balgas (ej. 2)##" + writer.NewLine +
                "## puerto= nombre del puerto serial conectado para este punto (ej. COM1)##" + writer.NewLine +
                "## frecuencia= numero de minutos/horas de la frecuencia de toma de datos##" + writer.NewLine +
                "## escala= escala de tiempo para la frecuencia, minutos, horas o dias ##" + writer.NewLine + writer.NewLine);
            writer.WriteLine("punto:" + punto );
            writer.WriteLine("punto_id:" + punto_id.ToString());
            writer.WriteLine("puerto:" + puerto);
            writer.WriteLine("frecuencia:" + frecuencia);
            writer.WriteLine("escala:" + escala);
            writer.WriteLine("rata_modbus:" + rata_modbus);
            writer.WriteLine("timeout:" + timeout);
            writer.WriteLine("id_modbus:" + id_modbus);
            writer.Close();

           
            DataGridViewRow myRow = dg_modbus.Rows[0];
            string indice = myRow.Cells[0].Value.ToString();
            string tipo = myRow.Cells[1].Value.ToString();
            string fecha = myRow.Cells[2].Value.ToString();
            string hora = myRow.Cells[3].Value.ToString();
            string presion = myRow.Cells[4].Value.ToString();
            string temperatura = myRow.Cells[5].Value.ToString();
            string energia = myRow.Cells[6].Value.ToString();
            string volumen = myRow.Cells[7].Value.ToString();
            string masa = myRow.Cells[8].Value.ToString();
            string poder = myRow.Cells[9].Value.ToString();
            string densidad = myRow.Cells[10].Value.ToString();
            string rata = myRow.Cells[11].Value.ToString();
            string tempbat = myRow.Cells[12].Value.ToString();
            string aux1 = myRow.Cells[13].Value.ToString();
            string aux2 = myRow.Cells[14].Value.ToString();

            //Consulta SQL 
            //Set de la Consulta SQL
            String SqlStr = punto_id + "," + indice + "," + tipo + "," + fecha + "," + hora + "," + presion + "," + temperatura + "," + energia + "," + volumen + "," 
                + masa + "," + poder + "," + densidad + "," + rata + "," + tempbat + "," + aux1 + "," + aux2 +"";

            String rta = sc.enviarN("mod", SqlStr) ;
          
            this.Text = "Telemetria     -   punto:" + punto;
            carga_modbus();
            MessageBox.Show("Actualzado Correctamente");
            notifyIcon1.Text = punto;
        }

        public void read_config()
        {
            string fileName = Application.StartupPath + "\\config.ini";
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                char[] x = { ':' }; // delimitador

                while (!reader.EndOfStream)
                {
                    string[] campos = reader.ReadLine().Split(x);
                    
                    if (campos[0] == "punto")
                    {
                        punto = campos[1];
                        cb_puntos.Text = punto;
                    }else if (campos[0] == "punto_id")
                    {
                        int.TryParse(campos[1], out punto_id);
                    }else if (campos[0] == "puerto")
                    {
                        puerto = campos[1];
                        cb_puertos.Text = puerto;
                        puertoserial.PortName = puerto;
                    }else if (campos[0] == "frecuencia")
                    {
                        int.TryParse(campos[1], out frecuencia);
                        tb_frecuencia.Text= frecuencia.ToString();
                    }else if (campos[0] == "escala")
                    {
                        escala = campos[1];
                        cb_escala.Text = escala;
                    }
                    else if (campos[0] == "rata_modbus")
                    {
                        rata_modbus = campos[1];
                        cb_rata.Text = rata_modbus;
                    }
                    else if (campos[0] == "timeout")
                    {
                        timeout = campos[1];
                        tb_timeout.Text = timeout;
                    }
                    else if (campos[0] == "id_modbus")
                    {
                        id_modbus = campos[1];
                        tb_id.Text = id_modbus;
                    }
                }
                reader.Close();
                stream.Close();
                this.Text = "Telemetria     -   punto:" + punto;
                notifyIcon1.Text = punto;

            }
            else
            {              
                punto = "";
                punto_id = 0;
                frecuencia = 1;
                escala = "Minutos";
                puerto = "COM0";

                write_config();
            }
        }

    }
}
