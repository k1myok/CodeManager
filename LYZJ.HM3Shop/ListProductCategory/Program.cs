using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListProductCategory
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>()
                                    {
                                        "1_44",
                                        "1_45",
                                        "1_47",
                                        "2_77",
                                        "2_54",
                                        "3_42",
                                        "a_55",
                                    };

            ////进行字典分组
            //Dictionary<string, List<string>> dicStr = new Dictionary<string, List<string>>();
            ////if(dicStr.ContainsKey(key))


            //分组
            var groups = from str in list
                         group str by str.Split('_')[0]
                             into g
                             select g;

            //最终集合
            List<string> strResult = new List<string>();


            //遍历所有的分组
            foreach (var strGroup in groups)
            {
                //定义临时集合
                List<string> temp = new List<string>();

                foreach (var item in strGroup)
                {
                    //如果临时变量里面没有元素，将第一组item放进去
                    if (strResult.Count() <= 0)
                    {
                        temp.Add(item);
                    }
                    else
                    {
                        foreach (var target in strResult)
                        {
                            temp.Add(target + "," + item);
                        }
                    }
                }

                strResult.Clear();
                strResult.AddRange(temp);
            }

            for (int i = 0; i < strResult.Count; i++)
            {
                if (i % groups.Count() == 0)
                {
                    Console.Write("\n");
                }
                Console.Write(strResult[i] + "\n");
            }

            Console.ReadKey();
        }
    }
}