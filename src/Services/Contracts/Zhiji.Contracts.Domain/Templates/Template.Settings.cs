namespace Zhiji.Contracts.Domain.Templates
{
    public partial class Template
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 20;

        public const int BillingDateMonthMinValue = 1;
        public const int BillingDateMonthMaxValue = 12;

        public const int BillingDateDayMinValue = 1;
        public const int BillingDateDayMaxValue = 28;
    }
}
