using System;
using System.Collections.Generic;

namespace Evaluation_venussoftop.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Surname { get; set; }

    public string? Prefix { get; set; }

    public DateTime? Dob { get; set; }

    public string? Address { get; set; }

    public byte[]? Attachment { get; set; }

    public virtual ICollection<PatientEmail> PatientEmails { get; set; } = new List<PatientEmail>();

    public virtual ICollection<PatientPhoneNumber> PatientPhoneNumbers { get; set; } = new List<PatientPhoneNumber>();
}
