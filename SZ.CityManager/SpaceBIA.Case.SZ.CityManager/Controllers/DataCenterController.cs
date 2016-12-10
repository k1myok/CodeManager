using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceBIA.Case.SZ.CityManager.Controllers
{
    public class DataCenterController : Controller
    {
        // GET: DataCenter
        public JsonResult DataSource()
        {
            var source = new List<DataSource>()
            {
                new DataSource() {
                    Title = "基础地理",
                    Children = new List<Controllers.DataSource>() {
                        new DataSource() {
                            Title = "电子地图",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "城市道路" },
                                new DataSource() { Title = "城市水系" },
                                new DataSource() { Title = "湖泊图层" },
                                new DataSource() { Title = "注记图层" },
                                new DataSource() { Title = "行政区划" },
                                new DataSource() { Title = "城市桥梁" }
                            }
                        },
                        new DataSource() {
                            Title = "行政区划",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "蓬朗街道" },
                                new DataSource() { Title = "兵希街道" },
                                new DataSource() { Title = "青阳街道" },
                                new DataSource() { Title = "长江街道" },
                                new DataSource() { Title = "中华园街道" },
                                new DataSource() { Title = "综保区" }
                            }
                        },
                        new DataSource() {
                            Title = "卫星影像",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "0.1米" }
                            }
                        },
                        new DataSource() {
                            Title = "三维建模",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "30平方公里" },
                                new DataSource() { Title = "地上建筑" },
                                new DataSource() { Title = "地下管线" }
                            }
                        },
                        new DataSource() {
                            Title = "地名地址",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "开发区全区域" }
                            }
                        }
                    }
                },
                new DataSource() {
                    Title = "业务专题库",
                    Children = new List<Controllers.DataSource>() {
                        new DataSource() {
                            Title = "规划专题",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "总规" },
                                new DataSource() { Title = "控规" }
                            }
                        },
                        new DataSource() {
                            Title = "绿化专题",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "绿地" },
                                new DataSource() { Title = "树木" }
                            }
                        },
                        new DataSource() {
                            Title = "市政专题",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "道路" },
                                new DataSource() { Title = "桥梁" }
                            }
                        },
                        new DataSource() {
                            Title = "房产专题",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "小区" },
                                new DataSource() { Title = "楼栋" },
                                new DataSource() { Title = "户室" }
                            }
                        },
                        new DataSource() {
                            Title = "地下管线",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "供水" },
                                new DataSource() { Title = "污水" }
                            }
                        },
                        new DataSource() {
                            Title = "燃气专题",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "管线" },
                                new DataSource() { Title = "企业" }
                            }
                        },
                        new DataSource() {
                            Title = "项目管理",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "项目" },
                                new DataSource() { Title = "造价" }
                            }
                        },
                        new DataSource() {
                            Title = "建工专题",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "钻孔" },
                                new DataSource() { Title = "地质层" }
                            }
                        },
                        new DataSource() {
                            Title = "房屋征收",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "民房征收" },
                                new DataSource() { Title = "企业征收" },
                                new DataSource() { Title = "征收红线" }
                            }
                        },
                        new DataSource() {
                            Title = "规划监察",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "违章企业" }
                            }
                        }
                    }
                },
                new DataSource() {
                    Title = "行业专题库",
                    Children = new List<Controllers.DataSource>() {
                        new DataSource() {
                            Title = "人口库",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "总计人口：10万" },
                                new DataSource() { Title = "常驻人口：N万" },
                                new DataSource() { Title = "流动人口：N万" },
                                new DataSource() { Title = "男性人口：N万" },
                                new DataSource() { Title = "女性人口：N万" }
                            }
                        },
                        new DataSource() {
                            Title = "企业库",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "企业数量：N家" },
                                new DataSource() { Title = "国内企业：N家" },
                                new DataSource() { Title = "外资企业：N家" },
                                new DataSource() { Title = "合资企业：N家" },
                                new DataSource() { Title = "个体工商户：N家" }
                            }
                        },
                        new DataSource() {
                            Title = "经济库",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "生产总值：N亿" },
                                new DataSource() { Title = "第三产业：N亿" },
                                new DataSource() { Title = "一般公共预算收入：N亿" },
                                new DataSource() { Title = "公共财政预算支出：N亿" },
                                new DataSource() { Title = "工业总产值：N亿" }
                            }
                        },
                        new DataSource() {
                            Title = "信用库",
                            Children = new List<Controllers.DataSource>() {
                                new DataSource() { Title = "违章企业：N家" },
                                new DataSource() { Title = "同比增长：N%" }
                            }
                        }
                    }
                }
            };
            return Json(source, JsonRequestBehavior.AllowGet);
        }
    }

    public class DataSource
    {
        public string Title { get; set; }

        public List<DataSource> Children { get; set; }
    }
}