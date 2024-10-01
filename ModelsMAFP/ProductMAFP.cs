using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsMAFP.Shared;

namespace ModelsMAFP
{
    public class ProductMAFP: BaseEntityMAFP
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(250)]
        public  Dictionary<string, string>? Description { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [MaxLength(250)]
        public List<string> ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        [ForeignKey("CategoryMAFP")]
        public int CategoryMAFPId { get; set; }
        public ICollection<CategoryMAFP> Categories { get; set; }  //one category or many??
        public ICollection<ProductOrderMAFP>? ProductOrders { get; set; }
        [ForeignKey("SellerMAFP")]
        public int? SellerMAFPId { get; set; }
        public SellerMAFP? Seller { get; set; }
    }
}
