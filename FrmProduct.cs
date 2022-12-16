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
using StockTracking.DAL.DTO;

namespace StockTracking
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
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

        public ProductDTO dto = new ProductDTO();
        public ProductDetailDTO detail = new ProductDetailDTO();
        public bool IsUpdate = false;
        private void FrmProduct_Load(object sender, EventArgs e)
        {

            cbCategory.DataSource = dto.Categories;
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "ID";
            cbCategory.SelectedIndex = -1;

            if (IsUpdate)
            {
                tbProductName.Text = detail.ProductName;
                tbPrice.Text = detail.Price.ToString();
                cbCategory.SelectedValue = Convert.ToInt32(detail.CategoryID);
            }
            
               
            
            
        }

        ProductBLL bll = new ProductBLL();
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (tbProductName.Text.Trim() == "")
                MessageBox.Show("Product name is empty");
            else if (cbCategory.SelectedIndex == -1)
                MessageBox.Show("Please select a category");
            else if (tbPrice.Text.Trim() == "")
                MessageBox.Show("Price is empty");
            else 
            {
                if (!IsUpdate)
                {
                    ProductDetailDTO product = new ProductDetailDTO();
                    product.ProductName = tbProductName.Text;
                    product.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
                    product.Price = Convert.ToInt32(tbPrice.Text);
                    if (bll.Insert(product))
                    {
                        MessageBox.Show("Product was added");
                        tbProductName.Clear();
                        tbPrice.Clear();
                        cbCategory.SelectedIndex = -1;
                    }

                }
                else
                {
                    if (detail.ProductName == tbProductName.Text &&
                        detail.Price == Convert.ToInt32(tbPrice.Text) &&
                        detail.CategoryID == Convert.ToInt32(cbCategory.SelectedValue))
                        MessageBox.Show("There is no change");
                    else
                    {
                        detail.ProductName = tbProductName.Text;
                        detail.Price = Convert.ToInt32(tbPrice.Text);
                        detail.CategoryID = Convert.ToInt32(cbCategory.SelectedValue);
                        if (bll.Update(detail))
                        {
                            MessageBox.Show("product was updated");
                            this.Close();

                        }

                    }




                }
               




            }
        }
    }
}
