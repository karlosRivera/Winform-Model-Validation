using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinFormModelValidation
{
    public class Utilities
    {
        public static Icon IconFromFilePath(bool IsError)
        {
            string strPath;
            Bitmap bitmap = null;
            Icon ico = null;

            #if DEBUG
                strPath = System.Reflection.Assembly.GetEntryAssembly().Location + "\\..\\..\\..\\images\\" + (IsError ? "Wrong.jpg" : "Right.jpg");
            #else
                strPath = System.Reflection.Assembly.GetEntryAssembly().Location + "\\images\\" + (IsError ? "Wrong.jpg" : "Right.jpg");
            #endif

            if (System.IO.File.Exists(strPath))
            {
                bitmap = (Bitmap)Image.FromFile(strPath);

                bitmap.MakeTransparent(Color.White);
                System.IntPtr icH = bitmap.GetHicon();
                ico = Icon.FromHandle(icH);
                bitmap.Dispose();
            }

            return ico;
        }
    }
}
