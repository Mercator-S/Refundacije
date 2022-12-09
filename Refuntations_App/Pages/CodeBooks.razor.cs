using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using MudBlazor;
using Refundation_App_Services.Repositories;
using Refundation_App_Services.Services;
using Refuntations_App.Pages.Dialogs;
using Refuntations_App_Data.Model;
using System.ComponentModel;


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
        [Inject]
        public ICodeBookRepository codeBookRepository { get; set; }
        [Inject]
        public IFileLoader fileLoader { get; set; }


        public object TargetedElement { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            Columns = new List<string>();
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
                    break;
                case "Inostrani dobavljači":
                    Columns = new List<string>(
                       new string[] { "Šifra inostranog dobavljača", "Naziv inostranog dobavljača" });
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra_ino_dobavljac", "naziv_ino_dobavljac" });
                    ExcelTemplateLocation="./IMPORT - INOSTRANI DOBAVLJACI.xlsx";
                    break;
                case "Kategorija - Interni nalog - mesto troška":
                    Columns = new List<string>(
                        new string[] { "Šifra kategorije", "Naziv kategorije", "Interni nalog", "Mesto troška" });
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra_kat", "naziv_kat", "interni_nalog", "mesto_troska"});
                    ExcelTemplateLocation = "./IMPORT - KATEGORIJA - INTERNI NALOG - MESTO TROSKA.xlsx";
                    break;
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    Columns = new List<string>(
                         new string[] { "Šifra akcijske aktivnosti", "Naziv akcijske aktivnosti", "Iznos PDV-a", "SAP ključ", "Materijal"});
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra_aa", "naziv_aa", "PDV", "SAP_Kljuc", "Materijal"});
                    ExcelTemplateLocation = "./IMPORT - Akcijska aktivnost - PDV - SAP Kljuc - MATERIJAL - BANER.xlsx";
                    break;
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    Columns = new List<string>(
     new string[] { "Brojač konačni obračun", "SAP šifra dobavljača", "Broj knjižnog zaduženja", "SAP ključ", "Iznos", "Mesec", "Godina"});
                    ColumnsToShow = new List<string>(
                         new string[] { "brojac", "SAP_sifra_dobavljac", "br_knjiznog_zaduzenja", "SAP_kljuc", "iznos", "mesec", "godina" });
                    ExcelTemplateLocation = "./IMPORT - BROJAC KONACNI OBRACUN - SAP SIFRA - BR KNJIZNOG ZADUZENJA - SAP KLJUC - IZNOS.xlsx";
                    break;
                case "Dobavljači - Email adrese":
                    Columns = new List<string>(
     new string[] { "Šifra dobavljača", "SAP šifra dobavljača", "Naziv dobavljača", "Email"});
                    ColumnsToShow = new List<string>(
                         new string[] { "sifra", "sap_sifra", "naziv", "mail"});
                    ExcelTemplateLocation = "./IMPORT - INTERNI DOBAVLJACI - EMAIL ADRESE.xlsx";
                    break;

            }
            TargetedElement = null;
            StateHasChanged();
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
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true  };
            var parameters = new DialogParameters();
            parameters.Add("TargetEntity", TargetedElement);
            parameters.Add("SelectedValue", mudSelect.SelectedValues.First());
            var result = await DialogService.Show<DeleteCodeBookEntityDialog>("Da li ste sigurni?", parameters,options).Result;
            
            if (result.Data != null) {
                Elements = codeBookService.GetEntitiesAsync(SelectedValue).Result;
                TargetedElement = null;
                StateHasChanged();
            }

        }
        public async Task UploadFileAsync(IBrowserFile  file)
        {
            FileInfo? FileInfo = null;
            try
            {
                string filePath = "";
                filePath = file.Name;
                await using FileStream fs = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream().CopyToAsync(fs);
                
                FileInfo = new FileInfo(filePath);
                LoadAndSaveItems(FileInfo);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (FileInfo != null)
                    File.Delete(FileInfo.FullName);
            }

        }

        private async void LoadAndSaveItems(FileInfo fileInfo)
        {
            List<int> fails = new List<int>();
            var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var parameters = new DialogParameters();
            string error;
            DialogResult result;
            switch (SelectedValue)
            {
                case "Interni dobavljači":
                  List<InternalSupplier> internalSuppliers= fileLoader.loadInternalSuppliersFromExcel(fileInfo, out fails, out error);
                    parameters.Add("Fails", fails);
                    parameters.Add("Error", error);
                    result =await  DialogService.Show<MessageDialog>("", parameters, options).Result;
                    if (result != null && !result.Cancelled)
                    {
                       await codeBookRepository.AddInternalSuppliers(internalSuppliers);
                    }
                  
                    break;
                case "Inostrani dobavljači":
                    List<ForeignSupplier> foreignSuppliers = fileLoader.loadForeignSuppliersFromExcel(fileInfo, out fails, out error);
                    parameters.Add("Fails", fails);
                    result = await DialogService.Show<MessageDialog>("", parameters, options).Result;
                    if (result != null && !result.Cancelled)
                    {
                        await codeBookRepository.AddForeignSuppliers(foreignSuppliers);
                    }
                    break;
                case "Kategorija - Interni nalog - mesto troška":
                    List<CategoryInternalOrderCostLocation> categories = fileLoader.loadCategoryInternalOrderAndCostLocationFromExcel(fileInfo, out fails, out error);
                    parameters.Add("Fails", fails);
                    parameters.Add("Error", error);
                    result = await DialogService.Show<MessageDialog>("", parameters, options).Result;
                    if (result != null && !result.Cancelled)
                    {
                        await codeBookRepository.AddCategoryInternalOrderCostLocation(categories);
                    }
                    
                    break;
                case "Akcijska aktivnost - PDV - SAP ključ - Materijal":
                    List<AAPdvSAPKeyMaterial> activities = fileLoader.loadActitiviesWithPDVAndSAPKeyAndMaterialFromExcel(fileInfo, out fails, out error);
                    parameters.Add("Fails", fails);
                    parameters.Add("Error", error);
                    result = await DialogService.Show<MessageDialog>("", parameters, options).Result;
                    if (result != null && !result.Cancelled)
                    {
                        await codeBookRepository.AddAAPdvSAPKeyMaterials(activities);
                    }
                    break;
                case "Brojač - SAP šifra - Broj knjižnog zaduženja - SAP ključ - Iznos":
                    List<CounterSapIdSapKeyAmount> counters = fileLoader.loadCounterSAPIdAndAmountFromExcel(fileInfo, out fails, out error);
                    parameters.Add("Fails", fails);
                    parameters.Add("Error", error);
                    result = await DialogService.Show<MessageDialog>("", parameters, options).Result;
                    if (result != null && !result.Cancelled)
                    {
                        await codeBookRepository.AddCounterSAPIdAmount(counters);
                    }

                    break;
                case "Dobavljači - Email adrese":
                    List<EmailImport> mails = fileLoader.loadEmailsFromExcel(fileInfo, out fails, out error);
                    parameters.Add("Fails", fails);
                    parameters.Add("Error", error);
                    result = await DialogService.Show<MessageDialog>("", parameters, options).Result;
                    if (result != null && !result.Cancelled)
                    {
                        await codeBookRepository.AddEmails(mails);
                    }

                    break;
            }
          
      
            Elements = codeBookService.GetEntitiesAsync(SelectedValue).Result;
            StateHasChanged();
        }

        public void HandleRefreshPage()
        {
            Elements = codeBookService.GetEntitiesAsync(SelectedValue).Result;
            TargetedElement = null;
            StateHasChanged();
            
        }
    }
}
