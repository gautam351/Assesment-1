namespace AvalphaTechnologies.CommissionCalculator.CommissionCalculator
{
    public class CommissionFactory
    {
        public static ICommission CreateCommissionCalculator(string type, IConfiguration configuration)
        {
            return type switch
            {
                "Avalpha" => new AvlphaCommission(configuration),
                "Competitor" => new CompetitorCommission(configuration),
                _ => throw new ArgumentException("Invalid commission type"),
            };
        }
    }
}
