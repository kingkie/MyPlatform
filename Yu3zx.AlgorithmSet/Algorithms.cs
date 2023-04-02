using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yu3zx.AlgorithmSet
{
    /// <summary>
    /// 
    /// </summary>
    public class Algorithms
    {
        /// <summary>
        /// 找换人民币需要的最少张数算法
        /// </summary>
        /// <param name="totalFunds">总数</param>
        /// <returns></returns>
        public static List<int> ExchangeRmb(int totalFunds)
        {
            List<int> lRmb = new List<int>();
            if (totalFunds < 1)
            {
                return lRmb;
            }
            int[] rmb = { 100, 50, 20, 10, 5, 2, 1 };//人民币面值
            int x = totalFunds;
            int count = 0;//支付的总张数
            for (int i = 0; i < rmb.Length; i++)
            {
                int use = x / rmb[i]; // 这就是当前使用人命币金额的数量
                count += use;
                for(int j=0;j < use;j++ )
                {
                    lRmb.Add(rmb[i]);
                }
                // 计算下一个人命币的数量
                x = x - use * rmb[i];
                if(x < 1)
                {
                    break;
                }
                // okk 
                Console.WriteLine($"需要的面额为{rmb[i]}的{use}张数");
            }
            return lRmb;
        }

        /// <summary>
        /// 菲不那契数列--方法一
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long FibonacciNumber1(int n)
        {
            if (n < 2)
            {
                return n;
            }
            else
            {
                Console.WriteLine("*");
                return FibonacciNumber1(n - 1) + FibonacciNumber1(n - 2);
            }
        }

        /// <summary>
        /// 菲不那契数列--方法二
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long FibonacciNumber2(int n)
        {
            // 先计算f1,在计算f2,f3，f4,f5 
            //首先定义一个变量，存储之前的结果
            long[] total = new long[n];
            if (n == 1 || n == 2)
            {
                return 1;
            }
            total[0] = 0;
            total[1] = 1;
            total[2] = 1;
            for (int i = 3; i <= n - 1; i++)
            {
                total[i] = total[i - 1] + total[i - 2];
            }
            return total[n - 1];
        }
        /// <summary>
        /// 菲不那契数列--方法三
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long FibonacciNumber3(int n)
        {
            long last = 1;
            long nextlast = 1;
            long result = 1;
            if (n == 1 || n == 2)
            {
                return 1;
            }

            for (int i = 2; i <= n - 1; i++)
            {
                result = last + nextlast;
                // 每次替换最近的两个距离的结果
                nextlast = last;
                last = result;
            }
            return result;
        }

        //max(fun(i)+fun(i-2),fun(i-1))
        public static int Rob(int[] numbers)
        {
            int n = numbers.Length;
            if (n == 0) return 0;
            if (n == 1) return 0;

            int[] dp = new int[n];
            dp[0] = numbers[0];
            dp[1] = Math.Max(dp[0], dp[1]);
            for (int i = 2; i < n; i++)
            {
                dp[i] = Math.Max(numbers[i] + dp[i - 2], dp[i - 1]);
            }
            return dp[n - 1];
        }
    }
}
