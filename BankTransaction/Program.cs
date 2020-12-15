using System;
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
            int minimum = 50000;
            XmlDocument docAccount = new XmlDocument();
            docAccount.Load("Account.xml");
            XmlDocument docTransaction = new XmlDocument();
            docTransaction.Load("Transaction.xml");

            Menu();
            int number = int.Parse(Console.ReadLine());
            switch (number)
            {
                case 1:
                    Console.WriteLine("Enter user name: ");
                    string userName = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Enter pass word: ");
                    string passWord = Convert.ToString(Console.ReadLine());
<<<<<<< HEAD
                    Login(userName, passWord);

                    Transaction newTransaction = new Transaction();                    
=======
                    Login(userName,passWord);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml("Account.xml");
                    XmlNode node;
                    XmlElement root = doc.DocumentElement;
                    node = root.SelectSingleNode("Account[userName='" + userName + "]");
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
                    Options();
                    
                    int number2 = int.Parse(Console.ReadLine());
                    XmlElement element = docAccount.DocumentElement;
                    XmlNode nodeElement = element.SelectSingleNode("Account[userName='" + userName + "']");
                    switch (number2)
                    {
                        case 1:
<<<<<<< HEAD
                            var toAccount = new double();
                            double.TryParse(nodeElement.ChildNodes[1].InnerText,out toAccount);
                            newTransaction.toAccount = toAccount;
                            Deposit(docTransaction, newTransaction);
                            nodeElement.ChildNodes[8].InnerText = (double.Parse(nodeElement.ChildNodes[8].InnerText) + newTransaction.amount).ToString();
                            docAccount.Save("Account.xml");
=======
                            
                            
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
                            break;
                        case 2:
                            Withdraw(docTransaction, newTransaction);
                            if ((int.Parse(nodeElement.ChildNodes[8].InnerText) - minimum) > newTransaction.amount)
                            {
                                nodeElement.ChildNodes[8].InnerText = (int.Parse(nodeElement.ChildNodes[8].InnerText) - newTransaction.amount).ToString();
                                docAccount.Save("Account.xml");
                            }
                            else
                            {
                                Console.WriteLine("insufficient balance!!!!!");
                                Console.ReadLine();
                            }
                            break;
                        case 3:
                            Transfer(docTransaction, newTransaction);
                            XmlNode nodeToAccount = element.SelectSingleNode("Account[accountNumber='" + newTransaction.toAccount.ToString() + "']");

                            if ((int.Parse(nodeElement.ChildNodes[8].InnerText) - minimum) > newTransaction.amount)
                            {
                                nodeToAccount.ChildNodes[8].InnerText = (int.Parse(nodeToAccount.ChildNodes[8].InnerText) + newTransaction.amount).ToString();
                                nodeElement.ChildNodes[8].InnerText = (int.Parse(nodeElement.ChildNodes[8].InnerText) - newTransaction.amount).ToString();
                                docAccount.Save("Account.xml");
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

                                    XmlDocument docSaving = new XmlDocument();
                                    docSaving.Load("Saving.xml");
                                    XmlNodeList node = docSaving.GetElementsByTagName("Saving");

                                    newSaving.idSaving = node.Count + 1;

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

                                    nodeChild.AppendChild(nodeId);
                                    nodeChild.AppendChild(nodeAccount);
                                    nodeChild.AppendChild(nodeDuration);
                                    nodeChild.AppendChild(nodeAmount);
                                    nodeChild.AppendChild(nodeInteres);
                                    nodeChild.AppendChild(nodeTimeStart);

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
                            break;
                        case 6:
                            break;
                        case 7:
                            Account newAccount = new Account();
                            newAccount.accountNumber = int.Parse(nodeElement.ChildNodes[1].InnerText);


                       
                            break;
                        case 8:

                            break;
                        default:
                            break;

                    }
                    break;
                case 2:
                    Register();
                    Console.ReadLine();
                    break;
                default:
                    break;
            }

        }
        static void Menu()
        {
            Console.WriteLine(">>>>>Menu<<<<<");
            Console.WriteLine("1: Login!!!");
            Console.WriteLine("2: Register!!!");
        }
        static void Login(string userName, string passWord)
        {
<<<<<<< HEAD
=======
            

>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
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
                    break;
                }
                else
                {
                    Console.WriteLine("login faild!!!");
                    Console.WriteLine("Please log in again!!!");
                    Console.ReadLine();
<<<<<<< HEAD
=======
                    Login(userName,passWord);
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a
                }
                break;
            }
        }
        static void Options()
        {
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
        static void Register()
        {
            Account newAccount = new Account();
<<<<<<< HEAD
            newAccount.AddAccount();
            XmlDocument document = new XmlDocument();
            document.Load("Account.xml");

            XmlNodeList node = document.GetElementsByTagName("Account");
=======
            XDocument doc = XDocument.Load(@"Account.xml");
            XElement xElement = new XElement("Account");
            xElement.SetElementValue("idAccount",newAccount.idAccount);
            xElement.SetElementValue("acountNumber",newAccount.accountNumber);
            xElement.SetElementValue("fullName",newAccount.userName);
            xElement.SetElementValue("dateOfBirth",newAccount.dateOfBirth);
            xElement.SetElementValue("phoneNumber");
            xElement.SetElementValue("email");
            xElement.SetElementValue("userName");
            xElement.SetElementValue("address");

            //XmlDocument document = new XmlDocument();
            //document.Load("Account.xml");
            //XmlNodeList list = document.GetElementsByTagName("Account");
            //int nodeNumber = list.Count;
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a

            newAccount.idAccount = node.Count + 1;
            newAccount.idCustomer = node.Count + 1;


<<<<<<< HEAD
            XmlNode nodeChild = document.CreateNode(XmlNodeType.Element, "Account", null);

            XmlNode nodeIdAccount = document.CreateElement("idAccount");
            nodeIdAccount.InnerText = newAccount.idAccount.ToString();

            XmlNode nodeAccountNumber = document.CreateElement("accountNumber");
            nodeAccountNumber.InnerText = (newAccount.idAccount + 1000000).ToString();
=======

            //Account newAccount = new Account();
            //newAccount.AddAccount();
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a

            XmlNode nodeFullName = document.CreateElement("fullName");
            nodeFullName.InnerText = newAccount.fullName.ToString();

<<<<<<< HEAD
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

            document.DocumentElement.AppendChild(nodeChild);
            document.Save("Account.xml");
            Console.WriteLine("REGISTER SUCCESSFUL!!!!!");
            Console.ReadLine();
        }
        static void Deposit(XmlDocument docTransaction, Transaction newTransaction)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;
            newTransaction.AddDeposit();


            XmlNode nodeChild = docTransaction.CreateNode(XmlNodeType.Element, "Transaction", null);

            XmlNode nodeId = docTransaction.CreateElement("id");
            nodeId.InnerText = newTransaction.idTransaction.ToString();

            XmlNode nodeContent = docTransaction.CreateElement("content");
            nodeContent.InnerText = newTransaction.content.ToString();

            XmlNode nodeAmount = docTransaction.CreateElement("amount");
            nodeAmount.InnerText = newTransaction.amount.ToString();
=======
            //XmlDocument doc = new XmlDocument();
            //doc.Load("Account.xml");
            //XmlNode root = doc.SelectSingleNode("Accounts");
            //XmlElement account = doc.CreateElement("Account");
            //root.AppendChild(account);

            //XmlElement idAccount = doc.CreateElement("idAccount");
            ////idAccount.Value = Convert.ToString(nodeNumber ++);
            //account.AppendChild(idAccount);

            //XmlElement acountNumber = doc.CreateElement("acountNumber");
            //acountNumber.Value = Convert.ToString(newAccount.accountNumber);
            //account.AppendChild(acountNumber);

            //XmlElement fullName = doc.CreateElement("fullName");
            //fullName.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(fullName);

            //XmlElement dateOfBirth = doc.CreateElement("dateOfBirth");
            //dateOfBirth.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(dateOfBirth);

            //XmlElement phoneNumber = doc.CreateElement("phoneNumber");
            //phoneNumber.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(phoneNumber);

            //XmlElement email = doc.CreateElement("email");
            //email.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(email);

            //XmlElement userName = doc.CreateElement("userName");
            //userName.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(userName);

            //XmlElement passWord = doc.CreateElement("passWord");
            //passWord.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(passWord);

            //XmlElement balance = doc.CreateElement("balance");
            //balance.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(balance);

            //XmlElement address = doc.CreateElement("address");
            //address.Value = Convert.ToString(newAccount.idAccount);
            //account.AppendChild(address);

            //Console.WriteLine(doc.InnerXml);
>>>>>>> 0e834b2c421faa2c99e2fa3d591a1baa9278978a

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
        static void Withdraw(XmlDocument docTransaction, Transaction newTransaction)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;
            newTransaction.AddWithDraw();

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

            nodeChild.AppendChild(nodeId);
            nodeChild.AppendChild(nodeBalance);
            nodeChild.AppendChild(nodeContent);
            nodeChild.AppendChild(nodeAmount);
            nodeChild.AppendChild(nodeDateTine);
            nodeChild.AppendChild(nodeFromAccount);
            nodeChild.AppendChild(nodeToAccount);


            docTransaction.DocumentElement.AppendChild(nodeChild);
            docTransaction.Save("Transaction.xml");
            Console.ReadLine();

        }
        static void Transfer(XmlDocument docTransaction, Transaction newTransaction)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList node = docTransaction.GetElementsByTagName("Transaction");
            newTransaction.idTransaction = node.Count + 1;
            newTransaction.AddTransfer();

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
        static void Maturity()
        {
            XmlDocument docSaving = new XmlDocument();
            XmlElement savingElement = docSaving.DocumentElement;
            docSaving.Load("Saving.xml");
           // XmlNode node = savingElement.SelectSingleNode("Saving[='" + TimeSpan + "']");
        }
    }
}
