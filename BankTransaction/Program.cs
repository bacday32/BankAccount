﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;

namespace BankTransaction
{
    class Program
    {

        static void Main(string[] args)
        {
            do
            {
                int minimum = 50000;
                XmlDocument docAccount = new XmlDocument();
                docAccount.Load("Account.xml");
                XmlDocument docTransaction = new XmlDocument();
                docTransaction.Load("Transaction.xml");
                XmlDocument docSaving = new XmlDocument();
                docSaving.Load("Saving.xml");
                Menu();
                int number = int.Parse(Console.ReadLine());
                switch (number)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("Enter user name: ");
                            string userName = Convert.ToString(Console.ReadLine());
                            Console.WriteLine("Enter pass word: ");
                            string passWord = Convert.ToString(Console.ReadLine());
                            Login(userName, passWord);
                            Transaction newTransaction = new Transaction();
                            Options();
                            int number2 = int.Parse(Console.ReadLine());
                            XmlElement element = docAccount.DocumentElement;
                            XmlNode nodeElement = element.SelectSingleNode("Account[userName='" + userName + "']");
                            var fromAccount = new double();
                            double.TryParse(nodeElement.ChildNodes[1].InnerText, out fromAccount);
                            switch (number2)
                            {
                                case 1:
                                    newTransaction.toAccount = fromAccount;
                                    newTransaction.fromAccount = fromAccount;
                                    Deposit(docTransaction, newTransaction, minimum);
                                    nodeElement.ChildNodes[8].InnerText = (double.Parse(nodeElement.ChildNodes[8].InnerText) + newTransaction.amount).ToString();
                                    docAccount.Save("Account.xml");
                                    Console.WriteLine("Deposit successful!!!");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    newTransaction.toAccount = fromAccount;
                                    newTransaction.fromAccount = fromAccount;
                                    Withdraw(docTransaction, newTransaction, minimum);
                                    if ((double.Parse(nodeElement.ChildNodes[8].InnerText) - minimum) > newTransaction.amount)
                                    {
                                        nodeElement.ChildNodes[8].InnerText = (double.Parse(nodeElement.ChildNodes[8].InnerText) - newTransaction.amount).ToString();
                                        docAccount.Save("Account.xml");
                                        Console.WriteLine("Withdraw successful!!!");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("insufficient balance!!!!!");
                                        Console.ReadLine();
                                    }
                                    break;
                                case 3:
                                    newTransaction.fromAccount = fromAccount;
                                    Transfer(docTransaction, newTransaction, minimum);
                                    XmlNode nodeToAccount = element.SelectSingleNode("Account[accountNumber='" + newTransaction.toAccount.ToString() + "']");
                                    if ((double.Parse(nodeElement.ChildNodes[8].InnerText) - minimum) > newTransaction.amount)
                                    {
                                        nodeToAccount.ChildNodes[8].InnerText = (double.Parse(nodeToAccount.ChildNodes[8].InnerText) + newTransaction.amount).ToString();
                                        nodeElement.ChildNodes[8].InnerText = (double.Parse(nodeElement.ChildNodes[8].InnerText) - newTransaction.amount).ToString();
                                        docAccount.Save("Account.xml");
                                        Console.WriteLine("Transfer successful!!!");
                                        Console.ReadLine();
                                    }
                                    else
                                    {
                                        Console.WriteLine("insufficient balance!!!!!");
                                        Console.ReadLine();
                                    }
                                    break;
                                case 4:
                                    do
                                    {
                                        Saving newSaving = new Saving();
                                        newSaving.AddSaving();
                                        if (newSaving.amount < double.Parse(nodeElement.ChildNodes[8].InnerText) - minimum)
                                        {
                                            Saving(docTransaction, newTransaction, newSaving);
                                            XmlNodeList node = docSaving.GetElementsByTagName("Saving");
                                            newSaving.idSaving = node.Count + 1;
                                            //create node and element
                                            XmlNode nodeChild = docSaving.CreateNode(XmlNodeType.Element, "Saving", null);
                                            XmlNode nodeId = docSaving.CreateElement("id");
                                            nodeId.InnerText = newSaving.idSaving.ToString();
                                            XmlNode nodeAccount = docSaving.CreateElement("accountNumber");
                                            nodeAccount.InnerText = nodeElement.ChildNodes[1].InnerText;
                                            XmlNode nodeDuration = docSaving.CreateElement("duration");
                                            nodeDuration.InnerText = newSaving.duration.ToString();
                                            XmlNode nodeAmount = docSaving.CreateElement("amount");
                                            nodeAmount.InnerText = newSaving.amount.ToString();
                                            XmlNode nodeInteres = docSaving.CreateElement("interes");
                                            nodeInteres.InnerText = (newSaving.duration * newSaving.interesRate).ToString();
                                            XmlNode nodeTimeStart = docSaving.CreateElement("timeAccount");
                                            nodeTimeStart.InnerText = DateTime.Now.ToString();
                                            //append element in node
                                            nodeChild.AppendChild(nodeId);
                                            nodeChild.AppendChild(nodeAccount);
                                            nodeChild.AppendChild(nodeDuration);
                                            nodeChild.AppendChild(nodeAmount);
                                            nodeChild.AppendChild(nodeInteres);
                                            nodeChild.AppendChild(nodeTimeStart);
                                            //append node in root and save file
                                            docSaving.DocumentElement.AppendChild(nodeChild);
                                            docSaving.Save("Saving.xml");
                                            nodeElement.ChildNodes[8].InnerText = (double.Parse(nodeElement.ChildNodes[8].InnerText) - newSaving.amount).ToString();
                                            docAccount.Save("Account.xml");
                                            Console.WriteLine("Open saving account successful!!!!!");
                                            Console.ReadLine();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("insufficient balance!!!!!");
                                            Console.ReadLine();
                                        }
                                    }
                                    while (true);
                                    break;
                                case 5:
                                    XmlElement elementSaving = docSaving.DocumentElement;
                                    XmlNode nodeElementSaving = elementSaving.SelectSingleNode("Saving[accountNumber='" + nodeElement.ChildNodes[1].InnerText + "']");
                                    DateTime current = DateTime.Now;
                                    DateTime maturity = DateTime.Parse(nodeElementSaving.ChildNodes[5].InnerText);
                                    TimeSpan time = current - maturity;
                                    if (Convert.ToDouble(time.Days) <= double.Parse(nodeElement.ChildNodes[2].InnerText))
                                    {
                                        double total = double.Parse(nodeElementSaving.ChildNodes[3].InnerText) + double.Parse(nodeElementSaving.ChildNodes[4].InnerText);
                                        nodeElement.ChildNodes[8].InnerText = Convert.ToString(double.Parse(nodeElement.ChildNodes[8].InnerText) + total);
                                        docAccount.Save("Account.xml");
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    break;
                                case 6:
                                    
                                    break;
                                case 7:
                                    //Account newAccount = new Account();
                                    //newAccount.accountNumber = int.Parse(nodeElement.ChildNodes[1].InnerText);
                                    
                                    XmlElement elementTransaction = docTransaction.DocumentElement;
                                    XmlNodeList nodeList = docTransaction.SelectNodes("Transaction");
                                    foreach (XmlNode nodeElementTransaction in nodeList)
                                    {
                                        if (nodeElementTransaction == elementTransaction.SelectSingleNode("Transaction[fromAccount='" + nodeElement.ChildNodes[1].InnerText + "']"))
                                        {
                                            Console.WriteLine(nodeElementTransaction.ToString());
                                            Console.WriteLine("transaction");
                                            Console.ReadLine();                                 
                                        }
                                        
                                    }
                                    Console.ReadLine();

                                    break;
                                case 8:
                                                                        
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                        while (true);

                        break;
                    case 2:
                        Register();
                        Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }
            while (true);

        }
        static void Menu()
        {
            Console.WriteLine(">>>>>Menu<<<<<");
            Console.WriteLine("1: Login!!!");
            Console.WriteLine("2: Register!!!");
        }
        static void Options()
        {
<<<<<<< HEAD
=======
            Console.WriteLine("Choose a number");
            Console.WriteLine("1: >>>>>Deposit!!!");
            Console.WriteLine("2: >>>>>Withdraw!!!");
            Console.WriteLine("3: >>>>>Transfer!!!");
            Console.WriteLine("4: >>>>>Create a account saving!!!");
            Console.WriteLine("5: >>>>>Saving maturity!!!");
            Console.WriteLine("6: >>>>>Account freeze!!! ");
            Console.WriteLine("7: >>>>>Display history transaction!!!");
            Console.WriteLine("8: >>>>>Logout<<<<<");
        }
        public static void Login(string userName, string passWord)
        {
            int count = 0;
>>>>>>> b435b62514b1f41ce5c3289e50a15edf0a227d54
            XElement element = XElement.Load("Account.xml");
            IEnumerable<XElement> accounts = element.Elements();
            foreach (var account in accounts)
            {
                string us = account.Element("userName").Value;
                string pass = account.Element("passWord").Value;
                if ((userName == us) && (passWord == pass))
                {
                    Console.WriteLine("LOGIN SUCCESSFUL");
                    Console.ReadLine();
                    count++;
                    break;
                }
<<<<<<< HEAD
                else
                {
                    Console.WriteLine("login faild!!!");
                    Console.WriteLine("Please log in again!!!");
                    Console.ReadLine();
                }
                break;
=======
>>>>>>> b435b62514b1f41ce5c3289e50a15edf0a227d54
            }
            if (count == 0)
            {
                Console.WriteLine("login faild!!!");
                Console.WriteLine("Please log in again!!!");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        static void Register()
        {
            Account newAccount = new Account();
            newAccount.AddAccount();
            XmlDocument document = new XmlDocument();
            document.Load("Account.xml");
            XmlNodeList node = document.GetElementsByTagName("Account");
<<<<<<< HEAD

            newAccount.idAccount = node.Count + 1;
            newAccount.idCustomer = node.Count + 1;


=======
            newAccount.idAccount = node.Count + 1;
            newAccount.idCustomer = node.Count + 1;
            //create node and element
>>>>>>> b435b62514b1f41ce5c3289e50a15edf0a227d54
            XmlNode nodeChild = document.CreateNode(XmlNodeType.Element, "Account", null);
            XmlNode nodeIdAccount = document.CreateElement("idAccount");
            nodeIdAccount.InnerText = newAccount.idAccount.ToString();
            XmlNode nodeAccountNumber = document.CreateElement("accountNumber");
            nodeAccountNumber.InnerText = (newAccount.idAccount + 1000000).ToString();
<<<<<<< HEAD

            XmlNode nodeFullName = document.CreateElement("fullName");
            nodeFullName.InnerText = newAccount.fullName.ToString();

=======
            XmlNode nodeFullName = document.CreateElement("fullName");
            nodeFullName.InnerText = newAccount.fullName.ToString();
>>>>>>> b435b62514b1f41ce5c3289e50a15edf0a227d54
            XmlNode nodeDateOfBirth = document.CreateElement("dateOfBirth");
            nodeDateOfBirth.InnerText = newAccount.dateOfBirth.ToString();
            XmlNode nodePhoneNumber = document.CreateElement("phoneNumber");
            nodePhoneNumber.InnerText = newAccount.phoneNumber.ToString();
            XmlNode nodeEmail = document.CreateElement("email");
            nodeEmail.InnerText = newAccount.email.ToString();
            XmlNode nodeUserName = document.CreateElement("userName");
            nodeUserName.InnerText = newAccount.userName.ToString();
            XmlNode nodePassWord = document.CreateElement("passWord");
            nodePassWord.InnerText = newAccount.passWord.ToString();
            XmlNode nodeBalance = document.CreateElement("balance");
            nodeBalance.InnerText = newAccount.balance.ToString();
            XmlNode nodeAddress = document.CreateElement("address");
            nodeAddress.InnerText = newAccount.address.ToString();
            XmlNode nodeTypeAccount = document.CreateElement("typeAccount");
            nodeTypeAccount.InnerText = newAccount.typeAccount.ToString();
            // append element in node
            nodeChild.AppendChild(nodeIdAccount);
            nodeChild.AppendChild(nodeAccountNumber);
            nodeChild.AppendChild(nodeFullName);
            nodeChild.AppendChild(nodeDateOfBirth);
            nodeChild.AppendChild(nodePhoneNumber);
            nodeChild.AppendChild(nodeEmail);
            nodeChild.AppendChild(nodeUserName);
            nodeChild.AppendChild(nodePassWord);
            nodeChild.AppendChild(nodeBalance);
            nodeChild.AppendChild(nodeAddress);
            nodeChild.AppendChild(nodeTypeAccount);
            //append node in root and save file
            document.DocumentElement.AppendChild(nodeChild);
            document.Save("Account.xml");
            Console.WriteLine("REGISTER SUCCESSFUL!!!!!");
            Console.ReadLine();
        }
        static void Deposit(XmlDocument docTransaction, Transaction newTransaction, int minimum)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;
            newTransaction.AddDeposit(minimum);
            //create node and element
            XmlNode nodeChild = docTransaction.CreateNode(XmlNodeType.Element, "Transaction", null);
            XmlNode nodeId = docTransaction.CreateElement("id");
            nodeId.InnerText = newTransaction.idTransaction.ToString();
            XmlNode nodeContent = docTransaction.CreateElement("content");
            nodeContent.InnerText = newTransaction.content.ToString();
            XmlNode nodeAmount = docTransaction.CreateElement("amount");
            nodeAmount.InnerText = newTransaction.amount.ToString();
<<<<<<< HEAD

=======
>>>>>>> b435b62514b1f41ce5c3289e50a15edf0a227d54
            XmlNode nodeDateTine = docTransaction.CreateElement("dateTime");
            nodeDateTine.InnerText = newTransaction.transactionTime.ToString();
            XmlNode nodeFromAccount = docTransaction.CreateElement("fromAccount");
            nodeFromAccount.InnerText = newTransaction.fromAccount.ToString();
            XmlNode nodeToAccount = docTransaction.CreateElement("toAccount");
            nodeToAccount.InnerText = newTransaction.toAccount.ToString();
            XmlNode nodeBalance = docTransaction.CreateElement("balance");
            nodeBalance.InnerText = String.Format("{0:0.00}", (newTransaction.balance + newTransaction.amount));
            XmlNode nodeTypeTransaction = docTransaction.CreateElement("typeTransaction");
            nodeTypeTransaction.InnerText = newTransaction.typeTransaction;
            // append element in node
            nodeChild.AppendChild(nodeId);
            nodeChild.AppendChild(nodeBalance);
            nodeChild.AppendChild(nodeContent);
            nodeChild.AppendChild(nodeAmount);
            nodeChild.AppendChild(nodeDateTine);
            nodeChild.AppendChild(nodeFromAccount);
            nodeChild.AppendChild(nodeToAccount);
            nodeChild.AppendChild(nodeTypeTransaction);
            //append node in root and save file
            docTransaction.DocumentElement.AppendChild(nodeChild);
            docTransaction.Save("Transaction.xml");
            Console.ReadLine();
        }
        static void Withdraw(XmlDocument docTransaction, Transaction newTransaction, int minimum)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;
            newTransaction.AddWithDraw(minimum);
            //create and element
            XmlNode nodeChild = docTransaction.CreateNode(XmlNodeType.Element, "Transaction", null);
            XmlNode nodeId = docTransaction.CreateElement("id");
            nodeId.InnerText = newTransaction.idTransaction.ToString();
            XmlNode nodeContent = docTransaction.CreateElement("content");
            nodeContent.InnerText = newTransaction.content.ToString();
            XmlNode nodeAmount = docTransaction.CreateElement("amount");
            nodeAmount.InnerText = newTransaction.amount.ToString();
            XmlNode nodeDateTine = docTransaction.CreateElement("dateTime");
            nodeDateTine.InnerText = newTransaction.transactionTime.ToString();
            XmlNode nodeFromAccount = docTransaction.CreateElement("fromAccount");
            nodeFromAccount.InnerText = newTransaction.fromAccount.ToString();
            XmlNode nodeToAccount = docTransaction.CreateElement("toAccount");
            nodeToAccount.InnerText = newTransaction.toAccount.ToString();
            XmlNode nodeBalance = docTransaction.CreateElement("balance");
            nodeBalance.InnerText = (newTransaction.balance - newTransaction.amount).ToString();
            XmlNode nodeTypeTransaction = docTransaction.CreateElement("typeTransaction");
            nodeTypeTransaction.InnerText = newTransaction.typeTransaction;
            // append element in node
            nodeChild.AppendChild(nodeId);
            nodeChild.AppendChild(nodeBalance);
            nodeChild.AppendChild(nodeContent);
            nodeChild.AppendChild(nodeAmount);
            nodeChild.AppendChild(nodeDateTine);
            nodeChild.AppendChild(nodeFromAccount);
            nodeChild.AppendChild(nodeToAccount);
            nodeChild.AppendChild(nodeTypeTransaction);


            docTransaction.DocumentElement.AppendChild(nodeChild);
            docTransaction.Save("Transaction.xml");
            Console.ReadLine();

        }
        static void Transfer(XmlDocument docTransaction, Transaction newTransaction, int minimum)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;
            newTransaction.AddTransfer(minimum);
            //create node and element 
            XmlNode nodeChild = docTransaction.CreateNode(XmlNodeType.Element, "Transaction", null);
            XmlNode nodeId = docTransaction.CreateElement("id");
            nodeId.InnerText = newTransaction.idTransaction.ToString();
            XmlNode nodeContent = docTransaction.CreateElement("content");
            nodeContent.InnerText = newTransaction.content.ToString();
            XmlNode nodeAmount = docTransaction.CreateElement("amount");
            nodeAmount.InnerText = newTransaction.amount.ToString();
            XmlNode nodeDateTine = docTransaction.CreateElement("dateTime");
            nodeDateTine.InnerText = newTransaction.transactionTime.ToString();
            XmlNode nodeFromAccount = docTransaction.CreateElement("fromAccount");
            nodeFromAccount.InnerText = newTransaction.fromAccount.ToString();
            XmlNode nodeToAccount = docTransaction.CreateElement("toAccount");
            nodeToAccount.InnerText = newTransaction.toAccount.ToString();
            XmlNode nodeBalance = docTransaction.CreateElement("balance");
            nodeBalance.InnerText = (newTransaction.balance - newTransaction.amount).ToString();
            XmlNode nodeTypeTransaction = docTransaction.CreateElement("typeTransaction");
            nodeTypeTransaction.InnerText = newTransaction.typeTransaction;
            //append elenment in node
            nodeChild.AppendChild(nodeId);
            nodeChild.AppendChild(nodeBalance);
            nodeChild.AppendChild(nodeContent);
            nodeChild.AppendChild(nodeAmount);
            nodeChild.AppendChild(nodeDateTine);
            nodeChild.AppendChild(nodeFromAccount);
            nodeChild.AppendChild(nodeToAccount);
            nodeChild.AppendChild(nodeTypeTransaction);
            //append node in root and save file
            docTransaction.DocumentElement.AppendChild(nodeChild);
            docTransaction.Save("Transaction.xml");
            Console.ReadLine();
        }
        static void Saving(XmlDocument docTransaction, Transaction newTransaction, Saving newSaving)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;


            XmlNode nodeChild = docTransaction.CreateNode(XmlNodeType.Element, "Transaction", null);

            XmlNode nodeId = docTransaction.CreateElement("id");
            nodeId.InnerText = newTransaction.idTransaction.ToString();

            XmlNode nodeContent = docTransaction.CreateElement("content");
            nodeContent.InnerText = "open saving";

            XmlNode nodeAmount = docTransaction.CreateElement("amount");
            nodeAmount.InnerText = newSaving.amount.ToString();

            XmlNode nodeDateTine = docTransaction.CreateElement("dateTime");
            nodeDateTine.InnerText = DateTime.Now.ToString();

            XmlNode nodeFromAccount = docTransaction.CreateElement("fromAccount");
            nodeFromAccount.InnerText = newSaving.accountNumber.ToString();

            XmlNode nodeToAccount = docTransaction.CreateElement("toAccount");
            nodeToAccount.InnerText = newSaving.accountNumber.ToString();

            XmlNode nodeBalance = docTransaction.CreateElement("balance");
            nodeBalance.InnerText = 0.ToString();

            XmlNode nodeTypeTransaction = docTransaction.CreateElement("typeTransaction");
            nodeTypeTransaction.InnerText = "open saving";



            nodeChild.AppendChild(nodeId);
            nodeChild.AppendChild(nodeBalance);
            nodeChild.AppendChild(nodeContent);
            nodeChild.AppendChild(nodeAmount);
            nodeChild.AppendChild(nodeDateTine);
            nodeChild.AppendChild(nodeFromAccount);
            nodeChild.AppendChild(nodeToAccount);
            nodeChild.AppendChild(nodeTypeTransaction);

            docTransaction.DocumentElement.AppendChild(nodeChild);
            docTransaction.Save("Transaction.xml");
            Console.ReadLine();
        }
        //static void DisplayTransaction(XmlDocument docTransaction)
        //{
        //    docTransaction.Load("Transaction.xml");
        //    XmlNodeList nodes = docTransaction.SelectNodes("Transaction");
        //    foreach (XmlNode node in nodes)
        //    {
        //        if(node.SelectSingleNode(""))
        //        {

        //        }
        //    }
        //}
        static void Maturity()
        {
            XmlDocument docSaving = new XmlDocument();
            XmlElement savingElement = docSaving.DocumentElement;
            docSaving.Load("Saving.xml");
            // XmlNode node = savingElement.SelectSingleNode("Saving[='" + TimeSpan + "']");
        }
        static void ShowAccount()
        {
            Account newAccount = new Account();
            newAccount.DisplayAccount();
        }
        static void ShowTransaction()
        {
            Transaction newTransaction = new Transaction();
            newTransaction.DisplayTransaction();
        }
    }
}
