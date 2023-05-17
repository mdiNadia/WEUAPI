using WEUPanel.Pages.Transaction;
using WEUPanel.Wrappers;

namespace WEUPanel.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<GetNameAndId>> GetAll();

        Task<PagedResponse<IEnumerable<TransactionModels.Transaction>>> GetAllByPaging(int pageIndex, int pageSize);
        Task<PagedResponse<IEnumerable<TransactionModels.Transaction>>> GetAllByWalletId(int id, int pageIndex, int pageSize);
    }
}
