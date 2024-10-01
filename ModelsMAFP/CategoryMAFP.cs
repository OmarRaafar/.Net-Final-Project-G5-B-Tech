using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsMAFP.Shared;

namespace ModelsMAFP
{
    public class CategoryMAFP : BaseEntityMAFP
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }
        [MaxLength(150)]
        public string? Description { get; set; }
        [MaxLength(250)]
        public string? ImageUrl { get; set; }
        public ICollection<ProductMAFP> Products { get; set; }
    }
}
