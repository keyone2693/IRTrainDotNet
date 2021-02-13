using System;

namespace IRTrainDotNet.Models
{
  public  class GetWagonAvailableSeatCountParams
    {
        public int FromStation { get; set; }
        public int ToStation { get; set; }
        /// <summary>
        /// IT SHOULD BE LOCAL TIME IRAN
        /// </summary>
        public DateTime GoingDate { get; set; }
        /// <summary>
        /// IT SHOULD BE LOCAL TIME IRAN
        /// </summary>
        public DateTime? ReturnDate { get; set; }
        public int Gender { get; set; }
        public bool ExclusiveCompartment { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int InfantsCount { get; set; }
    }
}
