
using System.Collections.Generic;


namespace IRTrainDotNet.Models
{
   public class GetWagonAvailableSeatCountResult
    {
       public IEnumerable<WagonAvailableSeatCount> GoingResults { get; set; }
        public IEnumerable<WagonAvailableSeatCount> ReturnResults { get; set; }

    }
}
