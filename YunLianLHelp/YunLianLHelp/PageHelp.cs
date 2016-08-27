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
        public int Cpage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int Fpage { get; set; }
        public int NextPage { get; set; }
        public int PageRow { get; set; } 
    }
}
