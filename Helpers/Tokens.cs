using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KinderKulturServer.Auth;
using KinderKulturServer.Models;
using Newtonsoft.Json;

namespace KinderKulturServer.Helpers
{
    /// <summary>
    /// JWT Token Helpers
    /// </summary>
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
