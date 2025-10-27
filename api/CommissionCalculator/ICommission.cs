namespace AvalphaTechnologies.CommissionCalculator.CommissionCalculator
{
    public interface ICommission
    {
        public decimal CalculateCommission(int localSalesCount, int foreignSalesCount, decimal averageSaleAmount); 
        

    }
}
