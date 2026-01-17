using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFramework.Data;
using Microsoft.EntityFrameworkCore;
using Model;
using Patient.Request_Responce;

namespace Patient.Service
{

    public class PatientService : IPatientService
    {
        private readonly BPDbContext _context;

        public PatientService(BPDbContext context)
        {
            _context = context;
        }

        public PatientService()
        {
            
        }

        public async Task<PatientPayload> Login(string ssn)
        {
            try
            {
                var user = await _context.patientsTables.SingleOrDefaultAsync(x => x.SSN == ssn);
                if (user == null)
                {
                    return new PatientPayload(new GeneralResponce(404, "No User Found"));
                }
                return new PatientPayload(new GeneralResponce(200, "Success"), user);
            }
            catch (Exception ex)
            {
                return new PatientPayload(new GeneralResponce(500, "Error in Login"));
            }
        }

        public async Task<GeneralResponce> Register(Patients patients)
        {
            try
            {
                _context.patientsTables.Add(patients);
                await _context.SaveChangesAsync();
                return new GeneralResponce(200, "Success");
            }
            catch (Exception ex)
            {
                return new GeneralResponce(500, "Error in Register");
            }
        }
    }
}