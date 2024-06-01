using System;
using System.Collections.Generic;

namespace Evaluation_venussoftop.Models;

public partial class PatientPhoneNumber
{
    public int Id { get; set; }

    public int? PatientId { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual Patient? Patient { get; set; }
}
