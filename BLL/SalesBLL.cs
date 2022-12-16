using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DAO.DTO;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.BLL
{
    public class SalesBLL : IBLL<SalesDetailDTO, SalesDTO>
    {
        SalesDAO dao = new SalesDAO();
        ProductDAO productdao = new ProductDAO();
        CategoryDAO categorydao = new CategoryDAO();
        CustomerDAO customerdao = new CustomerDAO();

        public bool Delete(SalesDetailDTO entity)
        {
            SALES sale = new SALES();
            sale.ID = entity.SalesID;
            dao.Delete(sale);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.StockAmount = entity.StockAmount + entity.SalesAmount;
            productdao.Update(product);
            return true;
        }

        public bool GetBack(SalesDetailDTO entity)
        {
            dao.GetBack(entity.SalesID);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.StockAmount = temp;
            productdao.Update(product);
            return true;
        }

        public bool Insert(SalesDetailDTO entity)
        {
            SALES sale = new SALES();
            sale.CategoryID = entity.CategoryID;
            sale.ProductID = entity.ProductID;
            sale.CustomerID = entity.CustomerID;
            sale.ProductSalesPrice = entity.Price;
            sale.ProductSalesAmount = entity.SalesAmount;
            sale.SalesDate = entity.SalesDate;
            dao.Insert(sale);
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            int temp = entity.StockAmount - entity.SalesAmount;
            product.StockAmount = temp;
            productdao.Update(product);
            return true;
        }

        public SalesDTO Select()
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productdao.Select();
            dto.Customers = customerdao.Select();
            dto.Categories = categorydao.Select();
            dto.Sales = dao.Select();
            return dto;
        }
        public SalesDTO Select(bool Isdeleted)
        {
            SalesDTO dto = new SalesDTO();
            dto.Products = productdao.Select(Isdeleted);
            dto.Customers = customerdao.Select(Isdeleted);
            dto.Categories = categorydao.Select(Isdeleted);
            dto.Sales = dao.Select(Isdeleted);
            return dto;
        }

        public bool Update(SalesDetailDTO entity)
        {
            SALES sale = new SALES();
            sale.ID = entity.SalesID;
            sale.ProductSalesAmount = entity.SalesAmount;
            PRODUCT product = new PRODUCT();
            product.ID = entity.ProductID;
            product.StockAmount = entity.StockAmount;
            productdao.Update(product);
            return dao.Update(sale);

        }
    }
}

