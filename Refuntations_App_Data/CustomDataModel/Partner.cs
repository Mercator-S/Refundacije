namespace Refuntations_App_Data.CustomDataModel
{
    public class Partner
    {
        public int? id_Partner { get; set; }
        public string? partner_Name { get; set; }
        public Partner(int? id_partner, string? partner_name)
        {
            id_Partner = id_partner;
            partner_Name = partner_name;
        }
    }
}
