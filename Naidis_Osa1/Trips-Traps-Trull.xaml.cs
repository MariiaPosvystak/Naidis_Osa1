using Microsoft.Maui.Graphics.Text;

namespace Naidis_Osa1;

public partial class Trips_Traps_Trull : ContentPage
{
    Label lbl, lblTurn;
    Grid grid;
    Button btnStart, btnWhoStarts, btnNewGame;
    HorizontalStackLayout hslNav;
    VerticalStackLayout vsl;
    ScrollView sv;

    string player1Name = "Mängija 1";
    string player2Name = "Mängija 2";
    string player1Symbol = "X";
    string player2Symbol = "O";

    bool isRobotPlaying = false;
    bool gameOver = false;
    bool isXTurn = true;

    List<string> navButtons = new() { "Tagasi", "Avaleht", "Edasi" };

    public Trips_Traps_Trull()
    {
        BackgroundColor = Color.FromHex("#D1CBC7");
        Title = "Trips Traps Trull";
        lbl = new Label
        {
            Text = "Trips Traps Trull",
            FontSize = 36,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#907564"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };

        lblTurn = new Label
        {
            FontSize = 22,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Color.FromHex("#907564")
        };

        btnStart = new Button
        {
            Text = "Alusta mäng",
            FontSize = 24,
            TextColor = Color.FromHex("#D1CBC7"),
            BackgroundColor = Color.FromHex("#907564"),
            HorizontalOptions = LayoutOptions.Center
        };
        btnStart.Clicked += Options;

        btnWhoStarts = new Button
        {
            Text = "Kes alustab?",
            TextColor = Color.FromHex("#D1CBC7"),
            BackgroundColor = Color.FromHex("#907564")
        };
        btnWhoStarts.Clicked += WhoStarts;

        btnNewGame = new Button
        {
            Text = "Uus mäng",
            TextColor = Color.FromHex("#D1CBC7"),
            BackgroundColor = Color.FromHex("#907564")
        };
        btnNewGame.Clicked += NewGame;
        grid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        hslNav = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };

        foreach (var name in navButtons)
        {
            Button b = new Button
            {
                Text = name,
                FontSize = 28,
                FontFamily = "LowerWestSide400",
                TextColor = Color.FromHex("#D1CBC7"),
                BackgroundColor = Color.FromHex("#907564"),
                CornerRadius = 10,
                HeightRequest = 50,
                ZIndex = navButtons.IndexOf(name)
            };
            b.Clicked += NavigationClick;
            hslNav.Add(b);
        }

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { lbl, btnStart, hslNav }
        };

        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private async void Options(object? sender, EventArgs e)
    {
        bool answer = await DisplayAlertAsync("Küsimus", "Kas mängid robotiga?", "Jah", "Ei");
        isRobotPlaying = answer;

        string? name1 = await DisplayPromptAsync("Mängija 1", "Mis on mängija 1 nimi?");
        if (string.IsNullOrWhiteSpace(name1)) name1 = "Mängija 1";
        player1Name = name1;

        if (!isRobotPlaying)
        {
            string? name2 = await DisplayPromptAsync("Mängija 2", "Mis on mängija 2 nimi?");
            if (string.IsNullOrWhiteSpace(name2)) name2 = "Mängija 2";
            player2Name = name2;
        }
        else player2Name = "Robot";

        SetupGrid();
        ResetGame();
    }
    private void SetupGrid()
    {
        grid.RowDefinitions.Clear();
        grid.ColumnDefinitions.Clear();
        grid.Children.Clear();

        for (int i = 0; i < 3; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Border frame = new Border
                {
                    BackgroundColor = Color.FromHex("#907564"),
                    Padding = 0,
                    MinimumHeightRequest = 70,
                    MinimumWidthRequest = 70
                };

                Label label = new Label
                {
                    Text = "",
                    FontSize = 40,
                    TextColor = Color.FromHex("#D1CBC7"),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                frame.Content = label;
                grid.Add(frame, c, r);

                var tap = new TapGestureRecognizer();
                tap.Tapped += CellTapped;
                frame.GestureRecognizers.Add(tap);
            }
        }

        vsl.Children.Clear();
        vsl.Children.Add(lbl);

        HorizontalStackLayout hslTop = new()
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            Children = { btnWhoStarts, btnNewGame, }
        };

