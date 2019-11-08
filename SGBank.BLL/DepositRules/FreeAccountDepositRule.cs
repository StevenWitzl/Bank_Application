using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;

namespace SGBank.BLL.DepositRules
{
    public class FreeAccountDepositRule : IDeposit
    {
        public AccountDepositResponse Deposit(Account account, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();
            if (account.Type != AccountType.Free)
            {
                response.Success = false;
                response.Message = "Error: A non free account hit the Free Deposit Rule. Contact IT";
                return response;
            }

            // cant deposit more than $100 in a day
            if (amount > 100)
            {
                response.Success = false;
                response.Message = "free account cannot deposit more than $100 in a day";
                return response;
            }

            // has to deposit more than $0
            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amount must be greater than $0";
                return response;
            }

            response.OldBalance = account.Balance;
            account.Balance += amount;
            response.Account = account;
            response.Amount = amount;
            response.Success = true;

            return response;
        }
    }
}
