namespace WEUPanel.Shared.Common
{
    public class State
    {
        public string SelectedPage { get; private set; }
        
        public event Action OnChange;

        public void SetPage(string page)
        {
            SelectedPage = page;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
