using AutoMapper;
using CreditRatingService.UseCase;
using CreditRatingService.UseCase.Entities;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcCredit.Services
{
    public class CreditService : CreditRating.CreditRatingBase
    {
        private readonly ILogger<CreditService> _logger;
        private readonly ICreditRatingUseCase _creditRatingUseCase;
        private readonly IMapper _mapper;
        public CreditService(ILogger<CreditService> logger, ICreditRatingUseCase creditRatingUseCase,
            IMapper mapper)
        {
            _logger = logger;
            _creditRatingUseCase = creditRatingUseCase;
            _mapper = mapper;
        }

        /*
			Override because (CreditRating) inherits from (CreditRatingBase) class that
			hasn't implementation of CheckCreditRequest (throws an exception) forcing to 
			implement the method.
		 */
        public override async Task<CreditResponse> CheckCreditRequest(CreditRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"CheckCreditRequest :: executed");

            InfoCredit creditRequest = _mapper.Map<InfoCredit>(request);
            InfoCreditResponse? response = await _creditRatingUseCase.ChequearCupoCredito(creditRequest);
            return _mapper.Map<CreditResponse>(response);
        }

        public override async Task<FeeResponse> GetCreditFee(IAsyncStreamReader<FeeRequest> requestStream, ServerCallContext context)
        {
            _logger.LogInformation($"GetCreditFee :: executed");

            List<FeeRequest>? requests = new List<FeeRequest>();

            await foreach (var request in requestStream.ReadAllAsync())
            {
                requests.Add(request);
            }

            List<InfoFee> messages = _mapper.Map<List<InfoFee>>(requests);
            InfoFeeResponse? response = await _creditRatingUseCase.ObtenerCuotaCreditoStream(messages);

            return _mapper.Map<FeeResponse>(response);
        }


        public override async Task<CreditUserResponse> GetMaxCreditQuota(Empty request, ServerCallContext context)
        {
            _logger.LogInformation($"GetMaxCreditQuota :: executed");

            InfoCreditUserResponse? response = await _creditRatingUseCase.ObtenerUsuarioCupoMayor();
            return _mapper.Map<CreditUserResponse>(response);
        }
    }
}
