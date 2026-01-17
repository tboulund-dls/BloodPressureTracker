using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Patient.Request_Responce;

namespace Patient.Service
{
    public interface IPatientService
    {
        Task<PatientPayload> Login(string ssn);

        Task<GeneralResponce> Register(Patients patients);
    }
}