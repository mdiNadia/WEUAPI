namespace Application.Dtos.AppSetting
{
    public class GetAppsettingDto
    {
        public int Id { get; set; }
        public decimal MinBoostAmount { get; set; }
        public int MinWeuPerVisit { get; set; }
        public int MinView { get; set; }
        public int AppFee { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
