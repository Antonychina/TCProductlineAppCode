using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;

namespace Mubea.GUI.CustomControl
{
    ///   <summary>   
    ///   ListViewVM provide a base class for Listview virtual mode   
    ///   </summary>   
	public class ListViewVM : ListView
	{
        #region 虚拟模式相关操作

        ///<summary>
        /// 前台行集合
        ///</summary>
        public List<ListViewItem> CurrentCacheItemsSource;

        public ListViewVM()
        {
            this.CurrentCacheItemsSource = new List<ListViewItem>();
            this.VirtualMode = true;
            this.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_RetrieveVirtualItem);
        }

        ///<summary>
        /// 重置listview集合
        ///</summary>
        ///<param name="l"></param>
        public void ReSet(IList<ListViewItem> l)
        {
            this.CurrentCacheItemsSource.Clear();
            this.CurrentCacheItemsSource = new List<ListViewItem>();
            foreach (var item in l)
            {
                this.CurrentCacheItemsSource.Add(item);
            }
            this.VirtualListSize = this.CurrentCacheItemsSource.Count;
        }

        ///<summary>
        /// 虚拟模式事件
        ///</summary>
        ///<param name="sender"></param>
        ///<param name="e"></param>
        private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (this.CurrentCacheItemsSource == null || this.CurrentCacheItemsSource.Count == 0)
            {
                return;
            }

            ListViewItem lv = this.CurrentCacheItemsSource[e.ItemIndex];
            e.Item = lv;
        }

        ///<summary>
        /// 获取选中的第一行的指定tag值
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        public string FirstSelectItemValue(string key)
        {
            int i = GetColumnsIndex(key);

            return this.CurrentCacheItemsSource[this.SelectedIndices[0]].SubItems[i].Text;
        }

        ///<summary>
        /// 获取列名的索引
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        public int GetColumnsIndex(string key)
        {
            int i = 0;
            for (; i < this.Columns.Count; i++)
            {
                if (this.Columns[i].Name == key)
                {
                    break;
                }
            }

            return i;
        }

        ///<summary>
        /// 获取选中项
        ///</summary>
        ///<returns></returns>
        public List<ListViewItem> GetSelectItem()
        {
            List<ListViewItem> l = new List<ListViewItem>();
            foreach (var item in this.SelectedIndices)
            {
                l.Add(this.CurrentCacheItemsSource[int.Parse(item.ToString())]);
            }
            return l;
        }

        ///<summary>
        /// 获取选中行的某列集合
        ///</summary>
        ///<param name="key"></param>
        ///<returns></returns>
        public List<string> GetListViewField(string key)
        {
            List<string> ids = new List<string>();

            foreach (var item in this.SelectedIndices)
            {
                string id = this.CurrentCacheItemsSource[int.Parse(item.ToString())].SubItems[GetColumnsIndex(key)].Text;
                ids.Add(id);
            }
            return ids;
        }

        #endregion
	}
}
