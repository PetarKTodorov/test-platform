namespace TestPlatform.Application.Infrastructures.Filtres
{
    using System;

    /// <summary>
    /// This attribute is used for creating search criteria 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomSearchFieldAttribute : Attribute
    {
    }
}
