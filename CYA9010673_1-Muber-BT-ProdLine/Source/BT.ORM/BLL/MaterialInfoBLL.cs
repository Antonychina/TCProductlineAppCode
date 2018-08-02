using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GP.MAGICL6800.ORM.Mubea_BTDataSetTableAdapters;

/// <summary>
///  Data Table Result Operations define
///  Include Query Update Insert and Delete
/// </summary>
namespace GP.MAGICL6800.ORM
{

    public class MaterialInfoBLL
    {
        private static  MaterialsInfoTableAdapter _materialInfoAdapter;

        private static Mubea_BTDataSet.MaterialsInfoDataTable _materialInfoTable;
        private static Mubea_BTDataSet.MaterialsInfoDataTable _materialNameTable;

        /// <summary>
        ///  common query, just use applydate as the filter 
        /// </summary>
        /// <param name="_applyDate"></param>
        /// <returns></returns>
        public static Mubea_BTDataSet.MaterialsInfoDataTable GetMaterialInfo(string _materialNo)
        {
            _materialInfoTable = _materialInfoAdapter.GetMaterialInfoByMaterialNo(_materialNo);

            return _materialInfoTable;
        }

        public static Mubea_BTDataSet.MaterialsInfoDataTable GetMaterialInfos()
        {
            _materialInfoTable = _materialInfoAdapter.GetData();

            return _materialInfoTable;
        }


        static MaterialInfoBLL()
        {
            _materialInfoAdapter = new MaterialsInfoTableAdapter();
        }

        public static string GetMaterialName(string _materialNo)
        {
            _materialNameTable = _materialInfoAdapter.GetMaterialNameByMaterialNo(_materialNo);

            foreach (var val in _materialNameTable)
            {
                if (!val.IsMaterialNameNull())
                {
                    return val.MaterialName;
                }
            }
            return "";
        }

        public static byte GetMaterialPostion(string _materialNo)
        {
            _materialNameTable = _materialInfoAdapter.GetMaterialNameByMaterialNo(_materialNo);

            foreach (var val in _materialNameTable)
            {
                if (val.Position > 0 && val.Position < 20)
                {
                    return val.Position;
                }
            }
            return 0xFF;
        }


        public static bool InsertMaterial(string _materialNo, string _materialName, byte _position, int _countPerBox, int _usageTimePerBox)
        {
            try
            {
                _materialInfoAdapter.InsertMaterialInfo(_materialNo, _materialName, _countPerBox, _usageTimePerBox, 0, _position);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            
            return true;
        }


        public static bool UpdateMaterial(string _materialNo, string _materialName, byte _position, int _countPerBox, int _usageTimePerBox)
        {
            try
            {
                _materialInfoAdapter.UpdateMaterialInfo(_materialName, _countPerBox, _usageTimePerBox, _position, _materialNo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }


            return true;
        }
 
    }
}
