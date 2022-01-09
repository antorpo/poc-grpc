namespace CreditRatingService.UseCase.Entities
{
    public class InfoCreditResponse
    {
        public string? Message { get; set; }

        public CreditCode Code { get; set; }

    }
    public enum CreditCode : int
    {
        REJECTED = 0,
        ACCEPTED = 1,
    }
}
