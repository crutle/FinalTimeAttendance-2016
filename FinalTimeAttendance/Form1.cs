using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FinalTimeAttendance
{
    public partial class Form1 : Form
    {
        string displayInput, displayLine;
        string searchResult, searchDateResult, checkInResult;
        string displayTime, displayDay, displayDate;
        string dayNow, dateToday;
        string getCheckInTime, getCheckOutTime;
        string collectedStaffId, collectedStaffName;
        string tableName;

        bool newResult, checkInTimeResult, checkOutTimeResult;
        bool canCheckOut, canCheckIn;

        List<Staff> staffIdList;

        public Form1()
        {
            InitializeComponent();
            StartTimer();     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            displayInput = "";


            //Create new row for the day
            dayNow = DateTime.Today.DayOfWeek.ToString();
            dateToday = DateTime.Today.Date.ToString("yyyy-MM-dd");

            //Get All StaffId, put in list
            staffIdList = getAllStaff();
            Staff staff = new Staff();
            
            foreach(Staff id in staffIdList)
            {
                //Get StaffId
                collectedStaffId = id.StaffId.ToString();
                collectedStaffName = id.StaffName;
                
                //Check the relevant table

                searchDateResult = CheckRowRecord(dateToday, collectedStaffName);
                //Does not exists = 0
                if (searchDateResult.Contains("0"))
                {
                    //Add new row
                    newResult = AddNewRow(collectedStaffName, dayNow, collectedStaffId);
                }
        }//end of foreach

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 1;
            }
            else
            {
                displayInput = displayInput + 1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 2;
            }
            else
            {
                displayInput = displayInput + 2;
            }
        }
   
        private void button3_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 3;
            }
            else
            {
                displayInput = displayInput + 3;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 4;
            }
            else
            {
                displayInput = displayInput + 4;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 5;
            }
            else
            {
                displayInput = displayInput + 5;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 6;
            }
            else
            {
                displayInput = displayInput + 6;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 7;
            }
            else
            {
                displayInput = displayInput + 7;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 8;
            }
            else
            {
                displayInput = displayInput + 8;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 9;
            }
            else
            {
                displayInput = displayInput + 9;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + "#";
            }
            else
            {
                displayInput = displayInput + "#";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + 0;
            }
            else
            {
                displayInput = displayInput + 0;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (displayInput == "")
            {
                displayLine = "-----------------------------------------------";
                displayInput = displayInput + "*";
            }
            else
            {
                displayInput = displayInput + "*";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //Clear button
            displayLine = "";
            displayInput = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (displayInput.Length > 6)
            {
                displayLine = "Invalid id. Try again (Reason: more than 6 numbers)";
                displayInput = "";
            }

            if (displayInput.Length < 6)
            {
                displayLine = "Invalid id. Try again (Reason: less than 6 numbers)";
                displayInput = "";
            }


            if (displayInput.Length == 6)
            {
                if (displayInput.Contains("#")) {
                    displayLine = "Invalid id. Try again (Reason: contains special character)";
                    displayInput = "";
                }

                if (displayInput.Contains("*"))
                {
                    displayLine = "Invalid id. Try again (Reason: contains special character)";
                    displayInput = "";
                }
                //does not contain special characters
                else
                {
                    //CHECK if Staff Exist using StaffId
                    searchResult = CheckStaffRecord(displayInput);
                    //Get Name
                    foreach (Staff id in staffIdList)
                    {
                        if (id.StaffId.ToString().Contains(displayInput))
                        {
                            tableName = id.StaffName;
                        }
                        
                    }

                    //Exists
                    if (searchResult.Contains("1"))
                    {
                        //CHECK if CheckInTime is Null
                        checkInResult = CheckCheckInTime(dateToday, tableName);
                        //CHECK if can CheckIn
                        canCheckIn = CheckInTime();
                        //CHECK if can checkout
                        canCheckOut = CheckOutTime();

                        //NULL Column = Havent CheckIn [6am - 12pm]
                        if (checkInResult == "")
                        {
                            if (canCheckIn == true)
                            {
                                //Do checkin
                                bool checkinResult = UpdateStaffRowIn(displayTime.ToString(), dateToday, tableName);
                                displayLine = "Welcome and have a nice day!";
                                displayInput = "";
                            }
                            else
                            {
                                displayLine = "You have passed the time to check in attendance, try again from 7am";
                                displayInput = "";
                            }
                        }
                        //NOT NULL = checked in [12pm - 2359]
                        else
                        {
                            if (canCheckOut == true)
                            {
                                bool checkOutResult = UpdateStaffRowOut(displayTime.ToString(), dateToday, tableName);
                                getCheckInTime = CheckCheckInTime(dateToday, tableName); //Will not be null. If null, wont reach this line.
                                getCheckOutTime = CheckCheckOutTime(dateToday, tableName);

                                DateTime startTime = Convert.ToDateTime(getCheckInTime);
                                DateTime endTime = Convert.ToDateTime(getCheckOutTime);

                                double duration = (endTime - startTime).TotalSeconds;

                                TimeSpan ts = TimeSpan.FromSeconds(duration);
                                int hh = ts.Hours;
                                int mm = ts.Minutes;
                                int ss = ts.Seconds;
                                string workingHours = hh.ToString() + " Hour(s) " + mm.ToString() + " Minute(s)";

                                //Update Total WorkingHours
                                UpdateTotalWorkingHours(workingHours, dateToday, tableName);

                                displayLine = "Goodbye! See you again tomorrow!";
                                displayInput = "";
                            }
                            else
                            {
                                displayLine = "You cannot check out attendance yet, try again at 12pm";
                                displayInput = "";
                            }
                        }
                    }
                        //Does not exist
                        if (searchResult.Contains("0"))
                        {
                            displayLine = "Id does not exist";
                            displayInput = "";
                        }

                        //Error ( >1 record)
                        if (searchResult.Contains("2"))
                        {
                            displayLine = "ERROR. Please contact your administrator";
                            displayInput = "";
                        }
                    }
               
            }
        }//End of BtnOK


       
        ///////////////////////Methods/Functions////////////////////

        //Timer
        private void StartTimer()
        {
            Timer tmr = null;
            tmr = new System.Windows.Forms.Timer();
            tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Enabled = true;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            //Current Time
            displayTime = DateTime.Now.ToString("h:mm:ss tt");
            displayTime.ToUpper();

            //Current Day
            displayDay = DateTime.Today.DayOfWeek.ToString();

            //Current Date
            displayDate = DateTime.Today.Date.ToString("dd/MM/yyyy");

            displayLabel.Text = displayDate + "                       " + displayDay;
            displayLabel.Text += Environment.NewLine;
            displayLabel.Text += "                   WELCOME";
            displayLabel.Text += Environment.NewLine;
            displayLabel.Text += "                   " + displayTime;
            displayLabel.Text += Environment.NewLine;
            displayLabel.Text += Environment.NewLine;
            displayLabel.Text += displayLine;
            displayLabel.Text += Environment.NewLine;
            displayLabel.Text += displayInput;
        }



        //Check if staff exists
        public string CheckStaffRecord(string inStaffId)
        {
            string result;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cmd.Connection = cn;
            cmd.CommandText = "SELECT COUNT(*) FROM Fingerprints Where StaffId =";
            cmd.CommandText += "@inStaffId";

            cmd.Parameters.Add("@inStaffId", SqlDbType.Int).Value = inStaffId;

            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                result = cmd.ExecuteScalar().ToString();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return "0";
            }


        }//end if Check Staff exists

        //Check if row exists
        public string CheckRowRecord(string todayDate, string inStaffName)
        {
            string result;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cmd.Connection = cn;
            
            // cmd.CommandText = "SELECT COUNT(*) FROM @inStaffId WHERE AttendanceDate = @todayDate";
            var cmdString = string.Format("SELECT COUNT(*) FROM {0} WHERE AttendanceDate = @todayDate", inStaffName);
            cmd.CommandText = cmdString;           
            
            //cmd.Parameters.Add("inStaffName", SqlDbType.VarChar,50).Value = inStaffName;
            cmd.Parameters.Add("@todayDate", SqlDbType.DateTime).Value = todayDate;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                result = cmd.ExecuteScalar().ToString();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return "0";
            }


        }//end if Check Row exists

        //Check if CheckInTime is NULL
        public string CheckCheckInTime(string todayDate, string inStaffName)
        {
            string result;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cmd.Connection = cn;
           var cmdString = string.Format("SELECT CheckedInTime FROM {0} WHERE AttendanceDate = @todayDate", inStaffName);
            cmd.CommandText = cmdString;
            
            cmd.Parameters.Add("@todayDate", SqlDbType.DateTime).Value = todayDate;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                result = cmd.ExecuteScalar().ToString();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                //IF NULL
                return "0";
            }


        }//end if CheckInTime is NULL
      
        //Check if CheckOutTime is NULL
        public string CheckCheckOutTime(string todayDate, string inStaffName)
        {
            string result;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            cmd.Connection = cn;
            var cmdString = string.Format("SELECT CheckedOutTime FROM {0} WHERE AttendanceDate = @todayDate", inStaffName);
            cmd.CommandText = cmdString;
   
            cmd.Parameters.Add("@todayDate", SqlDbType.DateTime).Value = todayDate;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                result = cmd.ExecuteScalar().ToString();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                //IF NULL
                return "0";
            }


        }//end if CheckInTime is NULL


        //CheckInTime
        public bool CheckInTime()
        {
            //Check in available from 7am to 12pm
            TimeSpan start = new TimeSpan(7, 0, 0); 
            TimeSpan end = new TimeSpan(17, 01, 0); 

            TimeSpan now = DateTime.Now.TimeOfDay;

            //6am till 2359 can checkin
            if ((now > start) && (now < end))
            {
                checkInTimeResult = true;
                return checkInTimeResult;
                //match found
            }
            else
            {
                return false;
            }
        }

        //CheckOutTime
        public bool CheckOutTime()
        {
            //Check out available from 9am(12pm) to 23 59
            TimeSpan start = new TimeSpan(9, 0, 0);
            //Till before next day
            TimeSpan end = new TimeSpan(23, 59, 0);

            TimeSpan now = DateTime.Now.TimeOfDay;

            //12pm to 2359 can checkout
            if ((now > start) && (now < end))
            {
                checkOutTimeResult = true;
                return checkOutTimeResult;
                //match found
            }
            else
            {
                return false;
            }
        }



        //Auto Generate a new Row for 1 staff
        public bool AddNewRow(string inStaffName, string inDay, string inStaffId)
        {
            int numOfRecordsAffected = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = cn;
            var cmdString = string.Format("INSERT INTO {0} (StaffId, AttendanceDate, AttendanceDay, Present) VALUES (@inStaffId ,(getdate()), @inDay, 0)", inStaffName);
            cmd.CommandText = cmdString;

            //cmd.CommandText = "INSERT INTO Staff1 (StaffId, AttendanceDate, AttendanceDay, Present) VALUES (@inStaffId ,(getdate()), @inDay, 0)";
            cmd.Parameters.Add("@inStaffId", SqlDbType.Int).Value = inStaffId;
            cmd.Parameters.Add("@inDay", SqlDbType.VarChar,20).Value = inDay;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                numOfRecordsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (numOfRecordsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }//end of AddNewRow



        //Check in (use Staff1 table for now)
        public bool UpdateStaffRowIn(string checkInTime, string inDate, string inStaffName)
        {
            int numOfRecordsAffected = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = cn;
            var cmdString = string.Format("UPDATE {0} SET CheckedInTime = @checkInTime, UpdatedAt = (getdate()) WHERE AttendanceDate = @inDate", inStaffName);
            cmd.CommandText = cmdString;

            cmd.Parameters.Add("@inDate", SqlDbType.Date).Value = inDate;
            cmd.Parameters.Add("@checkInTime", SqlDbType.DateTime).Value = checkInTime;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                numOfRecordsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (numOfRecordsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }//end of UpdateRow

        //Check Out (use Staff1 table for now)
        public bool UpdateStaffRowOut(string checkOutTime, string inDate, string inStaffName)
        {
            int numOfRecordsAffected = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = cn;
            var cmdString = string.Format("UPDATE {0} SET CheckedOutTime = @checkOutTime, Present = '1', UpdatedAt = (getdate()) WHERE AttendanceDate = @inDate", inStaffName);
            cmd.CommandText = cmdString;

            cmd.Parameters.Add("@inDate", SqlDbType.Date).Value = inDate;
            cmd.Parameters.Add("@checkOutTime", SqlDbType.DateTime).Value = checkOutTime;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                numOfRecordsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (numOfRecordsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }//end of UpdateRow

        //TotalWorkingHours (use Staff1 table for now)
        public bool UpdateTotalWorkingHours(string totalWorkingHours, string inDate, string inStaffName)
        {
            int numOfRecordsAffected = 0;
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = cn;
            var cmdString = string.Format("UPDATE {0} SET TotalWorkingHours = @totalworkingHours, UpdatedAt = (getdate()) WHERE AttendanceDate = @inDate", inStaffName);
            cmd.CommandText = cmdString;

            cmd.Parameters.Add("@totalworkingHours", SqlDbType.VarChar, 50).Value = totalWorkingHours;
            cmd.Parameters.Add("@inDate", SqlDbType.Date).Value = inDate;
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";

            cn.Open();

            try
            {
                numOfRecordsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();

            if (numOfRecordsAffected == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }//end of UpdateRow



        //Get All StaffId From Staff
        public List<Staff> getAllStaff()
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection cn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
       
          //  List<String> staffList = new List<String>();
          //  da.SelectCommand = cmd;
            cmd.Connection = cn;
            
            cmd.CommandText = "SELECT StaffId, TableName FROM Staff WHERE Administrator = 0";
            cn.ConnectionString = "server=DIT-NB1431533\\SQLEXPRESS; database=FYP_MarkIn_DB;";
            cn.ConnectionString += "integrated security=true;";
            List<Staff> staffList = new List<Staff>();
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                
                Staff staff;

                while (dr.Read())
                {
                    staff = new Staff();
                    staff.StaffId = int.Parse(dr["StaffId"].ToString());
                    staff.StaffName= dr["TableName"].ToString();
                    staffList.Add(staff);
                }

                /*da.Fill(ds, "StaffData");
                foreach (DataRow dr in ds.Tables["StaffData"].Rows)
                {
                    Staff staff = new Staff();
                    staff.StaffId = Int32.Parse(dr["StaffId"].ToString());
                    staffList.Add(staff);
                }*/

            }
            catch (SqlException sqlEx)
            {
                throw new System.ArgumentException(sqlEx.Message);
            }

            //Send the Sql to the database for processing
            cn.Close();
            return staffList;

            

        }//end Get All StaffId From Staff

    }




    public class Staff
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
    }
}
