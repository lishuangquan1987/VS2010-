using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;

namespace Camera_MySelf
{
   public class Camera
    {
       public static FilterInfoCollection videodevices
       {
           get
           {
               FilterInfoCollection ff = new FilterInfoCollection(FilterCategory.VideoInputDevice);
               if (ff.Count == 0)
                   return null;
               else
                   return ff;
           }

       }
       public static VideoCaptureDevice connectDevice(FilterInfo videodevice)
       {

           if (videodevice == null)
               return null;
           VideoCaptureDevice videosource = new VideoCaptureDevice(videodevice.MonikerString);

           return videosource;
           
       }
       public static bool openDevice(VideoCaptureDevice videosource)
       {
           if (videosource == null)
               return false;
           if (!videosource.IsRunning)
           {
               videosource.Start();
               return true;
           }
           return true;
       }
       
    }
}
