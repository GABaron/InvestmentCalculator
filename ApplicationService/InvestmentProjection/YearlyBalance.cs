namespace ApplicationService.InvestmentProjection
{
    public class YearlyBalance
    {
        public YearlyBalance(int year, decimal balance, decimal totalAmountInvested)
        {
            this.Year = year;
            this.Balance = balance;        
            this.TotalAmountInvested = totalAmountInvested;
        }

        public int Year { get; }

        public decimal Balance { get; }     

        public decimal TotalAmountInvested { get; }
    }
}
