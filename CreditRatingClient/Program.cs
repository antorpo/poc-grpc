using CreditRatingService;
using Grpc.Net.Client;

// In this example we are using INSECURE gRPC because the address starts with (http://) instead of (https://)
var channel = GrpcChannel.ForAddress("http://localhost:5008");
var client = new CreditRatingCheck.CreditRatingCheckClient(channel);
var creditRequest = new CreditRequest { CustomerId = "id0201", Credit = 7000 };
var reply = await client.CheckCreditRequestAsync(creditRequest);

Console.WriteLine($"Credit for customer {creditRequest.CustomerId} {(reply.IsAccepted ? "approved" : "rejected")}!");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();