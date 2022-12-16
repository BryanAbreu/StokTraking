using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DAO.DTO;

namespace StockTracking.DAL.DAO
{
    public class CategoryDAO : StockContext, IDAO<CATEGORY, CategoryDetailDTO>
    {
        
        public bool Delete(CATEGORY entity)
        {
            try
            {
                CATEGORY category = db.CATEGORY.First(x=>x.ID == entity.ID);
                category.IsDeleted = true;
                category.DeletedDate = DateTime.Today;
                db.SaveChanges();
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
                CATEGORY category = db.CATEGORY.First(x=>x.ID == ID);
                category.IsDeleted = false;
                category.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Insert(CATEGORY entity)
        {
            try
            {
                db.CATEGORY.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CategoryDetailDTO> Select()
        {

            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORY.Where(x=>x.IsDeleted ==false).ToList();

            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.CategoryName = item.CategoryName;
                    dto.ID = item.ID;
                categories.Add(dto);
            }
            return categories;
               
        }
        public List<CategoryDetailDTO> Select(bool IsDeleted)
        {

            List<CategoryDetailDTO> categories = new List<CategoryDetailDTO>();
            var list = db.CATEGORY.Where(x => x.IsDeleted == true).ToList();

            foreach (var item in list)
            {
                CategoryDetailDTO dto = new CategoryDetailDTO();
                dto.CategoryName = item.CategoryName;
                dto.ID = item.ID;
                categories.Add(dto);
            }
            return categories;

        }

        public bool Update(CATEGORY entity)
        {
            try
            {
                CATEGORY category = db.CATEGORY.First(x => x.ID == entity.ID);
                category.CategoryName = entity.CategoryName;
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
