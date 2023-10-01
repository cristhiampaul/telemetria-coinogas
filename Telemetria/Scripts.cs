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
        public DataView get_data(String SqlStr, int col, DataTable table)
        {
            String[] filas = traerD(SqlStr);
            if (filas == null) { filas = new String[0]; }

            DataRow dr;
            //nominaciones
            for (int i = 0; i < filas.Length - 1; i++)
            {
                String[] items = filas[i].Split('&');
                dr = table.NewRow();
                for (int j = 0; j < col; j++)
                {
                    dr[j] = items[j].ToString();
                }
                table.Rows.Add(dr);
            }
            DataView dv = new DataView(table);
            return dv;
        }

        public UInt16 ModRTU_CRC(byte[] buf, int len)
        {
            UInt16 crc = 0xFFFF;

            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (UInt16)buf[pos];          // XOR byte into least sig. byte of crc

                for (int i = 8; i != 0; i--)
                {    // Loop over each bit
                    if ((crc & 0x0001) != 0)
                    {      // If the LSB is set
                        crc >>= 1;                    // Shift right and XOR 0xA001
                        crc ^= 0xA001;
                    }
                    else                            // Else LSB is not set
                        crc >>= 1;                    // Just shift right
                }
            }
            // Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
            return crc;
        }               

        public byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public String convertir_hexa(String Salida)
        {
            string hex_tmp = "", Respuesta = "";
            Encoding c1252 = Encoding.GetEncoding(1252);
            byte[] x = c1252.GetBytes(Salida);
            try
            {
                foreach (byte b in x)
                {
                    hex_tmp = String.Format("{0:x2}", (uint)System.Convert.ToUInt32(b.ToString()));

                    /* switch (hex_tmp)
                     {
                         case "20": hex_tmp = "00"; break;  // espacios deben ser 00
                     }
                     */
                    Respuesta += hex_tmp;
                }
            }
            catch { }
            // MessageBox.Show("rta-"+Respuesta.ToString());
            return Respuesta;
        }

        public string completar_hexa(string hexValue, int tipo )
        {
            int longitud = hexValue.Length;
            if (tipo == 1)
            {
                longitud = longitud + 2;
            }
            switch (longitud)
            {
                case 1:
                    hexValue = "000" + hexValue;
                    break;
                case 2:
                    hexValue = "00" + hexValue;
                    break;
                case 3:
                    hexValue = "0" + hexValue;
                    break;

            }

            return hexValue;
        }

        public byte[] crear_request(string hexa_id, string hexa_tipo, string hexa_indice, string hexa_cantidad)
        {
            string modbus_request = hexa_id + hexa_tipo + hexa_indice + hexa_cantidad;
            byte[] modbus_request_byte = StringToByteArray(modbus_request);
            UInt16 u_i2 = ModRTU_CRC(modbus_request_byte, modbus_request_byte.Length);
            string hexa_crc = completar_hexa(u_i2.ToString("X"), 0);
            hexa_crc = hexa_crc.Substring(2, 2) + hexa_crc.Substring(0, 2);
            modbus_request = modbus_request + hexa_crc;
            string modbus_enviar = "";
            int contador = 1;
            foreach (char x in modbus_request)
            {
                if (contador > 1)
                {
                    if ((contador % 2) == 0)
                    {
                        modbus_enviar += x;
                    }
                    else
                    {
                        modbus_enviar += " " + x;
                    }
                }
                else
                {
                    modbus_enviar += x;
                }

                contador += 1;
            }
            byte[] bytes_modbus = modbus_enviar.Split(' ').Select(s => Convert.ToByte(s, 16)).ToArray();
            return bytes_modbus;
        }

    }
}
