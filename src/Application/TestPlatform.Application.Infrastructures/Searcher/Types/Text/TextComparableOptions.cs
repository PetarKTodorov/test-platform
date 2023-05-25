namespace TestPlatform.Application.Infrastructures.Searcher.Types.Text
{
    using System.ComponentModel.DataAnnotations;

    public enum TextComparableOptions
    {
        [Display(Name = "Contains")]
        Contains,

        [Display(Name = "Equals")]
        Equal
    }
}
