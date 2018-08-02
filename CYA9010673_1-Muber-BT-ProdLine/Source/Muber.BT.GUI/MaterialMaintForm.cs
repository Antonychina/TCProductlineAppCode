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
    public partial class MaterialMaintForm : BaseViewForm
    {

        private ToolTip _logintip = new ToolTip();

        public MaterialMaintForm()
        {
            InitializeComponent();
            
            
        }

        private void MaterialMaint_Load(object sender, EventArgs e)
        {

            Load_lsv_MaterialInfo();
          
        }

        private void Load_lsv_MaterialInfo()
        {

            this.lsv_MaterialInfo.Items.Clear();
            int seq = 1;
            foreach(var materialtemp in MaterialInfos.GetMaterialInfosList())
            {
                ListViewItem materialItem = new ListViewItem();

                materialItem.Text = (seq++).ToString();
                materialItem.SubItems.Add(materialtemp.MaterialNo.Trim());
                materialItem.SubItems.Add(materialtemp.MaterialName.Trim());
                materialItem.SubItems.Add(materialtemp.Position.ToString());
                materialItem.SubItems.Add(materialtemp.CountPerBox.ToString());
                materialItem.SubItems.Add(materialtemp.UsageTimePerBox.ToString());
           

                this.lsv_MaterialInfo.Items.Add(materialItem);
            }
        }



        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.txt_MaterialNo.Text.Trim().Length > 0 &&
                this.txt_MaterialName.Text.Trim().Length > 0 &&
                RegularHelper.isNumericString(this.txt_MaterialPos.Text.Trim()) && 
                RegularHelper.isNumericString(this.txt_CountPerBox.Text.Trim()) && 
                RegularHelper.isNumericString(this.txt_UsageTime.Text.Trim()))
            {
                if (MaterialInfoBLL.InsertMaterial(this.txt_MaterialNo.Text.Trim(), this.txt_MaterialName.Text.Trim(),
                    byte.Parse(this.txt_MaterialPos.Text.Trim()), int.Parse(this.txt_CountPerBox.Text.Trim()), int.Parse(this.txt_UsageTime.Text.Trim())))
                {
                    ShowLoginTip("零件信息添加成功.");
                    Load_lsv_MaterialInfo();
                }
                else if(MaterialInfoBLL.UpdateMaterial(this.txt_MaterialNo.Text.Trim(), this.txt_MaterialName.Text.Trim(),
                        byte.Parse(this.txt_MaterialPos.Text.Trim()), int.Parse(this.txt_CountPerBox.Text.Trim()), int.Parse(this.txt_UsageTime.Text.Trim())))
                {

                    ShowLoginTip("零件信息更新成功.");
                    
                    updateCurrentRow();
                }
                else
                {
                    SpMessageBox.Show(this.txt_MaterialName.Text.Trim() + "信息添加失败.", "零件信息维护");
                }
            }

        }


        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (this.txt_MaterialNo.Text.Trim().Length > 0 &&
               this.txt_MaterialName.Text.Trim().Length > 0 &&
               RegularHelper.isNumericString(this.txt_MaterialPos.Text.Trim()) &&
               RegularHelper.isNumericString(this.txt_CountPerBox.Text.Trim()) &&
               RegularHelper.isNumericString(this.txt_UsageTime.Text.Trim()))
            {
                if (MaterialInfoBLL.UpdateMaterial(this.txt_MaterialNo.Text.Trim(), this.txt_MaterialName.Text.Trim(),
                        byte.Parse(this.txt_MaterialPos.Text.Trim()), int.Parse(this.txt_CountPerBox.Text.Trim()), int.Parse(this.txt_UsageTime.Text.Trim())))
                {
                    ShowLoginTip("零件信息更新成功.");
                    updateCurrentRow();

                }
                else if(MaterialInfoBLL.InsertMaterial(this.txt_MaterialNo.Text.Trim(), this.txt_MaterialName.Text.Trim(),
                        byte.Parse(this.txt_MaterialPos.Text.Trim()), int.Parse(this.txt_CountPerBox.Text.Trim()), int.Parse(this.txt_UsageTime.Text.Trim())))
                {

                    ShowLoginTip("零件信息添加成功.");
                    Load_lsv_MaterialInfo();
                }
                else
                {
                    SpMessageBox.Show(this.txt_MaterialName.Text.Trim() + "信息更新失败.", "零件信息维护");
                }
            }
        }

        private void updateCurrentRow()
        {
            if (this.lsv_MaterialInfo.SelectedItems.Count == 1)
            {
                if (lsv_MaterialInfo.SelectedItems[0].SubItems[1].Text.Trim().Length > 0)
                {

                    lsv_MaterialInfo.SelectedItems[0].SubItems[1].Text = this.txt_MaterialNo.Text;
                    lsv_MaterialInfo.SelectedItems[0].SubItems[2].Text =  this.txt_MaterialName.Text ;
                    lsv_MaterialInfo.SelectedItems[0].SubItems[3].Text = this.txt_MaterialPos.Text;
                    lsv_MaterialInfo.SelectedItems[0].SubItems[4].Text = this.txt_CountPerBox.Text ;
                    lsv_MaterialInfo.SelectedItems[0].SubItems[5].Text =  this.txt_UsageTime.Text;
                }
            }
        }

        private void lsv_MaterialInfo_Click(object sender, EventArgs e)
        {
            if (this.lsv_MaterialInfo.SelectedItems.Count == 1)
            {
                if (lsv_MaterialInfo.SelectedItems[0].SubItems[1].Text.Trim().Length > 0)
                {

                    this.txt_MaterialNo.Text = lsv_MaterialInfo.SelectedItems[0].SubItems[1].Text.Trim();
                    this.txt_MaterialName.Text = lsv_MaterialInfo.SelectedItems[0].SubItems[2].Text.Trim();

                    this.txt_MaterialPos.Text = lsv_MaterialInfo.SelectedItems[0].SubItems[3].Text.Trim();
                    this.txt_CountPerBox.Text = lsv_MaterialInfo.SelectedItems[0].SubItems[4].Text.Trim();
                    this.txt_UsageTime.Text = lsv_MaterialInfo.SelectedItems[0].SubItems[5].Text.Trim();
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
