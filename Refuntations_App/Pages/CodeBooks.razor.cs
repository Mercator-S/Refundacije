namespace Refuntations_App.Pages
{
    partial class CodeBooks
    {
        private List<string> Columns { get; set; } = null;
        private List<object> Elements { get; set; } = new List<object>();
        protected override async Task OnInitializedAsync()
        {
            Columns = new List<string>();
            Columns.Add("First");
            Columns.Add("Second");
            Columns.Add("Third");
            Columns.Add("Fourth");
        }

    }
}
