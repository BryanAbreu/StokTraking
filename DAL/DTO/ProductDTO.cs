using StockTracking.DAL.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DTO
{
    public class ProductDTO
    {
       public List<ProductDetailDTO> products { get; set; }
       public List<CategoryDetailDTO> Categories { get; set; }
        
    }
}
