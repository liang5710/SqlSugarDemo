using ExampleDemo.Token.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExampleDemo.Token
{
    public class Token
    {
        public Token() { }

        public static string IssueJWT(TokenModel tokenModel, TimeSpan expiresSliding, TimeSpan expiressAbsoulte) 
        {
            DateTime UTC = DateTime.UtcNow;
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,tokenModel.Sub), //Subject
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //JWT ID,JWT的唯一标识
                new Claim(JwtRegisteredClaimNames.Iat,UTC.ToString(),ClaimValueTypes.Integer64) //Issued At,JWT颁发时间，采用标准unix时间，用于验证过期
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: "", //jwt签发者，非必须
                audience: tokenModel.Uname,//jwt的接收该方，非必须
                claims: claims, //声明集合
                expires: UTC.AddHours(12),//指定token的生命周期，unix时间戳格式，非必须
                signingCredentials: new Microsoft.IdentityModel.Tokens
                    .SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("liang")),
                    SecurityAlgorithms.HmacSha256)); //使用私钥进行签名加密

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);//生成JWT字符串
            //将JWT字符串和tokenModel作为key和value存入缓存
            DemoMemoryCache.AddMemoryCache(encodedJwt, tokenModel, expiresSliding, expiressAbsoulte);

            return encodedJwt;

        }
    }
}
