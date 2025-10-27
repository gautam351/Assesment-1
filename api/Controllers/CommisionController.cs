using AvalphaTechnologies.CommissionCalculator.CommissionCalculator;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public CommisionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [HttpPost]
        public IActionResult Calculate(CommissionCalculationRequest calculationRequest)
        {


            try
            {
                // validate inputs 
                if(!calculationRequest.ValidateRequest())
                {
                    return BadRequest("Invalid input values. Sales counts and average sale amount must be non-negative.");
                }

                // create commission calculators using factory
                ICommission avalphaCommission = CommissionFactory.CreateCommissionCalculator("Avalpha", _configuration);
                ICommission competitorCommission = CommissionFactory.CreateCommissionCalculator("Competitor", _configuration);


                decimal avalpahaCommissionAmount = avalphaCommission.CalculateCommission(calculationRequest.LocalSalesCount, calculationRequest.ForeignSalesCount, calculationRequest.AverageSaleAmount);
                decimal competitorCommissionAmount = competitorCommission.CalculateCommission(calculationRequest.LocalSalesCount, calculationRequest.ForeignSalesCount, calculationRequest.AverageSaleAmount);

                var response = new CommissionCalculationResponse
                {
                    AvalphaTechnologiesCommissionAmount = avalpahaCommissionAmount,
                    CompetitorCommissionAmount = competitorCommissionAmount
                };
                return Ok(response);

            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while calculating commissions.");

            }
        }
    }

    // adding validations
    public class CommissionCalculationRequest
    {
        [Required]
        public int LocalSalesCount { get; set; }
        [Required]
        public int ForeignSalesCount { get; set; }
        [Required]
        public decimal AverageSaleAmount { get; set; }


        public  bool ValidateRequest()
        {
            if (LocalSalesCount < 0 || ForeignSalesCount < 0 || AverageSaleAmount < 0)
            {
                return false;
            }
            return true;
        }   
    }

    public class CommissionCalculationResponse
    {
        public decimal AvalphaTechnologiesCommissionAmount { get; set; }

        public decimal CompetitorCommissionAmount { get; set; }
    }
}
