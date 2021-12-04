using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using http5112_assignment4_cummulative_p2.Models;

namespace http5112_assignment4_cummulative_p2.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the Teachers table of our School database.
        /// <summary>
        /// Returns a list of Teachers in the system and allow us to search based on hiredate,salary,teacherfname,teacherlname and teacherfname teacherlname
        /// </summary>
        /// <param name="SearchKey">eg:cummings</param>
        /// <example>api/teacherdata/listteachers/{searchkey?}</example>
        /// <returns>
        /// A list of Teachers (teacherfname,teacherlname,teacherid,employeenumber,hiredate,salary)
        /// </returns>

        [HttpGet]
        [Route("api/teacherdata/listteachers/{searchkey?}")]
        public List<Teacher> ListTeachers(string SearchKey = null)
        {
            //Debug.WriteLine(SearchKey);
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //sql query is stored in a string datatype
            string query = "Select * from teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower(concat(teacherfname, ' ', teacherlname)) like lower(@key) or salary like @key or hiredate like @key";

            //SQL QUERY
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> TeacherNames = new List<Teacher> { };


            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //create a instance of Teacher model
                Teacher AllTeacher = new Teacher();

                //Access Column information by the DB column name as an index
                AllTeacher.teacherFName = ResultSet["teacherfname"].ToString();
                AllTeacher.teachetLName = ResultSet["teacherlname"].ToString();
                AllTeacher.teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                AllTeacher.employeeNumber = ResultSet["employeenumber"].ToString();
                AllTeacher.hiredate = ResultSet["hiredate"].ToString();
                AllTeacher.salary = Convert.ToDecimal(ResultSet["salary"]);


                //Add the Teacher Name to the List
                TeacherNames.Add(AllTeacher);

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return TeacherNames;
        }


        /// <summary>
        /// Returns a Teacher in the system based on the teacher id
        /// </summary>
        /// <param name="id">eg:1</param>
        /// <example>api/teacherdata/findteacher/{id}</example>
        /// <returns>
        /// A Teacher (teacherfname,teacherlname,teacherid,employeenumber,hiredate,salary)
        /// </returns>

        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            //sql query is stored in a string datatype
            string teacherQuery = "select *  from teachers where teacherid=" + id;
            //"select * from classes,teachers where classes.teacherid=teachers.teacherid and classes.teacherid=" + id;
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = teacherQuery;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //create a instance of Teacher model
            Teacher SelectedTeacher = new Teacher();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                SelectedTeacher.teacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.teachetLName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.employeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.hiredate = ResultSet["hiredate"].ToString();
                SelectedTeacher.salary = Convert.ToDecimal(ResultSet["salary"]);

            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return SelectedTeacher;
        }

        /// <summary>
        /// Add a New Teacher into the database
        /// </summary>
        /// <param name="NewTeacher">New Teacher Object</param>

        public void AddTeacher(Teacher NewTeacher)
        {
            string query = "insert into teachers(teacherid,teacherfname,teacherlname,employeenumber,hiredate,salary) values(0,@teacherFName,@teachetLName,@employeeNumber,@hiredate,@salary)";



                //Create an instance of a connection
                MySqlConnection Conn = School.AccessDatabase();

                //Open the connection between the web server and database
                Conn.Open();

                //if (NewTeacher.teacherFName == null)
                //{
                //  Conn.Close();
                //}

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = query;

                cmd.Parameters.AddWithValue("@teacherFName", NewTeacher.teacherFName);
                cmd.Parameters.AddWithValue("@teachetLName", NewTeacher.teachetLName);
                cmd.Parameters.AddWithValue("@employeeNumber", NewTeacher.employeeNumber);
                cmd.Parameters.AddWithValue("@hiredate", NewTeacher.hiredate);
                cmd.Parameters.AddWithValue("@salary", NewTeacher.salary);



                cmd.Prepare();

                cmd.ExecuteNonQuery();

                Conn.Close();
            
        }

        /// <summary>
        /// Delete a Teacher in the database based on the teacherid provided.
        /// </summary>
        /// <param name="id">the primary key teacherid of the Teacher</param>
        public void DeleteTeacher(int id)
        {
            string query = "delete from teachers where teacherid=@id";

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }


    }
}
