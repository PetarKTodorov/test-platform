namespace TestPlatform.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    using TestPlatform.Common.Attributes;

    public enum StatusType
    {
        [Display(Name = "Private")]
        [Uid("63F53166-7DA4-4276-A3A3-2E074983A7B8")]
        Private = 1,

        [Display(Name = "Pending")]
        [Uid("109D17AA-04CD-4A20-8ECD-DDB450C2DF8B")]
        Pending = 2,

        [Display(Name = "Ready")]
        [Uid("1622f3f8-2fc9-4b79-9a80-3575d464e972")]
        Ready = 3,

        [Display(Name = "Public")]
        [Uid("1C15F1F7-5CEC-48F6-B4B2-2475B3A5FB09")]
        Public = 4,
    }
}
