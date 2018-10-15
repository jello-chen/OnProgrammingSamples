namespace DipSample
{
    class ExportController
    {
        private readonly Exporter exporter;

        public ExportController(Exporter exporter)
        {
            this.exporter = exporter;
        }

        public void RunExport(Document document)
        {
            string exportedString = this.exporter.ConvertDocumentToString(document);
            string exportedFilePath = GetSaveFilePath();
            WriteStringToFile(exportedString, exportedFilePath);
        }

        private string GetSaveFilePath() => null;

        private void WriteStringToFile(string exportedString, string exportedFilePath)
        {

        }
    }
}