        vsl.Children.Add(lblTurn);
        vsl.Children.Add(grid);
        vsl.Children.Add(hslTop);
        vsl.Children.Add(hslNav);
    }
    private async void CellTapped(object? sender, EventArgs e)
    {
        if (gameOver) return;
        if (sender is not Border frame) return;

        int row = Grid.GetRow(frame);
        int col = Grid.GetColumn(frame);

        await MakeMove(row, col);
    }

    private Label GetLabel(int row, int col)
    {
        foreach (var child in grid.Children)
        {
            if (child is Border f &&
                Grid.GetRow(f) == row &&
                Grid.GetColumn(f) == col &&
                f.Content is Label lbl)
                return lbl;
        }
        return null;
    }
    private async Task MakeMove(int row, int col)
    {
        var label = GetLabel(row, col);
        if (label == null || label.Text != "") return;

        string symbol = isXTurn ? player1Symbol : player2Symbol;
        string name = isXTurn ? player1Name : player2Name;

        label.Text = symbol;

        if (CheckWinner(symbol))
        {
            gameOver = true;
            await DisplayAlertAsync("Mäng läbi", $"{name} võitis!", "OK");
            return;
        }

        if (IsDraw())
        {
            gameOver = true;
            await DisplayAlertAsync("Mäng läbi", "Viik!", "OK");
            return;
        }

        isXTurn = !isXTurn;
        lblTurn.Text = $"Mängib {(isXTurn ? player1Name : player2Name)}";

        if (isRobotPlaying && !isXTurn)
            await SmartBotMove();
    }
    private async Task SmartBotMove()
    {
        await Task.Delay(300);

        if (TryToWinOrBlock(player2Symbol)) return;
        if (TryToWinOrBlock(player1Symbol)) return;

        var empty = GetEmptyCells();
        if (empty.Count > 0)
        {
            var rnd = new Random();
            var move = empty[rnd.Next(empty.Count)];
            await MakeMove(move.row, move.col);
        }
    }

    private bool TryToWinOrBlock(string symbol)
    {
        for (int i = 0; i < 3; i++)
        {
            if (TryLine(i, 0, i, 1, i, 2, symbol)) return true;
            if (TryLine(0, i, 1, i, 2, i, symbol)) return true;
        }

        if (TryLine(0, 0, 1, 1, 2, 2, symbol)) return true;
        if (TryLine(0, 2, 1, 1, 2, 0, symbol)) return true;

        return false;
    }

    private bool TryLine(int r1, int c1, int r2, int c2, int r3, int c3, string symbol)
    {
        var b1 = GetLabel(r1, c1);
        var b2 = GetLabel(r2, c2);
        var b3 = GetLabel(r3, c3);

        var line = new[] { b1, b2, b3 };
        int count = line.Count(l => l.Text == symbol);
        var empty = line.FirstOrDefault(l => l.Text == "");

        if (count == 2 && empty != null)
        {
            int row = Grid.GetRow(empty.Parent as Border);
            int col = Grid.GetColumn(empty.Parent as Border);
            _ = MakeMove(row, col);
            return true;
        }

        return false;
    }

    private List<(int row, int col)> GetEmptyCells()
    {
        var list = new List<(int, int)>();
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                if (GetLabel(r, c).Text == "")
                    list.Add((r, c));
        return list;
    }
    private bool CheckWinner(string symbol)
    {
        for (int i = 0; i < 3; i++)
        {
            if (Enumerable.Range(0, 3).All(j => GetLabel(i, j).Text == symbol)) return true;
            if (Enumerable.Range(0, 3).All(j => GetLabel(j, i).Text == symbol)) return true;
        }

        if (Enumerable.Range(0, 3).All(i => GetLabel(i, i).Text == symbol)) return true;
        if (Enumerable.Range(0, 3).All(i => GetLabel(i, 2 - i).Text == symbol)) return true;

        return false;
    }

    private bool IsDraw()
    {
        foreach (var child in grid.Children)
        {
            if (child is Border f && f.Content is Label lbl && lbl.Text == "")
                return false;
        }
        return true;
    }
    private void NewGame(object sender, EventArgs e)
    {
        ResetGame();
    }

    private void WhoStarts(object sender, EventArgs e)
    {
        Random rnd = new Random();
        isXTurn = rnd.Next(2) == 0;
        lblTurn.Text = $"{(isXTurn ? "X" : "O")} alustab!";
    }

    private void ResetGame()
    {
        foreach (var child in grid.Children)
        {
            if (child is Border f && f.Content is Label lbl)
                lbl.Text = "";
        }

        gameOver = false;
        isXTurn = true;
        lblTurn.Text = $"Mängib {player1Name}";
    }
    private void NavigationClick(object? sender, EventArgs e)
    {
        Button b = sender as Button;

        if (b.ZIndex == 0)
            Navigation.PushAsync(new PickerImagePage());
        else if (b.ZIndex == 1)
            Navigation.PopToRootAsync();
        else if (b.ZIndex == 2)
            Navigation.PushAsync(new TableView_Page());
    }
}
