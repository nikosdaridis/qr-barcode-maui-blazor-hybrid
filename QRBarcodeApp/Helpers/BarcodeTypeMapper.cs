using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeApp.Helpers
{
    public static class BarcodeTypeMapper
    {
        public static BarcodeType MapScanFormatToBarcodeType(string? scanFormat)
        {
            return scanFormat switch
            {
                "Code128" => BarcodeType.Code128,
                "Code39" => BarcodeType.Code39,
                "Code93" => BarcodeType.Code93,
                "CodaBar" => BarcodeType.Codabar,
                "Ean13" => BarcodeType.Ean13,
                "Ean8" => BarcodeType.Ean8,
                "Upca" => BarcodeType.UpcA,
                "Upce" => BarcodeType.UpcE,
                "Itf" or "I2OF5" => BarcodeType.Interleaved2Of5,
                _ => BarcodeType.Unspecified
            };
        }
    }
}
