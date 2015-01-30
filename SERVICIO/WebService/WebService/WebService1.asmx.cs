using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://localhost/pruebas")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola Mundo";
        }
        [WebMethod]
        public void UpdateLCO()
        {
            conexion c = new conexion();
            c.CreateFile();
            string[] archivos=new string[4];
            archivos[0] = "a1";
            archivos[1] = "a2";
            archivos[2] = "a3";
            archivos[3] = "a4";
            //descargar
            for (int i = 0; i<archivos.Length; i++)
            {
                c.DownloadFiles(archivos[i] + ".gz");
            }
            if (c.ReadFile() == "")
            {
                //descomprimir
                for (int i = 0; i < archivos.Length; i++)
                {
                    c.UnzipFiles(archivos[i] + ".gz");
                }
            }
            if (c.ReadFile() == "")
            {
                //limpiar
                for (int i = 0; i < archivos.Length; i++)
                {
                    c.ExecuteCommand(archivos[i] + ".xml");
                }
            }
            if (c.ReadFile() == "")
            {
                //insertar registros
                for (int i = 1; i <= archivos.Length; i++)
                {
                    c.ExecuteProcedure("INSERTARXML" + i);
                }
            }
            if (c.ReadFile() == "")
            {
                for (int i = 0; i < archivos.Length; i++)
                {
                    c.DeleteAllFiles(archivos[i] + ".gz");
                    c.DeleteAllFiles(archivos[i] + ".xml");
                    c.DeleteAllFiles(archivos[i] + "nuevo" + ".xml");
                }
            }
        }
    }
}
