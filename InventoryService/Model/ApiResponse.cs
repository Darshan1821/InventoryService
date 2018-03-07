using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InventoryService.Model
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public Product Product { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
