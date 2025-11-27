using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Car_Dealership_Razor_Pages.Pages
{
    public class CarDealershipModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Customer Name can't be left empty!")]
        public string CusName { get; set; } //store customer's name

        [BindProperty]
        [Required(ErrorMessage = "Contact number can't be left empty!")]
        public string ContactNumber { get; set; }   //store customer's phone number

        [BindProperty]
        public string CarModel { get; set; }    //store car model selected

        [BindProperty]
        public bool ExtendedWarranty { get; set; }  //extended warranty checkbox option

        [BindProperty]
        public bool FCInsurance { get; set; }   //full coverage insurance checkbox option

        [BindProperty]
        public bool SportPackage { get; set; }  //sport package checkbox option

        [BindProperty]
        public DateTime CollectionDate { get; set; } = System.DateTime.Now; //Store collection date & time

        [BindProperty]
        public string Delivery { get; set; }

        public string[] DeliveryMethods = { "Self-Collection", "Home-Delivery" };

        public string CustomerInfo { get; set; }    //final customer information
        public string OrderInfo { get; set; }   //final order information
        public float TotalCost { get; set; }    //calculated final cost
        public string CurrencySymbol { get; set; }  //Currency symbols

        public string DateErrorPrompt;
        public string HideTable = "hidden";

        public string GetCusInfo()
        {
            return (CusName + ", " + ContactNumber);    //Customer's name and contact number combined
        }

        //calculate and return cost according to carmodel
        public float GetCarCost()
        {
            float Cost = 0.00f;
            if (CarModel == "BMW 325i")
            {
                Cost += 156000f;
            }
            else if (CarModel == "Mercedes-AMG A 35 4MATIC")
            {
                Cost += 356000f;
            }
            else if (CarModel == "McLaren P1 (Limited Edition)")
            {
                Cost += 1700000f;
            }
            return Cost;
        }

        //calculate and return service type & cost if any is selected
        public float GetServiceCost()
        {
            float ServiceCost = 0.00f;
            if (ExtendedWarranty)
            {
                OrderInfo += ", Extended Warranty";
                ServiceCost += 4000f;
            }

            if (FCInsurance)
            {
                OrderInfo += ", Full Coverage Insurance";
                ServiceCost += (GetCarCost() * 0.1f);
            }

            if (SportPackage)
            {
                OrderInfo += ", Sport Package";
                ServiceCost += 5700f;
            }

            return ServiceCost;
        }

        //check whether date selected is more than 2 days from current date
        public bool CheckDate()
        {
            if (CollectionDate <= System.DateTime.Now.AddDays(2) && Delivery == "Self-Collection")
            {
                DateErrorPrompt = "CollectionDate must be at least 3 days after purchase date!";
                return true;
            }
            else
            {
                DateErrorPrompt = null;
                return false;
            }
        }

        //button for "Display cost in USD along with all other info"
        public IActionResult OnPostUSD()
        {
            if (ModelState.IsValid)
            {
                if (CheckDate())
                    return Page();
                CustomerInfo = GetCusInfo();
                OrderInfo = CarModel;
                TotalCost = (GetCarCost() + GetServiceCost()) * 1.00f;
                CurrencySymbol = "$ ";
                UnhideTable();
                return Page();
            }
            else
            {
                return Page();
            }
        }

        //button for "Display cost in SGD along with all other info"
        public IActionResult OnPostSGD()
        {
            if (ModelState.IsValid)
            {
                if (CheckDate())
                    return Page();
                CustomerInfo = GetCusInfo();
                OrderInfo = CarModel;
                TotalCost = (GetCarCost() + GetServiceCost()) * 1.33f;
                CurrencySymbol = "S$ ";
                UnhideTable();
                return Page();
            }
            else
            {
                return Page();
            }
        }

        //button for "Display cost in MYR along with all other info"
        public IActionResult OnPostMYR()
        {
            if (ModelState.IsValid)
            {
                if (CheckDate())
                    return Page();
                CheckDate();
                CustomerInfo = GetCusInfo();
                OrderInfo = CarModel;
                TotalCost = (GetCarCost() + GetServiceCost()) * 4.65f;
                CurrencySymbol = "RM ";
                UnhideTable();
                return Page();
            }
            else
            {
                return Page();
            }
        }

        //Unhide table
        public void UnhideTable()
        {
            HideTable = "";
        }
    }
}
