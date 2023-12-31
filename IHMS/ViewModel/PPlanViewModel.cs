﻿using IHMS.Models;

namespace IHMS.ViewModel
{
    public class PPlanViewModel
    {
        public int PlanId { get; set; }

        public string? MemberName { get; set; }

        public string Pname { get; set; } = null!;

        public int Weight { get; set; }

        public double BodyPercentage { get; set; }

        public DateTime? RegisterDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<DateTime>? DietDate { get; set; }

        public List<int>? DietId { get; set; }
        public List<DateTime>? SportDate { get; set; }
        public List<int>? SportId { get; set; }

    }
}
