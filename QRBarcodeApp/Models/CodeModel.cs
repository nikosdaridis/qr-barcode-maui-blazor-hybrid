namespace QRBarcodeApp.Models
{
    public class CodeModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Value { get; set; }
        public string? Type { get; set; }
        public string? Format { get; set; }
        public string? Source { get; set; }
        public bool? Favorite { get; set; }
    }
}
