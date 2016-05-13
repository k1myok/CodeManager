using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataReptile.DataImport
{
    public class ReptileResult
    {
        public static ReptileResult Success = new ReptileResult() { Code = 99, Message = "成功" };
        public static ReptileResult Failed = new ReptileResult() { Code = -1, Message = "失败" };
        public static ReptileResult Empty = new ReptileResult() { Code = 0, Message = "空结果" };

        public int Code { get; set; }

        public string Message { get; set; }
    }
}
