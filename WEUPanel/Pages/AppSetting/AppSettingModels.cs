namespace WEUPanel.Pages.AppSetting
{
    public class AppSettingModels
    {
        public class EditAppSetting
        {
            public int Id { get; set; }
            public decimal MinBoostAmount { get; set; }
            public decimal MinCostPerVisit { get; set; }
            public int MinView { get; set; }
            public int AppFee { get; set; }

        }
        public class AppSetting
        {
            public int Id { get; set; }
            public decimal MinBoostAmount { get; set; }
            public decimal MinCostPerVisit { get; set; }
            public int MinView { get; set; }
            public int AppFee { get; set; }

        }
    }
}
