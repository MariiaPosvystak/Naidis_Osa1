using Microsoft.Maui.ApplicationModel.Communication;
using Microsoft.Maui.Media;

namespace Naidis_Osa1;

public partial class Sobrade_kont : ContentPage
{
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    Entry nimi, email_phone, telefon, sonum;
    Image foto;

    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };

    public Sobrade_kont()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Sõbrade kontaktandmed";

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
        var table = new TableView
        {
            Intent = TableIntent.Form,
            Root = new TableRoot("Sõbra andmed")
            {
                new TableSection("Kontakt")
                {
                    new ViewCell { View = (nimi = new Entry { Placeholder = "Nimi" }) },
                    new ViewCell { View = (email_phone = new Entry { Placeholder = "Email" }) },
                    new ViewCell { View = (telefon = new Entry { Placeholder = "Telefon" }) },
                    new ViewCell { View = (sonum = new Entry { Placeholder = "Sisesta sõnum" }) }
                },

                new TableSection("Foto")
                {
                    new ViewCell
                    {
                        View = new VerticalStackLayout
                        {
                            Children =
                            {
                                (foto = new Image
                                {
                                    Source = "default_friend.png",
                                    HeightRequest = 150,
                                    WidthRequest = 150
                                }),
                                new Button
                                {
                                    Text = "Muuda fotot",
                                    BackgroundColor = Color.FromHex("#E6CCB2"),
                                    TextColor = Color.FromHex("#7F5539")
                                }.Also(b => b.Clicked += MuudaFoto_Clicked)
                            }
                        }
                    }
                },

                new TableSection("Lisavõimalused")
                {
                    new ViewCell
                    {
                        View = new HorizontalStackLayout
                        {
                            Spacing = 10,
                            Children =
                            {
                                new Button { Text = "HELISTA" }.Also(b => b.Clicked += Helista_Clicked),
                                new Button { Text = "SAADA SMS" }.Also(b => b.Clicked += Saada_sms_Clicked),
                                new Button { Text = "SAADA EMAIL" }.Also(b => b.Clicked += Saada_email_Clicked)
                            }
                        }
                    }
                }
            }
        };

        vsl.Children.Insert(0, table);

        Content = vsl;
    }

    private void Helista_Clicked(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(telefon.Text))
            PhoneDialer.Open(telefon.Text);
    }

    private async void Saada_sms_Clicked(object? sender, EventArgs e)
    {
        if (!Sms.Default.IsComposeSupported)
        {
            await DisplayAlert("Viga", "SMS ei ole toetatud", "OK");
            return;
        }

        SmsMessage sms = new SmsMessage(sonum.Text, telefon.Text);
        await Sms.Default.ComposeAsync(sms);
    }

    private async void Saada_email_Clicked(object? sender, EventArgs e)
    {
        if (!Email.Default.IsComposeSupported)
        {
            await DisplayAlert("Viga", "E-maili saatmine pole toetatud", "OK");
            return;
        }

        EmailMessage email_msg = new EmailMessage
        {
            Subject = "Sõbra kontakt",
            Body = sonum.Text,
            To = new List<string> { email_phone.Text }
        };

        await Email.Default.ComposeAsync(email_msg);
    }

    private async void MuudaFoto_Clicked(object? sender, EventArgs e)
    {
        var valik = await DisplayActionSheet("Vali foto", "Tühista", null, "Kaamera", "Galerii");

        FileResult? pilt = null;

        if (valik == "Kaamera")
            pilt = await MediaPicker.CapturePhotoAsync();
        else if (valik == "Galerii")
            pilt = await MediaPicker.PickPhotoAsync();

        if (pilt != null)
        {
            var stream = await pilt.OpenReadAsync();
            foto.Source = ImageSource.FromStream(() => stream);
        }
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
            Navigation.PushAsync(new TableView_Page());
        else if (nupp.ZIndex == 1)
            Navigation.PopToRootAsync();
        else if (nupp.ZIndex == 2)
            Navigation.PushAsync(new List_Page());
    }
}
public static class Ext
{
    public static T Also<T>(this T obj, Action<T> act)
    {
        act(obj);
        return obj;
    }
}