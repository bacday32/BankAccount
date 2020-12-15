using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BankTransaction
{
    class Interes
    {
        XmlDocument doc = new XmlDocument();
        XmlElement root;
        string fileName = @"Interes.xml";
        public int idInteres { get; set; }
        public int duration { get; set; }
        public float rate { get; set; }

        public Interes()
        {
            doc.Load(fileName);
            root = doc.DocumentElement;
        }
        public Interes(int idInteres, int duration, float rate)
        {
            this.idInteres = idInteres;
            this.duration = duration;
            this.rate = rate;
        }
        public override string ToString()
        {
            return this.idInteres + " , " + this.duration + " , " + this.rate;
        }
        public void AddInteres()
        {
            Console.WriteLine("Enter duration: ");
            this.duration = int.Parse(Console.ReadLine());
        }
        
    }
}
