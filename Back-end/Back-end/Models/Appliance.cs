using Cassandra.Mapping.Attributes;

namespace Back_end.Models
{
    [Table("big_data.appliance")]
    public class Appliance
    {
        public Guid id { get; set; }
        public int applianceid { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public double applianceconsumption { get; set; }
        public string energytype { get; set; }
    }
}
