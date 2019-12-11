using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
  public  class GetWagonAvailableSeatCountParams
    {
        public int FromStation { get; set; }
        public int ToStation { get; set; }
        public DateTime GoingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Gender;
        public bool ExclusiveCompartment { get; set; }
        public int AdultsCount { get; set; }
        public int ChildrenCount { get; set; }
        public int InfantsCount { get; set; }
    }
}
