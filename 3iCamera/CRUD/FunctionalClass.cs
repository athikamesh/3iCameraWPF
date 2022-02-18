using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

namespace _3iCamera
{
     class FunctionalClass
    {
        #region Doctor

        SQLiteConnection Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");

        public string SaveDoctor(DoctorClass doctorclass)
        {
            string status = "Data not Insert Successfully";

            if (doctorclass!=null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("insert into tbl_doctor (Doctorname,Specality,Sedefault) values (@1,@2,@3)",Con);
                sQLiteCommand.Parameters.AddWithValue("@1", doctorclass.doctorname);
                sQLiteCommand.Parameters.AddWithValue("@2", doctorclass.speciality);
                sQLiteCommand.Parameters.AddWithValue("@3", doctorclass.setDefault);
                int i=sQLiteCommand.ExecuteNonQuery();
                if(i>0)
                {
                    status = "Data Insert Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status; 
        }

        public string UpdateDoctor(DoctorClass doctorclass)
        {
            string status = "Data not Update Successfully";

            if (doctorclass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("update tbl_doctor set Doctorname=@1,Specality=@2 where Sno=@3", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", doctorclass.doctorname);
                sQLiteCommand.Parameters.AddWithValue("@2", doctorclass.speciality);               
                sQLiteCommand.Parameters.AddWithValue("@3", doctorclass.Sno);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Update Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;
        }

        public string RemoveDoctor(int Sno)
        {
            string status = "Data not Removed Successfully";

            if (Sno>0)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("delete from tbl_doctor where Sno="+Sno, Con);              
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Removed Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;
        }

        public List<DoctorClass> ListDoctor()
        {
            List<DoctorClass> DC = new List<DoctorClass>();
            Con.Open();
            SQLiteCommand sQLiteCommand = new SQLiteCommand("Select * from tbl_doctor ", Con);
            SQLiteDataReader DR = sQLiteCommand.ExecuteReader();
           
                while(DR.Read())
                {
                    DoctorClass DRC = new DoctorClass();
                    DRC.Sno = int.Parse(DR.GetValue(0).ToString());
                    DRC.doctorname =DR.GetValue(1).ToString();
                    DRC.speciality = DR.GetValue(2).ToString();
                    DRC.setDefault = Convert.ToBoolean(DR.GetValue(3).ToString());
                    DC.Add(DRC);                  

                 }
           
            sQLiteCommand.Dispose();
            Con.Close();
            return DC;
        }

        public DoctorClass GetDoctor(int Sno)
        {
            DoctorClass DRC = new DoctorClass();
            Con.Open();
            SQLiteCommand sQLiteCommand = new SQLiteCommand("Select * from tbl_doctor where Sno=@1", Con);
            sQLiteCommand.Parameters.AddWithValue("@1", Sno);
            SQLiteDataReader DR = sQLiteCommand.ExecuteReader();

            if (DR.Read())
            {
                
                DRC.Sno = int.Parse(DR.GetValue(0).ToString());
                DRC.doctorname = DR.GetValue(1).ToString();
                DRC.speciality = DR.GetValue(2).ToString();
                DRC.setDefault = Convert.ToBoolean(DR.GetValue(3).ToString());  

            }

            sQLiteCommand.Dispose();
            Con.Close();
            return DRC;
        }

        #endregion

        #region Utility 
          
        public string SaveUtility(UtilityClass utilityClass)
        {
            string status = "Data not Save Successfully";

            if (utilityClass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("insert into tbl_Utitlity (CameraId,Devicename,VResolution,IResolution,AspectRatio,Spath,Mirror) values (@1,@2,@3,@4,@5,@6,@7)", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", utilityClass.CameraId);
                sQLiteCommand.Parameters.AddWithValue("@2", utilityClass.Devicename);
                sQLiteCommand.Parameters.AddWithValue("@3", utilityClass.VResolution);
                sQLiteCommand.Parameters.AddWithValue("@4", utilityClass.IResolution);
                sQLiteCommand.Parameters.AddWithValue("@5", utilityClass.AspectRatio.ToString());
                sQLiteCommand.Parameters.AddWithValue("@6", utilityClass.Spath);
                sQLiteCommand.Parameters.AddWithValue("@7", utilityClass.Mirror.ToString());
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Save Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;

           
        }

        public string UpdateUtility(UtilityClass utilityClass)
        {
            string status = "Data not update successfully";

            if (utilityClass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("update tbl_Utitlity set CameraId=@1,Devicename=@2,VResolution=@3,IResolution=@4,AspectRatio=@5,Spath=@6,Mirror=@7", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", utilityClass.CameraId);
                sQLiteCommand.Parameters.AddWithValue("@2", utilityClass.Devicename);
                sQLiteCommand.Parameters.AddWithValue("@3", utilityClass.VResolution);
                sQLiteCommand.Parameters.AddWithValue("@4", utilityClass.IResolution);
                sQLiteCommand.Parameters.AddWithValue("@5", utilityClass.AspectRatio.ToString());
                sQLiteCommand.Parameters.AddWithValue("@6", utilityClass.Spath);
                sQLiteCommand.Parameters.AddWithValue("@7", utilityClass.Mirror.ToString());
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Update Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;


        }

        public UtilityClass GetUtiltiy()
        {
            try
            {
                Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");
                UtilityClass DRC = new UtilityClass();
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("Select * from tbl_Utitlity", Con);
                SQLiteDataReader DR = sQLiteCommand.ExecuteReader();

                if (DR.Read())
                {

                    DRC.CameraId = int.Parse(DR.GetValue(0).ToString());
                    DRC.Devicename = DR.GetValue(1).ToString();
                    DRC.VResolution = int.Parse(DR.GetValue(2).ToString());
                    DRC.IResolution = int.Parse(DR.GetValue(3).ToString());
                    DRC.AspectRatio = Convert.ToBoolean(DR.GetValue(4).ToString());
                    DRC.Spath = DR.GetValue(5).ToString();
                    DRC.Mirror = Convert.ToBoolean(DR.GetValue(6).ToString());

                }

                sQLiteCommand.Dispose();
                Con.Close();
                return DRC;
            }
            catch(Exception ex) { return null; }
           
        }

        #endregion

        #region ReportSetting

        public string SaveReportSetting(ReportSettingClass reportSettingClass)
        {
            string status = "Data not Save Successfully";
            if (reportSettingClass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("insert into tbl_ReportData (ReportType,Hospitalname,Doctorname,Address,Phone,Mobile,Emailid,Time,Logo) values (@1,@2,@3,@4,@5,@6,@7,@8,@9)", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", reportSettingClass.ReportType);
                sQLiteCommand.Parameters.AddWithValue("@2", reportSettingClass.Hospitalname);
                sQLiteCommand.Parameters.AddWithValue("@3", reportSettingClass.Doctorname);
                sQLiteCommand.Parameters.AddWithValue("@4", reportSettingClass.Address);
                sQLiteCommand.Parameters.AddWithValue("@5", reportSettingClass.Phone);
                sQLiteCommand.Parameters.AddWithValue("@6", reportSettingClass.Mobile);
                sQLiteCommand.Parameters.AddWithValue("@7", reportSettingClass.Emailid);
                sQLiteCommand.Parameters.AddWithValue("@8", reportSettingClass.Time);
                sQLiteCommand.Parameters.AddWithValue("@9", reportSettingClass.Logo);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Save Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;
        }

        public string UpdateReportSetting(ReportSettingClass reportSettingClass)
        {
            string status = "Data not update successfully";
            if (reportSettingClass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("update tbl_ReportData set ReportType=@1,Hospitalname=@2,Doctorname=@3,Address=@4,Phone=@5,Mobile=@6,Emailid=@7,Time=@8,Logo=@9", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", reportSettingClass.ReportType);
                sQLiteCommand.Parameters.AddWithValue("@2", reportSettingClass.Hospitalname);
                sQLiteCommand.Parameters.AddWithValue("@3", reportSettingClass.Doctorname);
                sQLiteCommand.Parameters.AddWithValue("@4", reportSettingClass.Address);
                sQLiteCommand.Parameters.AddWithValue("@5", reportSettingClass.Phone);
                sQLiteCommand.Parameters.AddWithValue("@6", reportSettingClass.Mobile);
                sQLiteCommand.Parameters.AddWithValue("@7", reportSettingClass.Emailid);
                sQLiteCommand.Parameters.AddWithValue("@8", reportSettingClass.Time);
                sQLiteCommand.Parameters.AddWithValue("@9", reportSettingClass.Logo);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Update Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;
        }

        public ReportSettingClass GetReportSetting()
        {
            try
            {
                Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");
                ReportSettingClass DRC = new ReportSettingClass();
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("Select * from tbl_ReportData", Con);
                SQLiteDataReader DR = sQLiteCommand.ExecuteReader();

                if (DR.Read())
                {
                    DRC.ReportType =DR.GetValue(0).ToString();
                    DRC.Hospitalname = DR.GetValue(1).ToString();
                    DRC.Doctorname = DR.GetValue(2).ToString();
                    DRC.Address = DR.GetValue(3).ToString();
                    DRC.Phone = DR.GetValue(4).ToString();
                    DRC.Mobile = DR.GetValue(5).ToString();
                    DRC.Emailid = DR.GetValue(6).ToString();
                    DRC.Time = DR.GetValue(7).ToString();
                    const int CHUNK_SIZE = 2 * 1024;
                    byte[] buffer = new byte[CHUNK_SIZE];
                    long bytesRead;
                    long fieldOffset = 0;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        while ((bytesRead = DR.GetBytes(8, fieldOffset, buffer, 0, buffer.Length)) > 0)
                        {
                            stream.Write(buffer, 0, (int)bytesRead);
                            fieldOffset += bytesRead;
                        }
                        DRC.Logo = stream.ToArray();
                    }
                  
                }

                sQLiteCommand.Dispose();
                Con.Close();
                return DRC;
            }
            catch (Exception ex) { return null; }

        }

        #endregion

        #region Patient
        public string SavePatient(PatientClass patientClass)
        {
            string status = "Data not Save Successfully";
            if (patientClass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("insert into tbl_Patient (PatientID,FirstName,LastName,DOB,Gender,EDoctor,RDR,Archive,Address,Email,Mobile,DiagInfo,CVisit,LVisit) values (@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14)", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", patientClass.PatientID);
                sQLiteCommand.Parameters.AddWithValue("@2", patientClass.FirstName);
                sQLiteCommand.Parameters.AddWithValue("@3", patientClass.LastName);
                sQLiteCommand.Parameters.AddWithValue("@4", patientClass.DOB);
                sQLiteCommand.Parameters.AddWithValue("@5", patientClass.Gender);
                sQLiteCommand.Parameters.AddWithValue("@6", patientClass.EDR);
                sQLiteCommand.Parameters.AddWithValue("@7", patientClass.RDR);
                sQLiteCommand.Parameters.AddWithValue("@8", patientClass.Archive);
                sQLiteCommand.Parameters.AddWithValue("@9", patientClass.Address);
                sQLiteCommand.Parameters.AddWithValue("@10", patientClass.Email);
                sQLiteCommand.Parameters.AddWithValue("@11", patientClass.Mobile);
                sQLiteCommand.Parameters.AddWithValue("@12", patientClass.DiagInfo);
                sQLiteCommand.Parameters.AddWithValue("@13", patientClass.CVisit);
                sQLiteCommand.Parameters.AddWithValue("@14", patientClass.LVisit);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Save Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;
        }

        public string UpdatePatient(PatientClass patientClass)
        {
            string status = "Data not Update Successfully";
            if (patientClass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("update tbl_Patient set LastName=@1,DOB=@2,Gender=@3,EDoctor=@4,Email=@5,Mobile=@6,LVisit=@7 where PatientID=@8", Con);                         
                sQLiteCommand.Parameters.AddWithValue("@1", patientClass.LastName);
                sQLiteCommand.Parameters.AddWithValue("@2", patientClass.DOB);
                sQLiteCommand.Parameters.AddWithValue("@3", patientClass.Gender);
                sQLiteCommand.Parameters.AddWithValue("@4", patientClass.EDR);                
                sQLiteCommand.Parameters.AddWithValue("@5", patientClass.Email);
                sQLiteCommand.Parameters.AddWithValue("@6", patientClass.Mobile);               
                sQLiteCommand.Parameters.AddWithValue("@7", patientClass.LVisit);
                sQLiteCommand.Parameters.AddWithValue("@8", patientClass.PatientID);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Update Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            return status;
        }

        public string SavePatientVisit(PatientVisitClass patientvisitclass)
        {
            string status = "Data not Save Successfully";
            if (patientvisitclass != null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("insert into tbl_PatientVisit (Visitid,MRNO,PName,PAge,PGender,VDate,EDR,Proce,EEye) values (@1,@2,@3,@4,@5,@6,@7,@8,@9)", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", patientvisitclass.Visitid);
                sQLiteCommand.Parameters.AddWithValue("@2", patientvisitclass.MRNO);
                sQLiteCommand.Parameters.AddWithValue("@3", patientvisitclass.PName);
                sQLiteCommand.Parameters.AddWithValue("@4", patientvisitclass.PAge);
                sQLiteCommand.Parameters.AddWithValue("@5", patientvisitclass.PGender);
                sQLiteCommand.Parameters.AddWithValue("@6", patientvisitclass.VDate);
                sQLiteCommand.Parameters.AddWithValue("@7", patientvisitclass.EDR);
                sQLiteCommand.Parameters.AddWithValue("@8", patientvisitclass.Proce);
                sQLiteCommand.Parameters.AddWithValue("@9", patientvisitclass.EEye);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Save Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();
            }
            return status;
        }

        public List<Patientvisit_detail> GetPatientDetail()
        {
            List<Patientvisit_detail> PVD = new List<Patientvisit_detail>();
            try
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("Select * from PatientList_View", Con);
                SQLiteDataReader DR = sQLiteCommand.ExecuteReader();

                while (DR.Read())
                {
                    Patientvisit_detail DRC = new Patientvisit_detail();
                    DRC.Visitid = DR.GetValue(0).ToString();
                    DRC.MRNO = DR.GetValue(1).ToString();
                    DRC.PName = DR.GetValue(2).ToString();
                    DRC.PAge = DR.GetValue(3).ToString();
                    DRC.PGender = DR.GetValue(4).ToString();
                    DRC.VDate = DR.GetValue(5).ToString();
                    DRC.EDR = DR.GetValue(6).ToString();
                    DRC.Proce = DR.GetValue(7).ToString();
                    DRC.EEye = DR.GetValue(8).ToString();
                    DRC.Summary = DR.GetValue(9).ToString();
                    DRC.PatientFolder = DR.GetValue(10).ToString();
                    PVD.Add(DRC);

                }
                sQLiteCommand.Dispose();
                Con.Close();

            }
            catch(Exception ex) { }
            return PVD;
        }

        public bool GetPatienidStatus(string patientid)
        {
            Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");
            bool sts = false;
            try
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("Select count(*)  from tbl_Patient where PatientID=@1", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", patientid);
                var count = sQLiteCommand.ExecuteScalar();
                sQLiteCommand.Dispose();
                Con.Close();
                if (count.ToString() == "1")
                {
                    sts = true;
                }
            }
            catch(Exception ex) { }
            return sts;
        }

        public int GenerateVisitID()
        {
            int sts = 0;
            try
            {
                Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");                
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("Select visitid from tbl_PatientVisit ORDER BY Visitid DESC LIMIT 1", Con);
                var count = sQLiteCommand.ExecuteReader();
                if (count.Read())
                {
                    sts = int.Parse(count.GetValue(0).ToString());
                }
                else { sts = 1000; }
                sQLiteCommand.Dispose();
                Con.Close();
               
            }
            catch(Exception ex) { }
            return sts;
        }

        public PatientClass GetPatientinfo(string patientid)
        {
            PatientClass PC = null;
            try
            {
                
                Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");

                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("Select PatientID,FirstName,LastName,DOB,Gender,EDoctor,RDR,Archive,Address,Email,Mobile,DiagInfo,CVisit,LVisit from tbl_Patient where PatientID=@1", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", patientid);
                SQLiteDataReader count = sQLiteCommand.ExecuteReader();
                if (count.Read())
                {
                    PC = new PatientClass();
                    PC.PatientID = count.GetValue(0).ToString();
                    PC.FirstName = count.GetValue(1).ToString();
                    PC.LastName = count.GetValue(2).ToString();
                    PC.DOB = count.GetValue(3).ToString();
                    PC.Gender = count.GetValue(4).ToString();
                    PC.EDR = count.GetValue(5).ToString();
                    PC.RDR = count.GetValue(6).ToString();
                    PC.Archive = count.GetValue(7).ToString();
                    PC.Address = count.GetValue(8).ToString();
                    PC.Email = count.GetValue(9).ToString();
                    PC.Mobile = count.GetValue(10).ToString();
                    PC.DiagInfo = count.GetValue(11).ToString();
                    PC.CVisit = count.GetValue(12).ToString();
                    PC.LVisit = count.GetValue(13).ToString();
                }
                sQLiteCommand.Dispose();
                Con.Close();              
            }
            catch(Exception ex) { }
            return PC;
        }

        #endregion

        #region CameraSetting
        public string SaveCameraSetting(CameraSettingClass cameraSettingClass)
        {
            string status = "Data not Save Successfully";
            if(cameraSettingClass!=null)
            {
                Con.Open();
                SQLiteCommand sQLiteCommand = new SQLiteCommand("update tbl_CameraData set BacklightCompensation=@1,Brightness=@2,Contrast=@3,Gain=@4,Gamma=@5,Hue=@6,Saturation=@7,Sharpness=@8,WhiteBalance=@9,Exposure=@10,Focus=@11,Iris=@12,Pan=@13,Roll=@14,Tilt=@15,Zoom=@16,AWB=@17,AEX=@18 where Mode=@19", Con);
                sQLiteCommand.Parameters.AddWithValue("@1", cameraSettingClass.BacklightCompensation);
                sQLiteCommand.Parameters.AddWithValue("@2", cameraSettingClass.Brightness);
                sQLiteCommand.Parameters.AddWithValue("@3", cameraSettingClass.Contrast);
                sQLiteCommand.Parameters.AddWithValue("@4", cameraSettingClass.Gain);
                sQLiteCommand.Parameters.AddWithValue("@5", cameraSettingClass.Gamma);
                sQLiteCommand.Parameters.AddWithValue("@6", cameraSettingClass.Hue);
                sQLiteCommand.Parameters.AddWithValue("@7", cameraSettingClass.Saturation);
                sQLiteCommand.Parameters.AddWithValue("@8", cameraSettingClass.Sharpness);
                sQLiteCommand.Parameters.AddWithValue("@9", cameraSettingClass.WhiteBalance);
                sQLiteCommand.Parameters.AddWithValue("@10", cameraSettingClass.Exposure);
                sQLiteCommand.Parameters.AddWithValue("@11", cameraSettingClass.Focus);
                sQLiteCommand.Parameters.AddWithValue("@12", cameraSettingClass.Iris);
                sQLiteCommand.Parameters.AddWithValue("@13", cameraSettingClass.Pan);
                sQLiteCommand.Parameters.AddWithValue("@14", cameraSettingClass.Roll);
                sQLiteCommand.Parameters.AddWithValue("@15", cameraSettingClass.Tilt);
                sQLiteCommand.Parameters.AddWithValue("@16", cameraSettingClass.Zoom);
                sQLiteCommand.Parameters.AddWithValue("@17", cameraSettingClass.AWB);
                sQLiteCommand.Parameters.AddWithValue("@18", cameraSettingClass.AEX);
                int i = sQLiteCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    status = "Data Update Successfully";
                }
                sQLiteCommand.Dispose();
                Con.Close();
            }
            return status;
        }

        public CameraSettingClass GetCameraSetting(string TypeMode)
        {
            CameraSettingClass CSC = new CameraSettingClass();
            Con = new SQLiteConnection("Data Source = " + CommanHelper.databasepath + ";Version=3;New=False;Compress=True;");
            Con.Open();
            SQLiteCommand sQLiteCommand = new SQLiteCommand("Select * from tbl_CameraData where Mode=@1", Con);
            sQLiteCommand.Parameters.AddWithValue("@1", TypeMode);
            SQLiteDataReader count = sQLiteCommand.ExecuteReader();
            if(count.Read())
            {
                CSC.Mode = count.GetValue(1).ToString();
                CSC.BacklightCompensation = int.Parse(count.GetValue(2).ToString());
                CSC.Brightness = int.Parse(count.GetValue(3).ToString());
                CSC.Contrast = int.Parse(count.GetValue(4).ToString());
                CSC.Gain = int.Parse(count.GetValue(5).ToString());
                CSC.Gamma = int.Parse(count.GetValue(6).ToString());
                CSC.Hue = int.Parse(count.GetValue(7).ToString());
                CSC.Saturation = int.Parse(count.GetValue(8).ToString());
                CSC.Sharpness = int.Parse(count.GetValue(9).ToString());
                CSC.WhiteBalance = int.Parse(count.GetValue(10).ToString());
                CSC.Exposure = int.Parse(count.GetValue(11).ToString());
                CSC.Focus = int.Parse(count.GetValue(12).ToString());
                CSC.Iris = int.Parse(count.GetValue(13).ToString());
                CSC.Pan = int.Parse(count.GetValue(14).ToString());
                CSC.Roll = int.Parse(count.GetValue(15).ToString());
                CSC.Tilt = int.Parse(count.GetValue(16).ToString());
                CSC.Zoom = int.Parse(count.GetValue(17).ToString());
                CSC.AWB = 1;
                CSC.AEX = 1;
            }
            return CSC;
        }
        #endregion
    }
    public static class Image_Convertion
    {
        public static byte[] ImagetoByte(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        public static BitmapImage ToBitmapImage(this byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {

                BitmapImage img = new BitmapImage();                
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }


                return img;
            }
        }

      
    }

}
