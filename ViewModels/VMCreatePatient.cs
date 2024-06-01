using System;
using System.Collections.Generic;

namespace Evaluation_venussoftop.Models;

public partial class VMCreatePatient
{
    public string? Firstname { get; set; }

    public string? Surname { get; set; }

    public string? Prefix { get; set; }

    public DateTime? Dob { get; set; }

    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

}
