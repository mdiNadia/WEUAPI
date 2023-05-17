

namespace Application.Interfaces
{
    public interface ISearchParams
    {
        string q { get; set; }
        List<int> category { get; set; }
    }
}
