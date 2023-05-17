using System.Net;

namespace WebAPI.Repositories
{
    public class GoogleAuthRepository : IGoogleAuthRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GoogleAuthRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public UriBuilder AuthRequest()
        {
            HttpClient googleHttpClient = _httpClientFactory.CreateClient("Google");
            string token = Guid.NewGuid().ToString();
            //string query = "?response_type=code" +
            //                "&client_id=254855841315-rmg1jjthsbhgqvl7kqcuopgb5d0n4kpf.apps.googleusercontent.com" +
            //                "&scope=openid%20email&" +
            //                "&redirect_uri=https%3A//localhost:50001/api/users/signin-google" +
            //                $"&state={token}" +
            //                "&login_hint=jpark2111@gmail.com";
            var query = new Dictionary<string, string>()
            {
                {"response_type","code"},
                {"client_id" , "254855841315-rmg1jjthsbhgqvl7kqcuopgb5d0n4kpf.apps.googleusercontent.com"},
                {"scope", "openid%20email&"},
                {"redirect_uri", "https%3A//localhost:50001/api/users/signin-google"},
                {"state", token}
            };

            // Project the dictionary into a collection of name/value pairs and join them into a string
            var queryString = string.Join('&', query.Select(q => $"{q.Key}={q.Value}"));
            var builder = new UriBuilder("https", "accounts.google.com", 443, "o/oauth2/v2/auth", $"?{queryString}");
            return builder;

           
        }
    }
}
