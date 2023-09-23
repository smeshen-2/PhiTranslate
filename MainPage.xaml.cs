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
        string word = (Entry.Text ?? "").ToLower();
        if (senderB.Text == "To Pocerpish") ToPoc(word);
        if (senderB.Text == "To English") ToEn(word);
    }
    private void ToPoc(string word)
    {
        word = word.Replace("remember", "rember 😁").Replace("forget", "forgor 💀");
        try
        {
            Output.Text = App.pocerpish[App.english.FindIndex(w => w == word)];
        }
        catch
        {
            Output.Text = "Unknown word";
        }
    }
    private void ToEn(string word)
    {
        try
        {
            Output.Text = App.english[App.pocerpish.FindIndex(w => w == word)];
        }
        catch
        {
            Output.Text = "Unknown word";
        }
    }
}

