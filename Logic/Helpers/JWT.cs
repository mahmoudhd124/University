using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Helpers;

public class Jwt
{
    public string Key { get; set; }
    public byte[] KeyBytes => Encoding.UTF8.GetBytes(Key);
    public SecurityKey SecurityKey => new SymmetricSecurityKey(KeyBytes);

}