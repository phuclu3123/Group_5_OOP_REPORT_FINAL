using System;

namespace Logistics.Core.Utilities
{
    public static class DateTimeHelper
    {
        public static string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string FormatDate(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        public static string FormatTime(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss");
        }

        public static string ToRelative(DateTime dateTime)
        {
            TimeSpan diff = DateTime.Now - dateTime;

            if (diff.TotalSeconds < 60)
            {
                return "Just now";
            }
            if (diff.TotalMinutes < 60)
            {
                return (int)diff.TotalMinutes + " minutes ago";
            }
            if (diff.TotalHours < 24)
            {
                return (int)diff.TotalHours + " hours ago";
            }
            if (diff.TotalDays < 7)
            {
                return (int)diff.TotalDays + " days ago";
            }

            return FormatDate(dateTime);
        }

        public static bool IsExpired(DateTime dueDate)
        {
            return DateTime.Now > dueDate;
        }
    }
}
