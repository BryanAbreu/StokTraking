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
using StockTracking.DAL.DTO;
namespace StockTracking
{
    public partial class FrmSales : Form
    {
        public FrmSales()
        {
            InitializeComponent();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
        public SalesDTO dto = new SalesDTO();
        public SalesDetailDTO detail = new SalesDetailDTO();
        public bool IsUpdate = false;

        private void FrmSales_Load(object sender, EventArgs e)
        {
            cbCategory.DataSource = dto.Categories;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "ID";
            cbCategory.SelectedIndex = -1;

            if (!IsUpdate)
            {

                gridProduct.DataSource = dto.Products;

                gridProduct.Columns[0].HeaderText = "Product Name";
                gridProduct.Columns[1].HeaderText = "Category";
                gridProduct.Columns[2].HeaderText = "Stock Amount";
                gridProduct.Columns[3].HeaderText = "Price";
                gridProduct.Columns[4].Visible = false;
                gridProduct.Columns[5].Visible = false;

                gridCustomers.DataSource = dto.Customers;
                gridCustomers.Columns[0].Visible = false;
                gridCustomers.Columns[1].HeaderText = "Customer Name";
                if (dto.Categories.Count > 0)
                    combofull = true;
            }
            else
            {
                panel1.Hide();
                tbCustomer.Text = detail.CategoryName;
                tbProductName.Text = detail.ProductName;
                tbPrice.Text = detail.Price.ToString();
                tbSalesAmount.Text = detail.SalesAmount.ToString();
                ProductDetailDTO product = dto.Products.First(x => x.ProductID == detail.ProductID);
                detail.StockAmount = product.StockAmount;
                tbStock.Text = detail.StockAmount.ToString();

            
            }
           
        }

        bool combofull = false;
        SalesBLL bll = new SalesBLL();
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                List<ProductDetailDTO> list = dto.Products;
                list = list.Where(x => x.CategoryID == Convert.ToInt32(cbCategory.SelectedValue)).ToList();
                gridProduct.DataSource = list;

                if (list.Count == 0)
                {
                    tbProductName.Clear();
                    tbPrice.Clear();
                    tbStock.Clear();
                }
            }
        }

        private void tbCustomerse_TextChanged(object sender, EventArgs e)
        {
            List<CustomerDetailDTO> list = dto.Customers;
            list = list.Where(x => x.CustomerName.Contains(tbCustomerse.Text)).ToList();
            gridCustomers.DataSource = list;
            if (list.Count == 0)
            {
                tbCustomer.Clear();
            
            }
        }

        
        private void gridProduct_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.ProductName = gridProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.Price = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[3].Value);
            detail.StockAmount = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[2].Value);
            detail.ProductID = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryID = Convert.ToInt32(gridProduct.Rows[e.RowIndex].Cells[5].Value);

            tbProductName.Text = detail.ProductName;
            tbPrice.Text = detail.Price.ToString();
            tbStock.Text = detail.StockAmount.ToString();

        }

        private void gridCustomers_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.CustomerName = gridCustomers.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CustomerID =Convert.ToInt32( gridCustomers.Rows[e.RowIndex].Cells[0].Value);
            tbCustomer.Text = detail.CustomerName;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbSalesAmount.Text.Trim() == "")
                MessageBox.Show("Please fill the sales amount area");
            else
            {
                if(!IsUpdate)
                {
                    if (detail.ProductID == 0)
                        MessageBox.Show("Please select a product from product table");
                    else if (detail.CustomerID == 0)
                        MessageBox.Show("Please select customer from table");
                    else if (detail.StockAmount < Convert.ToInt32(tbSalesAmount.Text))
                        MessageBox.Show("You have enough product for sale");
                    else
                    {
                        detail.SalesAmount = Convert.ToInt32(tbSalesAmount.Text);
                        detail.SalesDate = DateTime.Today;
                        if (bll.Insert(detail))
                        {
                            MessageBox.Show("Sales was added");
                            bll = new SalesBLL();
                            dto = bll.Select();
                            gridProduct.DataSource = dto.Products;
                            dto.Customers = dto.Customers;
                            combofull = false;
                            cbCategory.DataSource = dto.Categories;
                            if (dto.Products.Count > 0)
                                combofull = true;
                            tbSalesAmount.Clear();
                        }
                    }
                }
                else
                {
                    if (detail.SalesAmount == Convert.ToInt32(tbSalesAmount.Text))
                        MessageBox.Show("There is no a change");
                    else
                    {
                        int temp = detail.SalesAmount + detail.StockAmount;
                        if (temp < Convert.ToInt32(tbSalesAmount.Text))
                            MessageBox.Show("You have enough product for sale");
                        else 
                        {
                            detail.SalesAmount = Convert.ToInt32(tbSalesAmount.Text);
                            detail.StockAmount = temp - detail.SalesAmount;
                            if (bll.Update(detail))
                            {
                                MessageBox.Show("Sale was update");
                                this.Close();
                            }
                        
                        }

                    }
                        
                            

                }

            }

        }
    }
}
