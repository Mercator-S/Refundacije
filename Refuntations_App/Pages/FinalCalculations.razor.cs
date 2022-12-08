using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Refuntations_App.Pages
{
    public class FinalCalculationsBase : ComponentBase
    {

        public List<Element> selectedItems1 = new List<Element>();
        public bool isLoading = true;
        public string searchString = "";
        public IEnumerable<Element> Elements = new List<Element>();
        public int selectedRowNumber { get; set; } = -1;
        public MudTable<Element> mudTable { get; set; }
        public List<string> clickedEvents { get; set; } = new();
        public string[] Columns = { "Element Id", "Element Name", "Element Number" };


        protected override void OnInitialized()
        {
            Element e = new Element();
            e.elementName = "First element";
            e.ElementId = 0;
            e.Number = 11111;
            Element e2 = new Element();
            e2.elementName = "Second element";
            e2.ElementId = 1;
            e2.Number = 22222;
            Element e3 = new Element();
            e3.elementName = "Third element";
            e3.ElementId = 2;
            e3.Number = 33333;
            Element e4 = new Element();
            e4.elementName = "Fourth element";
            e4.ElementId = 3;
            e4.Number = 44444;
            Elements = Elements.Append(e);
            Elements = Elements.Append(e2);
            Elements = Elements.Append(e3);
            Elements = Elements.Append(e4);
            isLoading = false;
        }
        public class Element
        {
            public long ElementId { get; set; }
            public int Number { get; set; }
            public string elementName { get; set; }


        }

         public void ElementChangedHandler(TableRowClickEventArgs<Object> e)
        {
            Element v =(Element) e.Row.Item;
          
            //this should be usesd as info about which object is targeted for operations such is deleting, details etc.



        }
        /* public bool FilterFunc(Element element) => FilterFuncWithString(element, searchString);

          public bool FilterFuncWithString(Element element, string searchString)
          {
              if (string.IsNullOrWhiteSpace(searchString))
                  return true;
              if (element.elementName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                  return true;
              if (element.Number.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                  return true;
              return false;
          }
          public void RowClickEvent(TableRowClickEventArgs<Element> tableRowClickEventArgs)
          {
              clickedEvents.Add("Row has been clicked");
          }

          public string SelectedRowClassFunc(Element element, int rowNumber)
          {
              if (selectedRowNumber == rowNumber)
              {
                  selectedRowNumber = -1;
                  clickedEvents.Add("Selected Row: None");
                  return string.Empty;
              }
              else if (mudTable.SelectedItem != null && mudTable.SelectedItem.Equals(element))
              {
                  selectedRowNumber = rowNumber;
                  clickedEvents.Add($"Selected Row: {rowNumber}");
                  return "selected";
              }
              else
              {
                  return string.Empty;
              }
          }*/
    }
}
