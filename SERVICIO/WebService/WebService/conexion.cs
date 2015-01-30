using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using System.Web;

namespace WebService
{
    public class conexion
    {
        string ConnectionString = "Data Source=BSKT-PC;" + "Initial Catalog=BdContribuyentes;Integrated Security=true";
        public SqlConnection cnn;
        public SqlCommand cmd;
        string rutas = System.Web.Hosting.HostingEnvironment.MapPath("~"+@"\archivo\");
        public conexion()
        {
            cnn = new SqlConnection(ConnectionString);
        }
        public void DownloadFiles(string NameFile)
        {
            try
            {
                string File = NameFile;
                string DirPath = rutas + NameFile;
                var WClient = new WebClient();
                WClient.Proxy = null;
                WClient.DownloadFile("http://www.gestionix.com/Zip/" + NameFile, DirPath);
            }
           catch(WebException e)
            {
                WriteFile(e.Message); 
            }
        }
        public void UnzipFiles(string arch)
        {
            try
            {
                string DirPath = rutas + arch;
                DirectoryInfo direc = new DirectoryInfo(DirPath);
                FileInfo archivo = new FileInfo(DirPath);

                FileStream ArchivoOriginal = archivo.OpenRead();
                string NombreArchivo = archivo.FullName;
                string NuevoNombre = NombreArchivo.Remove(NombreArchivo.Length - archivo.Extension.Length);
                FileStream ArchivoDescomprimido = File.Create(NuevoNombre + ".xml");
                GZipStream Descomprimir = new GZipStream(ArchivoOriginal, CompressionMode.Decompress);
                Descomprimir.CopyTo(ArchivoDescomprimido);
            }
            catch(DirectoryNotFoundException e)
            {
                WriteFile(e.Message);
            }
        }
        public void ExecuteCommand(string archivo)
        {
            try
            {
                string archivoInicio = rutas + archivo;
                string nombre = Path.GetFileNameWithoutExtension(archivo);
                string ArchivoFinal = rutas + nombre + "nuevo.xml";
                Opencmd(rutas+@"openssl\bin\openssl smime -decrypt -verify -inform DER -in " + archivoInicio + " -noverify -out " + ArchivoFinal);
            }
            catch(Exception e)
            {
                WriteFile(e.Message);
            }
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
            cnn.Open();
            SqlCommand com = new SqlCommand("select * from tblCertificado where RFC='" + rfc + "'", cnn);
            com.CommandTimeout = 65535;
            com.ExecuteReader();
            cnn.Close();
            DataTable dt = new DataTable();
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
        }
        public void ExecuteProcedure(string procedimiento)
        {
            try
            {
                cnn.Open();
                cmd = new SqlCommand(procedimiento, cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 65535;
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception e)
            {
                WriteFile(e.Message);
            }
        }
        public void DeleteAllFiles(string fileName)
        {
            if (System.IO.File.Exists(rutas + fileName))
            {
                System.IO.File.Delete(rutas + fileName);
            }
        }
        public void CreateFile()
        {
            try
            {
                string PathFile = rutas+@"error\";
                string fileName = PathFile + "ErrorFile.txt";
                if (Directory.Exists(PathFile))
                {
                    if (!File.Exists(PathFile))
                    {
                        var FileStream = File.Create(fileName);
                        FileStream.Close();
                    }
                }
                else
                {
                    Directory.CreateDirectory(PathFile);
                    var FileStream = File.Create(fileName);
                    FileStream.Close();
                }
            }
            catch (Exception e)
            {
                WriteFile(e.Message);
            }
        }
        private void WriteFile(string texto)
        {
            try 
            {
                string fileName = rutas+@"error\ErrorFile.txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
                file.WriteLine(texto);
                file.Close();
            }
            catch (Exception e)
            {
                WriteFile(e.Message);
            }

        }
        public string ReadFile()
        {
            StreamReader lee = new StreamReader(rutas+@"error\ErrorFile.txt");
            return lee.ReadLine();
        }
    }
}