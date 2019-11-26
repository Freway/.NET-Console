using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace Reverse.Geocode.KMLFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            using (StreamReader sr = new StreamReader(path + "/end.txt"))
            {
                using (StreamWriter sw = File.CreateText(path + "/resolvido.KML"))
                {
                    using (StreamWriter swe = File.CreateText(path + "/erros.txt"))
                    {
                        string line;
                        int count = 0;

                        sw.WriteLine("<?xml version = \"1.0\" encoding = \"UTF-8\" ?>");
                        sw.WriteLine("<kml xmlns = \"http://www.opengis.net/kml/2.2\">");
                        sw.WriteLine("<Document>");
                        sw.WriteLine("<name>Teste</name>");

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
                                Console.WriteLine(line);

                                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                                XElement locationElement = result.Element("geometry").Element("location");
                                XElement lat = locationElement.Element("lat");
                                XElement lng = locationElement.Element("lng");

                                sw.WriteLine("<Placemark><name>ofn" + count + "</name>");

                                lat1 = lat.ToString();
                                lat1 = lat1.Substring(5, 8);
                                lng1 = lng.ToString();
                                lng1 = lng1.Substring(5, 8);
                                sw.WriteLine("<Point><coordinates>" + lng1 + "," + lat1 + "</coordinates></Point></Placemark>");

                            }

                            catch
                            {
                                swe.WriteLine("Erro linha: " + count + " (" + line + ")");
                            }
                        }
                        sw.WriteLine("</Document></kml>");
                    }
                }
            }
        }
    }
}

