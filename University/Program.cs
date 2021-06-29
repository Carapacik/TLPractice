using System;

namespace University
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Available commands:");
            UniversityHelp();
            for (;;)
            {
                var command = Console.ReadLine();
                try
                {
                    switch (command)
                    {
                        case "AddStudent":
                            Console.WriteLine("Enter student name");
                            var studentName = Console.ReadLine();
                            CheckNameFormat(studentName);
                            Console.WriteLine("Enter student age");
                            var age = int.Parse(Console.ReadLine());
                            if (age is < 0 or > 150) throw new FormatException("Age must be in the range (0, 150)");

                            Queries.InsertStudent(studentName, age);
                            break;
                        case "AddInstructor":
                            Console.WriteLine("Enter instructor name");
                            var instructorName = Console.ReadLine();
                            CheckNameFormat(instructorName);

                            Queries.InsertInstructor(instructorName);
                            break;
                        case "AddCourse":
                            Console.WriteLine("Enter course name");
                            var courseName = Console.ReadLine();
                            CheckNameFormat(courseName);

                            Console.WriteLine("Available instructors");
                            var instructors = Queries.SelectInstructors();
                            foreach (var i in instructors) Console.WriteLine($"{i.InstructorId} | {i.Name}");
                            Console.WriteLine("Enter instructor id");
                            var instructorId = int.Parse(Console.ReadLine());
                            CheckId(instructorId, instructors.Count);

                            Queries.InsertCourse(courseName, instructorId);
                            break;
                        case "AddGroup":
                            Console.WriteLine("Enter group name");
                            var groupName = Console.ReadLine();
                            CheckNameFormat(groupName);

                            Queries.InsertGroup(groupName);
                            break;
                        case "AddStudentToGroup":
                            Console.WriteLine("Available groups");
                            foreach (var g in Queries.SelectGroups()) Console.WriteLine($"{g.GroupName}");
                            Console.WriteLine("Enter group name");
                            var groupNameForStudent = Console.ReadLine();

                            Console.WriteLine("Available students");
                            var students = Queries.SelectStudents();
                            foreach (var s in students) Console.WriteLine($"{s.StudentId} | {s.Name}");
                            Console.WriteLine("Enter student id");
                            var studentId = int.Parse(Console.ReadLine());
                            CheckId(studentId, students.Count);

                            Queries.InsertGroup(groupNameForStudent, studentId);
                            break;
                        case "AddGroupToCourse":
                            Console.WriteLine("Available groups");
                            var gr = Queries.SelectGroups();
                            foreach (var g in gr) Console.WriteLine($"{g.GroupName}");
                            Console.WriteLine("Enter group name");
                            var groupNameForCourse = Console.ReadLine();
                            if (gr.Find(x => x.GroupName == groupNameForCourse) == null)
                                throw new ArgumentNullException("This group does not exist");

                            Console.WriteLine("Available courses");
                            var courses = Queries.SelectCourses();
                            foreach (var c in courses) Console.WriteLine($"{c.Name}");
                            Console.WriteLine("Enter course name");
                            var courseNameForGroup = Console.ReadLine();
                            var courseItem = courses.Find(x => x.Name == courseNameForGroup);
                            if (courseItem == null) throw new ArgumentNullException("This course does not exist");

                            Queries.InsertCourse(courseNameForGroup, courseItem.InstructorId, groupNameForCourse);
                            break;

                        case "EditInstructorOnCourse":
                            Console.WriteLine("Available instructors");
                            var instructorsForCourse = Queries.SelectInstructors();
                            foreach (var i in instructorsForCourse) Console.WriteLine($"{i.InstructorId} | {i.Name}");
                            Console.WriteLine("Enter instructor id");
                            var instructorIdForCourse = int.Parse(Console.ReadLine());
                            CheckId(instructorIdForCourse, instructorsForCourse.Count);

                            Console.WriteLine("Available courses");
                            var coursesForInstructorsEdit = Queries.SelectCourses();
                            foreach (var c in coursesForInstructorsEdit) Console.WriteLine($"{c.Name}");
                            Console.WriteLine("Enter course name");
                            var courseNameForInstructorsEdit = Console.ReadLine();
                            if (coursesForInstructorsEdit.Find(x => x.Name == courseNameForInstructorsEdit) == null)
                                throw new ArgumentNullException("This course does not exist");

                            Queries.UpdateInstructorOnCourse(instructorIdForCourse, courseNameForInstructorsEdit);
                            break;
                        case "PrintCoursesReport":
                            break;
                        case "PrintGeneralReport":
                            break;
                        case "Help":
                            UniversityHelp();
                            continue;
                        case "0":
                            break;
                        case "End":
                            break;
                        default:
                            Console.WriteLine("There is no such command");
                            continue;
                    }

                    if (command is "0" or "End") break;
                    Console.WriteLine("Success\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void UniversityHelp()
        {
            Console.WriteLine(
                "  AddStudent\n  AddInstructor\n  AddCourse\n  AddGroup\n" +
                "  AddStudentToGroup\n  AddGroupToCourse\n" +
                "  PrintCoursesReport\n  PrintGeneralReport\n" +
                "  Help\n  End");
        }

        private static void CheckNameFormat(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new FormatException("Name must be not null");
        }

        private static void CheckId(int id, int count)
        {
            if (id > count) throw new IndexOutOfRangeException("There is no id");
        }
    }
}