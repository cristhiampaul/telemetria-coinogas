using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Data;
using System.Linq;

namespace Telemetria
{
    public partial class main : Form
    {
        Scripts sc = new Scripts();
        String[] filas_en;
        String punto, escala, puerto, rata_modbus, timeout, id_modbus;
        int punto_id,frecuencia,contador_response, contador_request;
        byte[] request_dia, request_hora, request_acumulado ;
        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
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
            table_dia.Columns.Add(new DataColumn("Temperatura", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_dia.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_dia.Columns.Add(new DataColumn("P. Calorifico", typeof(string)));
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
            table_hora.Columns.Add(new DataColumn("Temperatura", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_hora.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_hora.Columns.Add(new DataColumn("P. Calorifico", typeof(string)));
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
            table_acumulado.Columns.Add(new DataColumn("Temperatura", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Energia", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Volumen", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Masa", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("P. Calorifico", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Densidad", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Voltaje", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Rata", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Aux1", typeof(string)));
            table_acumulado.Columns.Add(new DataColumn("Aux2", typeof(string)));

            //Datos de los puntos
            String SqlStr_dia = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,aux1,aux2 FROM consumo2 Where punto=" + punto_id.ToString() + " AND tiporeg=1 Order by id Desc Limit 1";
            String SqlStr_hora = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,aux1,aux2 FROM consumo2 Where punto=" + punto_id.ToString() + " AND tiporeg=2 Order by id Desc Limit 1";
            String SqlStr_acumulado = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,aux1,aux2 FROM consumo2 Where punto=" + punto_id.ToString() + " AND tiporeg=3 Order by id Desc Limit 1";

            //DV para cada dg
            DataView dv_dia = sc.get_data(SqlStr_dia, 12, table_dia);
            DataView dv_hora = sc.get_data(SqlStr_hora, 12, table_hora);
            DataView dv_acumulado = sc.get_data(SqlStr_acumulado, 12, table_acumulado);

            dg_dia.DataSource = dv_dia;
            dg_hora.DataSource = dv_hora;
            dg_acumulado.DataSource = dv_acumulado;
            //carga modbus
            
            string tipo_modbus = "03";
            string cantidad_modbus = "1";
            string[] indices = dg_modbus.Rows[0].Cells[0].Value.ToString().Split('-');
            string tipo = dg_modbus.Rows[0].Cells[1].Value.ToString();
            string hexa_id = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(id_modbus)), 1);
            string hexa_tipo = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(tipo_modbus)), 1);
            string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(cantidad_modbus)), 0);
            string hexa_dia = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[0])), 0);

            if (tipo == "1")
            {
                request_dia = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
                request_hora = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
                request_acumulado = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
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

            //Datos de los puntos
            String SqlStr_modbus = "Select indice,tipo,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,rata,tempbat,aux1,aux2 FROM modbusindex Where punto=" + punto_id.ToString() + " Order by id ASC";

            //DV para cada dg
            DataView dv_ga = sc.get_data(SqlStr_modbus, 15, table_modbus);

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
            if (contador_request >= 3)
            {
                temporizador.Enabled = false;
                if (puertoserial.IsOpen)
                {
                    temporizador.Enabled = false;
                    puertoserial.Close();
                    MessageBox.Show("Timeout");
                }
            }
            else
            {
                contador_request += 1;
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
            puertoserial.PortName = puerto;
            puertoserial.BaudRate = int.Parse(rata_modbus);
            
            puertoserial.DataReceived += new SerialDataReceivedEventHandler(DatareceivedHandler);
            try { puertoserial.Open(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (puertoserial.IsOpen)
            {
                this.puertoserial.Write(request_dia, 0, request_dia.Length);
                this.puertoserial.Encoding = System.Text.Encoding.GetEncoding(1252);
                temporizador.Interval = int.Parse(timeout);
                temporizador.Enabled = true;
                contador_request = 0;
            }
          
        }

        private void DatareceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;
            String indata = sp.ReadExisting();
            Console.WriteLine("Data Received");
            Console.Write(indata);
            string cadena = sc.convertir_hexa(indata);
            if (cadena.Length > 12)
            {

                temporizador.Enabled = false;
                MessageBox.Show(cadena);
                if (puertoserial.IsOpen)
                {
                    puertoserial.Close();
                }

            }
            else
            {

                status1.Text="conteo:" + contador_request.ToString();
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
