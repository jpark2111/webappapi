using WebAPI.Models.Requests;

namespace WebAPI.Repositories
{
    public interface IOrderRepository
    {
        int Add(OrderRequest model);
    }
}