﻿using System;
using System.Collections.Generic;

namespace IHMS.Models;

public partial class Sport
{
    public int SportId { get; set; }

    public int PlanId { get; set; }

    public DateTime Registerdate { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<SportDetail> SportDetails { get; set; } = new List<SportDetail>();

    public virtual Plan SportNavigation { get; set; } = null!;
}
