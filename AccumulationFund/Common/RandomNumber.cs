using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelectProvidentFundService.Common
{
    public class RandomNumber
    {
        /// <summary>
        /// 随机数组
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] GetRandom(int minValue, int maxValue, int count)
        {
            int[] intList = new int[maxValue];
            for (int i = 0; i < maxValue; i++)
            {
                intList[i] = i + minValue;
            }
            int[] intRet = new int[count];
            int n = maxValue;
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = rand.Next(0, n);
                intRet[i] = intList[index];
                intList[index] = intList[--n];
            }

            return intRet;
        }
    }
}