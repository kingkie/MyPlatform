using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Yu3zx.PackingMonitor
{
    public class ImageHelper
    {
         /// <summary>
         /// 多张图片的上下合并
         /// </summary>
         public static Bitmap CombineImage(Bitmap img1,Bitmap img2)
         {
            if(img1 == null && img2 == null)
            {
                return null;
            }
            else if(img1 == null && img2 != null)
            {
                return img2;
            }
            else if (img1 != null && img2 == null)
            {
                return img1;
            }

            int newWidth = Math.Max(img1.Width, img2.Width);//使用最大张的图片作为
            int newHeight = img1.Height + img2.Height;
            // 初始化画布(最终的拼图画布)并设置宽高
            Bitmap bitMap = new Bitmap(newWidth, newHeight);
            // 初始化画板
            using (Graphics g = Graphics.FromImage(bitMap))
            {
                //将画布涂为白色(底部颜色可自行设置)
                g.Clear(Color.White);
                //在x=0，y=0处画上图一
                g.DrawImage(img1, 0, 0, img1.Width, img1.Height);
                //在x=0，y在图一往下10像素处画上图二
                g.DrawImage(img2, 0, img1.Height, img2.Width, img2.Height);
            }
            return bitMap;
        }


    }
}
