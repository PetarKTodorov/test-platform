namespace TestPlatform.Common.Enums
{
    using System.ComponentModel.DataAnnotations;
    using TestPlatform.Common.Attributes;

    public enum Roles
    {
        [Display(Name = "Administrator")]
        [Uid("2EE73BF7-FE5D-4096-BE3A-8E35400FD350")]
        Administrator = 1,

        [Display(Name = "Teacher")]
        [Uid("2EE73BF7-FE5D-4096-BE3A-8E35400FD352")]
        Teacher = 2,

        [Display(Name = "Student")]
        [Uid("2EE73BF7-FE5D-4096-BE3A-8E35400FD353")]
        Student = 3,
    }
}
