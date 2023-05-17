using WEUPanel.Pages.Wallet;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface IWalletService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<WalletModels.Wallet>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<Response<WalletModels.Wallet>> GetById(int id);

        Task<HttpResponseMessage> AddEntity(WalletModels.CreateWallet command);
        Task<HttpResponseMessage> UpdateEntity(int id, WalletModels.EditWallet command);


        Task<HttpResponseMessage> AddEntityFormFile(MultipartFormDataContent command);
        Task<HttpResponseMessage> UpdateEntityFormFile(int id, MultipartFormDataContent command);
    }
}
