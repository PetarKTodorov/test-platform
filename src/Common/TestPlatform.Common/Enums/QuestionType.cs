namespace TestPlatform.Common.Enums
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Attributes;

    public enum QuestionType
    {
        [Display(Name = "Multiple Choice")]
        [Uid("AE825D9C-EF76-45E9-A31C-561976E6387E")]
        MultipleChoice = 1,

        [Display(Name = "Single Choice")]
        [Uid("00F1EA00-27B4-4894-8AFE-877C36B9E41D")]
        SingleChoice = 2,
    }
}
