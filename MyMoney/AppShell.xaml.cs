namespace MyMoney
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetailsPage_1), typeof(DetailsPage_1));
            Routing.RegisterRoute(nameof(FormularzPage), typeof(FormularzPage));
        }
    }
}
