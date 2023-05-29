namespace TestPlatform.Common.Attributes
{
    using System;
    using TestPlatform.Common.Constants;

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class UidAttribute : Attribute
    {
        public Guid Uid = Guid.Empty;

        public UidAttribute(string uid)
        {
            if (!Guid.TryParse(uid, out Uid))
            {
                throw new Exception(string.Format(ExceptionMessages.CAN_NOT_PARSE_UID, uid));
            }
        }
    }
}
