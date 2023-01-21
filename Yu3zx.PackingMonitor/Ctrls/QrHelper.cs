using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ZXing;

namespace Yu3zx.PackingMonitor.Ctrls
{
    public class QrHelper
    {
        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="img">图片数据</param>
        /// <returns>返回解码结果</returns>
        public static string RQDecode(Bitmap img)
        {
            string errText = string.Empty;
            ZXing.Result result = null;
            if (img != null)
            {
                try
                {
                    result = new BarcodeReader().Decode(img);
                }
                catch
                {
                    return errText;
                }
                if (result != null)
                {
                    return result.Text;
                }
                else
                {
                    return errText;
                }
            }
            else
            {
                return errText;
            }
        }

        public static string QrDecode(Bitmap img)
        {
            try
            {
                string decodeText = string.Empty;
                // create a barcode reader instance
                IBarcodeReader reader = new BarcodeReader();
                // load a bitmap
                //var barcodeBitmap = (Bitmap)Image.LoadFrom("C:\\sample-barcode-image.png");
                // detect and decode the barcode inside the bitmap
                var result = reader.Decode(img);
                // do something with the result
                if (result != null)
                {
                    string Text = result.BarcodeFormat.ToString();//获取的格式
                    decodeText = result.Text;
                }
                return decodeText;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 解码二维码，条码等
        /// </summary>
        /// <param name="img">图片数据</param>
        /// <param name="barCodeFormat">解码得到的格式</param>
        /// <returns>返回解码结果</returns>
        public static string QrDecode(Bitmap img, out string barCodeFormat)
        {
            barCodeFormat = BarcodeFormat.QR_CODE.ToString();
            try
            {
                string decodeText = string.Empty;
                // create a barcode reader instance
                IBarcodeReader reader = new BarcodeReader();
                // load a bitmap
                //var barcodeBitmap = (Bitmap)Image.LoadFrom("C:\\sample-barcode-image.png");
                // detect and decode the barcode inside the bitmap
                var result = reader.Decode(img);
                // do something with the result
                if (result != null)
                {
                    barCodeFormat = result.BarcodeFormat.ToString();//获取的格式
                    decodeText = result.Text;
                }
                return decodeText;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string QrDecodeByThoughtWorks(Bitmap img, out string barCodeFormat)
        {
            string decoderStr = string.Empty;
            barCodeFormat = "QR_CODE";
            try
            {
                QRCodeDecoder decoder = new QRCodeDecoder();//实例化QRCodeDecoder  

                //通过.decoder方法把颜色信息转换成字符串信息  
                decoderStr = decoder.decode(new QRCodeBitmapImage(img), System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                //throw ex;

            }

            return decoderStr;//返回字符串信息  
        }
    }
}
