using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BankTransaction
{
    class Transaction
    {
        public int idTransaction { get; set; }
        public double balance { get; set; }
        public string content { get; set; } = "";
        public double amount { get; set; }
        public DateTime transactionTime { get; set; } = DateTime.Now;
        public double fromAccount { get; set; } = 0;
        public double toAccount { get; set; } = 0;
        public string typeTransaction { get; set; }
        public bool disable { get; set; } = false;
        public Transaction()
        {
        }
        public Transaction(int idTransaction, double balance, string content, float amount, DateTime transactionTime, int fromAccount, int toAccount, string typeTransaction)
        {
            this.idTransaction = idTransaction;
            this.balance = balance;
            this.content = "";
            this.amount = amount;
            this.transactionTime = DateTime.Now;
            this.fromAccount = fromAccount;
            this.toAccount = toAccount;
            this.typeTransaction = typeTransaction;
        }
        public void AddDeposit(int minimum)
        {
            do
            {               
                Console.WriteLine("Enter amount of money: ");
                this.amount = double.Parse(Console.ReadLine());

                if (this.amount > minimum)
                {
                    Console.WriteLine("Enter content transaction: ");
                    this.content = Convert.ToString(Console.ReadLine());
                    this.typeTransaction = "Deposit";
                    break;
                }
                else
                {
                    Console.WriteLine("The amount entered is not valid");
                    Console.ReadLine();
                }
            }
            while (true);
        }
        public void AddWithDraw(int minimum)
        {
            do
            {            
                Console.WriteLine("Enter amount of money:   ");
                this.amount = double.Parse(Console.ReadLine());
                if (this.amount >= minimum)
                {
                    Console.WriteLine("Enter content transaction: ");
                    this.content = Convert.ToString(Console.ReadLine());
                    this.typeTransaction = "Withdraw";
                    break;
                }
                else
                {
                    Console.WriteLine("The amount entered is not valid");
                    Console.ReadLine();
                }
            }
            while (true);
        }
        public void AddTransfer(int minium)
        {
            do
            {              
                Console.WriteLine("Enter amount of money: ");
                this.amount = double.Parse(Console.ReadLine());
                if(this.amount >= minium)
                {
                    Console.WriteLine("Enter content transaction: ");
                    this.content = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Enter receive account: ");
                    this.toAccount = double.Parse(Console.ReadLine());
                    this.typeTransaction = "Transfer";
                    break;
                }
                else
                {
                    Console.WriteLine("The amount entered is not valid");
                    Console.ReadLine();
                }
            }
            while (true);        
        }
        public override string ToString()
        {
            return this.idTransaction + " , " + this.balance + " , " + this.content + " , " + this.amount + " , " + this.transactionTime + " , " + this.fromAccount + " , " + this.toAccount;
        }
        public void DisplayTransaction()
        {
            List<Transaction> listTransaction = new List<Transaction>();
            XmlTextReader textReader = new XmlTextReader("Transaction.xml");
            textReader.Read();
            while (textReader.Read())
            {
                textReader.MoveToElement();
                Console.Write(textReader.Value);
            }
            Console.ReadLine();
        }             
    }
}
