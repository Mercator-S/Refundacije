﻿using OfficeOpenXml;
using Refuntations_App_Data.Data;
using Refuntations_App_Data.Model;

namespace Refundation_App_Services.Services.Impl
{
    public class FileLoader : IFileLoader
    {
        public List<AAPdvSAPKeyMaterial> loadActitiviesWithPDVAndSAPKeyAndMaterialFromExcel(FileInfo fileInfo, out List<int> fails, out string error)
        {
            error = "";
            fails = new List<int>();
            List<AAPdvSAPKeyMaterial> res = new List<AAPdvSAPKeyMaterial>();
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        error = Constants.NO_WORKSHEETS;
                    }
                    else
                    {
                        int totalColumn = worksheet.Dimension.End.Column;
                        int totalRow = worksheet.Dimension.End.Row;
                        res = extractAAPdvSADKeyMaterial(worksheet, totalColumn, totalRow, out fails, out error);
                    }
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CategoryInternalOrderCostLocation> loadCategoryInternalOrderAndCostLocationFromExcel(FileInfo fileInfo, out List<int> fails, out string error)
        {
            error = "";
            fails = new List<int>();
            List<CategoryInternalOrderCostLocation> res = new List<CategoryInternalOrderCostLocation>();
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        error = Constants.NO_WORKSHEETS;
                    }
                    else
                    {
                        int totalColumn = worksheet.Dimension.End.Column;
                        int totalRow = worksheet.Dimension.End.Row;
                         res = extractCategories(worksheet, totalColumn, totalRow, out fails, out error);
                    }
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Email> loadEmailsFromExcel(FileInfo fileInfo, out List<int> fails, out string error)
        {
            error = "";
            fails = new List<int>();
            List<Email> res = new List<Email>();
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                         error = Constants.NO_WORKSHEETS;
                    }
                    else
                    {
                        int totalColumn = worksheet.Dimension.End.Column;
                        int totalRow = worksheet.Dimension.End.Row;
                         res = extractEmails(worksheet, totalColumn, totalRow, out fails, out error);

                    }
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<CounterSapIdSapKeyAmount> loadCounterSAPIdAndAmountFromExcel(FileInfo fileInfo, out List<int> fails, out string error)
        {
            error = "";
            fails = new List<int>();
            List<CounterSapIdSapKeyAmount> res = new List<CounterSapIdSapKeyAmount>();
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                         error = Constants.NO_WORKSHEETS;
                    }
                    else
                    {

                        int totalColumn = worksheet.Dimension.End.Column;
                        int totalRow = worksheet.Dimension.End.Row;
                        res = extractCounterSapIdAndAmount(worksheet, totalColumn, totalRow, out fails, out error);
                    }

                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<ForeignSupplier> loadForeignSuppliersFromExcel(FileInfo fileInfo, out List<int> fails, out string error)
        {
            error = "";
            fails = new List<int>();
            List<ForeignSupplier> res = new List<ForeignSupplier>();
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        error = Constants.NO_WORKSHEETS;
                    }
                    else
                    {
                        int totalColumn = worksheet.Dimension.End.Column;
                        int totalRow = worksheet.Dimension.End.Row;
                        res = extractForeignSuppliers(worksheet, totalColumn, totalRow, out fails, out error);
                    }
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

       
        public List<InternalSupplier> loadInternalSuppliersFromExcel(FileInfo fileInfo, out List<int> fails, out string error)
        {
            error = "";
            fails = new List<int>();
            List<InternalSupplier> res = new List<InternalSupplier>();
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        error = Constants.NO_WORKSHEETS;
                    }
                    else
                    {
                        int totalColumn = worksheet.Dimension.End.Column;
                        int totalRow = worksheet.Dimension.End.Row;
                        res = extractInternalSuppliers(worksheet, totalColumn, totalRow, out fails, out error);
                    }
              
