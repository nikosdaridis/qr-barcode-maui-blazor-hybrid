using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class CalendarEventModel : IGenerateModel
    {
        [Required(ErrorMessage = "Summary is required")]
        [InputType(InputType.Text)]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "Start is required")]
        [InputType(InputType.DateTime)]
        public DateTime Start { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "End is required")]
        [InputType(InputType.DateTime)]
        public DateTime End { get; set; } = DateTime.Now;

        [InputType(InputType.Text)]
        public string? Location { get; set; }

        [InputType(InputType.Text)]
        public string? Description { get; set; }

        public string GetValue()
        {
            string startString = Start.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z");
            string endString = End.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z");

            StringBuilder? vCal = new StringBuilder();
            vCal.AppendLine("BEGIN:VCALENDAR");
            vCal.AppendLine("VERSION:2.0");
            vCal.AppendLine("BEGIN:VEVENT");
            vCal.AppendLine($"DTSTART:{startString}");
            vCal.AppendLine($"DTEND:{endString}");
            vCal.AppendLine($"SUMMARY:{Summary}");
            if (!string.IsNullOrWhiteSpace(Location))
                vCal.AppendLine($"LOCATION:{Location}");
            if (!string.IsNullOrWhiteSpace(Description))
                vCal.AppendLine($"DESCRIPTION:{Description}");
            vCal.AppendLine("END:VEVENT");
            vCal.AppendLine("END:VCALENDAR");

            return vCal.ToString();
        }
    }
}
