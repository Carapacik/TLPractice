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
        List<Group> SelectGroups();
        List<Student> SelectStudents();
        void UpdateStudentGroup(int groupId, int studentId);
        IList<Course> SelectCourses();
        void InsertCourseForGroup(int groupId, int courseId);
        List<StudentsPerCourse> SelectStudentsPerCourse();
        void UpdateCourseInstructor(int courseId, int instructorId);
        List<int> GeneralReport();
    }
}