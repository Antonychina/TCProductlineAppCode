using System;
using System.Collections.Generic;
using System.Text;
using GP.MAGICL6800.ORM;
using System.Windows.Forms;

namespace Mubea.AutoTest
{
    /// <summary>
    /// ResultSamples and some operation of  sample result for sample query
    /// </summary>
    public class ProductBOMs
    {

        List<ProductBOM> _productBOMs;

        /// <summary>
        /// Constructure of samples2
        /// </summary>
        public ProductBOMs()
        {
            _productBOMs = new List<ProductBOM>();
        }

        /// <summary>
        /// Get Samples2
        /// </summary>
        List<ProductBOM> ProductBOMList
        {
            get { return _productBOMs; }
        }


        /// <summary>
        /// List GetSampleQryResult, get 4 COLUMNs from samplemain and patien to a list to show in listview
        /// </summary>
        /// <returns></returns>
        public List<MaterialInfo> GetProductBOMList(string _productNo)
        {
            List<MaterialInfo> productMaterialsList = new List<MaterialInfo>();

            if (productMaterialsList.Count > 0)
                productMaterialsList.Clear();

            foreach (var val in ProductBOMBLL.GetProductBOMList(_productNo))
            {
                if (val.ProductNo != "" && val.ProductNo != null)
                {
                    if(!val.IsMaterial1NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();

                        materialTemp.MaterialNo = val.Material1No;
                        materialTemp.MaterialPcs = val.Material1Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }

                    if (!val.IsMaterial2NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material2No;
                        materialTemp.MaterialPcs = val.Material2Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial3NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material3No;
                        materialTemp.MaterialPcs = val.Material3Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial4NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material4No;
                        materialTemp.MaterialPcs = val.Material4Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial5NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material5No;
                        materialTemp.MaterialPcs = val.Material5Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial6NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material6No;
                        materialTemp.MaterialPcs = val.Material6Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial7NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material7No;
                        materialTemp.MaterialPcs = val.Material7Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial8NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material8No;
                        materialTemp.MaterialPcs = val.Material8Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial9NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material9No;
                        materialTemp.MaterialPcs = val.Material9Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial10NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material10No;
                        materialTemp.MaterialPcs = val.Material10Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial11NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material11No;
                        materialTemp.MaterialPcs = val.Material11Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial12NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material12No;
                        materialTemp.MaterialPcs = val.Material12Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial13NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material13No;
                        materialTemp.MaterialPcs = val.Material13Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial14NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material14No;
                        materialTemp.MaterialPcs = val.Material14Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial15NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material15No;
                        materialTemp.MaterialPcs = val.Material15Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial16NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material16No;
                        materialTemp.MaterialPcs = val.Material16Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial17NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material17No;
                        materialTemp.MaterialPcs = val.Material17Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial18NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material18No;
                        materialTemp.MaterialPcs = val.Material18Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial19NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material19No;
                        materialTemp.MaterialPcs = val.Material19Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }


                    if (!val.IsMaterial20NoNull())
                    {
                        MaterialInfo materialTemp = new MaterialInfo();
                        materialTemp.MaterialNo = val.Material20No;
                        materialTemp.MaterialPcs = val.Material20Count;
                        materialTemp.MaterialName = MaterialInfoBLL.GetMaterialName(materialTemp.MaterialNo);
                        materialTemp.MaterialSeq = MaterialInfoBLL.GetMaterialPostion(materialTemp.MaterialNo);
                        productMaterialsList.Add(materialTemp);
                    }
                }
            }

            return productMaterialsList;
        }


        //public void CheckOrderExsit(string _orderNo, string _productNo, int _orderCount)
        //{

        //    if (!OrderInfoBLL.OrderAlreadyExsit(_orderNo))
        //    {
        //        OrderInfoBLL.OrderInfoInsert(_orderNo, _productNo, _orderCount);
        //    }
            
        //}
      
    }
}
