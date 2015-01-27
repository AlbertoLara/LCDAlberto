using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;

namespace PrimerEjercicio
{
    class conexion
    {
        string ConnectionString = "Data Source=BSKT-PC;" + "Initial Catalog=BdContribuyentes;Integrated Security=true";
        public SqlConnection cnn;
        public SqlCommand cmd;
        public conexion()
        {
            cnn=new SqlConnection(ConnectionString);

        }
        public void DownloadFiles(string NameFile)
        {
            string File = NameFile;
            string DirPath = @"..\archivos\" + NameFile;

            var WClient = new WebClient();
            WClient.Proxy = null;
            Console.WriteLine("DESCARGANDO EN ARCHIVO... POR FAVOR ESPERE");
            WClient.DownloadFile("http://www.gestionix.com/Zip/" + NameFile, DirPath);
            Console.WriteLine("El archivo fue descargado correctamente");
            Console.Clear();
        }
        public void UnzipFiles(string arch)
        {
            Console.WriteLine("DESCOMPRIMIENDO... ESPERE...");
            string DirPath = @"..\archivos\" + arch;
            DirectoryInfo direc = new DirectoryInfo(DirPath);
            FileInfo archivo = new FileInfo(DirPath);

            FileStream ArchivoOriginal = archivo.OpenRead();
            string NombreArchivo = archivo.FullName;
            string NuevoNombre = NombreArchivo.Remove(NombreArchivo.Length - archivo.Extension.Length);
            FileStream ArchivoDescomprimido = File.Create(NuevoNombre + ".xml");
            GZipStream Descomprimir = new GZipStream(ArchivoOriginal, CompressionMode.Decompress);
            Descomprimir.CopyTo(ArchivoDescomprimido);
            Console.Clear();
            Console.WriteLine("EL ARCHIVO FUE DESCOMPRIMIDO COMPLETAMENTE");

        }
        public void ExecuteCommand(string archivo)
        {
                string archivoInicio =@"..\archivos\"+archivo;
                string nombre = Path.GetFileNameWithoutExtension(archivo);
                string ArchivoFinal = @"..\archivos\" +    nombre + "nuevo.xml";
                Console.WriteLine("Limpiando archivo "+archivoInicio);

                Opencmd(@"..\openssl\bin\openssl smime -decrypt -verify -inform DER -in " + archivoInicio + " -noverify -out " + ArchivoFinal);
                Console.WriteLine("El archivo "+archivoInicio+" fue limpiado con nombre de: "+ArchivoFinal);
        }
        private static void Opencmd(string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd", "/c " + command);
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.CreateNoWindow = false;
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.StandardOutput.ReadToEnd();
            }
        public void search(string rfc)
        {
            Console.WriteLine("Espere... se esta buscando el registro");
            cnn.Open();
            SqlCommand com = new SqlCommand("select * from tblCertificado where RFC='"+rfc+"'",cnn);
            com.CommandTimeout = 65535;
            com.ExecuteReader();
            cnn.Close();
            DataTable dt=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Console.WriteLine("RFC: " + rfc);
                Console.WriteLine("ValidezObligaciones: " + Convert.ToString(row["ValidezObligaciones"]));
                Console.WriteLine("EstatusCertificado: " + Convert.ToString(row["EstatusCertificado"]));
                Console.WriteLine("noCertificado: " + Convert.ToString(row["noCertificado"]));
                Console.WriteLine("FechaInicio: " + Convert.ToString(row["FechaInicio"]));
                Console.WriteLine("FechaFinal: " + Convert.ToString(row["FechaFinal"]));
            }
            else
            {
                Console.WriteLine("El registro no fue encontrado");
            }
        }
        public void ExecuteProcedure(string procedimiento)
        {
            Console.WriteLine("INSERTANDO REGISTROS... ESPERE...");
            try
            {
                cnn.Open();
                cmd = new SqlCommand(procedimiento, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 65535;
                cmd.ExecuteNonQuery();
                Console.WriteLine("Registros Insertados");
                cnn.Close();
            }
            catch
            {
                Console.WriteLine("Error al insertar los registros");
            }
        }
        public void DeleteAllFiles(string fileName)
        {
            if(System.IO.File.Exists ( @"..\archivos\"+fileName))
            {
                System.IO.File.Delete(@"..\archivos\" + fileName);
            }
        }
     }
}
