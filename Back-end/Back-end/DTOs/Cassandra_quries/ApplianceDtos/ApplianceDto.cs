namespace Back_end.DTOs.Cassandra_quries.ApplianceDtos
{
    public class ApplianceDto
    {
        public int ApplianceId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public double ApplianceConsumption { get; set; }
        public string EnergyType { get; set; }
    }
}
