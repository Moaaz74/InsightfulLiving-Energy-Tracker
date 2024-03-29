﻿using System.Text.Json.Serialization;

namespace Back_end.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int HomeId { get; set; }

        public Home home { get; set; } = default!;

        public int NumberOfDevices { get; set; }

        public string Type { get; set; } = String.Empty;

        [JsonIgnore]
        public virtual List<Device> devices { get; set; } = new List<Device>();

        public bool IsDeleted { get; set; }

    }
}
