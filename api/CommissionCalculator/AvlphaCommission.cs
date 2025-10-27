namespace AvalphaTechnologies.CommissionCalculator.CommissionCalculator
{
    public class AvlphaCommission : ICommission
    {
        private  decimal LocalSalesCommissionRate {get;set;}  
        private  decimal ForeignSalesCommissionRate {get;set;}  

        public AvlphaCommission(IConfiguration configuration)
        {
            LocalSalesCommissionRate = configuration.GetValue<decimal>("Commission:Avalpha:Local"); 
            ForeignSalesCommissionRate = configuration.GetValue<decimal>("Commission:Avalpha:Foreign"); 
        }

        public decimal CalculateCommission(int localSalesCount, int foreignSalesCount, decimal averageSaleAmount)
        {
            // calculate commission based on local and foreign sales 

            try
            {
                decimal localSalesCommission = localSalesCount * averageSaleAmount * LocalSalesCommissionRate;
                decimal foreignSalesCommission = foreignSalesCount * averageSaleAmount * ForeignSalesCommissionRate;
                return localSalesCommission + foreignSalesCommission;
            }
            catch (Exception)
            {

                throw new Exception("Error calculating Avalpha Technologies commission.");  
            }
        }
    }
}
