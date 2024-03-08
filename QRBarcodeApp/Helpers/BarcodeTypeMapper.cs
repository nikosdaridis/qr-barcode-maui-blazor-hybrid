using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeApp.Helpers
{
    public class BarcodeTypeMapper
    {
        private static readonly Dictionary<string, BarcodeType> BarcodeMapping = new Dictionary<string, BarcodeType>
        {
            { "Code128", BarcodeType.Code128 },
            { "Code39", BarcodeType.Code39 },
            { "Code93", BarcodeType.Code93 },
            { "CodaBar", BarcodeType.Codabar },
            { "Ean13", BarcodeType.Ean13 },
            { "Ean8", BarcodeType.Ean8 },
            { "Itf", BarcodeType.Interleaved2Of5 },
            { "Upca", BarcodeType.UpcA },
            { "Upce", BarcodeType.UpcE },
            { "I2OF5", BarcodeType.Industrial2Of5 },
        };

        public static BarcodeType MapScanFormatToBarcodeType(string? scanFormat)
        {
            if (string.IsNullOrWhiteSpace(scanFormat))
                return BarcodeType.Unspecified;

            if (BarcodeMapping.TryGetValue(scanFormat, out BarcodeType barcodeType))
                return barcodeType;
            else
                return BarcodeType.Unspecified;
        }
    }
}
