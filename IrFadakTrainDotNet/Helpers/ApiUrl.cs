using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Helpers
{
    public static class ApiUrl
    {
        public const string BaseUrl = "https://api.fadaktrains.com/rpTicketing";

        public const string Login = BaseUrl + "/Login";
        public const string Stations = BaseUrl + "/Station";
        public const string Station = BaseUrl + "/Station/";
        public const string GetLastVersion = BaseUrl + "/GetLastVersion";
        public const string GetWagonAvailableSeatCount = BaseUrl + "/GetWagonAvailableSeatCount";

    }
}
