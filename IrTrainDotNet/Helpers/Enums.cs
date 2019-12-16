using System;
using System.Collections.Generic;
using System.Text;

namespace IrTrainDotNet.Helpers
{
    public enum TarrifCodes
    {
        Full = 1,
        Child = 2,
        Shahed = 3,
        Janbaz = 4,
        CompartmentFiller = 5,
        Infant = 6
    }
    public enum Gender
    {
        Men = 1,
        Wemen = 2,
        Family = 3
    }
    public enum OffTime
    {
        First = 00000015, // 00:00 ---> 00:15
        Second = 06300645, // 06:30 ---> 06:45
        Third = 13301345, // 13:30 ---> 13:45
        Forth = 19301945  // 19:30 ---> 19:45
    }
    public enum Company
    {
        Raja = 1,
        Fadak = 2, 
        Safir = 3
    }
}
