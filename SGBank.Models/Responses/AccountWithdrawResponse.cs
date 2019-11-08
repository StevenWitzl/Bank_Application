namespace SGBank.Models.Responses
{
    public class AccountWithdrawResponse : Response
    {
        public Account Account { get; set; }
        public decimal OldBalance { get; set; }
        public decimal Amount { get; set; }
    }
}
