namespace WebAPI.Models.Responses
{
    public class ErrorResponse : BaseResponse
    {
        public List<string> Errors { get; set; }    
        public ErrorResponse(string errMsg)
        {
            IsSuccess = false;
            Errors = new List<string>();    
            Errors.Add(errMsg);
        }

        public ErrorResponse(IEnumerable<string> errMsg)
        {
            IsSuccess = false;
            Errors = new List<string>();
            Errors.AddRange(errMsg);
        }
    }
}
