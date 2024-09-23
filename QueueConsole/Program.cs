using QueueConsole;
using System.Globalization;

Console.WriteLine("asasa");

#region Renderizador csv

const string filePath = @"C:\sources\fila\QueueExample";

var csvFile = Directory.GetFiles(filePath, "*.csv");

using var canToken = new CancellationTokenSource();

try
{
    var linesCsv = await File.ReadAllLinesAsync(csvFile[0], canToken.Token);
    var header = linesCsv[0];
    var contentLines = linesCsv.Skip(1);
    var csvExampleModelList = new List<CsvExampleModel>();
    foreach(var contentLine in contentLines)
    {
        var line = contentLine.Split(',');
        decimal.TryParse(line[0], out var quotaAmount);
        DateOnly.TryParseExact(line[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate);
        csvExampleModelList.Add(new CsvExampleModel()
        {
            QuotaAmount = quotaAmount,
            StartDate = startDate,
            OwnerName = line[2],
            Username = line[3]
        });
    }

}
catch (Exception)
{
    await canToken.CancelAsync();
}
#endregion