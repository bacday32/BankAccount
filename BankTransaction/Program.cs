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
                                    Deposit(docTransaction, nodeElement, newTransaction, minimum);
                                    nodeElement.ChildNodes[8].InnerText = (double.Parse(nodeElement.ChildNodes[8].InnerText) + newTransaction.amount).ToString();
                                    docAccount.Save("Account.xml");
                                    Console.WriteLine("Deposit successful!!!");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    newTransaction.toAccount = fromAccount;
                                    newTransaction.fromAccount = fromAccount;
                                    Withdraw(docTransaction,nodeElement, newTransaction, minimum);
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
                                    Transfer(docTransaction, nodeElement, newTransaction, minimum);
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
                                            Saving(docTransaction, nodeElement, newTransaction, newSaving);
                                            XmlNodeList node = docSaving.GetElementsByTagName("Saving");
                                            int id = 0;                                        
                                            for (int i = 0; i <node.Count; i++)
                                            {
                                               id =int.Parse(node[node.Count - 1].ChildNodes[0].InnerText) + 1;
                                            }
                                            newSaving.idSaving = id;
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
                                            nodeInteres.InnerText =  newSaving.interesRate.ToString();
                                            XmlNode nodeTimeStart = docSaving.CreateElement("timeAccount");
                                            nodeTimeStart.InnerText = DateTime.Now.ToString();
                                            XmlNode nodeRate = docSaving.CreateElement("rate");
                                            nodeRate.InnerText = newSaving.rate.ToString();
                                            //append element in node
                                            nodeChild.AppendChild(nodeId);
                                            nodeChild.AppendChild(nodeAccount);
                                            nodeChild.AppendChild(nodeDuration);
                                            nodeChild.AppendChild(nodeAmount);
                                            nodeChild.AppendChild(nodeInteres);
                                            nodeChild.AppendChild(nodeTimeStart);
                                            nodeChild.AppendChild(nodeRate);
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
                                    MaturitySaving(docSaving, docAccount, nodeElement);
                                    break;
                                case 6:
                                    Console.WriteLine("1.Disable \n2.Active ");
                                    int num3 = int.Parse(Console.ReadLine());
                                    switch (num3)
                                    {
                                        case 1:
                                            DisableAccount(docAccount, nodeElement);
                                            break;
                                        case 2:
                                            ActiveAccount(docAccount, nodeElement);
                                            break;
                                    }
                                    break;
                                case 7:
                                    DisplayTransaction(docTransaction, nodeElement);                               
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
            Console.WriteLine("Choose a number");
            Console.WriteLine("1: >>>>>Deposit!!!");
            Console.WriteLine("2: >>>>>Withdraw!!!");
            Console.WriteLine("3: >>>>>Transfer!!!");
            Console.WriteLine("4: >>>>>Create a account saving!!!");
            Console.WriteLine("5: >>>>>Saving maturity!!!");
            Console.WriteLine("6: >>>>>Account freeze/active!!! ");
            Console.WriteLine("7: >>>>>Display history transaction!!!");
            Console.WriteLine("8: >>>>>Logout<<<<<");
        }
        public static void Login(string userName, string passWord)
        {
            int count = 0;
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
            newAccount.idAccount = node.Count + 1;
            newAccount.idCustomer = node.Count + 1;
            //create node and element
            XmlNode nodeChild = document.CreateNode(XmlNodeType.Element, "Account", null);
            XmlNode nodeIdAccount = document.CreateElement("idAccount");
            nodeIdAccount.InnerText = newAccount.idAccount.ToString();
            XmlNode nodeAccountNumber = document.CreateElement("accountNumber");
            nodeAccountNumber.InnerText = (newAccount.idAccount + 1000000).ToString();
            XmlNode nodeFullName = document.CreateElement("fullName");
            nodeFullName.InnerText = newAccount.fullName.ToString();
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
            XmlNode nodeDisable = document.CreateElement("disable");
            nodeDisable.InnerText = newAccount.disable.ToString();
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
            nodeChild.AppendChild(nodeDisable);
            //append node in root and save file
            document.DocumentElement.AppendChild(nodeChild);
            document.Save("Account.xml");
            Console.WriteLine("REGISTER SUCCESSFUL!!!!!");
            Console.ReadLine();
        }
        static void Deposit(XmlDocument docTransaction, XmlNode nodeAccount, Transaction newTransaction, int minimum)
        {
            XmlDocument docAccount = new XmlDocument();
            docAccount.Load("Account.xml");
            if (nodeAccount.ChildNodes[11].InnerText == false.ToString())
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
            }
            else
            {
                Console.WriteLine("The account has been disable!!!!!");
                Console.WriteLine("Re-Login and active account!!!!! ");
                Console.ReadLine();
                Environment.Exit(0);
            }           
        }
        static void Withdraw(XmlDocument docTransaction, XmlNode nodeAccount, Transaction newTransaction, int minimum)
        {
            XmlDocument docAccount = new XmlDocument();
            docAccount.Load("Account.xml");
            if (nodeAccount.ChildNodes[11].InnerText == false.ToString())
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
            else
            {
                Console.WriteLine("The account has been disable!!!!!");
                Console.WriteLine("Re-Login and active account!!!!! ");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        static void Transfer(XmlDocument docTransaction, XmlNode nodeAccount, Transaction newTransaction, int minimum)
        {
            XmlDocument docAccount = new XmlDocument();
            docAccount.Load("Account.xml");
            if (nodeAccount.ChildNodes[11].InnerText == false.ToString())
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
            else
            {
                Console.WriteLine("The account has been disable!!!!!");
                Console.WriteLine("Re-Login and active account!!!!! ");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        static void Saving(XmlDocument docTransaction, XmlNode nodeAccount, Transaction newTransaction, Saving newSaving)
        {
            XmlDocument docAccount = new XmlDocument();
            docAccount.Load("Account.xml");
            if (nodeAccount.ChildNodes[11].InnerText == false.ToString())
            {
                docTransaction.Load("Transaction.xml");
                //create node and element
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
                nodeFromAccount.InnerText = nodeAccount.ChildNodes[1].InnerText;
                XmlNode nodeToAccount = docTransaction.CreateElement("toAccount");
                nodeToAccount.InnerText = newSaving.accountNumber.ToString();
                XmlNode nodeBalance = docTransaction.CreateElement("balance");
                nodeBalance.InnerText = 0.ToString();
                XmlNode nodeTypeTransaction = docTransaction.CreateElement("typeTransaction");
                nodeTypeTransaction.InnerText = "open saving";                
                //append element in root
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
            else
            {
                Console.WriteLine("The account has been disable!!!!!");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        static void DisableAccount(XmlDocument docAccount, XmlNode nodeAccount)
        {
            nodeAccount.ChildNodes[11].InnerText = "True";
            docAccount.Save("Account.xml");
        }
        static void ActiveAccount(XmlDocument docAccount, XmlNode nodeAccount)
        {
            nodeAccount.ChildNodes[11].InnerText = "False";
            docAccount.Save("Account.xml");
        }
        static void DisplayTransaction(XmlDocument docTransaction, XmlNode nodeAccount)
        {
            docTransaction.Load("Transaction.xml");
            XmlNodeList nodes = docTransaction.SelectNodes("/Transactions/Transaction");
            int count = 1;
            foreach (XmlNode node in nodes)
            {
                if (node.ChildNodes[5].InnerText == nodeAccount.ChildNodes[1].InnerText || node.ChildNodes[6].InnerText == nodeAccount.ChildNodes[1].InnerText)
                {
                    string id = node["id"].InnerText;
                    string balance = node["balance"].InnerText;
                    string content = node["content"].InnerText;
                    string amount = node["amount"].InnerText;
                    string dateTime = node["dateTime"].InnerText;
                    string fromaccount = node["fromAccount"].InnerText;
                    string toAccount = node["toAccount"].InnerText;
                    string typeTransaction = node["typeTransaction"].InnerText;
                    Console.WriteLine("Transaction: " + count++);
                    Console.WriteLine("id: {0},balance: {1},content :{2}, amount: {3}, datetime: {4},fromAccount: {5} , toAccount : {6} , typeTransaction : {7} \n", id, balance, content, amount, dateTime, fromaccount, toAccount, typeTransaction);
                }
            }
            Console.ReadLine();
        }
        static void MaturitySaving(XmlDocument docSaving,XmlDocument docAccount, XmlNode nodeAccount)
        {
            double total;
            XmlElement elementSaving = docSaving.DocumentElement;
            XmlNode nodeElementSaving = elementSaving.SelectSingleNode("Saving[accountNumber='" + nodeAccount.ChildNodes[1].InnerText + "']");
            DateTime current = DateTime.Now;
            DateTime maturity = DateTime.Parse(nodeElementSaving.ChildNodes[5].InnerText);
            TimeSpan time = current - maturity;
            try
            {
                if (time.Days >= double.Parse(nodeElementSaving.ChildNodes[2].InnerText))
                {
                    total = double.Parse(nodeElementSaving.ChildNodes[3].InnerText) + double.Parse(nodeElementSaving.ChildNodes[4].InnerText);
                    nodeAccount.ChildNodes[8].InnerText = Convert.ToString(double.Parse(nodeAccount.ChildNodes[8].InnerText) + total);
                    elementSaving.RemoveChild(nodeElementSaving);
                    docSaving.Save("Saving.xml");
                    docAccount.Save("Account.xml");
                }
                else if (time.Days < double.Parse(nodeElementSaving.ChildNodes[2].InnerText))
                {
                    total = double.Parse(nodeElementSaving.ChildNodes[3].InnerText) + double.Parse(nodeElementSaving.ChildNodes[4].InnerText) * time.Days / double.Parse(nodeElementSaving.ChildNodes[2].InnerText);
                    nodeAccount.ChildNodes[8].InnerText = Convert.ToString(double.Parse(nodeAccount.ChildNodes[8].InnerText) + total);
                    elementSaving.RemoveChild(nodeElementSaving);
                    docSaving.Save("Saving.xml");
                    docAccount.Save("Account.xml");
                    Console.WriteLine("Maturity successful!!!!!");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("You don't have a savings account ", ex);
                Console.WriteLine("Please RE-LOGIN and create saving account!!!!!");
                Console.ReadLine();
            }
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
