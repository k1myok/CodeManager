using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigData.ModelBuilding.Models
{
    public class Table
    {
        public string Name { get; set;}
        public string SourceType { get; set;}
        public string Database { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }

    }
}