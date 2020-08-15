using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PowerPointRemote.WebApi.ApplicationSettings;
using PowerPointRemote.WebAPI.Data;
using PowerPointRemote.WebApi.Models.EntityFramework;
using PowerPointRemote.WebApi.Models.HttpRequests;

namespace PowerPointRemote.WebApi.Controllers
{
    public class ChannelController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly JwtSettings _jwtSettings;

        public ChannelController(ApplicationDbContext applicationDbContext, JwtSettings jwtSettings)
        {
            _applicationDbContext = applicationDbContext;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("create-channel")]
        public async Task<ActionResult> CreateChannel()
        {
            var channelId = RandomBase16(9);
            var now = DateTime.Now;

            var channel = new Channel
            {
                Id = channelId,
                LastUpdate = now
            };

            await _applicationDbContext.Channels.AddAsync(channel);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new {AccessToken = CreateHostAccessToken(channelId), ChannelId = channelId});
        }

        [HttpPost("join-channel")]
        public async Task<ActionResult> JoinChannel([FromBody] JoinChannelRequest joinChannelRequest)
        {
            var channel = await _applicationDbContext.Channels.FindAsync(joinChannelRequest.ChannelId.ToUpper());

            if (channel == null)
                return NotFound("This remote doesn't exist or has ended");

            var user = new User
            {
                Id = Guid.NewGuid(),
                ChannelId = joinChannelRequest.ChannelId,
                Name = joinChannelRequest.UserName
            };

            await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();

            return Ok(new {AccessToken = CreateUserAccessToken(joinChannelRequest.ChannelId, user.Id.ToString())});
        }

        private static string RandomBase16(byte length)
        {
            var bytes = new byte[length];
            var chars = new char[length];

            new RNGCryptoServiceProvider().GetBytes(bytes);

            for (var i = 0; i < length; i++)
            {
                var num = bytes[i] % 16;

                if (num < 10)
                    chars[i] = (char) ('0' + num);
                else
                    chars[i] = (char) ('A' + num - 10);
            }

            return new string(chars);
        }

        private string CreateHostAccessToken(string channelId)
        {
            return CreateAccessToken(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "Host"),
                new Claim("ChannelId", channelId)
            }));
        }

        private string CreateUserAccessToken(string channelId, string userId)
        {
            return CreateAccessToken(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userId),
                new Claim(ClaimTypes.Role, "user"),
                new Claim("ChannelId", channelId)
            }));
        }

        private string CreateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha384Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}