using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication_temp.Models
{
    public class Category
    {
        // In this area we will be creating multiple properties... 

        // The properties we want to add here in Category table (this is a database btw)
        // are the columns properties of the table Category... 

        // using prop and pressing tab twice we can make a customisable property 

        // We have something called Data Annotation 

        [Key] // The Entity Framework Core will know that the first one which is ID is our primary key
        public int Id { get; set; } // This is our primary key 
        [Required] // Another data annotation to add required to any of the properties NOT NULL in SQL  

        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]

        [Range(1, 100, ErrorMessage = "Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
