using System;
using System.ComponentModel;

namespace SpecflowMozart.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// To get the description of enum
        /// </summary>
        /// <param name="en">enum</param>
        /// <returns>Description</returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();
            string result = en.ToString();
            System.Reflection.MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    result = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return result;

        }
    }
}
