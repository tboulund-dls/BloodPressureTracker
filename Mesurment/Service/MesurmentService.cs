using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFramework.Data;
using Mesurment.Request_Responce;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Mesurment.Service
{
    public class MesurmentService : IMesurmentService
    {
        private readonly BPDbContext _context;

        public MesurmentService(BPDbContext context)
        {
            _context = context;
        }

        public MesurmentService()
        {
            
        }

        public async Task<MeasurmentListPayload> GetAllUserMeasurements(string ssn)
        {
            try
            {
                var Measurements = await _context.measurementsTables.Where(x => x.PatientSSN == ssn).ToListAsync();

                if (Measurements == null)
                {
                    return new MeasurmentListPayload(new GeneralResponce(404, "No Measurements Found"));
                }

                return new MeasurmentListPayload(new GeneralResponce(200, "Success"), Measurements);
            }
            catch (Exception ex)
            {
                return new MeasurmentListPayload(new GeneralResponce(500, "Error in GetAllUserMeasurements"));
            }
            
        }

        public async Task<GeneralResponce> CreateMeasurements(Measurements measurements)
        {
            try
            {
                _context.measurementsTables.Add(measurements);
                await _context.SaveChangesAsync();

                return new GeneralResponce(200, "Success");
            }
            catch (Exception ex)
            {
                return new GeneralResponce(500, "Error in CreateMeasurements");
            }
        }

        public async Task<GeneralResponce> DeleteMeasurements(int id)
        {
            try
            {
                var measurements = await _context.measurementsTables.FindAsync(id);
                if (measurements == null)
                {
                    return new GeneralResponce(404, "No Measurements Found");
                }
                _context.measurementsTables.Remove(measurements);
                await _context.SaveChangesAsync();

                return new GeneralResponce(200, "Success");
            }
            catch (Exception ex)
            {
                return new GeneralResponce(500, "Error in DeleteMeasurements");
            }
        }

        public async Task<GeneralResponce> updateMeasurements(Measurements measurements)
        {
            try
            {
                _context.measurementsTables.Update(measurements);
                await _context.SaveChangesAsync();

                return new GeneralResponce(200, "Success");
            }
            catch (Exception ex)
            {
                return new GeneralResponce(500, "Error in updateMeasurements");
            }
        }

        public async Task<GeneralResponce> CreateRandomMeasurements(Measurements measurements)
        {
            try
            {
                _context.measurementsTables.Add(measurements);
                await _context.SaveChangesAsync();

                return new GeneralResponce(200, "Success");
            }
            catch (Exception ex)
            {
                return new GeneralResponce(500, "Error in CreateMeasurements");
            }
        }
    }
}