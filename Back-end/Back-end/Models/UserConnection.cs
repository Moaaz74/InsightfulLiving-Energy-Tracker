using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end.Models
{
    public class UserConnection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}
