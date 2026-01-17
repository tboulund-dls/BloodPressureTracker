using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace Mesurment.Request_Responce
{
    public class MeasurmentListPayload
    {
        public List<Measurements>? measurements { get; set; }
        public GeneralResponce generalResponce { get; set; }

        public MeasurmentListPayload(GeneralResponce generalResponce, List<Measurements> measurements)
        {
            this.measurements = measurements;
            this.generalResponce = generalResponce;
        }
        public MeasurmentListPayload(GeneralResponce generalResponce)
        {
            this.generalResponce = generalResponce;
        }
    }
}