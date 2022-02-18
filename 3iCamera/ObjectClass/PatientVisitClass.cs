using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3iCamera
{
    public class PatientVisitClass
    {
        public int Sno { get; set; }
        public string Visitid { get; set; }
        public string MRNO { get; set; }
        public string PName { get; set; }
        public string PAge { get; set; }
        public string PDOB { get; set; }
        public string PGender { get; set; }
        public string VDate { get; set; }
        public string EDR { get; set; }
        public string Proce { get; set; }
        public string EEye { get; set; }
        public string Summary { get; set; }
        public string PatientFolder { get; set; }
    }
    public class Patientvisit_detail
    {
        public string Visitid { get; set; }
        public string MRNO { get; set; }
        public string PName { get; set; }
        public string PAge { get; set; }
        public string PGender { get; set; }
        public string VDate { get; set; }
        public string EDR { get; set; }
        public string Proce { get; set; }
        public string EEye { get; set; }
        public Image IVData { get; set; }
        public string Summary { get; set; }
        public string PatientFolder { get; set; }
    }
}
