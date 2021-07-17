﻿using System;
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
