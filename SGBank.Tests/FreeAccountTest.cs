using NUnit.Framework;
using SGBank.BLL;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    public class FreeAccountTest
    {
        [Test]
        public void CanLoadFreeAccountTestData()
        {

            AccountManager manager = AccountManagerFactory.Create();

            AccountLookupResponse response = manager.LookupAccount("12345");

            Assert.IsNotNull(response.Account);
            Assert.IsTrue(response.Success);
            Assert.AreEqual("12345", response.Account.AccountNumber);
        }

        [TestCase("12345", "Free Account", 100, AccountType.Free, 250, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, -100, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Basic, 50, false)]
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, true)]

        public void FreeAccountDepositRuleTest(string accountNumber,
                                                string name,
                                                decimal balance,
                                                AccountType accountType,
                                                decimal amount,
                                                bool expectedResult)
        {
            IDeposit deposit = new FreeAccountDepositRule();

            

            Account account = new Account
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("12345", "Free Account", 300, AccountType.Free, -250, false)] //Negative withdrawal over limit (fail)
        [TestCase("12345", "Free Account", 50, AccountType.Free, -80, false)]   //Overdraft (fail)
        [TestCase("12345", "Free Account", 100, AccountType.Basic, -50, false)] //Wrong account type (fail)
        [TestCase("12345", "Free Account", 100, AccountType.Free, 50, false)]   //Positive withdrawal amount (fail)
        [TestCase("12345", "Free Account", 100, AccountType.Free, -50, true)]   //Successful withdrawal (succeed)

        public void FreeAccountWithdrawRuleTest(string accountNumber,
                                        string name,
                                        decimal balance,
                                        AccountType accountType,
                                        decimal amount,
                                        bool expectedResult)
        {
           
            IWithdraw withdraw = new FreeAccountWithdrawRule();


            Account account = new Account
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };

            
            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
            Assert.AreEqual(expectedResult, response.Success);
        }

    }
}
