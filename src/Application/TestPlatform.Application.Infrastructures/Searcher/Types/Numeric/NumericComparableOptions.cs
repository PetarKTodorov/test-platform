namespace TestPlatform.Application.Infrastructures.Searcher.Types.Numeric
{
    using System.ComponentModel.DataAnnotations;

    public enum NumericComparableOptions
    {
        [Display(Name = "Less")]
        Less,

        [Display(Name = "Less or equal")]
        LessOrEqual,

        [Display(Name = "Equals")]
        Equal,

        [Display(Name = "Greater or equal")]
        GreaterOrEqual,

        [Display(Name = "Greater")]
        Greater,

        [Display(Name = "Range")]
        InRange
    }
}
