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
            string sCity = Console.ReadLine(); //Hier Frage ich nach einer Stadt. 
            Uri uApi = new Uri("http://api.openweathermap.org/data/2.5/weather?&mode=xml&lang=de&q=" + sCity.ToString() + "&APPID=6b306b9d59cdd9c3214fbf00b40c6688"); //Hier wird die URL erstellt. Diese besteht aus dem API Link, Stadt und anschließend die APPID, die durch den API Betreiber erstellt worden ist.
            XmlDocument dtWetter = XmlDatenAbholen(uApi); //Hier gebe ich den Link weiter an die Methode XmlDatenAbholen und Frage alle Daten ab und speichere diese in einem XML Document ab (dtWetter)
            string sTemperatur = dtWetter.SelectSingleNode("current/temperature").Attributes["value"].Value; //Hier lese ich die Temperatur in der einheit KELVIN ab und speichere den Wert im string sTemperatur ab.
            float fKelvin = float.Parse(sTemperatur.ToString(), CultureInfo.InvariantCulture.NumberFormat); //Hier wird der string in einem float umgewandelt. Dazu war auch die Library System.Globalization nötig. 
            float fBerechnungsWert = 273.15f; //Hier lege ich den KELVIN Berechnungswert fest. 
            double dCelsiuc = fKelvin - fBerechnungsWert; //Hier wird der KELVIN in Celsiuc umgewandelt. Berechnung: Celsiuc = Kelvin - 273.15
            dCelsiuc = Math.Round(dCelsiuc, 2); //Hier wird Celsius auf 2 Nachkommastellen gerundet. 
            Kleidungsempfehlung(dCelsiuc);//Aufruf der Methode Kleidungsempfehlug. Hier gebe ich die Temperatur in Celcius an die Methode weiter.
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
            XmlDocument XmlRuekgabe; //Hier definieren wir ein neues XML Dokument.
            HttpWebRequest anfrage = WebRequest.Create(Url) as HttpWebRequest; //Hier wird eine neue Webabfrage erstellt.
            HttpWebResponse antwort = anfrage.GetResponse() as HttpWebResponse; //Und hier holen wir uns die Antwort mit der "GetResponse" Funktion.
            XmlRuekgabe = new XmlDocument(); //Hier wird das neu definierte XML Dokument angelegt.
            XmlRuekgabe.Load(antwort.GetResponseStream()); //Hier wird die "Antwort" Funktion aufgerufen und geladen. Die abgerufenen Daten werden alle in XmlRuekgabe reingeschieben.
            return XmlRuekgabe; //Gibt XmlRuekgabe mit den Daten zurück.
        }
    }
}
