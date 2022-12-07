using OfficeOpenXml;
using Refuntations_App_Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Services.Impl
{
    public class FileLoader : IFileLoader
    {
        public List<AAPdvSAPKeyMaterial> loadActitiviesWithPDVAndSAPKeyAndMaterialFromExcel(FileInfo fileInfo)
        {
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        throw new Exception("Excel document has no worksheets!");
                    }

                    int totalColumn = worksheet.Dimension.End.Column;
                    int totalRow = worksheet.Dimension.End.Row;
                    List<AAPdvSAPKeyMaterial> res = extractAAPdvSADKeyMaterial(worksheet, totalColumn, totalRow);
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CategoryInternalOrderCostLocation> loadCategoryInternalOrderAndCostLocationFromExcel(FileInfo fileInfo)
        {
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        throw new Exception("Excel document has no worksheets!");
                    }

                    int totalColumn = worksheet.Dimension.End.Column;
                    int totalRow = worksheet.Dimension.End.Row;
                    List<CategoryInternalOrderCostLocation> res = extractCategories(worksheet, totalColumn, totalRow);
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<CounterSapIdSapKeyAmount> loadCounterSAPIdAndAmountFromExcel(FileInfo fileInfo)
        {
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        throw new Exception("Excel document has no worksheets!");
                    }

                    int totalColumn = worksheet.Dimension.End.Column;
                    int totalRow = worksheet.Dimension.End.Row;
                    List<CounterSapIdSapKeyAmount> res = extractCounterSapIdAndAmount(worksheet, totalColumn, totalRow);
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public List<ForeignSupplier> loadForeignSuppliersFromExcel(FileInfo fileInfo)
        {
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        throw new Exception("Excel document has no worksheets!");
                    }

                    int totalColumn = worksheet.Dimension.End.Column;
                    int totalRow = worksheet.Dimension.End.Row;
                    List<ForeignSupplier> res = extractForeignSuppliers(worksheet, totalColumn, totalRow);
                    return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

       
        public List<InternalSupplier> loadInternalSuppliersFromExcel(FileInfo fileInfo)
        {
            try
            {
                ExcelWorksheet worksheet = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                {
                    worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        throw new Exception("Excel document has no worksheets!");
                    }
              
                int totalColumn = worksheet.Dimension.End.Column;
                int totalRow = worksheet.Dimension.End.Row;
                List<InternalSupplier> res = extractInternalSuppliers(worksheet, totalColumn, totalRow);
                return res;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private List<InternalSupplier> extractInternalSuppliers(ExcelWorksheet worksheet, int totalColumn, int totalRow)
        {
            if (totalColumn == 0 || totalRow == 0)
            {
                throw new Exception("Excel document is empty!");
            }
            if (totalColumn != 2)
            {
                throw new Exception("Excel document is of unappropriate format!");
            }
            List<InternalSupplier> items = new List<InternalSupplier>();
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
                    item.active=true;
                    item.d_ins=DateTime.Now;
                    items.Add(item);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Row "+row+" is invalid");
                }
               
            }
            return items;
        }
        private List<ForeignSupplier> extractForeignSuppliers(ExcelWorksheet worksheet, int totalColumn, int totalRow)
        {
            if (totalColumn == 0 || totalRow == 0)
            {
                throw new Exception("Excel document is empty!");
            }
            if (totalColumn != 2)
            {
                throw new Exception("Excel document is of unappropriate format!");
            }
            List<ForeignSupplier> items = new List<ForeignSupplier>();
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
                catch(Exception e)
                {
                    Console.WriteLine("Row " + row + " is invalid");
                }
          
            }
            return items;
        }
        private List<CounterSapIdSapKeyAmount> extractCounterSapIdAndAmount(ExcelWorksheet worksheet, int totalColumn, int totalRow)
        {
            if (totalColumn == 0 || totalRow == 0)
            {
                throw new Exception("Excel document is empty!");
            }
            if (totalColumn != 7)
            {
                throw new Exception("Excel document is of unappropriate format!");
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
                }
               
            }
            return items;
        }
        private List<CategoryInternalOrderCostLocation> extractCategories(ExcelWorksheet worksheet, int totalColumn, int totalRow)
        {
            if (totalColumn == 0 || totalRow == 0)
            {
                throw new Exception("Excel document is empty!");
            }
            if (totalColumn != 4)
            {
                throw new Exception("Excel document is of unappropriate format!");
            }
            List<CategoryInternalOrderCostLocation> items = new List<CategoryInternalOrderCostLocation>();
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
                }

            }
            return items;
        }
        private List<AAPdvSAPKeyMaterial> extractAAPdvSADKeyMaterial(ExcelWorksheet worksheet, int totalColumn, int totalRow)
        {
            if (totalColumn == 0 || totalRow == 0)
            {
                throw new Exception("Excel document is empty!");
            }
            if (totalColumn != 5)
            {
                throw new Exception("Excel document is of unappropriate format!");
            }
            List<AAPdvSAPKeyMaterial> items = new List<AAPdvSAPKeyMaterial>();
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
                }

            }
            return items;
        }

    }
}
