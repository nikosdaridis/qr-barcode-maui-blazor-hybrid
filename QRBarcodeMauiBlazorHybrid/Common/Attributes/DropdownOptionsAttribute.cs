namespace QRBarcodeMauiBlazorHybrid.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DropdownOptionsAttribute(params string[] options) : Attribute
    {
        public string[] Options { get; } = options;
    }
}
