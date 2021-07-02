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
            using (var connection = new SqlConnection(ConnectionString))
            {
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
        }

        public void InsertGroup(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
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
        }

        public void InsertInstructor(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
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
        }

        public void InsertCourse(string name, int instructorId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
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
        }

        public List<Instructor> SelectInstructors()
        {
            var instructors = new List<Instructor>();
            using (var connection = new SqlConnection(ConnectionString))
            {
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
            }

            return instructors;
        }
        
        // public static List<Group> SelectGroups()
        // {
        //     var groups = new List<Group>();
        //     using (var connection = new SqlConnection(ConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT [GroupName]
        //                 FROM [dbo].[Group]
        //                 GROUP BY [GroupName]";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var group = new Group {GroupName = Convert.ToString(reader["GroupName"])};
        //                     groups.Add(group);
        //                 }
        //             }
        //         }
        //     }
        //
        //     return groups;
        // }
        //
        // public static List<Student> SelectStudents()
        // {
        //     var students = new List<Student>();
        //     using (var connection = new SqlConnection(ConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT [StudentId],[Name]
        //                 FROM [dbo].[Student]";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var student = new Student
        //                     {
        //                         StudentId = Convert.ToInt32(reader["StudentId"]),
        //                         Name = Convert.ToString(reader["Name"])
        //                     };
        //                     students.Add(student);
        //                 }
        //             }
        //         }
        //     }
        //
        //     return students;
        // }
        //
        //
        // public static List<Course> SelectCourses()
        // {
        //     var courses = new List<Course>();
        //     using (var connection = new SqlConnection(ConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT [Name], [InstructorId]
        //                 FROM [dbo].[Course]
        //                 GROUP BY [Name], [InstructorId]";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var course = new Course
        //                     {
        //                         Name = Convert.ToString(reader["Name"]),
        //                         InstructorId = Convert.ToInt32(reader["InstructorId"] != DBNull.Value
        //                             ? reader["InstructorId"]
        //                             : "0")
        //                     };
        //                     courses.Add(course);
        //                 }
        //             }
        //         }
        //     }
        //
        //     return courses;
        // }
        //
        // public static void UpdateInstructorOnCourse(int instructorId, string courseName)
        // {
        //     using (var connection = new SqlConnection(ConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 UPDATE [dbo].[Course]
        //                 SET [InstructorId] = @instructorId
        //                 WHERE [Name] = @courseName";
        //
        //             cmd.Parameters.Add("@instructorId", SqlDbType.Int).Value = instructorId;
        //             cmd.Parameters.Add("@courseName", SqlDbType.NVarChar).Value = courseName;
        //
        //             cmd.ExecuteNonQuery();
        //         }
        //     }
        // }
        //
        // //Get number of students per course
        // public static List<CourseWithStudents> SelectStudentsPerCourse()
        // {
        //     var courses = new List<CourseWithStudents>();
        //     using (var connection = new SqlConnection(ConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT [Course].[Name], COUNT([Group].StudentId) AS StudentsCount 
        //                 FROM [Course]
        //                 LEFT JOIN [Group] ON [Course].[GroupName] = [Group].[GroupName]
        //                 GROUP BY [Course].[Name]
        //                 ORDER BY StudentsCount DESC";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var course = new CourseWithStudents
        //                     {
        //                         Name = Convert.ToString(reader["Name"]),
        //                         StudentsCount = Convert.ToInt32(reader["StudentsCount"] != DBNull.Value
        //                             ? reader["StudentsCount"]
        //                             : "0")
        //                     };
        //                     courses.Add(course);
        //                 }
        //             }
        //         }
        //     }
        //
        //     return courses;
        // }
        //
        // //Get students, instructors and courses quantity
        // public static List<int> GeneralReport()
        // {
        //     var quantity = new List<int>();
        //     using (var connection = new SqlConnection(ConnectionString))
        //     {
        //         connection.Open();
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT COUNT([StudentId]) AS StudentsCount 
        //                 FROM [Student]";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var studentsCount = Convert.ToInt32(reader["StudentsCount"] != DBNull.Value
        //                         ? reader["StudentsCount"]
        //                         : "0");
        //
        //                     quantity.Add(studentsCount);
        //                 }
        //             }
        //         }
        //
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT COUNT([Instructor].[InstructorId]) AS InstructorCount 
        //                 FROM [Instructor]";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var instructorCount = Convert.ToInt32(reader["InstructorCount"] != DBNull.Value
        //                         ? reader["InstructorCount"]
        //                         : "0");
        //
        //                     quantity.Add(instructorCount);
        //                 }
        //             }
        //         }
        //
        //         using (var cmd = connection.CreateCommand())
        //         {
        //             cmd.Connection = connection;
        //             cmd.CommandText = @"
        //                 SELECT COUNT(CoursesNames.[Name]) AS CoursesCount 
        //                 FROM (
        //                  SELECT [Name] 
        //                  FROM [Course]
        //                  GROUP BY [Name]
        //                 ) AS CoursesNames";
        //
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     var coursesCount = Convert.ToInt32(reader["CoursesCount"] != DBNull.Value
        //                         ? reader["CoursesCount"]
        //                         : "0");
        //
        //                     quantity.Add(coursesCount);
        //                 }
        //             }
        //         }
        //     }
        //
        //     return quantity;
        // }
    }
}