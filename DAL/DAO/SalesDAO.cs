using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class SalesDAO : StockContext, IDAO<SALES, SalesDetailDTO>

    {
        public bool Delete(SALES entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    SALES sales = db.SALES.First(x => x.ID == entity.ID);
                    sales.IsDeleted = true;
                    sales.DaletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                else if (entity.ProductID != 0)
                {
                    List<SALES> sales = db.SALES.Where(X => X.ProductID == entity.ProductID).ToList();
                    foreach (var item in sales)
                    {
                        item.IsDeleted = true;
                        item.DaletedDate = DateTime.Today;

                    }
                    db.SaveChanges();

                }
                else if (entity.CustomerID != 0)
                {
                    List<SALES> sales = db.SALES.Where(X => X.CustomerID == entity.CustomerID).ToList();
                    foreach (var item in sales)
                    {
                        item.IsDeleted = true;
                        item.DaletedDate = DateTime.Today;

                    }
                    db.SaveChanges();

                }
                
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                SALES sale = db.SALES.First(x=>x.ID == ID);
                sale.IsDeleted = false;
                sale.DaletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
           
           
        }

        public bool Insert(SALES entity)
        {
            try
            {
                db.SALES.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<SalesDetailDTO> Select(bool IsDeleted)
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();

                var list = (from s in db.SALES.Where(x => x.IsDeleted == true)
                            join p in db.PRODUCT on s.ProductID equals p.ID
                            join c in db.CATEGORY on p.CategoryID equals c.ID
                            join cus in db.CUSTOMER on s.CustomerID equals cus.ID
                            select new
                            {
                                CustomerName = cus.CustomerName,
                                ProductName = p.ProductName,
                                CategoryName = c.CategoryName,
                                CustomerID = s.CustomerID,
                                ProductID = s.ProductID,
                                CategoryID = s.CategoryID,
                                SalesAmount = s.ProductSalesAmount,
                                Price = s.ProductSalesPrice,
                                SalesDate = s.SalesDate,
                                SalesID = s.ID,
                                StockAmunt = p.StockAmount,
                                CategoriIsDeleted = c.IsDeleted,
                                ProductIsDeleted = p.IsDeleted,
                                CustomerIsDeleted = cus.IsDeleted
                                
                            }).OrderBy(x => x.SalesDate).ToList();


                foreach (var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.ProductName = item.ProductName;
                    dto.ProductID = item.ProductID;
                    dto.CategoryName = item.CategoryName;
                    dto.CustomerID = item.CustomerID;
                    dto.CategoryID = item.CategoryID;
                    dto.SalesAmount = item.SalesAmount;
                    dto.Price = item.Price;
                    dto.SalesDate = item.SalesDate;
                    dto.CustomerName = item.CustomerName;
                    dto.SalesID = item.SalesID;
                    dto.StockAmount = item.StockAmunt;
                    dto.IsCategoryDeleted = item.CategoriIsDeleted;
                    dto.IsCustomerDeleted = item.CustomerIsDeleted;
                    dto.IsProductDeleted = item.ProductIsDeleted;
                    sales.Add(dto);

                }
                return sales;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public List<SalesDetailDTO> Select()
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();

                var list = (from s in db.SALES.Where(x=> x.IsDeleted ==false)
                            join p in db.PRODUCT on s.ProductID equals p.ID
                            join c in db.CATEGORY on p.CategoryID equals c.ID
                            join cus in db.CUSTOMER on s.CustomerID equals cus.ID
                            select new
                            {
                                CustomerName = cus.CustomerName,
                                ProductName = p.ProductName,
                                CategoryName = c.CategoryName,
                                CustomerID = s.CustomerID,
                                ProductID = s.ProductID,
                                CategoryID = s.CategoryID,
                                SalesAmount = s.ProductSalesAmount,
                                Price = s.ProductSalesPrice,
                                SalesDate = s.SalesDate,
                                SalesID = s.ID,
                                StockAmount = p.StockAmount
                            }).OrderBy(x => x.SalesDate).ToList();


                foreach (var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.ProductName = item.ProductName;
                    dto.ProductID = item.ProductID;
                    dto.CategoryName = item.CategoryName;
                    dto.CustomerID = item.CustomerID;
                    dto.CategoryID = item.CategoryID;
                    dto.SalesAmount = item.SalesAmount;
                    dto.Price = item.Price;
                    dto.SalesDate = item.SalesDate;
                    dto.CustomerName = item.CustomerName;
                    dto.SalesID = item.SalesID;
                    dto.StockAmount = item.StockAmount;
                    sales.Add(dto);

                }
                return sales;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(SALES entity)
        {
            try
            {
                SALES sales = db.SALES.First(x=>x.ID == entity.ID);
                sales.ProductSalesAmount = entity.ProductSalesAmount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
