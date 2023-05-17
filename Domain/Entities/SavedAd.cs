using Domain.Common;

namespace Domain.Entities
{
    public class SavedAd: BaseCreatioDate
    {

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }


        public int AdvertisingId { get; set; }
        public ConfirmResult Advertising { get; set; }


    }
}
