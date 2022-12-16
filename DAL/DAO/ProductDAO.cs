using StockTracking.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.DAL.DAO
{
    public class ProductDAO : StockContext, IDAO<PRODUCT, ProductDetailDTO>
    {
        public bool Delete(PRODUCT entity)
        {
            try
            {
                if (entity.ID != 0)
                {
                    PRODUCT product = db.PRODUCT.First(x => x.ID == entity.ID);
                    product.IsDeleted = true;
                    product.DeletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                else if (entity.CategoryID != 0)
                {
                    List<PRODUCT> list = db.PRODUCT.Where(x=>x.CategoryID == entity.CategoryID).ToList();
                    foreach (var item in list)
                    {
                        item.IsDeleted = true;
                        item.DeletedDate = DateTime.Today;
                        List<SALES> sales = db.SALES.Where(x=>x.ProductID==item.ID).ToList();
                        foreach (var s in sales)
                        {
                            s.IsDeleted = true;
                            s.DaletedDate = DateTime.Today;
                        }
                        db.SaveChanges();
                    }
                    db.SaveChanges();
                
                }
               
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                PRODUCT product = db.PRODUCT.First(x=>x.ID==ID);
                product.IsDeleted = false;
                product.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Insert(PRODUCT entity)
        {
            try
            {
                db.PRODUCT.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ProductDetailDTO> Select(bool IsDeleted)
        {
            try
            {
                List<ProductDetailDTO> products = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCT.Where(x => x.IsDeleted == true)
                            join c in db.CATEGORY on p.CategoryID equals c.ID
                            select new
                            {
                                ProductName = p.ProductName,
                                CategoryName = c.CategoryName,
                                StockAmount = p.StockAmount,
                                Price = p.Price,
                                ProductID = p.ID,
                                CategoryID = c.ID,
                                CategoryIsDelted = c.IsDeleted
                                

                            }).OrderBy(x => x.ProductName).ToList();

                foreach (var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ProductName = item.ProductName;
                    dto.CategoryName = item.CategoryName;
                    dto.StockAmount = item.StockAmount;
                    dto.Price = item.Price;
                    dto.ProductID = item.ProductID;
                    dto.CategoryID = item.CategoryID;
                    dto.IsCategoryDeleted = item.CategoryIsDelted;
                    products.Add(dto);
                }
                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<ProductDetailDTO> Select()
        {
            try
            {
                List<ProductDetailDTO> products = new List<ProductDetailDTO>();
                var list = (from p in db.PRODUCT.Where(x=> x.IsDeleted== false)
                            join c in db.CATEGORY on p.CategoryID equals c.ID
                            select new
                            {
                                ProductName = p.ProductName,
                                CategoryName = c.CategoryName,
                                StockAmount = p.StockAmount,
                                Price = p.Price,
                                ProductID = p.ID,
                                CategoryID = c.ID

                            }).OrderBy(x => x.ProductName).ToList();

                foreach (var item in list)
                {
                    ProductDetailDTO dto = new ProductDetailDTO();
                    dto.ProductName = item.ProductName;
                    dto.CategoryName = item.CategoryName;
                    dto.StockAmount = item.StockAmount;
                    dto.Price = item.Price;
                    dto.ProductID = item.ProductID;
                    dto.CategoryID = item.CategoryID;
                    products.Add(dto);
                }
                return products;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool Update(PRODUCT entity)
        {
            try
            {
                PRODUCT product = db.PRODUCT.First(x => x.ID == entity.ID);
                if (entity.CategoryID == 0)
                {

                    product.StockAmount = entity.StockAmount;
                   
                }
                else
                {
                    product.Price = entity.Price;
                    product.ProductName = entity.ProductName;
                    product.CategoryID = entity.CategoryID;
                }
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
