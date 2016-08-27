using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunLianLHelp
{
    public class PageHelp
    {
        public static PageList<T> ToPageList<T>(int PageRow, int Cpage, IQueryable<T> sql)
        {
            var page = new PageList<T>();
            page.Cpage = Cpage;
            page.PageRow = PageRow;
            page.TotalCount = sql.ToList().Count();
            page.TotalPage = (int)Math.Ceiling(sql.ToList().Count() / (double)PageRow);
            page.data = sql.Skip(Cpage * PageRow).Take(PageRow).ToList();
            if (Cpage < page.TotalPage)
            {
                page.NextPage = Cpage + 1;
            }
            if (Cpage > 1)
            {
                page.Fpage = Cpage - 1;
            }
            var result = page;
            return result;
        }
    }

    public class PageList<T>:Page
    {
        public List<T> data { get; set; }
    }
    public class Page
    {

        //当前页
        public int Cpage { get; set; }
        //数据总数
        public int TotalCount { get; set; }
        //总共多少页
        public int TotalPage { get; set; }
        //前一页
        public int Fpage { get; set; }
        //后一页
        public int NextPage { get; set; }
        //每页多少行
        public int PageRow { get; set; } 
    }
    public class Page<T>
    {

        //当前页
        public int Cpage { get; set; }
        //数据总数
        public int TotalCount { get; set; }
        //总共多少页
        public int TotalPage { get; set; }
        //前一页
        public int Fpage { get; set; }
        //后一页
        public int NextPage { get; set; }
        //每页多少行
        public int PageRow { get; set; }
        public List<T> data { get; set; }
    }
}
