using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sample.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Operations.TokenOperation
{
    public class TokenHanddler
    {
        private readonly IConfiguration _config;
        public TokenHanddler(IConfiguration config)
        {
            _config = config;
        }
        public Token CreateAccessToken(User user)
        {
            var token = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            token.Expiration = DateTime.Now.AddMinutes(15);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _config["Token:Issuer"],
                audience: _config["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
           );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            //Token üretiliyor
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreatRefreshToken();
            return token;

        }
        public string CreatRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
