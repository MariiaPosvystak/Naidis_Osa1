using System.Collections.ObjectModel;

namespace Naidis_Osa1;

public class Riik
{
    public string Nimi { get; set; }
    public string Pealinn { get; set; }
    public int Rahvaarv { get; set; }
    public string Lipp { get; set; } // pildi failinimi
}

public partial class Euroopa_riik : ContentPage
{
    ObservableCollection<Riik> riigid;
    ListView list;

    Entry entryNimi, entryPealinn, entryRahvaarv, entryLipp;
    Riik valitudRiik = null;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public Euroopa_riik()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Euroopa riikide rakendus";
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
        entryNimi = new Entry { Placeholder = "Riigi nimi (nt Eesti)" };
        entryPealinn = new Entry { Placeholder = "Pealinn (nt Tallinn)" };
        entryRahvaarv = new Entry { Placeholder = "Rahvaarv", Keyboard = Keyboard.Numeric };
        entryLipp = new Entry { Placeholder = "Lipu fail (nt estonia.png)" };

        Button btnLisa = new Button { Text = "Lisa riik", BackgroundColor = Colors.LightGreen };
        btnLisa.Clicked += Lisa_Clicked;

        Button btnMuuda = new Button { Text = "Salvesta muudatused", BackgroundColor = Colors.LightBlue };
        btnMuuda.Clicked += Muuda_Clicked;

        Button btnKustuta = new Button { Text = "Kustuta valitud", BackgroundColor = Colors.LightPink };
        btnKustuta.Clicked += Kustuta_Clicked;

        riigid = new ObservableCollection<Riik>
        {
            new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=1331000, Lipp="estonia.png" },
            new Riik { Nimi="Soome", Pealinn="Helsinki", Rahvaarv=5540000, Lipp="finland.png" },
            new Riik { Nimi="Läti", Pealinn="Riia", Rahvaarv=1900000, Lipp="latvia.png" }
        };

        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = riigid,
            SelectionMode = ListViewSelectionMode.Single
        };
        list.ItemTapped += List_ItemTapped;

        list.ItemTemplate = new DataTemplate(() =>
        {
            Image img = new Image
            {
                HeightRequest = 50,
                WidthRequest = 70,
                Aspect = Aspect.AspectFit,
                Margin = new Thickness(0, 0, 10, 0)
            };
            img.SetBinding(Image.SourceProperty, "Lipp");

            Label lblNimi = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
            lblNimi.SetBinding(Label.TextProperty, "Nimi");

            Label lblPealinn = new Label { TextColor = Colors.Gray };
            lblPealinn.SetBinding(Label.TextProperty, "Pealinn");

            var text = new VerticalStackLayout
            {
                Children = { lblNimi, lblPealinn }
            };
            return new ViewCell
            {
                View = new HorizontalStackLayout
                {
                    Padding = 10,
                    Children = { img, text }
                }
            };
        });

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 10,
                Children =
                {
                    entryNimi,
                    entryPealinn,
                    entryRahvaarv,
                    entryLipp,
                    btnLisa,
                    btnMuuda,
                    btnKustuta,
                    list,
                    hsl
                }
            }
        };
    }
    private async void Lisa_Clicked(object sender, EventArgs e)
    {
        string nimi = entryNimi.Text;

        if (string.IsNullOrWhiteSpace(nimi))
        {
            await DisplayAlert("Viga", "Riigi nimi on kohustuslik!", "OK");
            return;
        }

        bool olemas = riigid.Any(r => r.Nimi.Equals(nimi, StringComparison.OrdinalIgnoreCase));

        if (olemas)
        {
            await DisplayAlert("Viga", "See riik on juba nimekirjas!", "OK");
            return;
        }

        int rahvaarv = 0;
        int.TryParse(entryRahvaarv.Text, out rahvaarv);

        riigid.Add(new Riik
        {
            Nimi = entryNimi.Text,
            Pealinn = entryPealinn.Text,
            Rahvaarv = rahvaarv,
            Lipp = string.IsNullOrWhiteSpace(entryLipp.Text) ? "default_flag.png" : entryLipp.Text
        });
        TühjendaVäljad();
    }
    private async void Muuda_Clicked(object sender, EventArgs e)
    {
        if (valitudRiik == null)
        {
            await DisplayAlert("Viga", "Vali riik, mida muuta!", "OK");
            return;
        }

        valitudRiik.Nimi = entryNimi.Text;
        valitudRiik.Pealinn = entryPealinn.Text;
        int.TryParse(entryRahvaarv.Text, out int arv);
        valitudRiik.Rahvaarv = arv;
        valitudRiik.Lipp = entryLipp.Text;

        list.ItemsSource = null;
        list.ItemsSource = riigid;

        TühjendaVäljad();
    }
    private async void Kustuta_Clicked(object sender, EventArgs e)
    {
        if (valitudRiik == null)
        {
            await DisplayAlert("Viga", "Vali riik, mida kustutada!", "OK");
            return;
        }

        bool kinnitus = await DisplayAlert("Kustutamine",
            $"Kas kustutada {valitudRiik.Nimi}?",
            "Jah", "Ei");

        if (kinnitus)
        {
            riigid.Remove(valitudRiik);
            valitudRiik = null;
            TühjendaVäljad();
        }
    }
    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        valitudRiik = e.Item as Riik;

        if (valitudRiik != null)
        {
            await DisplayAlert("Riigi info",
                $"Riik: {valitudRiik.Nimi}\nPealinn: {valitudRiik.Pealinn}\nRahvaarv: {valitudRiik.Rahvaarv} inimest",
                "OK");

            entryNimi.Text = valitudRiik.Nimi;
            entryPealinn.Text = valitudRiik.Pealinn;
            entryRahvaarv.Text = valitudRiik.Rahvaarv.ToString();
            entryLipp.Text = valitudRiik.Lipp;
        }
    }

    private void TühjendaVäljad()
    {
        entryNimi.Text = "";
        entryPealinn.Text = "";
        entryRahvaarv.Text = "";
        entryLipp.Text = "";
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
            Navigation.PushAsync(new List_Page());
        else if (nupp.ZIndex == 1)
            Navigation.PopToRootAsync(); 
        else if (nupp.ZIndex == 2)
            Navigation.PushAsync(new Karussel());
    }
}