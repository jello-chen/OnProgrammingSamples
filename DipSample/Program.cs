namespace DipSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Document document = new Document();
            Exporter exporter = new CSVExporter();
            ExportController exportController = new ExportController(exporter);
            exportController.RunExport(document);
        }
    }
}
