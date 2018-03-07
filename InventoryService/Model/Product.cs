using System.ComponentModel.DataAnnotations;

namespace InventoryService.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Product Type")]
        public ProductType Type { get; set; }
    }
}
