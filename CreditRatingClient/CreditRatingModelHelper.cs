using GrpcCredit;

namespace CreditRatingClient
{
    public class CreditRatingModelHelper
    {
        public static CreditRequest creditRequest => new CreditRequest()
        {
            CustomerDocument = "11111111",
            CreditValue = 22500.55
        };

        public static List<FeeRequest> feeRequests => new List<FeeRequest>()
        {
            new FeeRequest() { FeeValue = 105200.55 },
            new FeeRequest() { FeeValue = 27000 },
            new FeeRequest() { FeeValue = 347200.20 },
            new FeeRequest() { FeeValue = 612000 },
            new FeeRequest() { FeeValue = 98200.34 }
        };

    }
}