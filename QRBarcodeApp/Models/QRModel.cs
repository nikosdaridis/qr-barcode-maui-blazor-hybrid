namespace QRBarcodeApp.Models
{
    public class QRModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
        public string? Format { get; set; }
        public string? Source { get; set; }
        public bool Favorite { get; set; }
    }
}
