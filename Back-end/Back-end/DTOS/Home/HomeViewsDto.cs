using System.ComponentModel.DataAnnotations;

namespace Back_end.DTOS.Home
{
    public class HomeViewsDto
    {
       
        public string UserId { get; set; }

        public int Id { get; set; }
        public  int NumberOfRooms { get; set; }
    }
}
