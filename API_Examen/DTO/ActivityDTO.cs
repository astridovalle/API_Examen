﻿namespace API_Examen.DTO
{
    public class ActivityDTO
    {
        public int PropertyId { get; set; }
        public DateTime Schedule { get; set; }
        public string Title { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}