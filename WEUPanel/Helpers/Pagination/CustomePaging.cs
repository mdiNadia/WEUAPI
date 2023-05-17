using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WEUPanel.ResourceFiles;

namespace WEUPanel.Helpers.Pagination
{
    public partial class CustomePaging
    {
        [Parameter]
        public int pageNumber { get; set; }
        [Parameter]
        public int pageSize { get; set; }
        [Parameter]
        public int totalRecords { get; set; }
        [Parameter]
        public int totalPages { get; set; }
        [Parameter]
        public Uri nextPage { get; set; }
        [Parameter]
        public Uri previousPage { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public int lastPage { get; set; }
        [Parameter]
        public int firstPage { get; set; }
        [Inject]
        public IStringLocalizer<Resource> _localizer { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        private List<PagingLink> _links;
        public bool HasNext { get; set; } = false;
        public bool HasPrevious { get; set; } = false;
        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            if (nextPage != null)
                HasNext = true;
            if (previousPage != null)
                HasPrevious = true;
            _links = new List<PagingLink>();
            _links.Add(new PagingLink(1, pageNumber == 1 ? false : true, _localizer["صفحه اول"]) { Active = pageNumber == 1 });
            _links.Add(new PagingLink(pageNumber - 1, previousPage != null ? true : false, _localizer["صفحه قبل"]) { Active = pageNumber == pageNumber - 1 });

            for (int i = 1; i <= totalPages; i++)
            {
                if (i >= pageNumber - Spread && i <= pageNumber + Spread)
                {
                    _links.Add(new PagingLink(i, true, i.ToString()) { Active = pageNumber == i });
                }
            }
            _links.Add(new PagingLink(pageNumber + 1, nextPage != null ? true : false, _localizer["صفحه بعد"]) { Active = pageNumber == pageNumber + 1 });
            _links.Add(new PagingLink(totalPages, pageNumber == totalPages ? false : true, _localizer["صفحه آخر"]) { Active = pageNumber == totalPages });

        }

        private async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == pageNumber || !link.Enabled)
                return;

            pageNumber = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
