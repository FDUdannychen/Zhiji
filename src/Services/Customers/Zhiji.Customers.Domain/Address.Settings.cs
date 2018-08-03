namespace Zhiji.Customers.Domain
{
    public partial class Address
    {
        public static readonly int CountryMinLength = 2;
        public static readonly int CountryMaxLength = 20;

        public static readonly int ProvinceMinLength = 2;
        public static readonly int ProvinceMaxLength = 20;

        public static readonly int CityMinLength = 2;
        public static readonly int CityMaxLength = 20;

        public static readonly int StreetMinLength = 2;
        public static readonly int StreetMaxLength = 50;
    }
}
