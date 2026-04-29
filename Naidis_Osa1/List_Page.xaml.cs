using System.Collections.ObjectModel;

namespace Naidis_Osa1;

public class Telefon
{
    public string Nimetus { get; set; }
    public string Tootja { get; set; }
    public int Hind { get; set; }
    public string Pilt { get; set; } // Hoiab pildi nime või seadme failiteed
}
public partial class List_Page : ContentPage
{
    ObservableCollection<Telefon> telefons;
    ListView list;
    Entry entryNimetus, entryTootja, entryHind;

    string valitudPildiTee = "";
    Label lblValitudPilt;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public List_Page()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Telefonide haldus";
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
        telefons = new ObservableCollection<Telefon>
        {
            new Telefon { Nimetus="Samsung Galaxy S22 Ultra", Tootja="Samsung", Hind=1349, Pilt="Galaxy.png" },
            new Telefon { Nimetus="Xiaomi Mi 11 Lite 5G NE", Tootja="Xiaomi", Hind=399, Pilt="default_phone.png" },
            new Telefon { Nimetus="iPhone 13 mini", Tootja="Apple", Hind=1179, Pilt="iPhone13.png" }
        };
        entryNimetus = new Entry { Placeholder = "Telefoni mudel (nt iPhone 14)" };
        entryTootja = new Entry { Placeholder = "Tootja (nt Apple)" };
        entryHind = new Entry { Placeholder = "Hind (täisarv)", Keyboard = Keyboard.Numeric };
        Button btnValiPilt = new Button { Text = "📷 Vali pilt galeriist", BackgroundColor = Colors.LightBlue };
        btnValiPilt.Clicked += BtnValiPilt_Clicked;
        lblValitudPilt = new Label { Text = "Pilti pole valitud (kasutatakse vaikimisi pilti)", FontSize = 12, TextColor = Colors.Gray };
        Button btnLisa = new Button { Text = "Lisa telefon", BackgroundColor = Colors.LightGreen };
        btnLisa.Clicked += Lisa_Clicked;
        Button btnKustuta = new Button { Text = "Kustuta valitud telefon", BackgroundColor = Colors.LightPink };
        btnKustuta.Clicked += Kustuta_Clicked;
        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = telefons,
            SelectionMode = ListViewSelectionMode.Single
        };
        list.ItemTapped += List_ItemTapped;

        list.ItemTemplate = new DataTemplate(() =>
        {
            Image imgPilt = new Image
            {
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 10, 0)
            };
            imgPilt.SetBinding(Image.SourceProperty, "Pilt");

            Label lblNimetus = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
            lblNimetus.SetBinding(Label.TextProperty, "Nimetus");

            Label lblTootja = new Label { TextColor = Colors.Gray };
            lblTootja.SetBinding(Label.TextProperty, "Tootja");

            Label lblHind = new Label { TextColor = Colors.DarkBlue, FontAttributes = FontAttributes.Bold };
            lblHind.SetBinding(Label.TextProperty, new Binding("Hind", stringFormat: "{0} €"));

            var textLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Children = { lblNimetus, lblTootja, lblHind }
            };

            // Kogu rea paigutus (Pilt vasakul, tekst paremal)
            var rowLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10),
                Children = { imgPilt, textLayout }
            };

            return new ViewCell { View = rowLayout };
        });
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children =
            {
                entryNimetus,
                entryTootja,
                entryHind,
                btnValiPilt,
                lblValitudPilt,
                btnLisa,
                btnKustuta,
                list,
                hsl
            }
        };
        Content = vsl;
    }
    private async void BtnValiPilt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();

            if (photo != null)
            {
                valitudPildiTee = photo.FullPath; // Jätame asukoha meelde
                lblValitudPilt.Text = $"Valitud: {photo.FileName}";
                lblValitudPilt.TextColor = Colors.Green;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", "Pildi valimine ebaõnnestus: " + ex.Message, "OK");
        }
    }
    private void Lisa_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(entryNimetus.Text) && !string.IsNullOrWhiteSpace(entryTootja.Text))
        {
            int hind = 0;
            int.TryParse(entryHind.Text, out hind);

            // Kui pilti ei valitud, kasutame vaikimisi faili
            string pildiNimi = string.IsNullOrWhiteSpace(valitudPildiTee) ? "default_phone.png" : valitudPildiTee;

            telefons.Add(new Telefon
            {
                Nimetus = entryNimetus.Text,
                Tootja = entryTootja.Text,
                Hind = hind,
                Pilt = pildiNimi
            });

            // Puhastame väljad uue sisestuse jaoks
            entryNimetus.Text = "";
            entryTootja.Text = "";
            entryHind.Text = "";

            // Lähtestame pildi valiku oleku
            valitudPildiTee = "";
            lblValitudPilt.Text = "Pilti pole valitud (kasutatakse vaikimisi pilti)";
            lblValitudPilt.TextColor = Colors.Gray;
        }
        else
        {
            DisplayAlert("Viga", "Palun täida vähemalt mudeli ja tootja väljad!", "OK");
        }
    }
    private async void Kustuta_Clicked(object sender, EventArgs e)
    {
        Telefon valitudTelefon = list.SelectedItem as Telefon;

        if (valitudTelefon != null)
        {
            bool vastus = await DisplayAlert("Kinnitus", $"Kas oled kindel, et soovid mudeli {valitudTelefon.Nimetus} kustutada?", "Jah", "Ei");

            if (vastus == true)
            {
                telefons.Remove(valitudTelefon);
                list.SelectedItem = null;
            }
        }
        else
        {
            await DisplayAlert("Viga", "Palun vali nimekirjast telefon, mida soovid kustutada.", "OK");
        }
    }
    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Telefon valitudTelefon = e.Item as Telefon;

        if (valitudTelefon != null)
        {
            await DisplayAlert("Telefoni info", $"Tootja: {valitudTelefon.Tootja}\nMudel: {valitudTelefon.Nimetus}\nHind: {valitudTelefon.Hind} €", "Sulge");
        }
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
            Navigation.PushAsync(new Sobrade_kont());
        else if (nupp.ZIndex == 1)
            Navigation.PopToRootAsync(); 
        else if (nupp.ZIndex == 2)
            Navigation.PushAsync(new Euroopa_riik());
    }
}