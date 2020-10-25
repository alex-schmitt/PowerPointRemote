using System.ComponentModel.DataAnnotations;

namespace PowerPointRemote.WebApi.Models.HttpRequests
{
    public class JoinChannelRequest
    {
        [Required(ErrorMessage = "A remote ID is required to connect")]
        public string ChannelId { get; set; }

        [Required(ErrorMessage = "A username is required to connect")]
        [MinLength(2, ErrorMessage = "Your username must be at least 22 characters")]
        [MaxLength(20, ErrorMessage = "Your username must be less than or equal to 20 character")]
        public string UserName { get; set; }
    }
}