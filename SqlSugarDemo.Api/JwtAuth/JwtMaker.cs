using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.JwtAuth
{
    public class JwtMaker
    {
        //public string Make()
        //{
        //    const string secret = "acQlZ1BOW6GWBpcLvQDkXPBLDFGLVK0sNSFixJLWSFDpiQr6a1";
        //    IDateTimeProvider provider = new UtcDateTimeProvider();
        //    var now = provider.GetNow();
        //    var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        //    var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
        //    var payload = new Dictionary<string, object>
        //    {
        //        { "name", "cardless" },
        //        { "iss", "Capinfo" },
        //        { "aud", "hosp" },
        //        {"exp",secondsSinceEpoch+100 },
        //        {"jti","testjwt" }
        //    };
        //    Console.WriteLine(secondsSinceEpoch);
        //    IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        //    IJsonSerializer serializer = new JsonNetSerializer();
        //    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        //    IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        //    var token = encoder.Encode(payload, secret);
        //    return token;
        //}
    }
}
