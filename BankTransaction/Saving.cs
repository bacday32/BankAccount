using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BankTransaction
{
    class Saving
    {
        public int idSaving { get; set; }
        public int accountNumber { get; set; }
        public int duration { get; set; }
        public double amount { get; set; }
        public double interesRate { get; set; }
        public DateTime timeStart { get; set; } = DateTime.Now;

        public Saving()
        {

        }
        public Saving(int idSaving, int accountNUmber, int duration, double amount, double interesRate, DateTime timeStart)
        {
            this.idSaving = idSaving;
            this.accountNumber = accountNumber;
            this.duration = duration;
            this.amount = amount;
            this.interesRate = interesRate;
            this.timeStart = DateTime.Now;
        }
        public override string ToString()
        {
            return this.idSaving + " , " + this.accountNumber + " , " + this.duration + " , " + this.amount + " , " + this.interesRate + " , " + this.timeStart;
        }
        public void Display()
        {
            Console.WriteLine("id saving : {0},account number :{1}, duration : {2},amount : {3},interesRate : {4},timeStart : {5}", idSaving, accountNumber, duration, amount, interesRate,timeStart);
        }
        public void AddSaving()
        {
            try
            {
                XmlDocument docInteres = new XmlDocument();
                docInteres.Load("Interes.xml");      
                Interes newInteres = new Interes();
                newInteres.AddInteres();
                XmlElement element = docInteres.DocumentElement;
                XmlNode nodeInteres = element.SelectSingleNode("interes[duration='" + newInteres.duration.ToString() + "']");                
                this.duration = newInteres.duration;               
                Console.WriteLine("Enter amount of saving account: ");
                this.amount = double.Parse(Console.ReadLine());
                this.interesRate = double.Parse(nodeInteres.ChildNodes[2].InnerText) * this.amount;
            }
            catch (Exception)
            {
                Console.WriteLine("duration not support");
                Console.ReadLine();
            }
        }
    }
}
