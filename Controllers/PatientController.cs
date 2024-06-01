using Evaluation_venussoftop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluation_venussoftop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly VenussoftopEvaluationContext _dbContext;

        public PatientController(VenussoftopEvaluationContext venussoftopEvaluationContext)
        {
            _dbContext = venussoftopEvaluationContext;
        }

        // GET: api/patient
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VMPatient>>> Get([FromQuery] int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var patient = await _dbContext.Patients
                        .Include(e => e.PatientEmails)
                        .Include(p => p.PatientPhoneNumbers)
                        .Where(p => p.Id == id.Value)
                        .Select(p => new VMPatient
                        {
                            Id = p.Id,
                            Firstname = p.Firstname,
                            Surname = p.Surname,
                            Prefix = p.Prefix,
                            Dob = p.Dob,
                            Address = p.Address,
                            Email = p.PatientEmails.Select(pe => pe.Email).FirstOrDefault(),
                            PhoneNumber = p.PatientPhoneNumbers.Select(pp => pp.PhoneNumber).FirstOrDefault()
                        })
                        .FirstOrDefaultAsync();

                    if (patient == null)
                    {
                        return NotFound();
                    }

                    return Ok(patient);
                }
                else
                {
                    var patients = await _dbContext.Patients
                        .Include(e => e.PatientEmails)
                        .Include(p => p.PatientPhoneNumbers)
                        .Select(p => new VMPatient
                        {
                            Id = p.Id,
                            Firstname = p.Firstname,
                            Surname = p.Surname,
                            Prefix = p.Prefix,
                            Dob = p.Dob,
                            Address = p.Address,
                            Email = p.PatientEmails.Select(pe => pe.Email).FirstOrDefault(),
                            PhoneNumber = p.PatientPhoneNumbers.Select(pp => pp.PhoneNumber).FirstOrDefault()
                        })
                        .ToListAsync();

                    return Ok(patients);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST: api/patient
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] VMCreatePatient patient)
        {
            try
            {
                var newPatient = new Patient
                {
                    Firstname = patient.Firstname,
                    Surname = patient.Surname,
                    Prefix = patient.Prefix,
                    Dob = patient.Dob,
                    Address = patient.Address,
                    PatientEmails = new List<PatientEmail>
                    {
                        new PatientEmail { Email = patient.Email }
                    },
                    PatientPhoneNumbers = new List<PatientPhoneNumber>
                    {
                        new PatientPhoneNumber { PhoneNumber = patient.PhoneNumber }
                    }
                };

                _dbContext.Patients.Add(newPatient);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = newPatient.Id }, new VMPatient
                {
                    Id = newPatient.Id,
                    Firstname = newPatient.Firstname,
                    Surname = newPatient.Surname,
                    Prefix = newPatient.Prefix,
                    Dob = newPatient.Dob,
                    Address = newPatient.Address,
                    Email = newPatient.PatientEmails.Select(pe => pe.Email).FirstOrDefault(),
                    PhoneNumber = newPatient.PatientPhoneNumbers.Select(pp => pp.PhoneNumber).FirstOrDefault()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT: api/patient
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] VMPatient patient)
        {
            try
            {
                var existingPatient = await _dbContext.Patients
                    .Include(e => e.PatientEmails)
                    .Include(p => p.PatientPhoneNumbers)
                    .FirstOrDefaultAsync(p => p.Id == patient.Id);

                if (existingPatient == null)
                {
                    return NotFound();
                }

                // Update the basic properties
                existingPatient.Firstname = patient.Firstname;
                existingPatient.Surname = patient.Surname;
                existingPatient.Prefix = patient.Prefix;
                existingPatient.Dob = patient.Dob;
                existingPatient.Address = patient.Address;
                existingPatient.Attachment = patient.Attachment;

                // Update the email
                if (existingPatient.PatientEmails.Any())
                {
                    var email = existingPatient.PatientEmails.First();
                    if (email.Email != patient.Email)
                    {
                        email.Email = patient.Email;
                    }
                }
                else
                {
                    existingPatient.PatientEmails.Add(new PatientEmail { Email = patient.Email });
                }

                // Update the phone number
                if (existingPatient.PatientPhoneNumbers.Any())
                {
                    var phone = existingPatient.PatientPhoneNumbers.First();
                    if (phone.PhoneNumber != patient.PhoneNumber)
                    {
                        phone.PhoneNumber = patient.PhoneNumber;
                    }
                }
                else
                {
                    existingPatient.PatientPhoneNumbers.Add(new PatientPhoneNumber { PhoneNumber = patient.PhoneNumber });
                }

                _dbContext.Patients.Update(existingPatient);
                await _dbContext.SaveChangesAsync();

                return Ok(new VMPatient
                {
                    Id = existingPatient.Id,
                    Firstname = existingPatient.Firstname,
                    Surname = existingPatient.Surname,
                    Prefix = existingPatient.Prefix,
                    Dob = existingPatient.Dob,
                    Address = existingPatient.Address,
                    Email = existingPatient.PatientEmails.Select(pe => pe.Email).FirstOrDefault(),
                    PhoneNumber = existingPatient.PatientPhoneNumbers.Select(pp => pp.PhoneNumber).FirstOrDefault()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // DELETE: api/patient/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var patient = await _dbContext.Patients
                    .Include(e => e.PatientEmails)
                    .Include(p => p.PatientPhoneNumbers)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (patient == null)
                {
                    return NotFound();
                }

                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();

                return Ok(new { message = "Record Deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
