using System;
using System.IO;
using System.Linq;

namespace University
{
    public class CommandHandler
    {
        private readonly Menu _menu;
        private readonly IQuery _queries;
        private readonly TextReader _textReader;
        private readonly TextWriter _textWriter;

        public CommandHandler(TextWriter textWriter, TextReader textReader)
        {
            _textWriter = textWriter;
            _textReader = textReader;
            _queries = new Queries();
            _menu = new Menu(_textWriter, _textReader);
            _menu.AddItem("help", "Show help", UniversityHelp);
            _menu.AddItem("addInstructor", "Add instructor to DB", InsertInstructor);
            _menu.AddItem("addGroup", "Add group to DB", InsertGroup);
            _menu.AddItem("addStudent", "Add student to DB", InsertStudent);
            _menu.AddItem("addCourse", "Add course to DB", InsertCourse);
            _menu.AddItem("addToGroup", "Add student to group", AddToGroup);
            _menu.AddItem("addToCourse", "Add group to course", AddToCourse);
            _menu.AddItem("editCourseInstructor", "Change instructor on course", UpdateCourseInstructor);
            _menu.AddItem("printCourses", "Print the total score for courses", PrintCourses);
            _menu.AddItem("printGeneral", "Print general report", PrintGeneral);
            _menu.AddItem("0", "Exit program", Exit);
        }


        public void Start()
        {
            _menu.Run();
        }

        private void UniversityHelp(string command)
        {
            _menu.UniversityHelp();
        }

        private void InsertStudent(string command)
        {
            _textWriter.WriteLine("Enter student name");
            var name = _textReader.ReadLine();
            if (name == null) throw new ArgumentException("Name can not be null");

            _textWriter.WriteLine("Enter student age");
            var age = int.Parse(_textReader.ReadLine() ?? string.Empty);
            if (age is < 0 or > 150) throw new FormatException("Age must be in the range (0, 150)");

            _queries.InsertStudent(name, age);
        }

        private void InsertGroup(string command)
        {
            _textWriter.WriteLine("Enter group name");
            var name = _textReader.ReadLine();
            if (name == null) throw new ArgumentException("Name can not be null");

            _queries.InsertGroup(name);
        }

        private void InsertInstructor(string command)
        {
            _textWriter.WriteLine("Enter instructor name");
            var name = _textReader.ReadLine();
            if (name == null) throw new ArgumentException("Name can not be null");

            _queries.InsertInstructor(name);
        }

        private void InsertCourse(string command)
        {
            _textWriter.WriteLine("Enter course name");
            var name = _textReader.ReadLine();
            if (name == null) throw new ArgumentException("Name can not be null");

            var instructorId = AvailableInstructors();

            _queries.InsertCourse(name, instructorId);
        }

        private void AddToGroup(string command)
        {
            _textWriter.WriteLine("Available students");
            var students = _queries.SelectStudents();
            foreach (var s in students)
                _textWriter.WriteLine($"{s.StudentId} | {s.Name}");
            _textWriter.WriteLine("Enter student Id");
            var studentId = int.Parse(_textReader.ReadLine() ?? string.Empty);
            var student = students.Find(x => x.StudentId == studentId);
            if (student == null)
                throw new IndexOutOfRangeException("There is no such Id");
            if (student.GroupId != null)
            {
                _textWriter.WriteLine($"This student is already in the group with id = {student.GroupId}. " +
                                      "Change his group? Y/n");
                var change = _textReader.ReadLine()?.ToLower();
                if (change != "y") return;
            }

            var groupId = AvailableGroups();


            _queries.UpdateStudentGroup(groupId, studentId);
        }

        private void AddToCourse(string command)
        {
            var groupId = AvailableGroups();

            var courseId = AvailableCourses();

            _queries.InsertCourseForGroup(groupId, courseId);
        }

        private void UpdateCourseInstructor(string command)
        {
            var courseId = AvailableCourses();

            var instructorId = AvailableInstructors();

            _queries.UpdateCourseInstructor(courseId, instructorId);
        }

        private void PrintCourses(string command)
        {
            var courses = _queries.SelectStudentsPerCourse();

            foreach (var c in courses)
                _textWriter.WriteLine($"{c.CourseName} | {c.StudentsCount}");
        }

        private void PrintGeneral(string command)
        {
            var counts = _queries.GeneralReport();

            _textWriter.WriteLine($"Courses = {counts[0]}, instructors = {counts[1]}, students = {counts[2]}");
        }

        private void Exit(string command)
        {
            _menu.Exit();
        }

        private int AvailableCourses()
        {
            _textWriter.WriteLine("Available courses");
            var courses = _queries.SelectCourses();
            foreach (var c in courses)
                _textWriter.WriteLine($"{c.CourseId} | {c.Name}");
            _textWriter.WriteLine("Enter course Id");
            var courseId = int.Parse(_textReader.ReadLine() ?? string.Empty);
            if (courses.All(x => x.CourseId != courseId))
                throw new IndexOutOfRangeException("There is no such Id");

            return courseId;
        }

        private int AvailableInstructors()
        {
            _textWriter.WriteLine("Available instructors");
            var instructors = _queries.SelectInstructors();
            foreach (var i in instructors)
                _textWriter.WriteLine($"{i.InstructorId} | {i.Name}");
            _textWriter.WriteLine("Enter instructor Id");
            var instructorId = int.Parse(_textReader.ReadLine() ?? string.Empty);
            if (instructors.All(x => x.InstructorId != instructorId))
                throw new IndexOutOfRangeException("There is no such Id");

            return instructorId;
        }

        private int AvailableGroups()
        {
            _textWriter.WriteLine("Available groups");
            var groups = _queries.SelectGroups();
            foreach (var g in groups)
                _textWriter.WriteLine($"{g.GroupId} | {g.Name}");
            _textWriter.WriteLine("Enter group Id");
            var groupId = int.Parse(_textReader.ReadLine() ?? string.Empty);
            if (groups.All(x => x.GroupId != groupId))
                throw new IndexOutOfRangeException("There is no such Id");

            return groupId;
        }
    }
}