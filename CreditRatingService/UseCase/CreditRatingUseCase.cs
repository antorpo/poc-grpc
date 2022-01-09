using CreditRatingService.UseCase.Entities;

namespace CreditRatingService.UseCase
{
    public class CreditRatingUseCase : ICreditRatingUseCase
    {
        private readonly ILogger<CreditRatingUseCase> _logger;

        public CreditRatingUseCase(ILogger<CreditRatingUseCase> logger)
        {
            _logger = logger;
        }

        public Task<InfoCreditResponse> ChequearCupoCredito(InfoCredit infoCredit)
        {
            bool aprobado = default;

            if (customerDB.TryGetValue(infoCredit.CustomerDocument, out dynamic? datos))
                aprobado = infoCredit.CreditValue <= datos.Cupo;

            if (!aprobado)
            {
                return Task.FromResult(new InfoCreditResponse()
                {
                    Message = "Credito Denegado",
                    Code = CreditCode.REJECTED
                });
            }

            return Task.FromResult(new InfoCreditResponse()
            {
                Message = "Credito Aceptado",
                Code = CreditCode.ACCEPTED
            });
        }

        public Task<InfoFeeResponse> ObtenerCuotaCreditoStream(List<InfoFee> infoFee)
        {
            double interes = 0.1, total = 0;

            foreach (var fee in infoFee)
            {
                total += fee.FeeValue * interes;
            }

            return Task.FromResult(new InfoFeeResponse()
            {
                TotalFeeValue = total,
            });
        }

        public Task<InfoCreditUserResponse> ObtenerUsuarioCupoMayor()
        {
            var infoCliente = customerDB.OrderByDescending(x => x.Value.Cupo).FirstOrDefault();

            return Task.FromResult(new InfoCreditUserResponse()
            {
                Name = infoCliente.Value.Nombre,
                CustomerDocument = infoCliente.Key,
                CreditQuota = infoCliente.Value.Cupo
            });
        }

        private static readonly Dictionary<string, dynamic> customerDB = new Dictionary<string, dynamic>()
        {
            {"11111111", new { Nombre = "Antonio", Cupo = 985000 }},
            {"22222222", new { Nombre = "Andres", Cupo = 345000 }},
            {"33333333", new { Nombre = "Manuel", Cupo = 500000 }},
            {"44444444", new { Nombre = "Oscar", Cupo = 999000 }},
            {"55555555", new { Nombre = "Ricardo", Cupo = 783000 }}
        };
    }
}
