using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigData.ModelBuilding.Models
{
    public class DataSourceType
    {
        public DataSourceType(int type, string typeName)
        {
            this.Type = type;
            this.TypeName = typeName;
        }

        public int Type { get; set; }

        public string TypeName { get; set; }


        private static List<DataSourceType> instance = null;
        public static List<DataSourceType> Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new List<DataSourceType>() { 
                        new DataSourceType(0, "表"),
                        new DataSourceType(1, "视图")
                    };

                }
                return instance;
            }
        }
    }
}