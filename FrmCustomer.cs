using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockTracking.BLL;
using StockTracking.DAL.DAO.DTO;


namespace StockTracking
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public CustomerDetailDTO detail = new CustomerDetailDTO();
        public bool IsUpdate = false;
        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                tbCustomer.Text = detail.CustomerName;
            }
        }

        CustomerBLL bll = new CustomerBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbCustomer.Text.Trim() == "")
                MessageBox.Show("Customer name is empty");
            else
            {
                if (!IsUpdate)
                {
                    CustomerDetailDTO customer = new CustomerDetailDTO();
                    customer.CustomerName = tbCustomer.Text;
                    if (bll.Insert(customer))
                    {
                        MessageBox.Show("Customer was added");
                        tbCustomer.Clear();

                    }
                }
                else
                {
                    if (tbCustomer.Text.Trim() == detail.CustomerName)
                        MessageBox.Show("There is no change");
                    else
                    {
                        detail.CustomerName = tbCustomer.Text;
                        if (bll.Update(detail))
                            MessageBox.Show("Customer was updated");
                         this.Close();

                    }
                
                }

            
            }
        }
    }
}
