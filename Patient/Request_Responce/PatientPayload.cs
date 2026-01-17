using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Patient.Request_Responce
{
    public class PatientPayload
    {
        public GeneralResponce generalResponce { get; set; }
        public Patients patients { get; set; }

        public PatientPayload(GeneralResponce generalResponce, Patients patients)
        {
            this.generalResponce = generalResponce;
            this.patients = patients;
        }

        public PatientPayload(GeneralResponce generalResponce)
        {
            this.generalResponce = generalResponce;
        }
    }
}