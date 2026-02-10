using System.Threading.Tasks;

namespace Naidis_Osa1;

public partial class StartPage : ContentPage
{
	VerticalStackLayout vst;
	ScrollView sv;
	public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(0), new FigurePage() };
	public List<string> LeheNimed = new List<string>() { "Tekst", "Kujund" };
	public StartPage()
	{
		//InitializeComponent();
		Title = "Avaleht";
		vst = new VerticalStackLayout { Padding=20, Spacing=15 };
		for (int i=0; i < Lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = LeheNimed[i],
				FontSize = 25,
				BackgroundColor = Colors.LightBlue,
				FontFamily = "LowerWestSide400",
				TextColor = Colors.Black,
				CornerRadius = 10,
				HeightRequest = 50,
				ZIndex = i
			};
			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = Lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
		}
		sv = new ScrollView { Content = vst };
		Content = sv;
	}
}