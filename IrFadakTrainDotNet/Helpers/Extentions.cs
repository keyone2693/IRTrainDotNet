using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Helpers
{
    public static class Extentions
    {
        public static string ToAuthorization(this string token)
        {
            return Constants.PreToken + token;
        }
    }
}
