using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using http5112_assignment4_cummulative_p2.Models;

namespace http5112_assignment4_cummulative_p2.Controllers
{
    public class TeacherController : Controller
    {

        /// <summary>
        /// Returns a list of Teachers in the system and allow us to search based on hiredate,salary,teacherfname,teacherlname and teacherfname teacherlname by providong it to view
        /// </summary>
        /// <param name="SearchKey">eg:cummings</param>
        /// <example>Teacher/List</example>
        /// <returns>
        /// A list of Teachers (teacherfname,teacherlname,teacherid,employeenumber,hiredate,salary)
        /// </returns>
        // GET: Teacher/List
        [HttpGet]
        public ActionResult List(string SearchKey)
        {


            //instanciate the datacontroller for teacher.
            TeacherDataController Controller = new TeacherDataController();

            //create teacher enumerable to store the list of teachers
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            return View(Teachers);
        }


        /// <summary>
        /// Returns a Teacher in the system based on the teacher id to the view
        /// </summary>
        /// <param name="id">eg:1</param>
        /// <example>Teacher/Show/{id}</example>
        /// <returns>
        /// A Teacher (teacherfname,teacherlname,teacherid,employeenumber,hiredate,salary)
        /// </returns>

        //get: Teacher/Show/{id}
        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Show(int id)
        {
            //instanciate the datacontroller for teacher.
            TeacherDataController Controller = new TeacherDataController();
            //create a variable to store the teacher data by id
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            //List<Classes> classforteacher = findclasses;

            return View(SelectedTeacher);
        }


        /// <summary>
        /// Returns a Teacher in the system based on the teacher id to the view
        /// </summary>
        /// <param name="id">eg:1</param>
        /// <example>Teacher/Show/{id}</example>
        /// <returns>
        /// A Teacher (teacherfname,teacherlname,teacherid,employeenumber,hiredate,salary)
        /// </returns>

        //get: Teacher/Show/{id}

        [HttpGet]
        [Route("Teacher/Show/{id}")]
        public ActionResult Update(int id)
        {
            //instanciate the datacontroller for teacher.
            TeacherDataController Controller = new TeacherDataController();
            //create a variable to store the teacher data by id
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            //List<Classes> classforteacher = findclasses;
            return View(SelectedTeacher);
        }

        /// <summary>
        /// Update page where there is an option to update the Teacher firstname,lastname,employeenumber based on their id
        /// </summary>
        /// <param name="id">eg:1</param>
        /// <example>Teacher/Update/{id}</example>


        //POST: Teacher/Update/{id}

        [HttpPost]
        [Route("Teacher/Update/{id}")]
        public ActionResult Update(int id, string teacherFName, string teachetLName, string employeeNumber)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = new Teacher();
            SelectedTeacher.teacherId = id;
            SelectedTeacher.teacherFName = teacherFName;
            SelectedTeacher.teachetLName = teachetLName;
            SelectedTeacher.employeeNumber = employeeNumber;
            Controller.UpdateTeacher(SelectedTeacher);
            return RedirectToAction("Show/" + id);
        }

        /// <summary>
        /// A page to enter details about the new Teacher
        /// </summary>
        /// <example>Teacher/New</example>
        /// <returns>
        /// A Teacher (teacherfname,teacherlname,teacherid,employeenumber,hiredate,salary)
        /// </returns>
        //GET: Teacher/New
        [HttpGet]
        [Route("Teacher/New")]
        public ActionResult New()
        {
            return View();
        }

        //POST: Teacher/Create
        [HttpPost]
        [Route("Teacher/Create")]
        public ActionResult create(string teacherFName, string teachetLName, string employeeNumber, string hiredate, decimal salary)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherFName = teacherFName;
            NewTeacher.teachetLName = teachetLName;
            NewTeacher.employeeNumber = employeeNumber;
            NewTeacher.hiredate = hiredate;
            NewTeacher.salary = salary;
            Controller.AddTeacher(NewTeacher);
            return RedirectToAction("List");
        }

        /// <summary>
        /// Confirmation page doe delete operation based on the Teacher's id
        /// </summary>
        /// <param name="id">eg:1</param>
        /// <example>Teacher/DeleteConfirm/{id}</example>


        //Get: Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// Delete page where there is an option to delete the Teacher based on their id
        /// </summary>
        /// <param name="id">eg:1</param>
        /// <example>Teacher/Delete/{id}</example>


        //POST: Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

    }
}