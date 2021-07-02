using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace University
{
    public class Queries : IQuery
    {
        private const string ConnectionString =
            @"Data Source=DESKTOP-PUQ06I7\SQLEXPRESS;Initial Catalog=university;Pooling=true;Integrated Security=SSPI";

        public void InsertStudent(string name, int age)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        INSERT INTO [dbo].[Student] (
                            [Name],
                            [Age])
                        VALUES (
                            @name,
                            @age)";

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@age", SqlDbType.Int).Value = age;
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertGroup(string name)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        INSERT INTO [dbo].[Group] (
                            [Name])
                        VALUES (
                            @name)";

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertInstructor(string name)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        INSERT INTO [dbo].[Instructor] (
                            [Name])
                        VALUES (
                            @name)";

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertCourse(string name, int instructorId)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        INSERT INTO [dbo].[Course] (
                            [Name],
                            [InstructorId])
                        VALUES (
                            @name,
                            @instructorId)";

                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@instructorId", SqlDbType.Int).Value = instructorId;
                cmd.ExecuteNonQuery();
            }
        }

        public List<Instructor> SelectInstructors()
        {
            var instructors = new List<Instructor>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"
                        SELECT 
                            [InstructorId],
                            [Name]
                        FROM [dbo].[Instructor]";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instructor = new Instructor
                        {
                            InstructorId = Convert.ToInt32(reader["InstructorId"]),
                            Name = Convert.ToString(reader["Name"])
                        };
                        instructors.Add(instructor);
                    }
                }
            }

            return instructors;
        }

        public List<Group> SelectGroups()
        {
            var groups = new List<Group>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"
                        SELECT 
                            [GroupId],
                            [Name]
                        FROM [dbo].[Group]";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var group = new Group
                        {
                            GroupId = Convert.ToInt32(reader["GroupId"]),
                            Name = Convert.ToString(reader["Name"])
                        };
                        groups.Add(group);
                    }
                }
            }

            return groups;
        }

        public List<Student> SelectStudents()
        {
            var students = new List<Student>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"
                        SELECT 
                            [StudentId],
                            [Name],
                            [GroupId]
                        FROM [dbo].[Student]";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            StudentId = Convert.ToInt32(reader["StudentId"]),
                            Name = Convert.ToString(reader["Name"]),
                            GroupId = Convert.ToInt32(reader["GroupId"] != DBNull.Value
                                ? reader["GroupId"]
                                : "0")
                        };
                        students.Add(student);
                    }
                }
            }

            return students;
        }

        public void UpdateStudentGroup(int groupId, int studentId)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        UPDATE [dbo].[Student]
                        SET [GroupId] = @groupId
                        WHERE 
                            [StudentId] = @studentId";

                cmd.Parameters.Add("@groupId", SqlDbType.NVarChar).Value = groupId;
                cmd.Parameters.Add("@studentId", SqlDbType.Int).Value = studentId;

                cmd.ExecuteNonQuery();
            }
        }

        public IList<Course> SelectCourses()
        {
            var courses = new List<Course>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"
                        SELECT 
                            [CourseId],
                            [Name]
                        FROM [dbo].[Course]";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var course = new Course
                        {
                            CourseId = Convert.ToInt32(reader["CourseId"]),
                            Name = Convert.ToString(reader["Name"])
                        };
                        courses.Add(course);
                    }
                }
            }

            return courses;
        }

        public void InsertCourseForGroup(int groupId, int courseId)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        INSERT INTO [dbo].[CourseForGroup] (
                            [GroupId],
                            [CourseId])
                        VALUES (
                            @groupId,
                            @courseId)";

                cmd.Parameters.Add("@groupId", SqlDbType.Int).Value = groupId;
                cmd.Parameters.Add("@courseId", SqlDbType.Int).Value = courseId;
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCourseInstructor(int courseId, int instructorId)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"
                        UPDATE [dbo].[Course]
                        SET [InstructorId] = @instructorId
                        WHERE 
                            [CourseId] = @courseId";

                cmd.Parameters.Add("@instructorId", SqlDbType.NVarChar).Value = instructorId;
                cmd.Parameters.Add("@courseId", SqlDbType.Int).Value = courseId;

                cmd.ExecuteNonQuery();
            }
        }

        public List<StudentsPerCourse> SelectStudentsPerCourse()
        {
            var courses = new List<StudentsPerCourse>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"
                    SELECT 
                        [Course].[Name], 
                        COUNT([Student].[StudentId]) AS StudentsCount
                    FROM [Course]
                    LEFT JOIN [CourseForGroup] ON 
                        [Course].[CourseId] = [CourseForGroup].[CourseId]
                    LEFT JOIN [Group] ON 
                        [CourseForGroup].[GroupId] = [Group].[GroupId]
                    LEFT JOIN [Student] ON 
                        [Student].[GroupId] = [Group].[GroupId]
                    GROUP BY 
                        [Course].[Name]";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var course = new StudentsPerCourse
                        {
                            StudentsCount = Convert.ToInt32(reader["StudentsCount"]),
                            CourseName = Convert.ToString(reader["Name"])
                        };
                        courses.Add(course);
                    }
                }
            }

            return courses;
        }

        public List<int> GeneralReport()
        {
            var counts = new List<int>();
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = @"
                    SELECT 
                        COUNT([CourseId]) AS CoursesCount, 
                        B.InstructorsCount, 
                        B.StudentsCount 
                    FROM [Course]
                    CROSS JOIN (
                        SELECT 
                            COUNT([StudentId]) AS StudentsCount, 
                            A.InstructorsCount 
                        FROM [Student]
                        CROSS JOIN (
                            SELECT 
                                COUNT([InstructorId]) AS InstructorsCount 
                            FROM [Instructor]) AS A
                            GROUP BY A.[InstructorsCount]) AS B
                        GROUP BY B.InstructorsCount, B.StudentsCount";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        counts.Add(Convert.ToInt32(reader["CoursesCount"] != DBNull.Value
                            ? reader["CoursesCount"]
                            : "0"));
                        ;
                        counts.Add(Convert.ToInt32(reader["InstructorsCount"] != DBNull.Value
                            ? reader["InstructorsCount"]
                            : "0"));
                        ;
                        counts.Add(Convert.ToInt32(reader["StudentsCount"] != DBNull.Value
                            ? reader["StudentsCount"]
                            : "0"));
                        ;
                    }
                }
            }

            return counts;
        }
    }
}