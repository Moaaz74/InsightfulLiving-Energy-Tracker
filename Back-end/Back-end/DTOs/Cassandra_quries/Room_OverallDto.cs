namespace Back_end.DTOs.Cassandra_quries
{
    public class Room_OverallDto
    {
        public int RoomId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public double RoomConsumption { get; set; }
        public string EnergyType { get; set; }
    }
}
