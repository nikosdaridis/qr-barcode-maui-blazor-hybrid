namespace QRBarcodeApp.Models
{
    /// <summary>
    /// Represents QR and Barcode
    /// </summary>
    public class CodeModel
    {
        /// <summary>
        /// Encoded Value
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Type (Text, Product, Url, ...)
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Format (QRCode, Upca, Code128, ...)
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Source (Scanned, Generated)
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// Favorite status
        /// </summary>
        public bool? Favorite { get; set; }
    }
}
