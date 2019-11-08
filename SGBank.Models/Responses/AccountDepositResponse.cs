namespace SGBank.Models.Responses
{
    public class AccountDepositResponse : Response
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalance { get; set; }
    }
}
