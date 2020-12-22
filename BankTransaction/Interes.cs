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
        public int idInteres { get; set; }
        public int duration { get; set; }
        public double rate { get; set; }
        public Interes()
        {
            doc.Load("Interes.xml");
            root = doc.DocumentElement;
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
