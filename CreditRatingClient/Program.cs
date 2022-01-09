using CreditRatingClient;
using Grpc.Net.Client;
using GrpcCredit;

var chanel = GrpcChannel.ForAddress("http://localhost:5008");
var client = new CreditRating.CreditRatingClient(chanel);

#region CheckCreditRequest
CreditRequest? requestCheck = CreditRatingModelHelper.creditRequest;
CreditResponse? responseCheck = await client.CheckCreditRequestAsync(requestCheck);
#endregion CheckCreditRequest

#region GetCreditFee
List<FeeRequest>? dataFee = CreditRatingModelHelper.feeRequests;
using var requestFee = client.GetCreditFee();

foreach (var x in dataFee)
{
    await requestFee.RequestStream.WriteAsync(x);
}

await requestFee.RequestStream.CompleteAsync();

FeeResponse? responseFee = await requestFee;
#endregion GetCreditFee

#region GetMaxCreditQuota
CreditUserResponse? responseMax = await client.GetMaxCreditQuotaAsync(new Google.Protobuf.WellKnownTypes.Empty());
#endregion GetMaxCreditQuota

Console.WriteLine("Press any key to exit...");
Console.ReadKey();