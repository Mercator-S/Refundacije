namespace Refuntations_App_Data.CustomModel
{
    public class YearAndMonth
    {
        public int Year { get; set; } 
        public int Month { get; set; }
        public YearAndMonth(int year, int month)
        {
            Year = year;
            Month = month;
        }
    }
}
