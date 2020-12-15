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
<<<<<<< HEAD
        public double balance { get; set; } = 0;
=======
        public int password { get; set; }
        public float balance { get; set; } = 0;
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a

        public List<Transaction> historyTransaction = new List<Transaction>();
        public Account()
        {
<<<<<<< HEAD
        }
        public Account(int idCustomer, string fullName, string dateOfBirth, string address, string email, string phoneNumber, int idAccount, int accountNumber, string passWord, string typeAccount, double balance, List<Transaction> historyTransaction) : base(idCustomer, fullName, dateOfBirth, address, email, phoneNumber)
=======
            
        }
        public Account(int idAccount, int accountNumber, string fullName, string dateOfBirth, int phoneNumber, string email, int password, float balance, string address)
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
        {
            this.phoneNumber = phoneNumber;
            this.fullName = fullName;
            this.address = address;
            this.email = email;
            this.dateOfBirth = dateOfBirth;            
            this.idAccount = idAccount;
            this.accountNumber = accountNumber;
            this.userName = userName;
            this.passWord = passWord;
            this.typeAccount = typeAccount;
            this.balance = balance;
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

<<<<<<< HEAD
        }     
        public override string ToString()
=======
        }
        public float Deposit()
        {
            Console.WriteLine("Enter amount of money>>> ");
            int amountOfMoney = int.Parse(Console.ReadLine());
            return this.balance = balance + amountOfMoney;

        }
        public float Withdraw()
        {
            Console.WriteLine("Enter amount of money>>>>> ");
            int amountOfMoney = int.Parse(Console.ReadLine());
            return this.balance = balance - amountOfMoney;
        }
        public void Transfer()
        {
            Console.WriteLine("Enter to account");

        }
        public void OpenSaving()
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
        {
            return this.idCustomer + " , " +this.fullName +" , "+this.dateOfBirth+" , " +this.address+" , " +this.email+" , "+ this.phoneNumber+" , " + this.idAccount + " , " + this.accountNumber + " , " + this.userName + " , " + this.passWord + " , " + this.balance  + " , " + this.historyTransaction;
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
            Console.ReadKey();
        }
    }
}
