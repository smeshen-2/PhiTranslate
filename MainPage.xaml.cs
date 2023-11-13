namespace PhiTranslate;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        if (!App.fileExists)
        {
            Output.Text = "No internet connection";
            return;
        }
        Button senderB = (Button)sender;
        string word = (Entry.Text ?? "").ToLower().Trim();
        try
        {
            if (senderB.Text == "To Pocerpish") ToPoc(word);
            if (senderB.Text == "To English") ToEn(word);
        }
        catch { Output.Text = "Unknown word"; }
    }
    private void ToPoc(string word)
    {
        word = word.Replace("remember", "rember 😁").Replace("forget", "forgor 💀");
        int item = Picker.SelectedIndex;
        switch (item)
        {
            case 1:
                Output.Text = App.pocerpishN[App.englishN.IndexOf(word)];
                return;
            case 2:
                Output.Text = App.pocerpishV[App.englishV.IndexOf(word)];
                return;
            case 3:
                Output.Text = App.pocerpishA[App.englishA.IndexOf(word)];
                return;
            case 4:
                Output.Text = App.pocerpishO[App.englishO.IndexOf(word)];
                return;
            case 0:
                if (App.englishN.Contains(word)) { Output.Text = App.pocerpishN[App.englishN.IndexOf(word)]; return; }
                if (App.englishV.Contains(word)) { Output.Text = App.pocerpishV[App.englishV.IndexOf(word)]; return; }
                if (App.englishA.Contains(word)) { Output.Text = App.pocerpishA[App.englishA.IndexOf(word)]; return; }
                if (App.englishO.Contains(word)) { Output.Text = App.pocerpishO[App.englishO.IndexOf(word)]; return; }
                throw new Exception();
        }
    }
    private void ToEn(string word)
    {
        int item = Picker.SelectedIndex;
        switch (item)
        {
            case 1:
                Output.Text = App.englishN[App.pocerpishN.IndexOf(word)];
                return;
            case 2:
                Output.Text = App.englishV[App.pocerpishV.IndexOf(word)];
                return;
            case 3:
                Output.Text = App.englishA[App.pocerpishA.IndexOf(word)];
                return;
            case 4:
                Output.Text = App.englishO[App.pocerpishO.IndexOf(word)];
                return;
            case 0:
                if (App.pocerpishN.Contains(word)) { Output.Text = App.englishN[App.pocerpishN.IndexOf(word)]; return; }
                if (App.pocerpishV.Contains(word)) { Output.Text = App.englishV[App.pocerpishV.IndexOf(word)]; return; }
                if (App.pocerpishA.Contains(word)) { Output.Text = App.englishA[App.pocerpishA.IndexOf(word)]; return; }
                if (App.pocerpishO.Contains(word)) { Output.Text = App.englishO[App.pocerpishO.IndexOf(word)]; return; }
                throw new Exception();
        }
    }
    private void Refresh_Clicked(object sender, EventArgs e)
    {
        App.Download();
        if (!App.fileExists) { Output.Text = "No internet connection"; return; }
        App.pocerpishN.Clear();
        App.pocerpishV.Clear();
        App.pocerpishA.Clear();
        App.pocerpishO.Clear();
        App.englishN.Clear();
        App.englishV.Clear();
        App.englishA.Clear();
        App.englishO.Clear();
        App.Define();
    }
}

