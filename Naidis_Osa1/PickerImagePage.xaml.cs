namespace Naidis_Osa1;

public partial class PickerImagePage : ContentPage
{
    Grid gr4x1, gr3x3;
    Picker picker;
    Image img;
    Switch s_pilt, s_grid;
    Random rnd=new Random();
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public PickerImagePage()
    {
        gr4x1 = new Grid 
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength ( 1, GridUnitType.Star ) },
                new RowDefinition { Height = new GridLength ( 3, GridUnitType.Star ) },
                new RowDefinition { Height = new GridLength ( 3, GridUnitType.Star ) },
                new RowDefinition { Height = new GridLength ( 1, GridUnitType.Star ) }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star ) },
                new ColumnDefinition { Width = new GridLength( 1, GridUnitType.Star ) }
            }
        };
        picker = new Picker 
        {
            Title = "Vali pilt",
            ItemsSource = new List<string> { "Cat", "Pilt 2", "Pilt 3" },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        picker.SelectedIndexChanged += Piltide_valik;
        img = new Image 
        {
            Source = "Grid_cat.png",
            HorizontalOptions = LayoutOptions.Center
        };
        s_pilt = new Switch 
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsToggled = true,
            IsEnabled = true
        };
        s_pilt.Toggled += (sender, e) =>
        {
            if (e.Value)
            {
                img.IsVisible = true;
            }
            else
            {
                img.IsVisible = false;
            }
        };
        s_grid = new Switch 
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsToggled = true,
            IsEnabled = true
        };
        s_grid.Toggled += (sender, e) =>
        {
            if (e.Value)
            {
                gr3x3=Täida_gr3x3();

                gr4x1.Add(gr3x3, 0, 2);
                gr4x1.SetColumnSpan(gr3x3, 2);
            }
            else
            {
                gr4x1.RemoveAt(4);
            }
        };
        gr4x1.Add(picker, 0, 0);
        gr4x1.SetColumnSpan(picker, 2);
        gr4x1.Add(img, 0, 1);
        gr4x1.SetColumnSpan(img, 2);
        gr4x1.Add(s_pilt, 0, 3);
        gr4x1.Add(s_grid, 1, 3);
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
                FontSize = 28,
                FontFamily = "LowerWestSide400",
                TextColor = Colors.Chocolate,
                BackgroundColor = Colors.Beige,
                CornerRadius = 10,
                HeightRequest = 50,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { gr4x1, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private Grid Täida_gr3x3()
    {
        Grid gr3x3 = new Grid();
        for (int i = 0; i < 3; i++)
        {
            gr3x3.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gr3x3.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                BoxView kast = new BoxView
                {
                    BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256))
                };
                gr3x3.Add(kast, c, r);
                int rida = r;
                int veerg = c;
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (s, args) =>
                {
                    kast.BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    await DisplayAlertAsync("Koordinaadid",$"Vajutasid lahtrisse:\nRida: {rida}\nVeerg: {veerg}", "Selge");
                };
                kast.GestureRecognizers.Add(tap);
            }
        }
        return gr3x3;
    }
    private void Piltide_valik(object? sender, EventArgs e)
    {
        if(picker.SelectedIndex == -1) return;
        if (picker.SelectedIndex == 0) img.Source = "grid_cat.png";
        else if (picker.SelectedIndex == 1) img.Source = "grid_koala.png";
        else if (picker.SelectedIndex == 2)img.Source = "grid_hamster.png";
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new PopUp());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new Trips_Traps_Trull());
        }
    }
}