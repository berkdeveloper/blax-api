﻿using System.ComponentModel;
using System.Globalization;

namespace BlaX.CryptoAutoTrading.Domain.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;

                        break;
                    }
            }
            return description;
        }

        public static string GetDescription<T>(this object value)
        {
            try
            {
                if (value != null)
                {
                    string description = ((T)Enum.Parse(typeof(T), value.ToString()) as Enum).GetDescription();
                    if (description != null) return description;
                }
                return string.Empty;
            }
            catch { return string.Empty; }
        }
    }
}
