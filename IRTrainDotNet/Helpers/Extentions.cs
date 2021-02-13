using IRTrainDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRTrainDotNet.Helpers
{
    public static class Extentions
    {
        public struct Age
        {
            public readonly int Years;
            public readonly int Months;
            public readonly int Days;
            public Age(int y, int m, int d) : this()
            {
                Years = y;
                Months = m;
                Days = d;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string TooAgeType(this DateTime dateTime)
        {
            var dt = dateTime.CalculateAge();
            if (dt.Years <= 1 || (dt.Years == 2 && dt.Months == 0 && dt.Days == 0))
            {
                return "INF";
            }
            else if ((dt.Years >= 2 && dt.Years < 12) || (dt.Years == 12 && dt.Months == 0 && dt.Days == 0))
            {
                return "CHD";
            }
            else if (dt.Years > 12 || (dt.Years == 12 && dt.Months > 0 && dt.Days > 0))
            {
                return "ADL";
            }
            else
            {
                return "ADL";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        internal static Age CalculateAge(this DateTime startDate)
        {
            var endDate = DateTime.Now;
            if (startDate.Date > endDate.Date)
            {
                return new Age(0, 0, 0);
            }

            int years = endDate.Year - startDate.Year;
            int months = 0;
            int days = 0;

            // Check if the last year, was a full year.
            if (endDate < startDate.AddYears(years) && years != 0)
            {
                years--;
            }

            // Calculate the number of months.
            startDate = startDate.AddYears(years);

            if (startDate.Year == endDate.Year)
            {
                months = endDate.Month - startDate.Month;
            }
            else
            {
                months = (12 - startDate.Month) + endDate.Month;
            }

            // Check if last month was a complete month.
            if (endDate < startDate.AddMonths(months) && months != 0)
            {
                months--;
            }

            // Calculate the number of days.
            startDate = startDate.AddMonths(months);

            days = (endDate - startDate).Days;

            return new Age(years, months, days);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string ToAuthorization(this string token)
        {
           
            return Constants.PreToken + token;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Uri ToUri(this string url, int id = -1)
        {
            if (id != -1)
                return new Uri(url + id, UriKind.Absolute);
            else
                return new Uri(url, UriKind.Absolute);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="seat"></param>
        /// <returns></returns>
        public static bool IsLockSeatBulk(this int count , WagonAvailableSeatCount seat)
        {
            if (seat.IsCompartment)
            {
                if(seat.CompartmentCapicity > count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static decimal GetSeatPrice(this DateTime date, Dictionary<TarrifCodes, int> prices)
        {
            var age = date.TooAgeType();
            switch (age)
            {
                case "ADL":
                    return prices.Single(p => p.Key == TarrifCodes.Full).Value;
                case "CHD":
                    return prices.Single(p => p.Key == TarrifCodes.Child).Value;
                case "INF":
                    return prices.Single(p => p.Key == TarrifCodes.Infant).Value;
                default:
                    return prices.Single(p => p.Key == TarrifCodes.Full).Value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>

        public static TarrifCodes ToTarif(this DateTime date)
        {
            var age = date.TooAgeType();
            switch (age)
            {
                case "ADL":
                    return TarrifCodes.Full;
                case "CHD":
                    return TarrifCodes.Child;
                case "INF":
                    return TarrifCodes.Infant;
                default:
                    return TarrifCodes.Full;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TarrifCodes ToTarrifEnum(this int input)
        {
            switch (input)
            {
                case 1:
                    return TarrifCodes.Full;
                case 2:
                    return TarrifCodes.Child;
                case 3:
                    return TarrifCodes.Shahed;
                case 4:
                    return TarrifCodes.Janbaz;
                case 5:
                    return TarrifCodes.CompartmentFiller;
                case 6:
                    return TarrifCodes.Infant;
                default:
                    return TarrifCodes.Full;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="tarif"></param>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static decimal AddSeatPrice(this DateTime date, TarrifCodes tarif, Dictionary<TarrifCodes, int> prices)
        {
            var age = date.TooAgeType();
            if (tarif == TarrifCodes.Full && age == "ADL")
            {
                return prices.Single(p => p.Key == TarrifCodes.Full).Value;
            }
            else if (tarif == TarrifCodes.Child && age == "CHD")
            {
                return prices.Single(p => p.Key == TarrifCodes.Child).Value;
            }
            else if (tarif == TarrifCodes.Infant && age == "INF")
            {
                return prices.Single(p => p.Key == TarrifCodes.Infant).Value;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsSystemError(this int code)
        {
            return Constants.SystemErrorCodes.Contains(code);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionId"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static string GetSystemErrorMessage(this int exceptionId, string exceptionMessage)
        {
            if (exceptionId.IsSystemError())
            {
                switch (exceptionId)
                {
                    case 100:
                        return "خطای نامشخص";
                    case 103:
                        return "خطا در ارتباط با سیستم فروش";
                    case 110:
                        return "قطار انتخابی امکان انتخاب غذا ندارد.";
                    case 111:
                        return "تعداد غذای مورد نظر از تعداد بلیت درخواستی بیشتر است.";
                    case 118:
                        return "قطار %1 انتخاب شده کوپه ای نبوده و امکان دربست کردن کوپه وجود ندارد. ";
                    case 123:
                        return "اطلاعات تراکنش مورد نظر یافت نشد. ";
                    case 200:
                        return "خطا در ارتباط با سرور بانک اطلاعاتی ";
                    case 201:
                        return "خطا در عملیات بانک اطلاعاتی ";
                    case 202:
                        return "نام کاربری یا کلمه عبور اشتباه است ";
                    case 203:
                        return "حساب کاربری شما غیر فعال است ";
                    case 204:
                        return "حساب کاربری شما مسدود است ";
                    case 205:
                        return "حساب کاربری شما تائید نگردیده است ";
                    case 206:
                        return "ممکن است به تعداد درخواستی شما در قطار، صندلی آزاد در کنار هم وجود نداشته باشد ";
                    case 207:
                        return "سیستم پایه فروش بلیت قادر به ارائه خدمات نمیباشد، لطفا پس از مدتی دوباره تلاش نمایید ";
                    case 208:
                        return "خطا در بازیابی نام و نام خانوادگی مسافر ";
                    case 209:
                        return "کد ملی و تاریخ تولد با هم مطابقت ندارند. ";
                    case 210:
                        return "خطای نامشخص در گرفتن لیست قطارها ";
                    case 211:
                        return "خطای نامشخص در ثبت بلیتها ";
                    case 212:
                        return "هیچ بلیتی با این مشخصات یافت نشد. ";
                    case 213:
                        return "اعتبار کاربر کافی نیست. ";
                    case 221:
                        return "آژانس شما غیر فعال است.";
                    case 226:
                        return "سرویس RegisterTickets قبل از سرویس SaveTicketsInfo فراخوانی شده است.";
                    default:
                        return "نامشخص";
                }
            }
            else
            {
                return exceptionMessage;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isCompartment"></param>
        /// <param name="compartmentCapicity"></param>
        /// <returns></returns>

        public static string ToWagonTypeName(this bool isCompartment, int compartmentCapicity)
        {
            if (isCompartment)
            {
                if (compartmentCapicity == 4)
                {
                    return "کوپه ای 4 نفره";
                }
                else if (compartmentCapicity == 6)
                {
                    return "کوپه ای 6 نفره";
                }
            }
            else
            {
                return "سالنی 4 ردیفه";
            }
            return "نامشخص";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isCompartment"></param>
        /// <param name="compartmentCapicity"></param>
        /// <returns></returns>
        public static WagonType ToWagonType(this bool isCompartment, int compartmentCapicity)
        {
            if (isCompartment)
            {
                if (compartmentCapicity == 4)
                {
                    return WagonType.CompartmentFour;
                }
                else if (compartmentCapicity == 6)
                {
                    return WagonType.CompartmentSix;
                }
            }
            else
            {
                return WagonType.SalonFour;
            }
            return WagonType.None;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string ToBaseUrl(this Company company, string url = "")
        {
            return (company == Company.Raja ?
                ApiUrl.RajaBaseUrl :
                company == Company.Fadak ?
ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl) + url;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static IEnumerable<T> OrEmpty<T>(this IEnumerable<T> sequence)
        {
            return sequence ?? Enumerable.Empty<T>();
        }

    }
}
