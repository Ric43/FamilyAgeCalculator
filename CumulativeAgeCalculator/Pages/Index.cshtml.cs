using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CumulativeAgeCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CumulativeAgeCalculator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AgeCalculatorService _ageCalculatorService;

        public decimal AgeInDays { get; set; }
        public decimal AgeInYears { get; set; }
        public bool ShowAge { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime FirstPersonDob { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime Child1Dob { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime Child2Dob { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime Child3Dob { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime Child4Dob { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime Child5Dob { get; set; }

        public IndexModel(AgeCalculatorService ageCalculatorService)
        {
            _ageCalculatorService = ageCalculatorService;
            ShowAge = false;

            if (FirstPersonDob == DateTime.MinValue)
            {
                FirstPersonDob = new DateTime(1946, 9, 14);
                Child1Dob = new DateTime(1973, 9, 18);
                Child2Dob = new DateTime(1975, 4, 7);
                Child3Dob = new DateTime(1976, 12, 19);
                Child4Dob = new DateTime(1983, 9, 2);
                Child5Dob = new DateTime(1983, 9, 2);
            }
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            List<DateTime> otherDobs = new List<DateTime>();
            otherDobs.Add(Child1Dob);
            otherDobs.Add(Child2Dob);
            otherDobs.Add(Child3Dob);
            otherDobs.Add(Child4Dob);
            otherDobs.Add(Child5Dob);

            decimal resultInDays = _ageCalculatorService.CalculateAge(FirstPersonDob, otherDobs);
            AgeInDays = resultInDays;
            AgeInYears = resultInDays / 365;
            ShowAge = true;
        }
    }
}
