namespace WebAPI.Models.Requests
{
    public class OrderRequest
    {
        #nullable disable
        public string PaymentIntentId { get; set; }
        public string AccountId { get; set; }
        public long? Total { get; set; }    
    }
}
