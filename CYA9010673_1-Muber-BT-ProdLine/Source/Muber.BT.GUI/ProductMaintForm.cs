using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO.Pipes;
using Mubea.GUI.CustomControl;
using System.Threading.Tasks;
using Mubea.AutoTest.GUI;
using AH.AutoServer;
using AH.Network;
using CommonHelper;
using GP.MAGICL6800.ORM;

namespace Mubea.AutoTest.GUI
{
    public partial class ProductMaintForm : BaseViewForm
    {

        private ToolTip _logintip = new ToolTip();

        public ProductMaintForm()
        {
            InitializeComponent();
            
            //InitializeListView(listView_CurrentOrder);
            
            //ClientStatus.ChangeStatus += setStaionState;
            //ClientGUFStatus.ChangeStationState += updateStaionState;
            
        }

        private void HomeOverView_Load(object sender, EventArgs e)
        {

     
        }

       
        private void btn_OK_Click(object sender, EventArgs e)
        {
           //product info update
            if(this.txt_ProductNo.Text.Trim().Length > 0)
            {
                try
                {
                    if (ProductBOMBLL.UpdateProductBOMInfo(this.txt_ProductNo.Text.Trim(), this.txt_ProductName.Text.Trim(),
                        this.textBox1.Text.Trim().Length > 0 ? this.textBox1.Text.Trim() : null, (this.comboBox1.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox1.Text.Trim())) ? byte.Parse(this.comboBox1.Text.Trim()) : (byte)0,
                        this.textBox2.Text.Trim().Length > 0 ? this.textBox2.Text.Trim() : null, (this.comboBox2.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox2.Text.Trim())) ? byte.Parse(this.comboBox2.Text.Trim()) : (byte)0,
                        this.textBox3.Text.Trim().Length > 0 ? this.textBox3.Text.Trim() : null, (this.comboBox3.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox3.Text.Trim())) ? byte.Parse(this.comboBox3.Text.Trim()) : (byte)0,
                        this.textBox4.Text.Trim().Length > 0 ? this.textBox4.Text.Trim() : null, (this.comboBox4.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox4.Text.Trim())) ? byte.Parse(this.comboBox4.Text.Trim()) : (byte)0,
                        this.textBox5.Text.Trim().Length > 0 ? this.textBox5.Text.Trim() : null, (this.comboBox5.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox5.Text.Trim())) ? byte.Parse(this.comboBox5.Text.Trim()) : (byte)0,
                        this.textBox6.Text.Trim().Length > 0 ? this.textBox6.Text.Trim() : null, (this.comboBox6.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox6.Text.Trim())) ? byte.Parse(this.comboBox6.Text.Trim()) : (byte)0,
                        this.textBox7.Text.Trim().Length > 0 ? this.textBox7.Text.Trim() : null, (this.comboBox7.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox7.Text.Trim())) ? byte.Parse(this.comboBox7.Text.Trim()) : (byte)0,
                        this.textBox8.Text.Trim().Length > 0 ? this.textBox8.Text.Trim() : null, (this.comboBox8.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox8.Text.Trim())) ? byte.Parse(this.comboBox8.Text.Trim()) : (byte)0,
                        this.textBox9.Text.Trim().Length > 0 ? this.textBox9.Text.Trim() : null, (this.comboBox9.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox9.Text.Trim())) ? byte.Parse(this.comboBox9.Text.Trim()) : (byte)0,
                        this.textBox10.Text.Trim().Length > 0 ? this.textBox10.Text.Trim() : null, (this.comboBox10.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox10.Text.Trim())) ? byte.Parse(this.comboBox10.Text.Trim()) : (byte)0,
                        this.textBox11.Text.Trim().Length > 0 ? this.textBox11.Text.Trim() : null, (this.comboBox11.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox11.Text.Trim())) ? byte.Parse(this.comboBox11.Text.Trim()) : (byte)0,
                        this.textBox12.Text.Trim().Length > 0 ? this.textBox12.Text.Trim() : null, (this.comboBox12.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox12.Text.Trim())) ? byte.Parse(this.comboBox12.Text.Trim()) : (byte)0,
                        this.textBox13.Text.Trim().Length > 0 ? this.textBox13.Text.Trim() : null, (this.comboBox13.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox13.Text.Trim())) ? byte.Parse(this.comboBox13.Text.Trim()) : (byte)0,
                        this.textBox14.Text.Trim().Length > 0 ? this.textBox14.Text.Trim() : null, (this.comboBox14.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox14.Text.Trim())) ? byte.Parse(this.comboBox14.Text.Trim()) : (byte)0,
                        this.textBox15.Text.Trim().Length > 0 ? this.textBox15.Text.Trim() : null, (this.comboBox15.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox15.Text.Trim())) ? byte.Parse(this.comboBox15.Text.Trim()) : (byte)0,
                        this.textBox16.Text.Trim().Length > 0 ? this.textBox16.Text.Trim() : null, (this.comboBox16.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox16.Text.Trim())) ? byte.Parse(this.comboBox16.Text.Trim()) : (byte)0
                        ))
                    {
                        ShowLoginTip("产品信息更新成功.");
                    }
                    else
                    {
                        SpMessageBox.Show(this.txt_ProductName.Text.Trim() + "信息更新失败.", "产品信息维护");
                    }
                }
                catch
                {
                
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            //product info add
            if (this.txt_ProductNo.Text.Trim().Length > 0 && this.txt_ProductName.Text.Trim().Length > 0)
            {
                try
                {
                    if (ProductBOMBLL.InsertProductBOMInfo(this.txt_ProductNo.Text.Trim(), this.txt_ProductName.Text.Trim(),
                            this.textBox1.Text.Trim().Length > 0 ? this.textBox1.Text.Trim() : null, (this.comboBox1.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox1.Text.Trim())) ? byte.Parse(this.comboBox1.Text.Trim()) : (byte)0,
                            this.textBox2.Text.Trim().Length > 0 ? this.textBox2.Text.Trim() : null, (this.comboBox2.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox2.Text.Trim())) ? byte.Parse(this.comboBox2.Text.Trim()) : (byte)0,
                            this.textBox3.Text.Trim().Length > 0 ? this.textBox3.Text.Trim() : null, (this.comboBox3.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox3.Text.Trim())) ? byte.Parse(this.comboBox3.Text.Trim()) : (byte)0,
                            this.textBox4.Text.Trim().Length > 0 ? this.textBox4.Text.Trim() : null, (this.comboBox4.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox4.Text.Trim())) ? byte.Parse(this.comboBox4.Text.Trim()) : (byte)0,
                            this.textBox5.Text.Trim().Length > 0 ? this.textBox5.Text.Trim() : null, (this.comboBox5.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox5.Text.Trim())) ? byte.Parse(this.comboBox5.Text.Trim()) : (byte)0,
                            this.textBox6.Text.Trim().Length > 0 ? this.textBox6.Text.Trim() : null, (this.comboBox6.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox6.Text.Trim())) ? byte.Parse(this.comboBox6.Text.Trim()) : (byte)0,
                            this.textBox7.Text.Trim().Length > 0 ? this.textBox7.Text.Trim() : null, (this.comboBox7.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox7.Text.Trim())) ? byte.Parse(this.comboBox7.Text.Trim()) : (byte)0,
                            this.textBox8.Text.Trim().Length > 0 ? this.textBox8.Text.Trim() : null, (this.comboBox8.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox8.Text.Trim())) ? byte.Parse(this.comboBox8.Text.Trim()) : (byte)0,
                            this.textBox9.Text.Trim().Length > 0 ? this.textBox9.Text.Trim() : null, (this.comboBox9.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox9.Text.Trim())) ? byte.Parse(this.comboBox9.Text.Trim()) : (byte)0,
                            this.textBox10.Text.Trim().Length > 0 ? this.textBox10.Text.Trim() : null, (this.comboBox10.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox10.Text.Trim())) ? byte.Parse(this.comboBox10.Text.Trim()) : (byte)0,
                            this.textBox11.Text.Trim().Length > 0 ? this.textBox11.Text.Trim() : null, (this.comboBox11.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox11.Text.Trim())) ? byte.Parse(this.comboBox11.Text.Trim()) : (byte)0,
                            this.textBox12.Text.Trim().Length > 0 ? this.textBox12.Text.Trim() : null, (this.comboBox12.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox12.Text.Trim())) ? byte.Parse(this.comboBox12.Text.Trim()) : (byte)0,
                            this.textBox13.Text.Trim().Length > 0 ? this.textBox13.Text.Trim() : null, (this.comboBox13.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox13.Text.Trim())) ? byte.Parse(this.comboBox13.Text.Trim()) : (byte)0,
                            this.textBox14.Text.Trim().Length > 0 ? this.textBox14.Text.Trim() : null, (this.comboBox14.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox14.Text.Trim())) ? byte.Parse(this.comboBox14.Text.Trim()) : (byte)0,
                            this.textBox15.Text.Trim().Length > 0 ? this.textBox15.Text.Trim() : null, (this.comboBox15.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox15.Text.Trim())) ? byte.Parse(this.comboBox15.Text.Trim()) : (byte)0,
                            this.textBox16.Text.Trim().Length > 0 ? this.textBox16.Text.Trim() : null, (this.comboBox16.Text.Trim().Length > 0 && RegularHelper.isNumericString(this.comboBox16.Text.Trim())) ? byte.Parse(this.comboBox16.Text.Trim()) : (byte)0

                            ))
                    {
                        ShowLoginTip("产品信息添加成功.");
                    }
                    else
                    {
                        SpMessageBox.Show(this.txt_ProductName.Text.Trim() + "信息添加失败.", "产品信息维护");
                    }
                }
                catch
                {

                }
            }
        }

    

        private void btn_Query_Click(object sender, EventArgs e)
        {
            //product info query
            if (this.txt_ProductNo.Text.Trim().Length > 0)
            {
                clearBOMText();
                List<MaterialInfo> materialsList = new List<MaterialInfo>();
                ProductBOMs _productBOMs = new ProductBOMs();
                if (this.txt_ProductNo.Text.Trim().Length > 0)
                {
                    materialsList = _productBOMs.GetProductBOMList(this.txt_ProductNo.Text.Trim());
                    materialsList = materialsList.OrderBy(o => o.MaterialSeq).ToList();//升序
                    if (materialsList.Count > 0)
                    {
                        fillMaterialsInfo(materialsList);
                        this.txt_ProductName.Text = ProductBOMBLL.GetProductName(this.txt_ProductNo.Text.Trim());
                    }
                    else
                    {
                        SpMessageBox.Show("没有查找到对应产品BOM信息, 请重新输入正确的产品号.", "产品信息维护");
                    }
                }
            }
            else
            { 
            
            }

        }


        private void clearBOMText()
        {
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            this.textBox4.Text = "";

            this.textBox5.Text = "";
            this.textBox6.Text = "";
            this.textBox7.Text = "";
            this.textBox8.Text = "";

            this.textBox9.Text = "";
            this.textBox10.Text = "";
            this.textBox11.Text = "";
            this.textBox12.Text = "";

            this.textBox13.Text = "";
            this.textBox14.Text = "";
            this.textBox15.Text = "";
            this.textBox16.Text = "";


            ///////////////////////////////////////////

            this.comboBox1.Text = "";
            this.comboBox2.Text = "";
            this.comboBox3.Text = "";
            this.comboBox4.Text = "";

            this.comboBox5.Text = "";
            this.comboBox6.Text = "";
            this.comboBox7.Text = "";
            this.comboBox8.Text = "";

            this.comboBox9.Text = "";
            this.comboBox10.Text = "";
            this.comboBox11.Text = "";
            this.comboBox12.Text = "";

            this.comboBox13.Text = "";
            this.comboBox14.Text = "";
            this.comboBox15.Text = "";
            this.comboBox16.Text = "";
        }

        private void fillMaterialsInfo(List<MaterialInfo> materialsList)
        {
            if (materialsList.Count > 0)
            {
                if (materialsList[0] != null)
                {
                    this.textBox1.Text = materialsList[0].MaterialNo.Trim();
                    this.comboBox1.Text = materialsList[0].MaterialPcs.ToString();
                }
            }
            if (materialsList.Count > 1)
            {
                if (materialsList[1] != null)
                {
                    this.textBox2.Text = materialsList[1].MaterialNo.Trim();
                    this.comboBox2.Text = materialsList[1].MaterialPcs.ToString();
                }
            }
            if (materialsList.Count > 2)
            {
                if (materialsList[2] != null)
                {
                    this.textBox3.Text = materialsList[2].MaterialNo.Trim();
                    this.comboBox3.Text = materialsList[2].MaterialPcs.ToString();
                }
            }
            if (materialsList.Count > 3)
            {
                if (materialsList[3] != null)
                {
                    this.textBox4.Text = materialsList[3].MaterialNo.Trim();
                    this.comboBox4.Text = materialsList[3].MaterialPcs.ToString();
                }
            }
            ////////////////////////
            if (materialsList.Count > 4)
            {
                if (materialsList[4] != null)
                {
                    this.textBox5.Text = materialsList[4].MaterialNo.Trim();
                    this.comboBox5.Text = materialsList[4].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 5)
            {
                if (materialsList[5] != null)
                {
                    this.textBox6.Text = materialsList[5].MaterialNo.Trim();
                    this.comboBox6.Text = materialsList[5].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 6)
            {
                if (materialsList[6] != null)
                {
                    this.textBox7.Text = materialsList[6].MaterialNo.Trim();
                    this.comboBox7.Text = materialsList[6].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 7)
            {
                if (materialsList[7] != null)
                {
                    this.textBox8.Text = materialsList[7].MaterialNo.Trim();
                    this.comboBox8.Text = materialsList[7].MaterialPcs.ToString();
                }
            }

            //////////////////////////////

            if (materialsList.Count > 8)
            {
                if (materialsList[0] != null)
                {
                    this.textBox9.Text = materialsList[8].MaterialNo.Trim();
                    this.comboBox9.Text = materialsList[8].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 9)
            {
                if (materialsList[9] != null)
                {
                    this.textBox10.Text = materialsList[9].MaterialNo.Trim();
                    this.comboBox10.Text = materialsList[9].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 10)
            {
                if (materialsList[10] != null)
                {
                    this.textBox11.Text = materialsList[10].MaterialNo.Trim();
                    this.comboBox11.Text = materialsList[10].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 11)
            {
                if (materialsList[11] != null)
                {
                    this.textBox12.Text = materialsList[11].MaterialNo.Trim();
                    this.comboBox12.Text = materialsList[11].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 12)
            {
                if (materialsList[12] != null)
                {
                    this.textBox13.Text = materialsList[12].MaterialNo.Trim();
                    this.comboBox13.Text = materialsList[12].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 13)
            {
                if (materialsList[13] != null)
                {
                    this.textBox14.Text = materialsList[13].MaterialNo.Trim();
                    this.comboBox14.Text = materialsList[13].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 14)
            {
                if (materialsList[14] != null)
                {
                    this.textBox15.Text = materialsList[14].MaterialNo.Trim();
                    this.comboBox15.Text = materialsList[14].MaterialPcs.ToString();
                }
            }

            if (materialsList.Count > 15)
            {
                if (materialsList[15] != null)
                {
                    this.textBox16.Text = materialsList[15].MaterialNo.Trim();
                    this.comboBox16.Text = materialsList[15].MaterialPcs.ToString();
                }
            }
        }

        private void ShowLoginTip(string tipstring)
        {
            Point pt = new Point(this.btn_Add.Location.X, this.btn_Add.Location.Y + this.btn_Add.Height);
            pt.Offset(20, 20);
            _logintip.UseFading = true;
            _logintip.RemoveAll();
            _logintip.Show(tipstring, this, pt, 2000);
        }

    }
}
