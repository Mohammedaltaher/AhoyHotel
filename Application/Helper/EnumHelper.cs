using System.ComponentModel;
using System.Reflection;

namespace Application.Helper;
public static class EnumHelper
{

    public static string GetDescription<T>(this T source)
    {
        FieldInfo fi = source.GetType().GetField(source.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute), false);

        return attributes != null && attributes.Length > 0 ? attributes[0].Description : source.ToString();
    }
}
