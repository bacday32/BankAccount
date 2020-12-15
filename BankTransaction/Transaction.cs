using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Transaction()
        {

        }
        public Transaction(int idTransaction, double balance, string content, float amount, DateTime transactionTime, int fromAccount, int toAccount,string typeTransaction)
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
        public void AddDeposit()
        {
            Console.WriteLine("Enter amount of money: ");
            this.amount = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter content transaction: ");
            this.content = Convert.ToString(Console.ReadLine());
            this.typeTransaction = "Deposit";
        }
        public void AddWithDraw()
        {
            Console.WriteLine("Enter amount of money:   ");
            this.amount = double.Parse(Console.ReadLine());
            this.typeTransaction = "Withdraw";
        }
        public void AddTransfer()
        {
            Console.WriteLine("Enter amount of money: ");
            this.amount = double.Parse(Console.ReadLine());
            Console.WriteLine("Enter content transaction: ");
            this.content = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter receive account: ");
            this.toAccount = double.Parse(Console.ReadLine());
            this.typeTransaction = "Transfer";
        }
        public override string ToString()
        {
            return this.idTransaction + " , " +  this.balance + " , " + this.content + " , " + this.amount + " , " + this.transactionTime + " , " + this.fromAccount + " , " + this.toAccount;
        }
        public void Display()
        {
            Console.WriteLine("id transaction : {0},balance :{1}, content : {2},amount : {3},transaction time : {4},from account : {5}, to account : {6}", idTransaction, balance, content, amount, transactionTime, fromAccount, toAccount);
        }
    }
}
