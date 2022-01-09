using CreditRatingService.UseCase.Entities;

namespace CreditRatingService.UseCase
{
    public interface ICreditRatingUseCase
    {
        Task<InfoCreditResponse> ChequearCupoCredito(InfoCredit infoCredit);

        Task<InfoFeeResponse> ObtenerCuotaCreditoStream(List<InfoFee> infoFee);

        Task<InfoCreditUserResponse> ObtenerUsuarioCupoMayor();
    }
}
