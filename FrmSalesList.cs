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
using StockTracking.DAL.DTO;

namespace StockTracking
{
    public partial class FrmSalesList : Form
    {
        public FrmSalesList()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void tbPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.IsNumber(e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmSales frm = new FrmSales();
            frm.dto = dto;
            this.Hide();
            frm.ShowDialog();
            this.Visible = true;
            dto = bll.Select();
            dataGridView1.DataSource = dto.Sales;
        }

        SalesBLL bll = new SalesBLL();
        SalesDTO dto = new SalesDTO();
        private void FrmSalesList_Load(object sender, EventArgs e)
        {
            dto = bll.Select();
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

            cbCategory.DataSource = dto.Categories;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "ID";
            cbCategory.SelectedIndex = -1;




        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SalesDetailDTO> list = dto.Sales;
            if (tbProductName.Text.Trim()!= "")
                list = list.Where(x=> x.ProductName.Contains(tbProductName.Text)).ToList();
            if (tbCustomer.Text.Trim() != "")
                list = list.Where(x => x.CustomerName.Contains(tbCustomer.Text)).ToList();
            if(cbCategory.SelectedIndex!= -1)
                list = list.Where(x => x.CategoryID ==Convert.ToInt32( cbCategory.SelectedValue)).ToList();

            if (tbPrice.Text.Trim() != "")
            {
                if (rbPriceEqual.Checked)
                    list = list.Where(x => x.Price == Convert.ToInt32(tbPrice.Text)).ToList();
                else if (rbPriceMore.Checked)
                    list = list.Where(x => x.Price > Convert.ToInt32(tbPrice.Text)).ToList();
                else if (rbPriceLess.Checked)
                    list = list.Where(x => x.Price < Convert.ToInt32(tbPrice.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from price group");
            }

            if (tbSalesAmount.Text.Trim() != "")
            {
                if (rbSalesEquals.Checked)
                    list = list.Where(x => x.SalesAmount == Convert.ToInt32(tbSalesAmount.Text)).ToList();
                else if (rbSalesMore.Checked)
                    list = list.Where(x => x.SalesAmount > Convert.ToInt32(tbSalesAmount.Text)).ToList();
                else if (rbSalesLess.Checked)
                    list = list.Where(x => x.SalesAmount < Convert.ToInt32(tbSalesAmount.Text)).ToList();
                else
                    MessageBox.Show("Please select a criterion from Sales group");      
            }

            if (chDate.Checked)
                list = list.Where(x => x.SalesDate > dptStart.Value && x.SalesDate < dptEnd.Value).ToList();


            dataGridView1.DataSource = list;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFilter();
        }

        private void clearFilter()
        {
            tbCustomer.Clear();
            tbPrice.Clear();
            tbSalesAmount.Clear();
            tbProductName.Clear();
            rbPriceEqual.Checked = false;
            rbPriceLess.Checked = false;
            rbPriceMore.Checked = false;
            rbSalesEquals.Checked = false;
            rbSalesLess.Checked = false;
            rbSalesMore.Checked = false;
            dptEnd.Value = DateTime.Today;
            dptStart.Value = DateTime.Today;
            chDate.Checked = false;
            cbCategory.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Sales;
        }


        SalesDetailDTO detail = new SalesDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail = new SalesDetailDTO();
            detail.CustomerName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            detail.ProductName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            detail.CategoryName = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.CustomerID =Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            detail.ProductID = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            detail.CategoryID = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            detail.SalesAmount = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[6].Value );
            detail.Price = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[7].Value );
            detail.SalesDate = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.StockAmount = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[9].Value );
            detail.SalesID = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[10].Value );


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.SalesID == 0)
                MessageBox.Show("please select sale from table");
            else
            {
                FrmSales frm = new FrmSales();
                frm.detail = detail;
                frm.IsUpdate = true;
                frm.dto = dto;
                this.Hide();
                frm.ShowDialog();
                this.Visible = true;
                bll = new SalesBLL();
                dto = bll.Select();
                dataGridView1.DataSource = dto.Sales;
                clearFilter();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (detail.SalesID == 0)
                MessageBox.Show("please select a sales from table");
            else
            {
                DialogResult resul = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                if (resul == DialogResult.Yes)
                {
                    if (bll.Delete(detail))
                        MessageBox.Show("Sales was deleted");
                    bll = new SalesBLL();
                    dto = bll.Select();
                    dataGridView1.DataSource = dto.Sales;
                    clearFilter();
                
                }

            
            
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    
}
