namespace ApplicationService.InvestmentProjection
{
    public class YearlyBalance
    {
        public YearlyBalance(int year, decimal balance)
        {
            this.Year = year;
            this.Balance = balance;
        }

        public int Year { get; }

        public decimal Balance { get; }
    }
}
