using System.Data;
using System.Data.SqlClient;
using WebAPI.Models.Requests;

namespace WebAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int Add(OrderRequest model)
        {
            int Id = 0;
            string connectionString = _configuration.GetValue<string>("ConnectionStrings:Default");

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.Orders_Insert";

                cmd.Parameters.AddWithValue("@PaymentIntentId", model.PaymentIntentId);
                cmd.Parameters.AddWithValue("@AccountId", model.AccountId);
                cmd.Parameters.AddWithValue("@Total", model.Total);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(idOut);

                cmd.ExecuteNonQuery();

                object oId = cmd.Parameters["@Id"].Value;
                int.TryParse(oId.ToString(), out Id);

                return Id;

            }
        }
    }
}
