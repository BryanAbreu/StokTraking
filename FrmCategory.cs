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
using StockTracking.DAL.DAO;

namespace StockTracking
{
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        CategoryBLL bll = new CategoryBLL();
        public CategoryDetailDTO detail = new CategoryDetailDTO();
        public bool ISUpdate = false;
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            if (ISUpdate)
                tbcategory.Text = detail.CategoryName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbcategory.Text.Trim() == "")
                MessageBox.Show("Category name is empty");
            else
            {
                if (!ISUpdate)//add
                {
                    CategoryDetailDTO category = new CategoryDetailDTO();
                    category.CategoryName = tbcategory.Text;
                    if (bll.Insert(category))
                    {
                        MessageBox.Show("Category was added");
                        tbcategory.Clear();
                    }

                }
                else
                {
                    if (detail.CategoryName == tbcategory.Text.Trim())
                        MessageBox.Show("There is no change");
                    else
                    {
                        detail.CategoryName = tbcategory.Text;
                        if (bll.Update(detail))
                            MessageBox.Show("Category was updated");
                        this.Close();

                    }


                }
            
            }

        }
    }
}
