using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBookWeb.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Range(1,1000,ErrorMessage = "Price cannot be lower than 0 or higher than 1000")]
        public float? Price { get; set; }
        
        [ForeignKey("Category")]
        public int CategoryNo { get; set; }
        public virtual Category Category { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
