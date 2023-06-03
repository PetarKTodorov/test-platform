namespace TestPlatform.Common.Constants
{
    using System;

    public static class ErrorMessages
    {
        public const string ENTER_AT_LEAST_TWO_ANSWERS_ERROR_MESSAGE = "Please, enter at least two answers!";

        public const string SELECT_ONE_CORRECT_ANSWER_ERROR_MESSAGE = "Please, select one correct asnwer!";

        public const string SELECT_AT_LEAST_TWO_CORRECT_ANSWERS_ERROR_MESSAGE = "Please, select two or more correct answers! If you want one correct answer you can use Single Choice";

        public const string ALL_ANSWERS_ARE_CORRECT_ERROR_MESSAGE = "You cannot select all answers as correct. Please, leave at least one as unmarked.";

        public const string EMAIL_IS_ALREADY_USED_ERROR_MESSAGE = "This email is already used on the platform";

        public const string INVALID_CREDENTIALS_ERROR_MESSAGE = "Invalid password or email.";

        public const string QUESTION_POINTS_MUST_BE_GREATER_THAN_ZERO = "Question points must be greater than zero";

        public const string TEST_DOESNT_HAVE_ENOUGH_QUESTION_POINTS = "In order to create a test you should add questions with at least {0} points at total";

        public const string THERE_ARE_ROOMS_IN_THE_FUTURE_WITH_THIS_TEST = "There are rooms in the future with this test. Count of the rooms: {0}";

        public const string PARTICIPANTS_ARE_REQUIRED = "The participants are required";
    }
}
