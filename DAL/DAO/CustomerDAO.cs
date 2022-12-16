using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL.DAO;
using StockTracking.DAL;
using StockTracking.DAL.DAO.DTO;

namespace StockTracking.DAL.DAO
{
    class CustomerDAO : StockContext, IDAO<CUSTOMER, CustomerDetailDTO>

    {
        public bool Delete(CUSTOMER entity)
        {
            try
            {
                CUSTOMER customer = db.CUSTOMER.First(x=>x.ID ==entity.ID);
                customer.IsDeleted = true;
                customer.DeletedDate = DateTime.Today;
                db.SaveChanges();
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
                CUSTOMER customer = db.CUSTOMER.First(x => x.ID == ID);
                customer.IsDeleted = false;
                customer.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public bool Insert(CUSTOMER entity)
        {
            try
            {
                db.CUSTOMER.Add(entity);
                db.SaveChanges();
                return true;        
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CustomerDetailDTO> Select(bool IsDeleted)
        {
            try
            {
                List<CustomerDetailDTO> customers = new List<CustomerDetailDTO>();
                var list = db.CUSTOMER.Where(x => x.IsDeleted == true).ToList();
                foreach (var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.ID = item.ID;
                    dto.CustomerName = item.CustomerName;
                    customers.Add(dto);

                }
                return customers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CustomerDetailDTO> Select()
        {
            try
            {
                List<CustomerDetailDTO> customers = new List<CustomerDetailDTO>();
                var list = db.CUSTOMER.Where(x=>x.IsDeleted==false).ToList();
                foreach (var item in list)
                {
                    CustomerDetailDTO dto = new CustomerDetailDTO();
                    dto.ID = item.ID;
                    dto.CustomerName = item.CustomerName;
                    customers.Add(dto);

                }
                return customers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update(CUSTOMER entity)
        {
            CUSTOMER customer = db.CUSTOMER.First(x=> x.ID == entity.ID);
            customer.CustomerName = entity.CustomerName;
            db.SaveChanges();

            return true;
        }
    }
}
