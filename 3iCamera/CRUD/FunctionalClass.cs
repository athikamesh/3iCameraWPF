using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace _3iCamera
{
    class FunctionalClass
    {
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
    }
}
