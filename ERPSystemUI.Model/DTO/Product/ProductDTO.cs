using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSystemUI.Model.DTO.Product
{
    public class ProductDTO
    {
        public Int64? Id { get; set; } = 0;
        public string? Name { get; set; } = "string";
        public string? BrandName { get; set; }="string";
        public Int64? CategoryId { get; set; } = 0;
        public string? Description { get; set; } = "string";
        public string Image { get; set; } = "string";

        //Response
        public string? CategoryName { get; set; } = null;

    }
}
