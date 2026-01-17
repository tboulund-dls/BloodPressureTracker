using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Patient.Request_Responce;
using Patient.Service;
using FeatureHub;

namespace Patient.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private bool _patientFlag;

        public PatientController(IPatientService patientService, FeatureService featureService)
        {
            _patientService = patientService;
            _patientFlag = featureService.FeatureFlagChecker(Features.PatientService);
        }

        [HttpGet("Login/{ssn}")]
        public async Task<PatientPayload> Login(string ssn)
        {
            if (String.IsNullOrEmpty(ssn))
            {
                return new PatientPayload(new GeneralResponce(404, "No User Found"));
            }

            if (!_patientFlag)
            {
                throw new Exception("Patient Service is disabled");
            }
        
            try
            { 
                return await _patientService.Login(ssn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new PatientPayload(new GeneralResponce(500, "Error in Login"));
            }
        }

        [HttpPost("Register")]
        public async Task<GeneralResponce> Register([FromBody]Patients patients)
        {
            if (!_patientFlag)
            {
                throw new Exception("Patient Service is disabled");
            }

            try
            {
                return await _patientService.Register(patients);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new GeneralResponce(500, "Error in Register");
            }
        }
    }
}