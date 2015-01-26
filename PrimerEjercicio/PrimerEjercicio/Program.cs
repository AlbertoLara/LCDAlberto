using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Web;

namespace PrimerEjercicio
{
    class Program
    {
        static void Main(string[] args)
        {
            conexion c = new conexion();
            string RFC = "";
            string[] archivos=new string[4];
            archivos[0] = "a1";
            archivos[1] = "a2";
            archivos[2] = "a3";
            archivos[3] = "a4";
            
            //descargar
            for (int i = 0; i<archivos.Length; i++)
            {
                Console.WriteLine("Descargando archivo " + archivos[i] + ".gz espere...");
                c.DownloadFiles(archivos[i] + ".gz");
            }
            //descomprimir
            for (int i = 0; i<archivos.Length; i++)
            {
                Console.WriteLine("Descomprimiendo archivo " + archivos[i] + ".gz espere...");
                c.UnzipFiles(archivos[i] + ".gz");
            }
            //limpiar
            for (int i = 0; i < archivos.Length; i++)
            {
                c.ExecuteCommand(archivos[i]+".xml");
            }
            //insertar registros
            for (int i = 1; i <=archivos.Length; i++)
            {
                c.ExecuteProcedure("INSERTARXML" + i);
            }
            
            Console.WriteLine("Eliminado archivos...Espere");
            for (int i = 0; i < archivos.Length; i++)
            {
                c.DeleteAllFiles(archivos[i]+".gz");
                c.DeleteAllFiles(archivos[i] + ".xml");
                c.DeleteAllFiles(archivos[i] +"nuevo"+".xml");
            }
            Console.WriteLine("Todos los archivos eliminados");
            
            Console.WriteLine("INTRODUCE LA RFC: ");
            RFC = Console.ReadLine();
            c.search(RFC);
            Console.WriteLine("Presione una tecla para salir");
            Console.ReadKey();
        }
    }
}
