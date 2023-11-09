using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data;
using System.Globalization;

namespace Telemetria
{
    class Scripts
    {

        public String[] traerD(String SqlStr)
        {
            String str;

            //  String SqlStr = "Select * from " + tabla + " Where punto=" + punto + tipo_r + " ORDER BY id DESC Limit 1";
            try
            {
                using (WebClient client = new WebClient())
                {
                    SqlStr = SqlStr.ToLower();
                    SqlStr = SqlStr.Replace("select", "se$$ ");
                    SqlStr = SqlStr.Replace("from", "f$$");
                    SqlStr = SqlStr.Replace("group", "g$$");
                    SqlStr = SqlStr.Replace("where", "w$$");
                    SqlStr = SqlStr.Replace("order", "o$$");
                    SqlStr = SqlStr.Replace("sum", "s$$");
                    SqlStr = SqlStr.Replace("avg", "a$$");
                    // client.Proxy = null;
                    byte[] ar = client.DownloadData("http://coinotel.com/sc/sc_balgas/get_bd.php?SQLStr=" + SqlStr);
                    str = System.Text.Encoding.UTF8.GetString(ar);

                    String[] filas = str.Split('%');
                    return filas;
                }
            }

            catch (Exception Except)
            {
                Console.WriteLine(Except.ToString());
                return null;
            }
        }

        public String enviarN(String tipo, String SqlStr)
        {
            String str;

            //  String SqlStr = " * from " + tabla + " Where punto=" + punto + tipo_r + " ORDER BY id DESC Limit 1";
            try
            {
                using (WebClient client = new WebClient())
                {
                    // client.Proxy = null;
                    byte[] ar = client.DownloadData("http://coinotel.com/sc/sc_balgas/set_bd.php?tipo=" + tipo + "&SQLStr=" + SqlStr);
                    str = System.Text.Encoding.UTF8.GetString(ar);
                    return str;
                }
            }

            catch (Exception Except)
            {
                Console.WriteLine(Except.ToString());
                return null;
            }
        }

    }
}
