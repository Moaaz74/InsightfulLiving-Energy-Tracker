using Cassandra.Mapping.Attributes;

namespace Back_end.Models
{
    [Table("big_data.temp_humidity")]
    public class Temp_Humidity
    {
        
        public Guid id { get; set; }      
        public int roomid { get; set; }
        public string datetime { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
    }
}
