using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Dto
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public string ImageName { get; set; }
        public string FolderPath { get; set; }
        public bool IsDeleted { get; set; }

    }
}
