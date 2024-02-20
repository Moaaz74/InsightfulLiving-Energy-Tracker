using Cassandra;
using Cassandra.Mapping.Attributes;
using Confluent.Kafka;

namespace Back_end.Models
{
    [Table("big_data.home_overall")]
    public class Home_Overall
    {
        public Guid id { get; set; }
        public int homeid { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public double homeconsumption { get; set; }
        public string energytype { get; set; }
    }
}
