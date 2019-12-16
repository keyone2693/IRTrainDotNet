using System;
using System.Collections.Generic;
using System.Text;

namespace IrTrainDotNet.Models
{
   public class GetWagonAvailableSeatCountResult
    {
       public List<WagonAvailableSeatCount> goingResults { get; set; }
        public List<WagonAvailableSeatCount> returnResults { get; set; }

    }
}
