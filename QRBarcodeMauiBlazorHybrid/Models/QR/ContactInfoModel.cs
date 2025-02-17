﻿using QRBarcodeMauiBlazorHybrid.Common.Attributes;
using QRBarcodeMauiBlazorHybrid.Common.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QRBarcodeMauiBlazorHybrid.Models.QR
{
    public class ContactInfoModel : IGenerateModel
    {
        [Required(ErrorMessage = "Name is required")]
        [InputType(InputType.Text)]
        public string? Name { get; set; }

        [InputType(InputType.Text)]
        public string? Company { get; set; }

        [InputType(InputType.Text)]
        public string? Title { get; set; }

        [InputType(InputType.Text)]
        public string? Address { get; set; }

        [RegularExpression("^\\d+$", ErrorMessage = "Invalid Phone")]
        [InputType(InputType.Number)]
        public ulong? Phone { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email")]
        [InputType(InputType.Text)]
        public string? Email { get; set; }

        [RegularExpression(@"^(?i)(?:(?:http|https):\/\/|www\.)?[\w-]+(?:\.[\w-]+)+", ErrorMessage = "Invalid Website")]
        [InputType(InputType.Text)]
        public string? Website { get; set; }

        public string GetValue()
        {
            StringBuilder? vCard = new StringBuilder();
            vCard.AppendLine("BEGIN:VCARD");
            vCard.AppendLine("VERSION:3.0");
            vCard.AppendLine($"FN:{Name}");
            if (!string.IsNullOrWhiteSpace(Company))
                vCard.AppendLine($"ORG:{Company}");
            if (!string.IsNullOrWhiteSpace(Title))
                vCard.AppendLine($"TITLE:{Title}");
            if (!string.IsNullOrWhiteSpace(Address))
                vCard.AppendLine($"ADR:{Address}");
            if (!string.IsNullOrWhiteSpace(Phone.ToString()))
                vCard.AppendLine($"TEL:{Phone}");
            if (!string.IsNullOrWhiteSpace(Email))
                vCard.AppendLine($"EMAIL:{Email}");
            if (!string.IsNullOrWhiteSpace(Website))
                vCard.AppendLine($"URL:{Website}");
            vCard.AppendLine("END:VCARD");

            return vCard.ToString();
        }
    }
}
