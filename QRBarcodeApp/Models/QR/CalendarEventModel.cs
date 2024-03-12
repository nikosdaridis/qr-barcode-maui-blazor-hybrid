using QRBarcodeApp.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QRBarcodeApp.Models.QR
{
    public class CalendarEventModel : IGenerateModel
    {
        [Required]
        public string? Summary { get; set; }

        [Required]
        public DateTime Start { get; set; } = DateTime.Now;

        [Required]
        public DateTime End { get; set; } = DateTime.Now;

        public string? Location { get; set; }

        public string? Description { get; set; }

        public string GetValue()
        {
            string? startString = Start.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z");
            string? endString = End.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z");

            StringBuilder? iCal = new StringBuilder();
            iCal.AppendLine("BEGIN:VCALENDAR");
            iCal.AppendLine("VERSION:2.0");
            iCal.AppendLine("BEGIN:VEVENT");
            iCal.AppendLine($"DTSTART:{startString}");
            iCal.AppendLine($"DTEND:{endString}");
            iCal.AppendLine($"SUMMARY:{Summary}");
            if (!string.IsNullOrWhiteSpace(Location))
            {
                iCal.AppendLine($"LOCATION:{Location}");
            }
            if (!string.IsNullOrWhiteSpace(Description))
            {
                iCal.AppendLine($"DESCRIPTION:{Description}");
            }
            iCal.AppendLine("END:VEVENT");
            iCal.AppendLine("END:VCALENDAR");

            return iCal.ToString();
        }
    }
}
