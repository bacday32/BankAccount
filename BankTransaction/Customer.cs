using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTransaction
{
    class Customer
    {
<<<<<<< HEAD
        public int idCustomer { get; set; }
        public string fullName { get; set; }
        public string dateOfBirth { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }        
=======
        protected int idCustomer { get; set; }
        protected string fullName { get; set; }
        protected string dateOfBirth { get; set; }
        protected string address { get; set; }
        protected string email { get; set; }
        protected int phoneNumber { get; set; }
        
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
        public Customer()
        {

        }
<<<<<<< HEAD
        public Customer(int idCustomer, string fullName, string dateOfBirth, string address, string email, string phoneNumber)
=======
        public Customer(int idCustomer, string fullName, string dateOfBirthDay, string address, string email, int phoneNumber)
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
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
