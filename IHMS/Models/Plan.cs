﻿using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Plan
{
    public int PlanId { get; set; }

    public int MemberId { get; set; }

    public string Pname { get; set; } = null!;

    public int Weight { get; set; }

    public double BodyPercentage { get; set; }

    public DateTime RegisterDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Diet> Diets { get; set; } = new List<Diet>();

    public virtual Sport? Sport { get; set; }

    public virtual ICollection<Water> Water { get; set; } = new List<Water>();
}
