using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gatepass
{
    internal class Utility
    {

        public static void onlyNumber(KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static String getUniqueId(String prefix)
        {
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return prefix + nano;
        }

        public static String getImageStorePath(String imageNmae)
        {
            return Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Images\\" + imageNmae + ".jpg"; 
        }

        public static void setImageInPictureBox(PictureBox pictureBoxProfile, String visitorId)
        {
            string path = getImageStorePath(visitorId);
            if(pictureBoxProfile.Image != null)
            {
                pictureBoxProfile.Image.Dispose();
                pictureBoxProfile.Image = null;
            }
            pictureBoxProfile.SizeMode =  PictureBoxSizeMode.AutoSize;
            pictureBoxProfile.Image = Image.FromFile(path);
        }
    }
}
