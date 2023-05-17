namespace TestPlatform.Application.Infrastructures.Searcher.Types.DateTime
{
    using System.ComponentModel.DataAnnotations;

    public enum DateTimeComparableOptions
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
