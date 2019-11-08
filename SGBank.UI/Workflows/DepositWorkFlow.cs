using SGBank.BLL;
using SGBank.Models.Responses;
using System;

namespace SGBank.UI.Workflows
{
    public class DepositWorkFlow
    {
        public void Execute()
        {
            Console.Clear();
            AccountManager accountManager = AccountManagerFactory.Create();

            Console.WriteLine("Enter an Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Enter a deposit amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            AccountDepositResponse response = accountManager.Deposit(accountNumber, amount);

            if (response.Success)
            {
                Console.WriteLine("Deposit completed.");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber:c}");
                Console.WriteLine($"Amount Deposited: {response.Amount:c}");
                Console.WriteLine($"Old balance: {response.OldBalance:c}");
                Console.WriteLine($"New balance: {response.Account.Balance:c}");
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
