namespace HouseRentingSystem.Services.Data
{
    public class DataConstants
    {
        public class Category
        {
            public const int NameMaxLength = 50;
        }

        public class House
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int AddressMaxLength = 150;
            public const int AddressMinLength = 30;

            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 50;
        }

        public class Agent
        {
            public const int PhoneNumberMaxLength = 15;
            public const int PhoneNumberMinLength = 7;

            public const int EmailMaxLength = 50;
            public const int EmailMinLength = 10;
        }

        public class User
        {
            public const int UserFirstNameMaxLength = 12;
            public const int UserFirstNameMinLength = 1;

            public const int UserLastNameMaxLength = 15;
            public const int UserLastNameMinLength = 3;
        }
    }
}