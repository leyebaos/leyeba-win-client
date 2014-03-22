using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Util
{
    public class BitmapHandler
    {
        /// <summary>
        /// 返回指定图片中的非透明区域；  
        /// </summary>
        /// <param name="img">位图</param>
        /// <param name="alpha">alpha 小于等于该值的为透明</param>
        /// <param name="imgX">图片的偏移坐标X</param>
        /// <param name="imgY">图片的偏移坐标Y</param>
        /// <returns></returns>
        public static GraphicsPath GetNoneTransparencyRegion(Bitmap img, byte alpha, int imgX = 0, int imgY = 0)
        {
            int height = img.Height;
            int width = img.Width;

            int xStart, xEnd;
            GraphicsPath grpPath = new GraphicsPath();
            for (int y = 0; y < height; y++)
            {
                //逐行扫描；  
                for (int x = 0; x < width; x++)
                {
                    //略过连续透明的部分；  
                    while (x < width && img.GetPixel(x, y).A <= alpha)
                    {
                        x++;
                    }
                    //不透明部分；  
                    xStart = x + imgX;
                    while (x < width && img.GetPixel(x, y).A > alpha)
                    {
                        x++;
                    }
                    xEnd = x + imgX;
                    if (img.GetPixel(x - 1, y).A > alpha)
                    {
                        grpPath.AddRectangle(new Rectangle(xStart, y + imgY, xEnd - xStart, 1));
                    }
                }
            }
            return grpPath;
        }
        /// <summary>
        /// 获取带鼠标的屏幕
        /// </summary>
        /// <returns></returns>
        public static Bitmap PrintScreenHaveCursor()
        {
            Bitmap bmp = PrintScreen();
            Graphics gh = Graphics.FromImage(bmp);

            Win32API.CURSORINFO pci = new Win32API.CURSORINFO();
            pci.cbSize = Marshal.SizeOf(pci);
            Win32API.GetCursorInfo(out pci);
            IntPtr dc = gh.GetHdc();
            Win32API.DrawIconEx(dc, pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor, 32, 32, 1, IntPtr.Zero, Win32API.DI_NORMAL);
            gh.ReleaseHdc();

            return bmp;
        }
        /// <summary>
        /// 获取屏幕
        /// </summary>
        /// <returns></returns>
        public static Bitmap PrintScreen()
        {
            Size sz = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            IntPtr hDesk = Win32API.GetDesktopWindow();
            IntPtr hSrce = Win32API.GetWindowDC(hDesk);
            IntPtr hDest = Win32API.CreateCompatibleDC(hSrce);
            IntPtr hBmp = Win32API.CreateCompatibleBitmap(hSrce, sz.Width, sz.Height);
            IntPtr hOldBmp = Win32API.SelectObject(hDest, hBmp);
            bool b = Win32API.BitBlt(hDest, 0, 0, sz.Width, sz.Height, hSrce, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
            Bitmap bmp = Bitmap.FromHbitmap(hBmp);
            Win32API.SelectObject(hDest, hOldBmp);
            Win32API.DeleteObject(hBmp);
            Win32API.DeleteDC(hDest);
            Win32API.ReleaseDC(hDesk, hSrce);

            return bmp;
        }

        public static void SetBits(Bitmap bitmap, Point localtion, IntPtr hwnd)
        {
            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("图片必须是32位带Alhpa通道的图片。");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32API.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32API.CreateCompatibleDC(screenDC);

            try
            {
                Win32API.Point topLoc = new Win32API.Point(localtion.X, localtion.Y);
                Win32API.Size bitMapSize = new Win32API.Size(bitmap.Width, bitmap.Height);
                Win32API.BLENDFUNCTION blendFunc = new Win32API.BLENDFUNCTION();
                Win32API.Point srcLoc = new Win32API.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32API.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32API.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32API.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32API.UpdateLayeredWindow(hwnd, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32API.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32API.SelectObject(memDc, oldBits);
                    Win32API.DeleteObject(hBitmap);
                }
                Win32API.ReleaseDC(IntPtr.Zero, screenDC);
                Win32API.DeleteDC(memDc);
            }
        }

        /// <summary>
        /// 按比例缩放图片
        /// </summary>
        /// <param name="bmp">原始图片</param>
        /// <returns></returns>
        public static Bitmap ScaleZoom(Bitmap bmp, int container_Width, int container_Height)
        {
            if (bmp != null)
            {
                double zoomScale;
                if (bmp.Width > container_Width || bmp.Height > container_Height)
                {
                    double imageScale = (double)bmp.Width / (double)bmp.Height;
                    double containerScale = (double)container_Width / (double)container_Height;

                    if (imageScale > containerScale)
                    {
                        zoomScale = (double)container_Width / (double)bmp.Width;
                        return BitMapZoom(bmp, container_Width, (int)(bmp.Height * zoomScale));
                    }
                    else
                    {
                        zoomScale = (double)container_Height / (double)bmp.Height;
                        return BitMapZoom(bmp, (int)(bmp.Width * zoomScale), container_Height);
                    }
                }
            }
            return bmp;
        }
        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="bmpSource">源图片</param>
        /// <param name="bmpSize">缩放图片的大小</param>
        /// <returns>缩放的图片</returns>
        public static Bitmap BitMapZoom(Bitmap bmpSource, int bmpWidth, int bmpHeight)
        {
            Bitmap zoomBmp = null;
            try
            {
                zoomBmp = new Bitmap(bmpWidth, bmpHeight);
                using (Graphics gh = Graphics.FromImage(zoomBmp))
                {                    
                    gh.DrawImage(bmpSource, new Rectangle(0, 0, bmpWidth, bmpHeight), new Rectangle(0, 0, bmpSource.Width, bmpSource.Height), GraphicsUnit.Pixel);
                }

                return zoomBmp;
            }
            catch
            { }
            finally
            {
                bmpSource.Dispose();
                //GC.SuppressFinalize(bmpSource);
                GC.Collect();
            }
            return null;
        }
    }
}
