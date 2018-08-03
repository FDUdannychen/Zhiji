namespace Zhiji.Contracts.Domain.Templates
{
    public partial class Template
    {
        public static readonly int NameMinLength = 2;
        public static readonly int NameMaxLength = 20;

        public static readonly int BillingDateMonthMinValue = 1;
        public static readonly int BillingDateMonthMaxValue = 12;

        public static readonly int BillingDateDayMinValue = 1;
        public static readonly int BillingDateDayMaxValue = 28;
    }
}
