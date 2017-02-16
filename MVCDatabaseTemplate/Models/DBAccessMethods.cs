using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace BestPracticeTemplate.Models
{
    /********************************************************/
    public class MSAccessMethods
    {
        private static string connStr =
                @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=|DataDirectory|\Student.accdb";

        public static List<StudentCourse> getCourses()
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();

            try
            {
                string sql = "SELECT * FROM Course";

                DataSet ds = new DataSet();
                OleDbConnection conn = new OleDbConnection(MSAccessMethods.connStr);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                studentCourses = OracleMethods.mapStudentCourses(ds);
            }
            catch (Exception ex)
            {
            }
            return studentCourses;
        }

        public static StudentCourse getCourse(int id)
        {
            StudentCourse course = new StudentCourse();
            try
            {
                string sql = "SELECT * FROM Course " +
                                "WHERE course_no = ?";

                DataSet ds = new DataSet();
                OleDbConnection conn = new OleDbConnection(connStr);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_no", id);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                course = OracleMethods.mapStudentCourse(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
            }

            return course;
        }

        public static StudentCourse updateCourse(StudentCourse course)
        {
            string sql = "UPDATE Course SET " +
                " description = ?, " +
                " cost = ?, " +
                " prerequisite = ?, " +
                " modified_by = ?, " +
                " modified_date = ? " +
                " WHERE course_no = ?";

            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            cmd.Parameters.AddWithValue("@description", course.description);
            cmd.Parameters.AddWithValue("@cost", course.cost);
            cmd.Parameters.AddWithValue("@prerequisite", course.prerequisite);
            cmd.Parameters.AddWithValue("@modified_by", course.modified_by);
            cmd.Parameters.AddWithValue("@modified_date", course.modified_date);
            cmd.Parameters.AddWithValue("@course_no", course.course_No);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return course;
        }
    }


    /********************* MS SQL Server Methods *****************************************/
    public class MSSQLMethods
    {
        public static List<StudentCourse> getCourses ()
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();
            CourseDbContext db = new CourseDbContext();
            studentCourses = db.courses.ToList();

            return studentCourses;
        }

        public static StudentCourse getCourse(int id)
        {
            StudentCourse course = new StudentCourse();
            CourseDbContext db = new CourseDbContext();
            course = db.courses.Find(id);

            return course;
        }


        public static StudentCourse updateCourse(StudentCourse course)
        {
            CourseDbContext db = new CourseDbContext();
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
            
            return course;
        }

        public static StudentCourse updateCourseSQL(StudentCourse course)
        {
            string sql = "UPDATE Course SET " +
                " description = @description, " +
                " cost = @cost, " +
                " prerequisite = @prerequisite, " +
                " modified_by = @modified_by, " +
                " modified_date = @modified_date " +
                " WHERE course_no = @course_no;";
            CourseDbContext db = new CourseDbContext();
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter("@description", course.description));
            sqlParams.Add(new SqlParameter("@cost", course.cost));
            sqlParams.Add(new SqlParameter("@prerequisite", course.prerequisite));
            sqlParams.Add(new SqlParameter("@modified_by", course.modified_by));
            sqlParams.Add(new SqlParameter("@modified_date", course.modified_date));
            sqlParams.Add(new SqlParameter("@course_no", course.course_No));

            
            db.Database.ExecuteSqlCommand(sql, sqlParams.ToArray());

            return course;
        }
    }

    /********************* Oracle  Methods  **************************************/

    public class OracleMethods
    {
        private static string connStr = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION="
                           + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=titan.cs.weber.edu)(PORT=1521)))"
                           + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=titan)));"
                           + "Locale Identifier=1033;"
                           + "User Id=CS4790Student;Password=aspnetmvc;";

        public static List<StudentCourse> getCourses()
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();

            try
            {
                string sql = "SELECT * FROM Course";

                DataSet ds = new DataSet();
                OleDbConnection conn = new OleDbConnection(connStr);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                studentCourses = mapStudentCourses(ds);
            }
            catch (Exception ex)
            {
            }
            return studentCourses;
        }


        public static StudentCourse getCourse(int id)
        {
            StudentCourse course = new StudentCourse();
            try
            {
                string sql = "SELECT * FROM Course " +
                                "WHERE course_no = ?";

                DataSet ds = new DataSet();
                OleDbConnection conn = new OleDbConnection(connStr);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@course_no", id);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(ds);
                course = mapStudentCourse(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
            }

            return course;
        }

        public static StudentCourse updateCourse(StudentCourse course)
        {
            string sql = "UPDATE Course SET " +
                " description = ?, " +
                " cost = ?, " +
                " prerequisite = ?, " +
                " modified_by = ?, " +
                " modified_date = ? " +
                " WHERE course_no = ?";

            OleDbConnection conn = new OleDbConnection(connStr);
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            cmd.Parameters.AddWithValue("@description", course.description);
            cmd.Parameters.AddWithValue("@cost", course.cost);
            cmd.Parameters.AddWithValue("@prerequisite", course.prerequisite);
            cmd.Parameters.AddWithValue("@modified_by", course.modified_by);
            cmd.Parameters.AddWithValue("@modified_date", course.modified_date);
            cmd.Parameters.AddWithValue("@course_no", course.course_No);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return course;
        }

        /************************** Shared Methods ********************************/

        public static StudentCourse mapStudentCourse(DataRow row)
        {
            StudentCourse studentCourse = new StudentCourse();
            studentCourse.course_No = int.Parse(row["Course_no"].ToString());
            studentCourse.description = row["Description"].ToString();
            if (row["Cost"] != DBNull.Value) {
                studentCourse.cost = Decimal.Parse(row["Cost"].ToString());
            }
            if (row["Prerequisite"] != DBNull.Value)
            {
                studentCourse.prerequisite = int.Parse(row["Prerequisite"].ToString());
            }
            studentCourse.created_by = row["Created_By"].ToString();
            studentCourse.created_date = Convert.ToDateTime(row["Created_Date"].ToString());
            studentCourse.modified_by = row["Modified_By"].ToString();
            studentCourse.modified_date = Convert.ToDateTime(row["Modified_Date"].ToString());

            return studentCourse;
        }

        public static List<StudentCourse> mapStudentCourses(DataSet ds)
        {
            List<StudentCourse> studentCourses = new List<StudentCourse>();

            try
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    StudentCourse studentCourse = new StudentCourse();
                    studentCourse.course_No = int.Parse(row["Course_no"].ToString());
                    studentCourse.description = row["Description"].ToString();
                    if (row["Cost"] != DBNull.Value) {
                        studentCourse.cost = Decimal.Parse(row["Cost"].ToString());
                    }
                    if (row["Prerequisite"] != DBNull.Value)
                    {
                        studentCourse.prerequisite = int.Parse(row["Prerequisite"].ToString());
                    }
                    studentCourse.created_by = row["Created_By"].ToString();
                    studentCourse.created_date = Convert.ToDateTime(row["Created_Date"].ToString());
                    studentCourse.modified_by = row["Modified_By"].ToString();
                    studentCourse.modified_date = Convert.ToDateTime(row["Modified_Date"].ToString());
                    studentCourses.Add(studentCourse);
                }
            } catch (Exception ex)
            {

            }

            return studentCourses;

        } 

    }
}

