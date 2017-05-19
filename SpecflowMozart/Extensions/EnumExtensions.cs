using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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


       public static T GetAttribute<T>(this Enum value)
       where T : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<T>()
                .SingleOrDefault();
        }

    }
}
