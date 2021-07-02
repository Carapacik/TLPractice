using System.Collections.Generic;

namespace University
{
    public interface IQuery
    {
        void InsertStudent(string name, int age);
        void InsertGroup(string name);
        void InsertInstructor(string name);
        void InsertCourse(string name, int instructorId);
        List<Instructor> SelectInstructors();
    }
}