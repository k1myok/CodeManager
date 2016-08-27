/*  作者：       tianzh
*  创建时间：   2012/7/23 16:49:20
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TZHSWEET.Entity;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// 我的收藏的UI显示ViewModel
    /// </summary>
    public class ViewModelFavorite
    {
        #region - 属性 -

        public int FavoriteID { get; set; }

        public string FavoriteTitle { get; set; }

        public string FavoriteContent { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        #endregion

        #region - 方法 -

        /// <summary>
        /// 转化为ViewModel
        /// </summary>
        /// <param name="favorite"></param>
        /// <returns></returns>
        public static ViewModelFavorite ToViewModel(tbFavorite favorite)
        {
            ViewModelFavorite item = new ViewModelFavorite();
            item.FavoriteID = favorite.FavoriteID;
            item.FavoriteTitle = favorite.FavoriteTitle;
            item.Url = favorite.Url;
            item.Icon = favorite.Icon;
            item.FavoriteContent = favorite.FavoriteContent;
            return item;

        }

        /// <summary>
        /// 转化为ViewModel 的list集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<ViewModelFavorite> ToListViewModel(IEnumerable<tbFavorite> list)
        {
            var listModel = new List<ViewModelFavorite>();
            foreach (tbFavorite item in list)
            {
                listModel.Add(ViewModelFavorite.ToViewModel(item));
            }
            return listModel;
        }

        #endregion

    }
}
