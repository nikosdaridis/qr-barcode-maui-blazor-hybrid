﻿namespace QRBarcodeApp.Models
{
    public class QRModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Value { get; set; }
        public string? Type { get; set; }
        public string? Format { get; set; }
    }
}
