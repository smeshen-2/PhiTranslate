using ClosedXML.Excel;
using System.Net;

namespace PhiTranslate;

public partial class App : Microsoft.Maui.Controls.Application
{
    static string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    static string filePath = Path.Join(appDataDirectory, "Pocerpish.xlsx");
    static string temporaryPath = Path.Join(appDataDirectory, "Temporary.xlsx");
    public static bool fileExists = File.Exists(filePath);
    static string link = "https://onedrive.live.com/download.aspx?resid=AF050EA4E751ECB!873&authkey=!AA0idZgO-e28Z_Y&ithint=file%2cxlsx&e=FPhvcp";

    public static List<string> pocerpishN = new List<string>();
    public static List<string> pocerpishV = new List<string>();
    public static List<string> pocerpishA = new List<string>();
    public static List<string> pocerpishO = new List<string>();
    public static List<string> englishN = new List<string>();
    public static List<string> englishV = new List<string>();
    public static List<string> englishA = new List<string>();
    public static List<string> englishO = new List<string>();

    public static void Download()
    {
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
    }
    public static void Define()
    {
        using (var workBook = new XLWorkbook(filePath))
        {
            var sheet = workBook.Worksheet("peresdous");

            englishN.AddRange(sheet.Column(3).CellsUsed().Select(c => c.GetText()).Skip(1));
            englishN.AddRange(sheet.Column(5).CellsUsed().Select(c => c.GetText()));
            englishN.AddRange(sheet.Column(7).CellsUsed().Select(c => c.GetText()));

            englishV.AddRange(sheet.Column(9).CellsUsed().Select(c => c.GetText()).Skip(1));
            englishV.AddRange(sheet.Column(11).CellsUsed().Select(c => c.GetText()));

            englishA.AddRange(sheet.Column(13).CellsUsed().Select(c => c.GetText()).Skip(1));
            englishA.AddRange(sheet.Column(15).CellsUsed().Select(c => c.GetText()));

            englishO.AddRange(sheet.Column(18).CellsUsed().Where(c => !sheet.Cell(c.Address.RowNumber, c.Address.ColumnNumber + 1).IsEmpty()).Select(c => c.GetText()));

            pocerpishN.AddRange(sheet.Column(4).CellsUsed().Select(c => c.GetText()));
            pocerpishN.AddRange(sheet.Column(6).CellsUsed().Select(c => c.GetText()));
            pocerpishN.AddRange(sheet.Column(8).CellsUsed().Select(c => c.GetText()));

            pocerpishV.AddRange(sheet.Column(10).CellsUsed().Select(c => c.GetText()));
            pocerpishV.AddRange(sheet.Column(12).CellsUsed().Select(c => c.GetText()));

            pocerpishA.AddRange(sheet.Column(14).CellsUsed().Select(c => c.GetText()));
            pocerpishA.AddRange(sheet.Column(16).CellsUsed().Select(c => c.GetText()));

            pocerpishO.AddRange(sheet.Column(19).CellsUsed().Select(c => c.GetText()));

            for (int i = 0; i < englishN.Count; i++)
                englishN[i] = englishN[i].Replace(" ·", "").Replace(" +", "").Replace(" ~", "").Replace(" *", "");
            for (int i = 0; i < englishV.Count; i++)
                englishV[i] = englishV[i].Replace(" ·", "").Replace(" +", "").Replace(" ~", "").Replace(" *", "");
            for (int i = 0; i < englishA.Count; i++)
                englishA[i] = englishA[i].Replace(" ·", "").Replace(" +", "").Replace(" ~", "").Replace(" *", "");
            for (int i = 0; i < englishO.Count; i++)
                englishO[i] = englishO[i].Replace(" ·", "").Replace(" +", "").Replace(" ~", "").Replace(" *", "");
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();

        if (!fileExists)
        {
            Download();
            if (!fileExists) return;
        }
        fileExists = true;
        Define();
    }
}
