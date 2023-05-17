using System.Data;
using System.Data.SqlClient;
using WebAPI.Models.Requests;

namespace WebAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        public OrderRepository()
        {

        }

        public int Add(OrderRequest model)
        {
            int Id = 0;
            string connectionString =
            "Server=127.0.0.1,1433; Database=test;User=sa;Password=Banggu!8;";

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
