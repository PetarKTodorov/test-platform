namespace TestPlatform.Common.Extensions
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using TestPlatform.Common.Attributes;

    public static class EnumExtensions
    {
        /// <summary>
        /// Gets an enum Uid Guid, if one exists on the enum value, otherwise returns an empty Guid.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static Guid GetUid(this IComparable enumValue)
        {
            var enumType = enumValue.GetType().GetTypeInfo();
            var field = enumType.GetDeclaredField(enumValue.ToString());
            var attribute = field.GetCustomAttribute(typeof(UidAttribute), false);
            return attribute != null ? ((UidAttribute)attribute).Uid : Guid.Empty;
        }

        /// <summary>
        /// Gets an enum display attribute Name, if one exists on the enum value, otherwise returns the enum value as a string.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this IComparable enumValue)
        {
            var displayAttribute = GetDisplayAttribute(enumValue);
            return displayAttribute != null ? displayAttribute.Name : enumValue.ToString();
        }

        /// <summary>
        /// Gets an enum display attribute Description, if one exists on the enum value, otherwise returns the enum value as a string.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayDescription(this IComparable enumValue)
        {
            var displayAttribute = GetDisplayAttribute(enumValue);
            return displayAttribute != null ? displayAttribute.Description : enumValue.ToString();
        }

        /// <summary>
        /// Gets the display name of the short.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public static string GetDisplayShortName(this IComparable enumValue)
        {
            var displayAttribute = GetDisplayAttribute(enumValue);
            return displayAttribute != null ? displayAttribute.ShortName : enumValue.ToString();
        }

        /// <summary>
        /// Gets the display attribute.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        private static DisplayAttribute GetDisplayAttribute(this IComparable enumValue)
        {
            DisplayAttribute displayAttribute = null;

            var enumType = enumValue.GetType().GetTypeInfo();
            var field = enumType.GetDeclaredField(enumValue.ToString());
            var attribute = field.GetCustomAttribute(typeof(DisplayAttribute), false);
            if (attribute != null)
            {
                displayAttribute = (DisplayAttribute)attribute;
            }

            return displayAttribute;
        }
    }
}
