using System;

namespace Gardener.Client.Base.PrintingJs
{
    /// <summary>
    /// Adapts the <see cref="PrintOptions"/> to the JavaScript version.
    /// </summary>
    public record PrintOptionsAdapter
    {
        public string? Printable { get; init; }
        public string Type { get; init; }
        public bool ShowModal { get; init; }
        public string ModalMessage { get; init; } = "Retrieving Document...";
        public bool? Base64 { get; set; }
        public string TargetStyles { get; set; } = "['*']";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PrintOptionsAdapter(PrintOptions options)
        {
            Printable = options.Printable;
            Type = options.Type.ToPrintJsString() ?? string.Empty;
            ShowModal = options.ShowModal;
            ModalMessage = options.ModalMessage;
            Base64 = options.Base64 == true ? true : null;
        }
    }
}
