namespace Refuntations_App_Data.Model
{
    public class FilterModel
    {
        public string? partnerName { get; set; }
        public string? categoryName { get; set; }
        public int? codeAa { get; set; }
        public DateTime? periodOf { get; set; }
        public DateTime? periodTo { get; set; }
        public bool PDV10 { get; set; }
        public bool PDV20 { get; set; }
        public string? Status { get; set; }

    }
}
