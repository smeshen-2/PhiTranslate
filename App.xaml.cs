using ClosedXML.Excel;
using System.Net;
using System.Linq;

namespace PhiTranslate;

public partial class App : Microsoft.Maui.Controls.Application
{
    public static string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    static string filePath = Path.Join(appDataDirectory, "Pocerpish.xlsx");
    static string temporaryPath = Path.Join(appDataDirectory, "Temporary.xlsx");
    public static bool fileExists;

    public static List<string> pocerpish = new List<string>();
    public static List<string> english = new List<string>();

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();

        string link = "https://onedrive.live.com/download.aspx?resid=AF050EA4E751ECB!873&authkey=!AA0idZgO-e28Z_Y&ithint=file%2cxlsx&e=FPhvcp";

        try
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(link, temporaryPath);
            }
            File.Move(temporaryPath, filePath, true);
        }
        catch { }
        if (File.Exists(temporaryPath)) File.Delete(temporaryPath);
        fileExists = File.Exists(filePath);
        if (!fileExists) return;

        using (var workBook = new XLWorkbook(filePath))
        {
            var sheet = workBook.Worksheet("peresdous");
            english.AddRange(sheet.Column(3).CellsUsed().Select(c => c.GetText()).Skip(1));
            english.AddRange(sheet.Column(5).CellsUsed().Select(c => c.GetText()));
            english.AddRange(sheet.Column(7).CellsUsed().Select(c => c.GetText()));
            english.AddRange(sheet.Column(9).CellsUsed().Select(c => c.GetText()).Skip(1));
            english.AddRange(sheet.Column(11).CellsUsed().Select(c => c.GetText()));
            english.AddRange(sheet.Column(13).CellsUsed().Select(c => c.GetText()).Skip(1));
            english.AddRange(sheet.Column(15).CellsUsed().Select(c => c.GetText()));
            for (int i = 0; i < english.Count; i++)
            {
                english[i] = english[i].Replace(" ·", "").Replace(" +", "").Replace(" ~", "").Replace(" *", "");
            }

            pocerpish.AddRange(sheet.Column(4).CellsUsed().Select(c => c.GetText()));
            pocerpish.AddRange(sheet.Column(6).CellsUsed().Select(c => c.GetText()));
            pocerpish.AddRange(sheet.Column(8).CellsUsed().Select(c => c.GetText()));
            pocerpish.AddRange(sheet.Column(10).CellsUsed().Select(c => c.GetText()));
            pocerpish.AddRange(sheet.Column(12).CellsUsed().Select(c => c.GetText()));
            pocerpish.AddRange(sheet.Column(14).CellsUsed().Select(c => c.GetText()));
            pocerpish.AddRange(sheet.Column(16).CellsUsed().Select(c => c.GetText()));
        }
    }
}
