using Grpc.Core;
namespace CreditRatingService.Services;

public class CreditRatingCheckService : CreditRatingCheck.CreditRatingCheckBase
{
    private readonly ILogger<CreditRatingCheckService> _logger;
    public CreditRatingCheckService(ILogger<CreditRatingCheckService> logger)
    {
        _logger = logger;
    }

    /*
        Override because (CreditRatingCheck) inherits from (CreditRatingCheckBase) class that
        hasn't implementation of CheckCreditRequest (throws an exception) forcing to 
        implement the method.
     */
    public override Task<CreditReply> CheckCreditRequest(CreditRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CreditReply
        {
            IsAccepted = IsEligibleForCredit(request.CustomerId, request.Credit)
        });
    }

    private bool IsEligibleForCredit(string customerId, Int32 credit)
    {
        bool isEligible = false;

        if (customerTrustedCredit.TryGetValue(customerId, out Int32 maxCredit))
        {
            isEligible = credit <= maxCredit;
        }

        return isEligible;
    }

    private static readonly Dictionary<string, Int32> customerTrustedCredit = new Dictionary<string, Int32>()
        {
            {"id0201", 10000},
            {"id0417", 5000},
            {"id0306", 15000}
        };
}

