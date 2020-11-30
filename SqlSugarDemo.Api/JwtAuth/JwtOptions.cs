using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDemo.Api.JwtAuth
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey => string.IsNullOrEmpty(SecurityKey) ?
                                                            throw new ArgumentNullException(nameof(SecurityKey)) :
                                                            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    }
}
