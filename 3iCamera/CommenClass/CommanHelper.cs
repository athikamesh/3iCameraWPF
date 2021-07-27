using System;
using System.Collections.Generic;
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
}
