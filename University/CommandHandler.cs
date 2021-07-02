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
            // _menu.AddItem("addToGroup", "Add student to group", AddToGroup);
            // _menu.AddItem("addToCourse", "Add group to course", AddToCourse);
            // _menu.AddItem("editCourseInstructor", "Change instructor on course", UpdateCourseInstructor);
            // _menu.AddItem("printCourses", "Print the total score for courses", PrintCourses);
            // _menu.AddItem("printGeneral", "Print general report", PrintGeneral);
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
            var age = int.Parse(_textReader.ReadLine());
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

            _textWriter.WriteLine("Available instructors");
            var instructors = _queries.SelectInstructors();
            foreach (var i in instructors)
                _textWriter.WriteLine($"{i.InstructorId} | {i.Name}");
            _textWriter.WriteLine("Enter instructor Id");
            var instructorId = int.Parse(_textReader.ReadLine());
            if (instructors.All(x => x.InstructorId != instructorId))
                throw new IndexOutOfRangeException("There is no such Id");

            _queries.InsertCourse(name, instructorId);
        }

        private void Exit(string command)
        {
            _menu.Exit();
        }
    }
}