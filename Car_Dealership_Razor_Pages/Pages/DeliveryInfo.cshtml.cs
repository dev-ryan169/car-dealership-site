using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Car_Dealership_Razor_Pages.Pages
{
    public class DeliveryInfoModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Name can't be empty!")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Contact Number can't be empty!")]
        public string ContactNo { get; set; }

        [BindProperty]
        public string Courier { get; set; }

        [BindProperty]
        public string ShippingOption { get; set; }

        [BindProperty]
        public string DeliveryDate { get; set; }

        [BindProperty]
        public string DeliverySlot { get; set; }

        [BindProperty]
        public List<string> AreChecked { get; set; }

        public string TableHidden = "hidden";

        public string[] CourierList = { "Premium Deluxe Delivery", "Cheap Stake Delivery", "X Delivery", "McLaren Delivery" };

        public string[] ShippingOptionList = { "Standard Shipping", "Insured Shipping", "Fully Armoured and Armed Shipping" };

        public string[] TimingList = { "9am-12pm", "12pm-3pm", "3pm-6pm", "6pm-9pm" };

        public int[] Sundays = { 3, 10, 17, 24, 31 };

        public string Prompt, Prompt1;

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (AreChecked == null)
                {
                    Prompt = "Please select your preferred delivery slots. Otherwise, slots will be allocated based on availability.";
                    return Page();
                }
                else
                {
                    Prompt1 = "Thank you. Below is your delivery information. Enjoy your ride soon!";
                    TableHidden = "";
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
