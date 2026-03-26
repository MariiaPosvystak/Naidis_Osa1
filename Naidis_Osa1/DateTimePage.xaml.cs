namespace Naidis_Osa1;

public partial class DateTimePage : ContentPage
{
    DatePicker datePicker;
    TimePicker timePicker;
    Label datetimeLabel;
    AbsoluteLayout al;
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public DateTimePage()
	{
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "DateTime";
        datePicker = new DatePicker
        {
            MinimumDate = DateTime.Now.AddDays(-15),
            MaximumDate = DateTime.Now.AddDays(15),
            Date = DateTime.Now,
            HorizontalOptions = LayoutOptions.Center,
            Format = "D"
        };
        datePicker.DateSelected += (sender, e) =>
        {
            datetimeLabel.Text = $"Valitud kuupäev: \n{datePicker.Date:D}";
        };
        timePicker = new TimePicker
        {
            Time =DateTime.Now.TimeOfDay,
            HorizontalOptions = LayoutOptions.Center,
            Format = "T"
        };
        timePicker.PropertyChanged += (sender, e) =>
        {
            datetimeLabel.Text = $"Valitud kellaaeg: \n{timePicker.Time:T}";
        };
        datetimeLabel = new Label
        {
            Text = "Vali kuupäev või aeg",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
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
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { hsl }
        };
        sv = new ScrollView { Content = vsl };
        al = new AbsoluteLayout { Children = { datePicker, timePicker, datetimeLabel, sv } };
        List<View> controls = new List<View> { datePicker, timePicker, datetimeLabel, sv };
        Content = al;
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new Valgusfoor());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new StepperSliderPage());
        }
    }
}