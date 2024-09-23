using QueueConsole;
using System.Globalization;

Console.WriteLine("asasa");

#region Renderizador csv



using var canToken = new CancellationTokenSource();

try
{
    var csvFile = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}csv", "*.csv")[0];
    Console.WriteLine($"Encontrado o CSV: {csvFile}");
    var linesCsv = await File.ReadAllLinesAsync(csvFile, canToken.Token);
    var header = linesCsv[0];
    var contentLines = linesCsv.Skip(1);
    var csvExampleModelList = new List<CsvExampleModel>();
    Console.WriteLine("Obtendo linhas do CSV");
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
    Console.WriteLine($"Finalizado, encontrado {csvExampleModelList.Count} linhas no csv {csvFile}");
    foreach(var csvLine in csvExampleModelList)
    {
        Console.WriteLine($"{csvLine.OwnerName}: {csvLine.QuotaAmount} | {csvLine.StartDate.ToShortDateString()}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
    await canToken.CancelAsync();
}
#endregion