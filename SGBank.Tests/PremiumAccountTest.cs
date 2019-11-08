using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.Tests
{
    [TestFixture]
    class PremiumAccountTest
    {
        



        [TestCase("66666", "Basic Account", 100, AccountType.Free, 250, false)]   //(fail, wrong account type)
        [TestCase("66666", "Basic Account", 100, AccountType.Basic, -100, false)] //(fail, negative number deposited)
        [TestCase("66666", "Basic Account", 100, AccountType.Basic, 250, true)]   //(success)

        public void PremiumAccountDepositRuleTests(string accountNumber,
                                                string name,
                                                decimal balance,
                                                AccountType accountType,
                                                decimal amount,
                                                bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();
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

        
        [TestCase("66666", "Basic Account", 100, AccountType.Free, -100, 100, false)]       //(fail, not a Premium account type)
        [TestCase("66666", "Basic Account", 100, AccountType.Premium, 100, 100, false)]       //(fail, positive number withdrawn)
        [TestCase("66666", "Basic Account", 100, AccountType.Premium, -150, -50, true)]        //((success)
        [TestCase("66666", "Basic Account", 100, AccountType.Premium, -750, -660, true)]       //(success, overdraft fee)
       
        public void PremiumAccountWithdrawRuleTests(string accountNumber,
                                        string name,
                                        decimal balance,
                                        AccountType accountType,
                                        decimal amount,
                                        decimal newBalance,
                                        bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();
            Account account = new Account
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = accountType
            };
       
            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);
       
            Assert.AreEqual(expectedResult, response.Success);
       
            if (response.Success == true)
            {
                Assert.AreEqual(newBalance, response.Account.Balance);
            }
       
        }
    }
}
