using System;
using System.Collections.Generic;

namespace Evaluation_venussoftop.Models;

public partial class PatientEmail
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? Email { get; set; }

    public virtual Patient? Patient { get; set; }
}
