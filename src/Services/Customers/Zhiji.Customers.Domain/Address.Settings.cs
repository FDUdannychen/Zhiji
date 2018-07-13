namespace Zhiji.Customers.Domain
{
    public partial class Address
    {
        public const int CountryMinLength = 2;
        public const int CountryMaxLength = 20;

        public const int ProvinceMinLength = 2;
        public const int ProvinceMaxLength = 20;

        public const int CityMinLength = 2;
        public const int CityMaxLength = 20;

        public const int StreetMinLength = 2;
        public const int StreetMaxLength = 50;
    }
}
