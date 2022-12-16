using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.BLL
{
    public class ProductBLL : IBLL<ProductDetailDTO, ProductDTO>
    {
        CategoryDAO categorydao = new CategoryDAO();
        ProductDAO dao = new ProductDAO();
        SalesDAO salesdao = new SalesDAO();

        public bool Delete(ProductDetailDTO entity)
        {
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            dao.Delete(product);
            SALES sales = new SALES();
            sales.ProductID = entity.ProductID;
            salesdao.Delete(sales);

            return true;
        }
        
        public bool GetBack(ProductDetailDTO entity)
        {
            return dao.GetBack(entity.ProductID);
        }

        public bool Insert(ProductDetailDTO entity)
        {
            PRODUCT prodruct = new PRODUCT();
            prodruct.ProductName = entity.ProductName;
            prodruct.CategoryID = entity.CategoryID;
            prodruct.Price = entity.Price;
            return dao.Insert(prodruct);
        }

        public ProductDTO Select()
        {
            ProductDTO dto = new ProductDTO();
            dto.Categories = categorydao.Select();
            dto.products = dao.Select();
            return dto;
        }

        public bool Update(ProductDetailDTO entity)
        {
            PRODUCT Product = new PRODUCT();
            Product.CategoryID = entity.CategoryID;
            Product.Price = entity.Price;
            Product.ProductName = entity.ProductName;
            Product.ID = entity.ProductID;
            Product.CategoryID = entity.CategoryID;
            Product.StockAmount = entity.StockAmount;
            return dao.Update(Product);
        }
    }
}
