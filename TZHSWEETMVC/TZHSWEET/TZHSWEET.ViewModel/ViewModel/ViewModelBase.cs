 /*  作者：       tianzh
 *  创建时间：   2012/7/31 0:03:03
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TZHSWEET.ViewModel
{
   public  class ViewModelBase
    {
        public int? CreateUserID { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? ModifyUserID { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsDeleted { get; set; }
        public string RecordStatus { get; set; }
    }
}
