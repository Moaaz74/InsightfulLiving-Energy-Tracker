namespace Back_end.DTOs.Cassandra_quries.Temp_HumidityDtos
{
    public class Temp_HumidityDto
    {
        public int SensorId { get; set; }
        public int HomeId { get; set; }
        public string DateTime { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
    }
}
