using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Refuntations_App.Shared
{
    partial class MainLayout
    {
        private MudTheme _theme = new();
        bool _drawerOpen = true;
        EventCallback closeNavMenu => EventCallback.Factory.Create(this, DrawerToggle);
        ErrorBoundary? errorBoundary;
        protected override void OnInitialized()
        {
            _theme.Palette.Primary = new MudBlazor.Utilities.MudColor("#d90142");
            _theme.Palette.Secondary = new MudBlazor.Utilities.MudColor("#4a4a4a");
        }
        protected override void OnParametersSet()
        {
            errorBoundary?.Recover();
        }
        public void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
