 /*  作者：       tianzh
 *  创建时间：   2012/7/23 0:18:34
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TZHSWEET.Common
{
   public static class TypeHelper
    {
       public static bool ObjIsInt(this object number)
       {
           //如果为空，认为验证不合格
           if (Tools.IsNullOrEmpty(number))
           {
               return false;
           }

           //清除要验证字符串中的空格
         string  strNum = number.ToString ().Trim();

           //模式字符串
           string pattern = @"^[0-9]+[0-9]*$";

           //验证
           return RegexHelper.IsMatch(strNum, pattern);
       }
       /// <summary>
       /// 验证字符串是否有sql注入字段
       /// </summary>
       /// <param name="input"></param>
       /// <returns></returns>
       public static bool IsValidInput(this object objInput)
       {
           try
           {
               if (Tools.IsNullOrEmpty(objInput))
                   return false;
               else
               {
                   string input = objInput.ToString();
                   //替换单引号
                   input = input.Replace("'", "''").Trim();

                   //检测攻击性危险字符串
                   string testString = "and |or |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                   string[] testArray = testString.Split('|');
                   foreach (string testStr in testArray)
                   {
                       if (input.ToLower().IndexOf(testStr) != -1)
                       {
                           //检测到攻击字符串,清空传入的值
                           input = "";
                           return false;
                       }
                   }

                   //未检测到攻击字符串
                   return true;
               }
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

     

    }
}
