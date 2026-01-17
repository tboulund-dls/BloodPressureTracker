using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mesurment.Request_Responce;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Mesurment.Service
{
    public interface IMesurmentService
    {
        Task<MeasurmentListPayload> GetAllUserMeasurements(string ssn);
        Task<GeneralResponce> CreateMeasurements(Measurements measurements);
        Task<GeneralResponce> DeleteMeasurements(int id);
        Task<GeneralResponce> updateMeasurements(Measurements measurement);


        Task<GeneralResponce> CreateRandomMeasurements(Measurements measurements);
    }
}