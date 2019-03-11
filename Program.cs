using System;
using System.Xml;
using System.Net;
using System.Globalization;

namespace Progammiertest
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bitte eine Stadt eingeben: ");
            string sCity = Console.ReadLine();
            Uri uApi = new Uri("http://api.openweathermap.org/data/2.5/weather?&mode=xml&lang=de&q=" + sCity.ToString() + "&APPID=6b306b9d59cdd9c3214fbf00b40c6688");
            XmlDocument dtWetter = XmlDatenAbholen(uApi);
            string sTemperatur = dtWetter.SelectSingleNode("current/temperature").Attributes["value"].Value;
            float fKelvin = float.Parse(sTemperatur.ToString(), CultureInfo.InvariantCulture.NumberFormat);
            float fBerechnungsWert = 273.15f;
            double dCelsiuc = fKelvin - fBerechnungsWert;
            dCelsiuc = Math.Round(dCelsiuc, 2);
            Kleidungsempfehlung(dCelsiuc);
            Console.ReadKey(false);
        }

        //Level 1: x >= 26 °C
        //Level 2: 21 < x <= 26 °C
        //Level 3: 15 < x <= 21 °C
        //Level 4: 5 < x <= 15 °C
        //Level 5: x <= 5 °C

        static void Kleidungsempfehlung(double dTemp)
        {
            int iLevel = 0;

            if(dTemp >= 26)
            {
                iLevel = 1;
            }
            else if(21 < dTemp && dTemp <= 26)
            {
                iLevel = 2;
            }
            else if (15 < dTemp && dTemp <= 21)
            {
                iLevel = 3;
            }
            else if (5 < dTemp && dTemp <= 15)
            {
                iLevel = 4;
            }
            else if (dTemp <= 5)
            {
                iLevel = 5;
            }

            switch (iLevel)
            {
                case 1:
                    Console.WriteLine("Sehr leichte Kleidung!");
                    break;
                case 2:
                    Console.WriteLine("Leichte Kleidung!");
                    break;
                case 3:
                    Console.WriteLine("T-Shirt Wetter!");
                    break;
                case 4:
                    Console.WriteLine("Ein Pullover ist auf jeden Fall noetig!");
                    break;
                case 5:
                    Console.WriteLine("Sehr warme Kleidung -> Eine dicke Jacke anziehen!");
                    break;
                default:
                    Console.WriteLine("");
                    break;
            }
        }

        static XmlDocument XmlDatenAbholen(Uri Url)
        {
            XmlDocument XmlRuekgabe;
            HttpWebRequest anfrage = WebRequest.Create(Url) as HttpWebRequest;
            HttpWebResponse antwort = anfrage.GetResponse() as HttpWebResponse;
            XmlRuekgabe = new XmlDocument();
            XmlRuekgabe.Load(antwort.GetResponseStream());
            return XmlRuekgabe;
        }
    }
}