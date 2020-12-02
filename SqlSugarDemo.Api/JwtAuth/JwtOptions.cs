using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.JwtAuth
{
    public class JwtSettings
    {
        /// <summary>
        /// 签发人（一般写接口请求地址）
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 受众（一般写接口请求地址)
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 签名的秘钥 
        /// </summary>
        public string SecurityKey { get; set; }

        //public SymmetricSecurityKey SymmetricSecurityKey => string.IsNullOrEmpty(SecurityKey) ?
        //                                                    throw new ArgumentNullException(nameof(SecurityKey)) :
        //                                                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
        //public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    }
}
