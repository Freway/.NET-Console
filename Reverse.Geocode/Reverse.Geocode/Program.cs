using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;

//By Guilherme Hernandes Monteiro

namespace Reverse.Geocode
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            // Inicia o componente Excel
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            using (StreamReader sr = new StreamReader(path + "/end.txt"))
            {
                using (StreamWriter error = File.CreateText(path + "/erros.txt"))
                {
                    string line;
                    int count = 0;

                    //Cria uma planilha temporária na memória do computador
                    xlApp = new Excel.Application();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    while ((line = sr.ReadLine()) != null)
                    {
                        string address = line;
                        string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(address), "AIzaSyCccJxCtZhZrYIKjh6pDEzfIrm9e3fj0bk");
                        count++;
                        string lat1, lng1;

                        try
                        {
                            WebRequest request = WebRequest.Create(requestUri);
                            WebResponse response = request.GetResponse();
                            XDocument xdoc = XDocument.Load(response.GetResponseStream());

                            //incluindo dados  
                            xlWorkSheet.Cells[count, 1] = "ofn" + count;
                            XElement result = xdoc.Element("GeocodeResponse").Element("result");
                            XElement locationElement = result.Element("geometry").Element("location");
                            XElement lat = locationElement.Element("lat");
                            lat1 = lat.ToString();
                            lat1 = lat1.Substring(5, 8);
                            xlWorkSheet.Cells[count, 2] = lat1;
                            XElement lng = locationElement.Element("lng");
                            lng1 = lng.ToString();
                            lng1 = lng1.Substring(5, 8);
                            xlWorkSheet.Cells[count, 3] = lng1;

                            Console.WriteLine(count + " " + line);
                        }
                        catch
                        {
                            error.WriteLine("Erro linha: " + count + " (" + line + ")");
                        }
                    }
                    //Salva o arquivo de acordo com a documentação do Excel.
                    xlWorkBook.SaveAs(path + "/arquivo.csv", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();
                }
            }
        }
    }
}