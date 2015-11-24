using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1_BookStore.Models
{
    public class BookStoreModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage="You must enter a book title.")]
        [StringLength(50, MinimumLength=5, ErrorMessage="Title should be between 5 and 50 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage="You must enter an Author's name.")]
        [StringLength(30, MinimumLength=8, ErrorMessage="Authors name should be between 8 and 30 characters long.")]
        public string Author { get; set; }

        [Required(ErrorMessage="Enter the date in this format: MM/DD/YYYY")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/(0[1-9]|1\d|2\d|3[01])\/\d{4}$", 
            ErrorMessage = "Enter the date in this format: MM/DD/YYYY")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name="Published Date")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Value of book has to be $.50 or greater.")]
        [Range(.50,1000000, ErrorMessage="Cost can range from $.50 to $1,000,000.")]
        [DataType(DataType.Currency)]
        public double Cost { get; set; }

        [Display(Name = "In Stock")]
        public bool InStock { get; set; }

        [Required(ErrorMessage = "Please include a binding type of either 'Soft Back' or 'Hard Back'.")]
        [Display(Name = "Binding Type")]
        public string BindingType {get; set;}

       
        public enum Binding
        {
            SoftBack, HardBack
        }


        //public (not sure what type to place here for pic) Cover {get; set;}
    }
}