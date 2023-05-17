using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using WebAPI.Models.Responses;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly ILogger<UserAPIController> _logger;
        private IGoogleAuthRepository _googleAuth;
        public UserAPIController(ILogger<UserAPIController> logger, IGoogleAuthRepository googleAuth)
        {
            _logger = logger;
            _googleAuth = googleAuth;
        }
        /// <summary>
        /// Add user into database
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="password">password</param>
        /// <returns>id value</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     {
        ///        "email": "email.com",
        ///        "password": "any string for now"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created id</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult Create(string email, string password)
        {
            ObjectResult result;
            int iCode = 200;
            string connectionString =
            "Server=127.0.0.1,1433; Database=test;User=sa;Password=Banggu!8;";


            string procName =
                "dbo.Users_Insert";


            string Email = email;
            string Password = password;
            int id = 0;

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.


                try
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();

                    command.CommandText = procName;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    // 4. Create input parameters and assign values


                    // 5. Create output parameter to receive the new ID from the stored procedure
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    command.Parameters.Add(idOut);

                    // 6. Call ExecuteNonQuery to send command
                    command.ExecuteNonQuery();

                    object oId = command.Parameters["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                    string url = Request.Path + "/" + id;
                    result = Created(url, id);

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    iCode = Response.StatusCode;
                    result = StatusCode(iCode, ex);
                }

            }


            return result;
        }
        [HttpGet("login-google")]
        public ActionResult<ItemResponse<string>> GoogleSignIn()
        {
            int iCode = 200;
            BaseResponse response;
            try
            {
                UriBuilder uriBuilder = _googleAuth.AuthRequest();
                response = new ItemResponse<string>() { Item = uriBuilder.ToString() };
                
               

            }
            catch(Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse(ex.Message);
                _logger.LogError(ex.ToString());
            }

            return StatusCode(iCode, response);
        }
    }
}
