using WebAPI.Models.Responses.Interfaces;

namespace WebAPI.Models.Responses
{
    public class ItemResponse<T> : SuccessResponse, IItemResponse
    {
        public T Item { get; set; }
        object IItemResponse.Item { get { return Item; } }
    }
}
