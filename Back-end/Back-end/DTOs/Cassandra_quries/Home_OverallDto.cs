

namespace Back_end.DTOs.Cassandra_quries
{
    
    public class Home_OverallDto
    {
        public int HomeId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public double HomeConsumption { get; set; }
        public string EnergyType { get; set; }
    }
}
