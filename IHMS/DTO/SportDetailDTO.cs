﻿namespace IHMS.DTO
{
    public class SportDetailDTO
    {
        
        public int SportDetailId { get; set; }

        public int SportId { get; set; }

        public string Sname { get; set; } = null!;

        public int? Min { get; set; }
        public int? Hour { get; set; }

        public int? Frequency { get; set; }

        public string Type { get; set; } = null!;

        public string? Time { get; set; }

        public DateTime Registerdate { get; set; }

        public int? Calories { get; set; }

        public bool? Isdone { get; set; }

        public int? Sets { get; set; }
    }
}