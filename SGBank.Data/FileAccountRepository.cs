using SGBank.Models;
using SGBank.Models.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {


        string path = ConfigurationManager.AppSettings["File"].ToString();
        List<Account> accountList = new List<Account>();
        Account _account = new Account();
        private void FR()
        {

            //string path = @"\Accounts.txt";

            string[] rows = File.ReadAllLines(path);
            for (int i = 0; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Account _aToList = new Account();
                _aToList.AccountNumber = columns[0];
                _aToList.Name = columns[1];
                _aToList.Balance = decimal.Parse(columns[2]);

                if (columns[3] == "F")
                {
                    _aToList.Type = AccountType.Free;
                }
                else if (columns[3] == "B")
                {
                    _aToList.Type = AccountType.Basic;
                }
                else if (columns[3] == "P")
                {
                    _aToList.Type = AccountType.Premium;
                }

                accountList.Add(_aToList);
            }


        }

        public Account LoadAccount(string AccountNumber)
        {
            FR();

            foreach (var a in accountList)
            {
                if (a.AccountNumber.Contains(AccountNumber))
                {
                    return a;
                }
            }
            return null;
        }

        public void SaveAccount(Account account)
        {
            var obj = accountList.Find(x => x.AccountNumber == account.AccountNumber);
            if (obj != null) obj.Balance = account.Balance;



            using (TextWriter tw = new StreamWriter(path))
            {

                foreach (Account a in accountList)
                {
                    string type = null;
                    if (a.Type == AccountType.Free)
                    {
                        type = "F";
                    }
                    else if (a.Type == AccountType.Basic)
                    {
                        type = "B";
                    }
                    else if (a.Type == AccountType.Premium)
                    {
                        type = "P";
                    }


                    tw.WriteLine(string.Format("{0},{1},{2},{3}", a.AccountNumber, a.Name, a.Balance, type));
                }
            }


        }


    }
}

