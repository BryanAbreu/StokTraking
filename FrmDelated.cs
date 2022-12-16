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
using StockTracking.DAL.DAO;
using StockTracking.DAL.DAO.DTO;
using StockTracking.DAL.DTO;


namespace StockTracking
{
    public partial class FrmDelated : Form
    {
        public FrmDelated()
        {
            InitializeComponent();
        }
        SalesDTO dto = new SalesDTO();
        SalesBLL bll = new SalesBLL();
        CategoryDetailDTO categorydetail = new CategoryDetailDTO();
        ProductDetailDTO ProductDetail = new ProductDetailDTO();
        SalesDetailDTO salesdetail = new SalesDetailDTO();
        CustomerDetailDTO customerdetail = new CustomerDetailDTO();
        CategoryBLL categorybll = new CategoryBLL();
        ProductBLL productbll = new ProductBLL();
        CustomerBLL CustomerBLL = new CustomerBLL();
        SalesBLL salesbll = new SalesBLL();

        private void FrmDelated_Load(object sender, EventArgs e)
        {
            cbDeletedData.Items.Add("Category");
            cbDeletedData.Items.Add("Product");
            cbDeletedData.Items.Add("Customers");
            cbDeletedData.Items.Add("Sales");
            dto = bll.Select(true);
            dataGridView1.DataSource = dto.Sales;
            dataGridView1.Columns[0].HeaderText = "Customer Name";
            dataGridView1.Columns[1].HeaderText = "Product Name";
            dataGridView1.Columns[2].HeaderText = "Category Name";
            dataGridView1.Columns[6].HeaderText = "Sales Amount";
            dataGridView1.Columns[7].HeaderText = "Price";
            dataGridView1.Columns[8].HeaderText = "Sales Data";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbDeletedData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDeletedData.SelectedIndex == 0)
            {
                dataGridView1.DataSource = dto.Categories;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Category Name";
            }

           else if (cbDeletedData.SelectedIndex ==1)
            {

                dataGridView1.DataSource = dto.Products;

                dataGridView1.Columns[0].HeaderText = "Product Name";
                dataGridView1.Columns[1].HeaderText = "Category";
                dataGridView1.Columns[2].HeaderText = "Stock Amount";
                dataGridView1.Columns[3].HeaderText = "Price";
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
            }

           else if (cbDeletedData.SelectedIndex == 2)
            {

               

                dataGridView1.DataSource = dto.Customers;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "Customer Name";

            }

           else if (cbDeletedData.SelectedIndex == 3)
            {

               

                dataGridView1.DataSource = dto.Sales;
                dataGridView1.Columns[0].HeaderText = "Customer Name";
                dataGridView1.Columns[1].HeaderText = "Product Name";
                dataGridView1.Columns[2].HeaderText = "Category Name";
                dataGridView1.Columns[6].HeaderText = "Sales Amount";
                dataGridView1.Columns[7].HeaderText = "Price";
                dataGridView1.Columns[8].HeaderText = "Sales Data";
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (cbDeletedData.SelectedIndex == 0)
            {
                categorydetail = new CategoryDetailDTO();
                categorydetail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                categorydetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }

            else if (cbDeletedData.SelectedIndex == 1)
            {
                ProductDetail = new ProductDetailDTO();
                ProductDetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                ProductDetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                ProductDetail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                ProductDetail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                ProductDetail.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                ProductDetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                ProductDetail.IsCategoryDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            }

           else if (cbDeletedData.SelectedIndex == 2)
            {
                customerdetail = new CustomerDetailDTO();
                customerdetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerdetail.ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            }

           else if (cbDeletedData.SelectedIndex == 3)
            {
                salesdetail = new SalesDetailDTO();

                salesdetail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                salesdetail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                salesdetail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                salesdetail.CustomerID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                salesdetail.ProductID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                salesdetail.CategoryID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                salesdetail.SalesAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                salesdetail.Price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                salesdetail.SalesDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                salesdetail.StockAmount = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                salesdetail.SalesID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                salesdetail.IsCategoryDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                salesdetail.IsCustomerDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
                salesdetail.IsProductDeleted = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[13].Value);

            }
        }

        private void btnGetBack_Click(object sender, EventArgs e)
        {
            if (cbDeletedData.SelectedIndex == 0)
            {
                if(categorybll.GetBack(categorydetail))
                {
                    MessageBox.Show("Category was Get Back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Categories;
                }
            }
            else if (cbDeletedData.SelectedIndex == 1)
            {
                if (ProductDetail.IsCategoryDeleted)
                    MessageBox.Show("Category was deleted firs get back category");
                else if (productbll.GetBack(ProductDetail))
                {
                     MessageBox.Show("Product was Get Back");
                     dto = bll.Select(true);
                     dataGridView1.DataSource = dto.Products;
                }
            }

            else if (cbDeletedData.SelectedIndex == 2)
            {
               if (CustomerBLL.GetBack(customerdetail))
                {
                    MessageBox.Show("Customer was Get Back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Customers;

                }
            }

            else if (cbDeletedData.SelectedIndex == 3)
            {
                if (salesdetail.IsCategoryDeleted || salesdetail.IsCustomerDeleted || salesdetail.IsProductDeleted)
                {
                    if (salesdetail.IsCategoryDeleted)
                        MessageBox.Show("Category was deleted firs get back category");
                    else if (salesdetail.IsCustomerDeleted)
                        MessageBox.Show("Customer was deleted firs get back Customer");
                    else if(salesdetail.IsProductDeleted)
                        MessageBox.Show("Product was deleted firs get back Product");
                }
                else if (salesbll.GetBack(salesdetail))
                {
                    MessageBox.Show("Sale was Get Back");
                    dto = bll.Select(true);
                    dataGridView1.DataSource = dto.Sales;
                }
            }
        }
    }
}
