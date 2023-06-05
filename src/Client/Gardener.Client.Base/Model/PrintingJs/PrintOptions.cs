namespace Gardener.Client.Base.PrintingJs
{
    /// <summary>
    /// Options for a specific print.
    /// </summary>
    public record PrintOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public PrintOptions()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="printable"></param>
        public PrintOptions(string printable)
        {
            Printable = printable;
        }

        public PrintOptions(string printable, string modalMessage, PrintType printType = PrintType.Pdf)
        {
            Printable = printable;
            ModalMessage = modalMessage;
            ShowModal = true;
            Type = printType;
        }
        /// <summary>
        /// Document source: pdf url or base64.
        /// </summary>
        public string? Printable { get; init; }
        /// <summary>
        /// Printable type.
        /// </summary>
        public PrintType Type { get; init; }
        /// <summary>
        /// Enable this option to show user feedback when retrieving or processing large PDF files.
        /// </summary>
        public bool ShowModal { get; init; }
        /// <summary>
        /// Message displayed to users when <see cref="ShowModal"/> is set to true.
        /// </summary>
        public string ModalMessage { get; init; } = "Retrieving Document...";
        /// <summary>
        /// Used when printing PDF documents passed as base64 data.
        /// </summary>
        public bool Base64 { get; set; }
    }
}
