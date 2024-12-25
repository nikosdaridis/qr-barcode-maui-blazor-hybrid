namespace QRBarcodeMauiBlazorHybrid.Common.Attributes
{
    public enum InputType
    {
        Text,
        Number,
        DateTime
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class InputTypeAttribute(InputType type) : Attribute
    {
        public string Type { get; } = type switch
        {
            InputType.Text => "text",
            InputType.Number => "number",
            InputType.DateTime => "datetime-local",
            _ => "text"
        };
    }
}
