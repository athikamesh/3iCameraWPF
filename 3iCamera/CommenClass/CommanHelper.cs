using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3iCamera
{
    class CommanHelper
    {
        public static string databasepath { get; set; }
      

        public static int Cm_CameraId { get; set; }
        public static string Cm_Devicename { get; set; }
        public static int Cm_VResolution { get; set; }
        public static int Cm_IResolution { get; set; }
        public static bool Cm_AspectRatio { get; set; }
        public static string Cm_Spath { get; set; }
        public static bool Cm_Mirror { get; set; }
    }
    class CommenMethod
    {
        public static bool Cleantemp()
        {
            bool stat = false;
            if(CommanHelper.Cm_Spath!="")
            {
                stat = true;
                if(Directory.Exists(CommanHelper.Cm_Spath)==true )
                {
                    foreach(var det in Directory.GetFiles(CommanHelper.Cm_Spath+"\\temp\\"))
                    {
                        File.Delete(det);
                    }
                }
            }
           return stat;
        }
    }
}
