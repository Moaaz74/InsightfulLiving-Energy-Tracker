using Cassandra;
using Cassandra.Mapping.Attributes;
using Confluent.Kafka;

namespace Back_end.Models
{
    [Table("big_data.room_overall")]
    public class Room_Overall
    {
        public Guid id { get; set; }
        public int roomid { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public double roomconsumption { get; set; }
        public string energytype { get; set; }
    }
}
