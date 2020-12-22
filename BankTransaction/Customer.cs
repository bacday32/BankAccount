using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransaction
{
    class Customer
    {
        public int idCustomer { get; set; }
        public string fullName { get; set; }
        public string dateOfBirth { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }        
        public Customer()
        {

        }
        public Customer(int idCustomer, string fullName, string dateOfBirth, string address, string email, string phoneNumber)
        {
            this.idCustomer = idCustomer;
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.address = address;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }
        public void AddCustomer()
        {          
            Console.WriteLine("Enter full name Customer>>>>> ");
            this.fullName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter date of birth Customer>>>>> ");
            this.dateOfBirth = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter address Customer>>>>> ");
            this.address = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter email address Customer>>>>> ");
            this.email = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter phone Customer>>>>> ");
            this.phoneNumber = Convert.ToString(Console.ReadLine());
        }
      
    }
}
