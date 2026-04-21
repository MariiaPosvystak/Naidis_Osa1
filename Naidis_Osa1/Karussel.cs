using System.Collections.ObjectModel;

namespace Naidis_Osa1;
public partial class Karussel : ContentPage
{
    private Microsoft.Maui.Controls.CarouselView carouselView;
    private ObservableCollection<CarouselItem> items;
    private int position = 0;
    HorizontalStackLayout hsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public Karussel()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Karussell - Dünaamiline lisamine";

        var items = new List<CarouselItem>
        {
            new CarouselItem { Title = "Päikesetõus", ImageUrl = "https://picsum.photos/id/1015/600/400" },
            new CarouselItem { Title = "Metsavaikus", ImageUrl = "https://picsum.photos/id/1016/600/400" },
            new CarouselItem { Title = "Järvepeegel", ImageUrl = "https://picsum.photos/id/1018/600/400" }
        };
        carouselView = new Microsoft.Maui.Controls.CarouselView
        {
            ItemsSource = items,
            HeightRequest = 300,
            IsBounceEnabled = true,
            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 15,
                    HasShadow = true,
                    Padding = 0,
                    Margin = new Thickness(5),
                    BackgroundColor = Colors.Black
                };
                var grid = new Grid();
                var image = new Image { Aspect = Aspect.AspectFill };
                image.SetBinding(Image.SourceProperty, "ImageUrl");
                var gradient = new BoxView
                {
                    Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 1),
                        EndPoint = new Point(0, 0),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Colors.Black.WithAlpha(0.7f), 0),
                            new GradientStop(Colors.Transparent, 1)
                        }
                    }
                };
                var label = new Label
                {
                    TextColor = Colors.White,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(15),
                    VerticalOptions = LayoutOptions.End
                };
                var tap = new TapGestureRecognizer();
                tap.Tapped += async (s, e) =>
                {
                    var tappedItem = ((Frame)s).BindingContext as CarouselItem;
                    await DisplayAlert("Valisid: ", tappedItem?.Title ?? "Tundmatu", "OK");
                };
                label.SetBinding(Label.TextProperty, "Title");
                frame.GestureRecognizers.Add(tap);
                grid.Children.Add(image);
                grid.Children.Add(gradient);
                grid.Children.Add(label);
                frame.Content = grid;
                return frame;
            })
        };
        var indicatorView = new IndicatorView
        {
            IndicatorColor = Colors.LightGray,
            SelectedIndicatorColor = Colors.DarkSlateBlue,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };
        carouselView.IndicatorView = indicatorView;
        var lisaNupp = new Button
        {
            Text = "Lisa uus pilt",
            TextColor = Color.FromHex("#E6CCB2"),
            BackgroundColor = Color.FromHex("#7F5539"),
            BorderColor = Color.FromHex("#7F5539"),
            CornerRadius = 10,
            Margin = new Thickness(0, 20, 0, 0)
        };
        lisaNupp.Clicked += (sender, e) =>
        {
            items.Add(new CarouselItem
            {
                Title = "Pildi",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/5/53/Pilt_v6handu_1.jpg"
            });
            carouselView.Position = items.Count - 1;
        };
        Device.StartTimer(TimeSpan.FromSeconds(4), () =>
        {
            if (items == null || items.Count == 0) return false;
            position = (position + 1) % items.Count;
            carouselView.Position = position;
            return true;
        });
        hsl = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 16,
                FontFamily = "LowerWestSide400",
                TextColor = Color.FromHex("#7F5539"),
                BackgroundColor = Color.FromHex("#E6CCB2"),
                BorderColor = Color.FromHex("#7F5539"),
                BorderWidth = 2,
                CornerRadius = 10,
                HeightRequest = 30,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }
        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 20, 
                Children =
                {
                    carouselView,
                    indicatorView,
                    lisaNupp,
                    hsl
                }
            }
        };
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
            Navigation.PushAsync(new Euroopa_riik());
        else if (nupp.ZIndex == 1)
            Navigation.PopToRootAsync();
        else if (nupp.ZIndex == 2)
            Navigation.PushAsync(new Karussel());
    }

    public class CarouselItem
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
    }
}
