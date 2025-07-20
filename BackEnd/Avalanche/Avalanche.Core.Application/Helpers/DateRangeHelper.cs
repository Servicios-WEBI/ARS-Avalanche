using Avalanche.Core.Application.Constants;
using Avalanche.Core.Application.Dtos.Common;

namespace Avalanche.Core.Application.Helpers
{
    public static class DateRangeHelper
    {
        public static (DateOnly start, DateOnly end) GetDateRange(int year, int month)
        {
            var start = new DateOnly(year, month, 1);

            // Si es diciembre, el mes siguiente es enero del siguiente año
            var nextMonth = month == 12 ? 1 : month + 1;
            var nextYear = month == 12 ? year + 1 : year;

            var end = new DateOnly(nextYear, nextMonth, 1).AddDays(-1);
            return (start, end);
        }

        public static (DateOnly prevStart, DateOnly prevEnd) GetPreviousMonthRange(int year, int month)
        {
            var prevMonth = month == 1 ? 12 : month - 1;
            var prevYear = month == 1 ? year - 1 : year;

            return GetDateRange(prevYear, prevMonth);
        }
    }
}
