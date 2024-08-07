﻿using BarcodeType = BarcodeStandard.Type;

namespace QRBarcodeApp.Helpers
{
    public static class BarcodeTypeMapper
    {
        /// <summary>
        /// Converts code format to BarcodeType
        /// </summary>
        public static BarcodeType ToBarcodeType(string format) =>
            format switch
            {
                "Code128" => BarcodeType.Code128,
                "Code39" => BarcodeType.Code39,
                "Code93" => BarcodeType.Code93,
                "CodaBar" => BarcodeType.Codabar,
                "Ean13" => BarcodeType.Ean13,
                "Ean8" => BarcodeType.Ean8,
                "Itf" => BarcodeType.Interleaved2Of5,
                "Upca" => BarcodeType.UpcA,
                "Upce" => BarcodeType.UpcE,
                "I2OF5" => BarcodeType.Industrial2Of5,
                _ => BarcodeType.Unspecified
            };
    }
}
