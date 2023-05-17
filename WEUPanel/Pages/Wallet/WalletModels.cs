using WEUPanel.Wrappers;

namespace WEUPanel.Pages.Wallet
{
    public class WalletModels
    {
        public class Wallet
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
            //واحد پول
            public GetNameAndId Currency { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime UpdateDate { get; set; }

            public decimal TotalCredit { get; set; }
            public GetNameAndIdString User { get; set; }
        }



        public class CreateWallet
        {

            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }

            //واحد پول
            public int CurrencyId { get; set; }

            public decimal TotalCredit { get; set; }
            public string UserId { get; set; }
        }
        public class EditWallet
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }


        }


    }
}
