using NestedTabbed.Pages;

namespace NestedTabbed
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            #region Routing

            Routing.RegisterRoute(nameof(Assignment), typeof(Assignment));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));
            Routing.RegisterRoute(nameof(NewPage2), typeof(NewPage2));
            Routing.RegisterRoute(nameof(NewPage3), typeof(NewPage3));
            #endregion
        }
    }
}
