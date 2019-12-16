using System;
using System.Collections.Generic;
using System.Text;

namespace IRTrainDotNet.Helpers
{
    public static class Constants
    {
        public const string PreToken = "bmhh ";
        public static readonly List<int> SystemErrorCodes = new List<int> {
            100, 103, 110, 111, 118, 123, 200, 201, 202,
             203, 204, 205, 206, 207, 208, 209,
            210, 211,212, 213, 221, 226};
    }
}
