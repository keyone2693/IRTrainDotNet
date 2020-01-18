using System;
using System.Linq;

namespace IRTrainDotNet.Helpers
{
    public static class Extentions
    {
        public static string ToAuthorization(this string token)
        {
            return Constants.PreToken + token;
        }
        public static Uri ToUri(this string url, int id = -1)
        {
            if (id != -1)
                return new Uri(url + id, UriKind.Absolute);
            else
                return new Uri(url, UriKind.Absolute);
        }

        public static bool IsSystemError(this int code)
        {
            return Constants.SystemErrorCodes.Contains(code);
        }
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

        public static string ToBaseUrl(this Company company, string url = "")
        {
            return company == Company.Raja ?
                ApiUrl.RajaBaseUrl :
                company == Company.Fadak ?
                ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl + "/" + url;
        }

    }
}
