using Application.Interfaces;

namespace WebApi.Filter
{
    public class SearchParams : ISearchParams
    {
        public string q { get; set; }
        public List<int> category { get; set; }
        public SearchParams()
        {
            this.q = "";
            this.category = null;
        }
    }
}
