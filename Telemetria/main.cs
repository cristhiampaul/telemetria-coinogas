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
        String[] filas_en, factor = new string[15];
        String punto, escala, puerto, rata_modbus, timeout, id_modbus, tipo_request, cadena_request = "", intentos;
        string hexa_dia_registros, hexa_hora_registros, hexa_acumulado_registros;
        //1-dia 2-hora 3-acumulado
        //x-1 index x-2 valores
        int punto_id,frecuencia, contador_request, longitud, frecuencia_global;
        int tipo_global = 1, tipo_modbus=1;
        byte[] request_dia, request_hora, request_acumulado, request_modbus;
        //705-dia 706-hora 707-acumulado
        public main()
        {
            InitializeComponent();
        }


        private void main_Load(object sender, EventArgs e)
        {
            puertoserial.DataReceived += new SerialDataReceivedEventHandler(DatareceivedHandler);
            estatus11.Text = "";
            estatus12.Text = "";
            estatus13.Text = "";
            estatus21.Text = "";
            estatus22.Text = "";
            estatus23.Text = "";
            carga_remitentes();
            carga_puertos();
            read_config();
            carga_modbus();
            carga_datos();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //request_modbus = request_dia;
            //temporizador.Enabled = true;
            //temporizador.Start();
            //contador_request = 1;
            //tipo_request = "1-1";
            //enviar_modbus(request_modbus, 18);
            Console.Out.WriteLine("test");
           
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tipo_request = "3-2";
            string cadena = "01033047c80b8044d480007f7a1d429cf6394906974d49ec318a4461c3533d4609cdc3c7bdc041c8a38f490000000000000000a03e";
            preparar_correo();
            //guardar_datos(cadena);

        }

        //SERVICIO
        private void bt_servicio_Click(object sender, EventArgs e)
        {
            if (bt_servicio.Text == "Iniciar")
            {
                bt_servicio.Text = "Detener";
                temporizador_global_Tick(this, e);
                temporizador_global.Enabled = true;
                temporizador_global.Start();
            }else if(bt_servicio.Text == "Detener")
            {
                bt_servicio.Text = "Iniciar";
                temporizador_global.Enabled = false;
                temporizador_global.Stop();
            }
        }

        //MODBUS
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
                    estatus11.Text = "No se pudo abrir el puerto: " + puerto;
                    //MessageBox.Show(ex.Message);
                    Console.Write(ex.Message);
                }
            }
           
            if (puertoserial.IsOpen)
            {
                this.puertoserial.Write(request_modbus, 0, request_modbus.Length);
                this.puertoserial.Encoding = System.Text.Encoding.GetEncoding(1252);
                estatus11.Text = "Puerto: " + puerto;
                estatus12.Text = " Intento: " + contador_request.ToString();
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
            string cadena = cadena_request;

            if (tipo_request == "1-1")
            {
               
                String hex = cadena.Substring(12, 2) + cadena.Substring(10, 2) + cadena.Substring(8, 2) + cadena.Substring(6, 2);
                var i = int.Parse(hex, NumberStyles.AllowHexSpecifier);
                var b = BitConverter.GetBytes(i);
                int index = int.Parse(BitConverter.ToSingle(b, 0).ToString());
                Console.Write("Index-dia: " + index.ToString());
                //request_dia = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
                
                string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", index), 0);
                request_modbus = sc.crear_request(hexa_id, hexa_tipo, hexa_dia_registros, hexa_cantidad);
                estatus22.Text = "";
                estatus23.Text = "";

                contador_request = 0;
                temporizador.Interval = int.Parse(timeout);
                temporizador.Enabled = true;
                temporizador.Start();
                tipo_request = "1-2";
                enviar_modbus(request_modbus, 106);
                list_logs.Items.Add(tipo_request);
                list_logs.Items.Add(hexa_id+ hexa_tipo+ hexa_dia_registros + hexa_cantidad);
                estatus23.Text = tipo_request;
            }
            else if (tipo_request == "1-2")
            {
                estatus23.Text = tipo_request;

                Console.Write("Datos-dia: " + cadena);
                //MessageBox.Show(cadena);
                tb_test.Text = cadena;
                guardar_datos(cadena);

                //hora
                contador_request = 0;
                request_modbus = request_hora;
                temporizador.Enabled = true;
                temporizador.Start();
                contador_request = 1;
                tipo_request = "2-1";
                enviar_modbus(request_modbus, 18);
                list_logs.Items.Add(tipo_request);
            }
            else if (tipo_request == "2-1")
            {
                String hex = cadena.Substring(12, 2) + cadena.Substring(10, 2) + cadena.Substring(8, 2) + cadena.Substring(6, 2);
                var i = int.Parse(hex, NumberStyles.AllowHexSpecifier);
                var b = BitConverter.GetBytes(i);
                int index = int.Parse(BitConverter.ToSingle(b, 0).ToString());
                Console.Write("Index-dia: " + index.ToString());
                string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", index), 0);
                request_modbus = sc.crear_request(hexa_id, hexa_tipo, hexa_hora_registros, hexa_cantidad);
                estatus22.Text = "";
                estatus23.Text = "";

                contador_request = 0;
                temporizador.Interval = int.Parse(timeout);
                temporizador.Enabled = true;
                temporizador.Start();
                tipo_request = "2-2";
                enviar_modbus(request_modbus, 106);
                list_logs.Items.Add(hexa_id+ hexa_tipo+ hexa_hora_registros + hexa_cantidad);
                estatus23.Text = tipo_request;
            }
            else if (tipo_request == "2-2")
            {
                estatus23.Text = tipo_request;

                Console.Write("Datos-dia: " + cadena);
                //MessageBox.Show(cadena);
                //list_logs.Items.Add(cadena);
                tb_test.Text = cadena;
                guardar_datos(cadena);
                //acumulado
                contador_request = 0;
                request_modbus = request_acumulado;
                temporizador.Enabled = true;
                temporizador.Start();
                contador_request = 1;
                tipo_request = "3-1";
                enviar_modbus(request_modbus, 18);
                list_logs.Items.Add(tipo_request);
                list_logs.Items.Add(request_modbus);

            }
            else if (tipo_request == "3-1")
            {
                String hex = cadena.Substring(12, 2) + cadena.Substring(10, 2) + cadena.Substring(8, 2) + cadena.Substring(6, 2);
                var i = int.Parse(hex, NumberStyles.AllowHexSpecifier);
                var b = BitConverter.GetBytes(i);
                int index = int.Parse(BitConverter.ToSingle(b, 0).ToString());
                Console.Write("Index-dia: " + index.ToString());
                string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", index), 0);
                request_modbus = sc.crear_request(hexa_id, hexa_tipo, hexa_acumulado_registros, hexa_cantidad);
                estatus22.Text = "";
                estatus23.Text = "";

                contador_request = 0;
                temporizador.Interval = int.Parse(timeout);
                temporizador.Enabled = true;
                temporizador.Start();
                tipo_request = "3-2";
                enviar_modbus(request_modbus, 106);
                list_logs.Items.Add(hexa_id + hexa_tipo + hexa_acumulado_registros + hexa_cantidad);
                list_logs.Items.Add("cadena: " + cadena);
                list_logs.Items.Add("index: " +index);
                estatus23.Text = tipo_request;
            }
            else if (tipo_request == "3-2")
            {
                estatus23.Text = tipo_request;

                Console.Write("Datos-dia: " + cadena);
                //MessageBox.Show(cadena);
                //list_logs.Items.Add(cadena);
                tb_test.Text = cadena;
                guardar_datos(cadena);
                //preparar_correo();



            }
        }

        //TEMPORIZADORES
        private void temporizador_Tick(object sender, EventArgs e)
        {
            int intentos_conexion = 3;
            int.TryParse(intentos, out intentos_conexion);
            estatus12.Text = " -" + contador_request.ToString();
            if (contador_request >= intentos_conexion)
            {
                temporizador.Enabled = false;
                temporizador.Stop();
                if (puertoserial.IsOpen)
                {
                    puertoserial.Close();
                    //MessageBox.Show("Timeout");
                    estatus22.Text = "Timeout";
                    list_logs.Items.Add("Timeout");
                }
                estatus22.Text = "Cerrado";
                estatus23.Text = "";
            }
            else
            {
                contador_request += 1;
                estatus22.Text += " -" + contador_request.ToString();
                enviar_modbus(request_modbus, longitud);
            }
        }

        private void temporizador_global_Tick(object sender, EventArgs e)
        {
            string minuto = DateTime.Now.ToString("mm");
            string hora = DateTime.Now.ToString("HH");
            int minuto_ejecucion = 0, minuto_diez_ejecucion=0;
            int.TryParse(minuto, out minuto_ejecucion);
            string minuto_diez="0";
            if (minuto.Length > 0)
            {
                minuto_diez = minuto[1].ToString();
            }
            int.TryParse(minuto_diez, out minuto_diez_ejecucion);
            int hora_ejecucion = 0;
            int.TryParse(hora, out hora_ejecucion);
            Console.Out.WriteLine(DateTime.Now.ToString());

            estatus21.Text = "Check: " + DateTime.Now;
            estatus22.Text = "Freq: "+escala;

            if (minuto_ejecucion < 15)
            {
                preparar_correo();
            }
            if (escala == "Minutos")
            {
                ejecucion();
            }
            else if (escala == "10Minutos")
            {                
                if (minuto_diez_ejecucion == frecuencia)
                {
                    ejecucion();
                }
            }
            else if (escala == "Horas")
            {
                if (minuto_ejecucion == frecuencia)
                {
                    ejecucion();
                }
            }
            else if (escala == "Dias")
            {
                if ((minuto_ejecucion == frecuencia) && (hora_ejecucion == 0))
                {
                    ejecucion();
                }
            }
        }

        private void ejecucion()
        {
            list_logs.Items.Clear();
            list_logs.Items.Add(DateTime.Now);
            longitud = 18;
            estatus23.Text = "Tipo: " + tipo_global.ToString();

            if (tipo_global == 1)
            {
                if (check(tipo_global))
                {
                    tipo_global = 2;
                    list_logs.Items.Add("Existe - Dia");
                }
                else
                {
                    request_modbus = request_dia;
                    list_logs.Items.Add("Dia");
                    tipo_request = "1-1";
                }
            }
            if (tipo_global == 2)
            {
                if (check(tipo_global))
                {
                    tipo_global = 3;
                    list_logs.Items.Add("Existe - Hora");
                }
                else
                {
                    request_modbus = request_hora;
                    list_logs.Items.Add("Hora");
                    tipo_request = "2-1";
                }
            }
            if (tipo_global == 3)
            {
                request_modbus = request_acumulado;
                list_logs.Items.Add("Acumulado");
                tipo_request = "3-1";
                tipo_global = 1;
            }
            temporizador.Enabled = true;
            temporizador.Start();
            contador_request = 1;
            enviar_modbus(request_modbus, longitud);
        }
        private Boolean check(int tipo)
        {

            string fecha_actual = DateTime.Now.ToString("yyyy-MM-dd");
            string hora_actual = DateTime.Now.ToString("HH");
            Boolean rta = false;
            DataGridView dg=dg_acumulado;
            switch (tipo)
            {
                case 1:
                    dg = dg_dia;
                    hora_actual = "00";
                    break;
                case 2:
                    dg = dg_hora;
                    break;
                default:
                    break;
            }

            try
            {
                string fecha= dg.Rows[0].Cells[1].Value.ToString();
                string hora = dg.Rows[0].Cells[2].Value.ToString();

                string[] array_fecha = fecha.Split('-');
                string[] array_hora = hora.Split(':');


                if ((fecha_actual==fecha) && (hora_actual == array_hora[0])){
                    rta = true;
                }

            }
            catch (Exception ex)
            {
                list_logs.Items.Add(ex.Message);
            }
            return rta;
        }
        //CONFIGURACION DEL PUERTO
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
                estatus21.Text = cadena.Length.ToString();
                //list_logs.Items.Add(cadena);
                list_logs.Items.Add("long: " + cadena.Length.ToString());
                if (cadena.Length >= longitud)
                {
                    //MessageBox.Show(cadena);
                    if (puertoserial.IsOpen)
                    {
                        //this.temporizador.Enabled=false;
                        //this.temporizador.Stop();
                        puertoserial.Close();
                        estatus21.Text = "Puerto Cerrado";
                    }
                    estatus22.Text = "";
                    estatus23.Text = "Respuesta: ok";
                    cadena_request = cadena;
                    this.Invoke(new EventHandler(procesar_modbus));
                    //procesar_modbus(cadena);
                }
                else
                {
                    estatus23.Text = "Respuesta: corta";
                    tb_test.Text = cadena;
                }
            }
            catch (Exception ex)
            {
                Console.Write("Error:");
                Console.Write(cadena);
                Console.Write(ex.Message);
            }
        }

        //BOTON DE GUARDAR CONFIG
        private void bt_guardar_Click(object sender, EventArgs e)
        {
            write_config();
        }

        private void guardar_datos(string cadena)
        {
            String[] datos_tmp = sc.obtener_datos(cadena);
            String[] datos = new string[15];
            String SqlStr = punto_id.ToString();
            int tmp = 0;
            string item = "";
            foreach (string x in datos_tmp)
            {
                //list_logs.Items.Add(x);
            }
            for (int i = 2; i < 16; i++)
            {
                item = dg_modbus.Rows[0].Cells[i].Value.ToString();
                if ((item != "0") && (item != "NULL") && (i<15))
                {
                    int.TryParse(item, out tmp);
                    datos[i - 2] = datos_tmp[tmp - 1];
                    if (factor[i - 1] != null)
                    {
                        double tmp_factor = 0;
                        double tmp_valor = 0;
                        double.TryParse(factor[i - 1], out tmp_factor);
                        double.TryParse(datos[i - 2], out tmp_valor);
                        datos[i - 2] = (tmp_factor * tmp_valor).ToString();
                    }
                    // list_logs.Items.Add(datos[i - 2].ToString());
                }
                if ((i == 2) && (datos[0] == null)) { datos[0] = DateTime.Now.ToString("yyyy-MM-dd") ; }
                if ((i == 3) && (datos[1] == null))
                {
                    if (tipo_request == "1-2")
                    {
                        datos[1] = "00:00";
                    }
                    else if (tipo_request == "2-2")
                    {
                        datos[1] =  DateTime.Now.ToString("HH:00")  ;
                    }
                    else if (tipo_request == "3-2")
                    {
                        datos[1] =  DateTime.Now.ToString("HH:mm") ;
                    }
                }

                if (i == 15)
                {
                    if (tipo_request == "1-2")
                    {
                        datos[13] = "1";
                    }
                    else if (tipo_request == "2-2")
                    {
                        datos[13] = "2";
                    }
                    else if (tipo_request == "3-2")
                    {
                        datos[13] = "3";
                    }
                }
                SqlStr += "," + datos[i - 2];

            }
            //Consulta SQL         
            String rta = sc.enviarN("con3", SqlStr);
            carga_datos();
            list_logs.Items.Add("Guardado correctamente");

        }

        //CONFIGURACION INICIAL DE LA APLICACION
        public void write_config()
        {
            string fileName = Application.StartupPath + "\\config.ini";
            punto = cb_puntos.Text;
            String[] tmp_punto = punto.Split('-');
            int.TryParse(tmp_punto[0],out punto_id);
            puerto = cb_puertos.Text;
            frecuencia = Convert.ToInt32(cb_minuto.Text);
            escala = cb_escala.Text;
            puertoserial.PortName = punto;
            rata_modbus = cb_rata.Text;
            timeout = tb_timeout.Text;
            id_modbus = tb_id.Text;
            intentos = cb_intentos.Text;
            if (escala == "Minutos")
            {
                frecuencia_global = frecuencia * 60000;
            }else if (escala == "Horas")
            {
                frecuencia_global = frecuencia * 3600000;
            }else if (escala == "Dias")
            {
                frecuencia_global = frecuencia * 86400000;
            }


            StreamWriter writer = File.CreateText(fileName);
            writer.WriteLine("Telemetria - COINOGAS SA ESP - Coinotel" + writer.NewLine);
            writer.WriteLine("## Se debe definir punto, puerto de comunicaciones, frecuencia y escala de tiempo segun la siguiente instrucción: ##" + writer.NewLine +
                "## punto= id y nombre del punto como esta registrado en Balgas (ej. 2-Cusianagas)##" + writer.NewLine +
                "## punto= id del punto como esta registrado en Balgas (ej. 2)##" + writer.NewLine +
                "## puerto= nombre del puerto serial conectado para este punto (ej. COM1)##" + writer.NewLine +
                "## frecuencia= numero de minutos/horas de la frecuencia de toma de datos##" + writer.NewLine +
                "## escala= escala de tiempo para la frecuencia, minutos, horas o dias ##" + writer.NewLine + writer.NewLine +
                "## rata_modbus= baud rate para el puerto##" + writer.NewLine +
                "## timeout= tiempo de espera de la conexión en cada intento##" + writer.NewLine + writer.NewLine +
                "## id_modbus= id modbus del computador de flujo##" + writer.NewLine +
                "## intentos= numero de intentos para la conexión ##" + writer.NewLine + writer.NewLine);
            writer.WriteLine("punto:" + punto );
            writer.WriteLine("punto_id:" + punto_id.ToString());
            writer.WriteLine("puerto:" + puerto);
            writer.WriteLine("frecuencia:" + frecuencia);
            writer.WriteLine("escala:" + escala);
            writer.WriteLine("rata_modbus:" + rata_modbus);
            writer.WriteLine("timeout:" + timeout);
            writer.WriteLine("id_modbus:" + id_modbus);
            writer.WriteLine("intentos:" + intentos);
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
            string ajuste = myRow.Cells[15].Value.ToString();

            //Consulta SQL 
            //Set de la Consulta SQL
            String SqlStr = punto_id + "," + indice + "," + tipo + "," + fecha + "," + hora + "," + presion + "," + temperatura + "," + energia + "," + volumen + "," 
                + masa + "," + poder + "," + densidad + "," + rata + "," + tempbat + "," + aux1 + "," + aux2 +"," + ajuste;

            String rta = sc.enviarN("mod", SqlStr) ;

           
            if (ajuste != "NULL")
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

            string id_m = "", nombre_m = "", tipo_m = "", correo_m = "";
            foreach ( DataGridViewRow row in dg_correo.Rows)
            {
                id_m = ""; nombre_m = ""; tipo_m = ""; correo_m = "";

                if (row.Cells[0].Value != null)
                {
                    id_m = row.Cells[0].Value.ToString();
                    nombre_m = row.Cells[1].Value.ToString();
                    correo_m = row.Cells[2].Value.ToString();
                    tipo_m = row.Cells[3].Value.ToString();
                }
                else if (row.Cells[1].Value != null)
                {
                    nombre_m = row.Cells[1].Value.ToString();
                    correo_m = row.Cells[2].Value.ToString();
                    tipo_m = row.Cells[3].Value.ToString();
                }
                String SqlStr_m = "";
                if (nombre_m != "")
                {
                    SqlStr_m = id_m + "," + nombre_m + "," + tipo_m + "," + correo_m + "," + punto_id;
                    String rta_m = sc.enviarN("mail", SqlStr_m);
                }

            }


            this.Text = "Telemetria - Punto: " + punto;
            carga_modbus();
            MessageBox.Show("Actualizado Correctamente");
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
                        if (cb_puertos.Text == "") { cb_puertos.Items.Add(puerto); cb_puertos.Text = puerto; }
                        puertoserial.PortName = puerto;
                    }else if (campos[0] == "frecuencia")
                    {
                        int.TryParse(campos[1], out frecuencia);
                        cb_minuto.Text= frecuencia.ToString();
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
                    else if (campos[0] == "intentos")
                    {
                        intentos = campos[1];
                        cb_intentos.Text = intentos;
                    }
                }
                if (escala == "Minutos")
                {
                    frecuencia_global = frecuencia * 60000;
                }
                else if (escala == "Horas")
                {
                    frecuencia_global = frecuencia * 3600000;
                }
                else if (escala == "Dias")
                {
                    frecuencia_global = frecuencia * 86400000;
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
            String SqlStr_dia = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,voltaje,rata,aux1,aux2 FROM consumo3 Where punto=" + punto_id.ToString() + " AND tiporeg=1 Order by id Desc Limit 1";
            String SqlStr_hora = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,voltaje,rata,aux1,aux2 FROM consumo3 Where punto=" + punto_id.ToString() + " AND tiporeg=2 Order by id Desc Limit 1";
            String SqlStr_acumulado = "Select id,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,voltaje,rata,aux1,aux2 FROM consumo3 Where punto=" + punto_id.ToString() + " AND tiporeg=3 Order by id Desc Limit 1";

            //DV para cada dg
            DataView dv_dia = sc.get_data(SqlStr_dia, 14, table_dia);
            DataView dv_hora = sc.get_data(SqlStr_hora, 14, table_hora);
            DataView dv_acumulado = sc.get_data(SqlStr_acumulado, 14, table_acumulado);

            dg_dia.DataSource = dv_dia;
            dg_hora.DataSource = dv_hora;
            dg_acumulado.DataSource = dv_acumulado;

            temporizador.Interval = int.Parse(timeout);

            //temporizador_global.Interval = frecuencia_global;
            //carga modbus

            string tipo_modbus = "03";
            string cantidad_modbus = "1";
            string tipo = dg_modbus.Rows[0].Cells[1].Value.ToString();
            string hexa_id = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(id_modbus)), 1);
            string hexa_tipo = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(tipo_modbus)), 1);
            string hexa_cantidad = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(cantidad_modbus)), 0);
            tipo_modbus = tipo;
            if (tipo == "1")
            {
                string[] indices = dg_modbus.Rows[0].Cells[0].Value.ToString().Split('-');
                string hexa_dia = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[0])), 0);
                string hexa_hora = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[1])), 0);
                string hexa_acumulado = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[2])), 0);
                hexa_dia_registros = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[3])), 0);
                hexa_hora_registros = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[4])), 0);
                hexa_acumulado_registros = sc.completar_hexa(string.Format("{0:x2}", Int16.Parse(indices[5])), 0);

                request_dia = sc.crear_request(hexa_id, hexa_tipo, hexa_dia, hexa_cantidad);
                request_hora = sc.crear_request(hexa_id, hexa_tipo, hexa_hora, hexa_cantidad);
                request_acumulado = sc.crear_request(hexa_id, hexa_tipo, hexa_acumulado, hexa_cantidad);

                //list_logs.Items.Add(hexa_id + hexa_tipo + hexa_dia + hexa_cantidad);
                //list_logs.Items.Add(hexa_id + hexa_tipo + hexa_hora + hexa_cantidad);
                //list_logs.Items.Add(hexa_id + hexa_tipo + hexa_acumulado + hexa_cantidad);
            }
            //array de ajustes
            string ajuste = dg_modbus.Rows[0].Cells[15].Value.ToString();


            if (ajuste != "NULL")
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

            //0-fecha 1-hora 2-presion 3-temperatura 4-energia 5-volumen 6-masa 7-PC 8-Densidad 9-TempBatt 10-Rata   
            //Crear datatable para rellenar datagrid correos
            DataTable table_correo = new DataTable("TablaModbus");
            table_correo.Columns.Add(new DataColumn("Id", typeof(string)));
            table_correo.Columns.Add(new DataColumn("Nombre", typeof(string)));
            table_correo.Columns.Add(new DataColumn("Correo", typeof(string)));
            table_correo.Columns.Add(new DataColumn("Tipo", typeof(string)));
            //Datos de los puntos
            String SqlStr_modbus = "Select indice,tipo,fecha,hora,presion,temperatura,energia,volumen,masa,poder,densidad,rata,tempbat,aux1,aux2,ajuste FROM modbusindex Where punto=" + punto_id.ToString() + " Order by id ASC";

            //DV para cada dg
            DataView dv_ga = sc.get_data(SqlStr_modbus, 16, table_modbus);

            dg_modbus.DataSource = dv_ga;

            //Datos de los puntos
            SqlStr_modbus = "Select id,nombre,correo,tipo FROM correos Where punto=" + punto_id.ToString() + " Order by id ASC";

            //DV para cada dg
            DataView dv_co = sc.get_data(SqlStr_modbus, 4, table_correo);

            dg_correo.DataSource = dv_co;

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

        private void cb_puntos_SelectedIndexChanged(object sender, EventArgs e)
        {
            punto = cb_puntos.Text;
            String[] tmp_punto = punto.Split('-');
            int.TryParse(tmp_punto[0], out punto_id);
            carga_modbus();
        }

        //CORREO

        private void preparar_correo()
        {
            Console.Out.WriteLine(DateTime.Now.ToString());

            String SQLStr_d = punto_id.ToString();
            String SQLStr_h = punto_id.ToString();
            int freq = 1;

                list_logs.Items.Add("Envio Correo");
            string c_dia = "", c_hora="";
            c_dia = dg_dia.Rows[0].Cells[13].Value.ToString();
            c_hora = dg_hora.Rows[0].Cells[13].Value.ToString();
            if (c_dia == "") {
                //string[] names = { "fec", "hor", "pre", "tem", "ene", "vol", "mas", "pod", "den", "teb", "rat" };
                String[] a_dia = new string[12], a_hora = new string[12], a_acumulado = new string[12];
                for (int i = 0; i < 11; i++)
                {
                    a_dia[i] = dg_dia.Rows[0].Cells[i + 1].Value.ToString();
                    SQLStr_d += "," + a_dia[i];
                    a_hora[i] = dg_hora.Rows[0].Cells[i + 1].Value.ToString();
                    SQLStr_h += "," + a_hora[i];
                    a_acumulado[i] = dg_acumulado.Rows[0].Cells[i + 1].Value.ToString();
                }
                freq = 2;
                enviar_correo(a_dia, a_hora, a_acumulado, freq);
                SQLStr_d += ",,'ok',1";
                String rta = sc.enviarN("con3", SQLStr_d);
                SQLStr_h += ",,'ok',2";
                rta = sc.enviarN("con3", SQLStr_h);
                carga_datos();

            }
            else if (c_hora == "")
            {
                //string[] names = { "fec", "hor", "pre", "tem", "ene", "vol", "mas", "pod", "den", "teb", "rat" };
                String[] a_dia = new string[12], a_hora = new string[12], a_acumulado = new string[12];
                for (int i = 0; i < 11; i++)
                {
                    a_dia[i] = dg_dia.Rows[0].Cells[i + 1].Value.ToString();
                    a_hora[i] = dg_hora.Rows[0].Cells[i + 1].Value.ToString();
                    SQLStr_h += "," + a_hora[i];
                    a_acumulado[i] = dg_acumulado.Rows[0].Cells[i + 1].Value.ToString();
                }
                freq = 1;
                enviar_correo(a_dia, a_hora, a_acumulado, freq);
                SQLStr_h += ",,'ok',2";
                String rta = sc.enviarN("con3", SQLStr_h);
                carga_datos();

            }
        }
        private void enviar_correo(String[] itemD, String[] itemH, String[] itemO, int freq)
        {

            string[] names = { "fec", "hor", "pre", "tem", "ene", "vol", "mas", "pod", "den", "teb", "rat" };            
            string mails = "";
            foreach (DataGridViewRow row in dg_correo.Rows)
            {

                if (row.Cells[0].Value != null)
                {
                    int tmp = 100;
                    int.TryParse(row.Cells[3].Value.ToString(), out tmp);
                    if (tmp <= freq)
                    {
                        mails += row.Cells[2].Value.ToString() +",";
                    }
                }
            }

            list_logs.Items.Add(mails);
            mails = "cristhiampaul@gmail.com ";

            string fileName = Directory.GetParent(Application.StartupPath) + "\\templates\\template_mail.htm";
            string Body = System.IO.File.ReadAllText(fileName);

            if (tipo_modbus != 1)
            {
                fileName = Directory.GetParent(Application.StartupPath) + "\\templates\\template_mail_4.htm";
                Body = System.IO.File.ReadAllText(fileName);
                for (int i = 0; i < names.Length; i++)
                {
                    Body = Body.Replace("#" + names[i] + "O#", itemO[i]);
                }
            }
            else
            {
                //registro.Items.Add(" - antes if-"+ itemD.Length);
                for (int i = 0; i < names.Length; i++)
                {
                    Body = Body.Replace("#" + names[i] + "D#", itemD[i]);
                    Body = Body.Replace("#" + names[i] + "H#", itemH[i]);
                    Body = Body.Replace("#" + names[i] + "O#", itemO[i]);
                }
            }

            Body = Body.Replace("#punto#", punto);

            // Configuracion
            string SMTP = "smtp.gmail.com";
            string Usuario = "operacion@coinogas.com";
            string Contraseña = "Operacion2019.";
            

            string Asunto = "DATOS TELEMETRIA - " + punto + " - " + itemO[0];// +" - " + itemO[1];
            int Puerto = 587;

            //Declaro la variable para enviar el correo
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(Usuario);
            mail.Subject = Asunto;
            mail.To.Add(mails);
            mail.IsBodyHtml = true;
            mail.Body = Body;


            //Configuracion del servidor
            System.Net.Mail.SmtpClient Servidor = new System.Net.Mail.SmtpClient();
            Servidor.Host = SMTP;
            Servidor.Port = Puerto;
            Servidor.EnableSsl = true;
            Servidor.Credentials = new System.Net.NetworkCredential(Usuario, Contraseña);
            try
            {
                Servidor.Send(mail);
                list_logs.Items.Add(" - Enviado");
                estatus11.Text = " - Enviado";

                //bar.Value = 20;
            }
            catch (Exception ex)
            {
                list_logs.Items.Add(" - FallaEnviado-");
                estatus11.Text = " - FallaEnviado-";
                list_logs.Items.Add(ex.Message);
            }
        }
    }
}
