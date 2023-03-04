using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SobKichuBookStore.Models
{
    public class Category
    {
        [Key]  
        public int Id { get; set; }



        [Required(ErrorMessage ="Enter Category Name")]
        [DisplayName("Enter Category Name")]
        public string Name { get; set; }    


        [DisplayName("Number of total order")]
        [Range(0,100,ErrorMessage = "Display Order must be between 1 and 100 only!!")]
        public int DisplayOrder { get; set; }



        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}