                return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private List<InternalSupplier> extractInternalSuppliers(ExcelWorksheet worksheet, int totalColumn, int totalRow, out List<int> fails, out string error)
        {
            fails = new List<int>();
            error = "";
            List<InternalSupplier> items = new List<InternalSupplier>();
            if (totalColumn == 0 || totalRow <2)
            {
                error = Constants.EMPTY_DOCUMENT;
                
            }
            else if (totalColumn != 2)
            {
                error = Constants.UNAPPROPRIATE_FORMAT;
            }
            else
            {
                for (int row = 2; row <= totalRow; row++)
                {
                    try
                    {
                        InternalSupplier item = new InternalSupplier();
                        for (int column = 1; column <= totalColumn; column++)
                        {
                            switch (column)
                            {
                                //first column is Id and should be blank
                                case 1:
                                    item.sifra_int_dobavljac = Int16.Parse(worksheet.Cells[row, column].Value.ToString());
                                    break;
                                case 2:
                                    item.naziv_int_dobavljac = worksheet.Cells[row, column].Value.ToString();
                                    break;
                            }
                        }
                        item.active = true;
                        item.d_ins = DateTime.Now;
                        items.Add(item);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Row " + row + " is invalid");
                        fails.Add(row);
                    }

                }
            }
           
          
            return items;
        }
        private List<ForeignSupplier> extractForeignSuppliers(ExcelWorksheet worksheet, int totalColumn, int totalRow, out List<int> fails, out string error)
        {
            fails = new List<int>();
            error = "";
            List<ForeignSupplier> items = new List<ForeignSupplier>();
            if (totalColumn == 0 || totalRow < 2)
            {
                error = Constants.EMPTY_DOCUMENT;
            }
            else if (totalColumn != 2)
            {
                error = Constants.UNAPPROPRIATE_FORMAT;

            }
            else {
                for (int row = 2; row <= totalRow; row++)
                {
                    try
                    {
                        ForeignSupplier item = new ForeignSupplier();
                        for (int column = 1; column <= totalColumn; column++)
                        {
                            switch (column)
                            {
                                //first column is Id and should be blank
                                case 1:
                                    item.sifra_ino_dobavljac = Int16.Parse(worksheet.Cells[row, column].Value.ToString());
                                    break;
                                case 2:
                                    item.naziv_ino_dobavljac = worksheet.Cells[row, column].Value.ToString();
                                    break;
                            }
                        }
                        item.active = true;
                        item.d_ins = DateTime.Now;
                        items.Add(item);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Row " + row + " is invalid");
                        fails.Add(row);
                    }

                }
            }

           
            return items;
        }
        private List<CounterSapIdSapKeyAmount> extractCounterSapIdAndAmount(ExcelWorksheet worksheet, int totalColumn, int totalRow, out List<int> fails, out string error)
        {
            fails = new List<int>();
            error = "";
            if (totalColumn == 0 || totalRow <2)
            {
                error = Constants.EMPTY_DOCUMENT;
            }
            if (totalColumn != 7)
            {
                error = Constants.UNAPPROPRIATE_FORMAT;

            }
            List<CounterSapIdSapKeyAmount> items = new List<CounterSapIdSapKeyAmount>();
            for (int row = 2; row <= totalRow; row++)
            {
                try
                {
                    CounterSapIdSapKeyAmount item = new CounterSapIdSapKeyAmount();
                    for (int column = 1; column <= totalColumn; column++)
                    {
                        switch (column)
                        {
                            case 1:
                                item.brojac = Int64.Parse(worksheet.Cells[row, column].Value.ToString());
                                break;
                            case 2:
                                item.SAP_sifra_dobavljac = worksheet.Cells[row, column].Value.ToString();
                                break;
                            case 3:
                                item.br_knjiznog_zaduzenja = worksheet.Cells[row, column].Value.ToString();
                                break;
                            case 4:
                                item.SAP_kljuc = worksheet.Cells[row, column].Value.ToString();
                                break;
                            case 5:
                                item.iznos = Double.Parse(worksheet.Cells[row, column].Value.ToString());
                                break;
                            case 6:
                                item.mesec = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                                break;
                            case 7:
                                item.godina = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                                break;
                        }
                    }
                    item.active = true;
                    item.d_ins = DateTime.Now;
                    items.Add(item);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Row " + row + " is invalid");
                    fails.Add(row);
                }
               
            }
            return items;
        }
        private List<CategoryInternalOrderCostLocation> extractCategories(ExcelWorksheet worksheet, int totalColumn, int totalRow, out List<int> fails, out string error)
        {
            fails = new List<int>();
            error = "";
            List<CategoryInternalOrderCostLocation> items = new List<CategoryInternalOrderCostLocation>();
            if (totalColumn == 0 || totalRow == 0)
            {
                error = Constants.EMPTY_DOCUMENT;
            }
            else if (totalColumn != 4)
            {
                error = Constants.UNAPPROPRIATE_FORMAT;

            }
            else
            {
                for (int row = 2; row <= totalRow; row++)
                {
                    try
                    {
                        CategoryInternalOrderCostLocation item = new CategoryInternalOrderCostLocation();
                        for (int column = 1; column <= totalColumn; column++)
                        {
                            switch (column)
                            {
                                case 1:
                                    item.sifra_kat = worksheet.Cells[row, column].Value.ToString();
                                    break;
                                case 2:
                                    item.naziv_kat = worksheet.Cells[row, column].Value.ToString();
                                    break;
                                case 3:
                                    item.interni_nalog = worksheet.Cells[row, column].Value.ToString();
                                    break;
                                case 4:
                                    item.mesto_troska = worksheet.Cells[row, column].Value.ToString();
                                    break;
                            }
                        }
                        item.active = true;
                        item.d_ins = DateTime.Now;
                        items.Add(item);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Row " + row + " is invalid");
                        fails.Add(row);
                    }

                }
            }

           
            return items;
        }
        private List<AAPdvSAPKeyMaterial> extractAAPdvSADKeyMaterial(ExcelWorksheet worksheet, int totalColumn, int totalRow, out List<int> fails, out string error)
        {
            fails = new List<int>();
            error = "";
            List<AAPdvSAPKeyMaterial> items = new List<AAPdvSAPKeyMaterial>();
            if (totalColumn == 0 || totalRow == 0)
            {
                error = Constants.EMPTY_DOCUMENT;
            }
            else if (totalColumn != 5)
            {
                error = Constants.UNAPPROPRIATE_FORMAT;

            }
            else {
                for (int row = 2; row <= totalRow; row++)
                {
                    try
                    {
                        AAPdvSAPKeyMaterial item = new AAPdvSAPKeyMaterial();
                        for (int column = 1; column <= totalColumn; column++)
                        {
                            switch (column)
                            {
                                case 1:
                                    item.sifra_aa = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                                    break;
                                case 2:
                                    item.naziv_aa = worksheet.Cells[row, column].Value.ToString();
                                    break;
                                case 3:
                                    item.PDV = Decimal.Parse(worksheet.Cells[row, column].Value.ToString());
                                    break;
                                case 4:
                                    item.SAP_Kljuc = worksheet.Cells[row, column].Value.ToString();
                                    break;
                                case 5:
                                    item.Materijal = worksheet.Cells[row, column].Value.ToString();
                                    break;
                            }
                        }
                        item.active = true;
                        item.d_ins = DateTime.Now;
                        items.Add(item);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Row " + row + " is invalid");
                        fails.Add(row);
                    }

                }
            }
           

            return items;
        }
        private List<Email> extractEmails(ExcelWorksheet worksheet, int totalColumn, int totalRow, out List<int> fails, out string error)
        {
            fails = new List<int>();
            error = "";
            List<Email> items = new List<Email>();
            if (totalColumn == 0 || totalRow < 2)
            {
                error = Constants.EMPTY_DOCUMENT;
            }
            else if (totalColumn != 4)
            {
                error = Constants.UNAPPROPRIATE_FORMAT;

            }
            else {    
            for (int row = 2; row <= totalRow; row++)
            {
                try
                {
                    Email item = new Email();
                    for (int column = 1; column <= totalColumn; column++)
                    {
                        switch (column)
                        {
                            case 1:
                                item.sifra = Int32.Parse(worksheet.Cells[row, column].Value.ToString());
                                break;
                            case 2:
                                item.sap_sifra = worksheet.Cells[row, column].Value.ToString();
                                break;
                            case 3:
                                item.naziv = worksheet.Cells[row, column].Value.ToString();
                                break;
                            case 4:
                                item.mail = worksheet.Cells[row, column].Value.ToString();
                                break;
                        }
                    }
                    item.active = true;
                    item.d_ins = DateTime.Now;
                    items.Add(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Row " + row + " is invalid");
                    fails.Add(row);
                }

            }
            }
        
            return items;
        }

    

    }
}
