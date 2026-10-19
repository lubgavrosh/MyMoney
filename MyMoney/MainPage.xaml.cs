namespace MyMoney
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnKartaTapped(object? sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage_1)}?id={e.Parameter}");
        }

    }
}