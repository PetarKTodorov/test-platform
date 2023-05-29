namespace TestPlatform.Common.Enums
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Attributes;

    public enum QuestionType
    {
        [Display(Name = "Text")]
        [Uid("4A1856B3-CDA0-4A21-847A-1C953F9528E6")]
        Text = 1,

        [Display(Name = "Multiple Choice")]
        [Uid("AE825D9C-EF76-45E9-A31C-561976E6387E")]
        MultipleChoice = 2,

        [Display(Name = "Single Choice")]
        [Uid("00F1EA00-27B4-4894-8AFE-877C36B9E41D")]
        SingleChoice = 3,
    }
}
