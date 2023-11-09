using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telemetria;

namespace GestorTelemetria
{
    public partial class Gestor : Form
    {
        Scripts sc = new Scripts();
        public Gestor()
        {
            InitializeComponent();
        }

        private void Gestor_Load(object sender, EventArgs e)
        {
            carga_puntos();
        }

        //Data

        private void carga_puntos()
        {
            String SqlStr_puntos = "SELECT id, nombre FROM puntos WHERE eliminado=0 order by id";
            String[] filas_en = sc.traerD(SqlStr_puntos);
            for (int i = 0; i <= filas_en.Length - 2; i++)
            {
                String[] items = filas_en[i].Split('&');
                //llenar combobox op
                cl_puntos.Items.Add(items[0] + "- " + items[1]);
            }
        }

        public void read_config()
        {
            string fileName = Application.StartupPath + "\\config.ini";
            if (File.Exists(fileName))
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);
                char[] x = { ':' }; // delimitador

                
                for (int i = 0; i <= (cl_puntos.Items.Count - 1); i++)
                {
                    while (!reader.EndOfStream)
                    {
                        string[] campos = reader.ReadLine().Split(x);
                        if (campos[0] == cl_puntos.Items[i].ToString())
                        {
                            cl_puntos.SetItemCheckState(i, CheckState.Checked);
                        }
                    }
                }                
                reader.Close();
                stream.Close();                
            }
            else
            {             
                //write_config();
            }
        }
    }
}
