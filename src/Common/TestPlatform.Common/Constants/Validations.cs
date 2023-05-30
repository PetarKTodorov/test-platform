namespace TestPlatform.Common.Constants
{
    public static class Validations
    {
        public const int ZERO = 0;

        public const int ONE = 1;

        public const int TWO = 2;

        public const int TWO_POWER_EIGHT = 256;

        public const int TWO_POWER_TEN = 1024;

        public const int TWO_POWER_THIRTEEN = 8192;

        public const int TWO_POWER_SIXTEEN = 65536;

        public const int MAX_QUESTION_ANSWERS_COUNT = 5;

        public const int REQUIRED_CORRECT_ANSWERS_FOR_MULTIPLE_CHOICE = 2;

        public const int REQUIRED_CORRECT_ANSWERS_FOR_SIGNLE_CHOICES = 1;

        public const int REQUIRRED_NUMBER_OF_ANSWERS_FOR_QUESTION = 2;

        public const string EMAIL_REGEX = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

        // Have at least 10 characters, including at least two uppercase letters, two lowercase letters, two numbers, and two special characters
        public const string PASSWORD_REGEX = "^(?=.*[a-z].*[a-z])(?=.*[A-Z].*[A-Z])(?=.*\\d.*\\d)(?=.*[!@#$%^&*()_+].*[!@#$%^&*()_+])[A-Za-z\\d!@#$%^&*()_+]{10,}$";
    }
}
