using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BankTransaction
{
    class Account : Customer
    {
        public int idAccount { get; set; }
        public int accountNumber { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string typeAccount { get; set; }
        public double balance { get; set; } = 0;
        public bool disable { get; set; } = false;
        public List<Transaction> historyTransaction = new List<Transaction>();
        public Account()
        {
        }
        public void AddAccount()
        {
            Console.WriteLine("Enter full name customer: ");
            this.fullName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter date of birth: ");
            this.dateOfBirth = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter user name");
            this.userName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter password: ");
            this.passWord = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter type account: ");
            this.typeAccount = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter email address: ");
            this.email = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter phone number: ");
            this.phoneNumber = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter address: ");
            this.address = Convert.ToString(Console.ReadLine());
            this.disable = true;

        }
        public override string ToString()
        {
            return this.idCustomer + " , " + this.fullName + " , " + this.dateOfBirth + " , " + this.address + " , " + this.email + " , " + this.phoneNumber + " , " + this.idAccount + " , " + this.accountNumber + " , " + this.userName + " , " + this.passWord + " , " + this.balance + " , " + this.disable + " , " + this.historyTransaction;
        }
        public void DisplayAccount()
        {
            List<Account> listAccount = new List<Account>();
            XmlTextReader textReader = new XmlTextReader("Account.xml");
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
