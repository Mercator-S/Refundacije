using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using MudBlazor;
using Refundation_App_Services.Services;
using Refuntations_App.Data;
using Refuntations_App.Pages.Dialogs;
using Refuntations_App_Data.Model;
using System.Reflection.Metadata;
using static MudBlazor.CategoryTypes;

namespace Refuntations_App.Pages
{
    partial class CodeBooks
    {
        private List<string> Columns { get; set; } = null;
        private List<string> ColumnsToShow { get; set; } = null;
        private string SelectedValue { get; set; } = "";
        string ExcelTemplateLocation { get; set; } = null;
        private List<object> Elements { get; set; } = new List<object>();
        [Inject]
        public IDialogService DialogService { get; set; }
        public MudSelect<string> mudSelect { get; set; }
        [Inject]
        public ICodeBookService codeBookService { get; set; }


        public object TargetedElement { get;set; }

        protected override async Task OnInitializedAsync()
        {
            Columns = new List<string>();
            Columns.Add("First");
            Columns.Add("Second");
            Columns.Add("Third");
            Columns.Add("Fourth");
        }
        public void HandleSelectionChange(string selected)
        {
            SelectedValue = selected;
            Elements =  codeBookService.GetEntitiesAsync(selected).Result;
            switch(selected) {
                case "Interni dobavljači":
                    Columns = new List<string>(
                        new string[] { "Šifra internog dobavljača", "Naziv internog dobavljača" });
                    ColumnsToShow = new List<string>(
                         new string[] {"sifra_int_dobavljac", "naziv_int_dobavljac"});
                    ExcelTemplateLocation = "./IMPORT - INTERNI DOBAVLJACI.xlsx";
                    TargetedElement = null;
                    StateHasChanged();
                    break;
                case "Inostrani dobavljači":
                    Columns = new List<string>(
                       new string[] { "Šifra inostranog dobavljača", "Naziv inostranog dobavljača" });
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra_ino_dobavljac", "naziv_ino_dobavljac" });
                    ExcelTemplateLocation="./IMPORT - INOSTRANI DOBAVLJACI.xlsx";
                    TargetedElement = null;
                    StateHasChanged();
                    break;
                case "Kategorija - Interni nalog - mesto troška":
                    Columns = new List<string>(
                        new string[] { "Šifra kategorije", "Naziv kategorije", "Interni nalog", "Mesto troška" });
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra_kat", "naziv_kat", "interni_nalog", "mesto_troska"});
                    ExcelTemplateLocation = "./IMPORT - KATEGORIJA - INTERNI NALOG - MESTO TROSKA.xlsx";
                    TargetedElement = null;
                    StateHasChanged();
                    break;
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    Columns = new List<string>(
                         new string[] { "Šifra akcijske aktivnosti", "Naziv akcijske aktivnosti", "Iznos PDV-a", "SAP ključ", "Materijal"});
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra_aa", "naziv_aa", "PDV", "SAP_Kljuc", "Materijal"});
                    ExcelTemplateLocation = "./IMPORT - Akcijska aktivnost - PDV - SAP Kljuc - MATERIJAL - BANER.xlsx";
                    TargetedElement = null;
                    StateHasChanged();
                    break;
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    Columns = new List<string>(
     new string[] { "Brojač konačni obračun", "SAP šifra dobavljača", "Broj knjižnog zaduženja", "SAP ključ", "Iznos", "Mesec", "Godina"});
                    ColumnsToShow = new List<string>(
                         new string[] { "brojac", "SAP_sifra_dobavljac", "br_knjiznog_zaduzenja", "SAP_kljuc", "iznos", "mesec", "godina" });
                    ExcelTemplateLocation = "./IMPORT - BROJAC KONACNI OBRACUN - SAP SIFRA - BR KNJIZNOG ZADUZENJA - SAP KLJUC - IZNOS.xlsx";
                    TargetedElement = null;
                    StateHasChanged();
                    break;

            }
        }
        public bool IsImportAndExportAllowed()
        {
            return ExcelTemplateLocation == null;
        }
        public bool IsDeletingAllowed()
        {
            return TargetedElement == null;
        }
        public string GetDownloadPath()
        {
            return "/download?fileName=" + ExcelTemplateLocation;
        }
        public void ElementChangedHandler(TableRowClickEventArgs<Object> e)
        {
            TargetedElement = e.Row.Item;

        }

            public async void OpenDialog()
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true  };
            var parameters = new DialogParameters();
            parameters.Add("TargetEntity", TargetedElement);
            parameters.Add("SelectedValue", mudSelect.SelectedValues.First());
            var result = await DialogService.Show<DeleteCodeBookEntityDialog>("Da li ste sigurni?", parameters,options).Result;
            
            if (result.Data != null) {
                Elements = codeBookService.GetEntitiesAsync(SelectedValue).Result;
                StateHasChanged();
            }

        }
        public void UploadFile(IBrowserFile  file)
        {
            Console.WriteLine(file.LastModified);
        }
        public void HandleRefreshPage()
        {
            Elements = codeBookService.GetEntitiesAsync(SelectedValue).Result;
            StateHasChanged();
        }
    }
}
