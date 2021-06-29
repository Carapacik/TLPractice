using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace University
{
    public static class Queries
    {
        private const string ConnectionString =
            @"Data Source=DESKTOP-PUQ06I7\SQLEXPRESS;Initial Catalog=university;Pooling=true;Integrated Security=SSPI";

        public static int InsertStudent(string name, int age)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Student] (
                                        [Name],
                                        [Age])
                                        VALUES (
                                        @name,
                                        @age)
                                        SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@age", SqlDbType.Int).Value = age;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int InsertInstructor(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Instructor] (
                                        [Name])
                                        VALUES (
                                        @name)
                                        SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int InsertCourse(string name, int instructorId, string groupName = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Course] (
                                        [Name],
                                        [InstructorId],
                                        [GroupName])
                                        VALUES (
                                        @name,
                                        @instructorId,
                                        @groupName)
                                        SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@instructorId", SqlDbType.Int).Value = instructorId;
                    cmd.Parameters.Add("@groupName", SqlDbType.NVarChar).Value = groupName;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static int InsertGroup(string name, int studentId = 1)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Group] (
                                        [Name],
                                        [StudentId])
                                        VALUES (
                                        @name,
                                        @studentId)
                                        SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@studentId", SqlDbType.Int).Value = studentId;
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public static List<Group> SelectGroups()
        {
            var groups = new List<Group>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"SELECT [Name]
                                    FROM [dbo].[Group]
                                    GROUP BY [Name]";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var group = new Group
                            {
                                GroupName = Convert.ToString(reader["Name"])
                            };
                            groups.Add(group);
                        }
                    }
                }
            }

            return groups;
        }

        public static List<Student> SelectStudents()
        {
            var students = new List<Student>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"SELECT [StudentId],[Name]
                                    FROM [dbo].[Student]";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                Name = Convert.ToString(reader["Name"])
                            };
                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }


        public static List<Instructor> SelectInstructors()
        {
            var instructors = new List<Instructor>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"SELECT [InstructorId],[Name]
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

        public static List<Course> SelectCourses()
        {
            var courses = new List<Course>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"SELECT [Name], [InstructorId]
                                    FROM [dbo].[Course]
                                    GROUP BY [Name], [InstructorId]";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var course = new Course
                            {
                                Name = Convert.ToString(reader["Name"]),
                                InstructorId = Convert.ToInt32(reader["InstructorId"] != DBNull.Value
                                    ? reader["InstructorId"]
                                    : "0")
                            };
                            courses.Add(course);
                        }
                    }
                }
            }

            return courses;
        }

        public static void UpdateInstructorOnCourse(int instructorId, string courseName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"UPDATE [dbo].[Course]
                                        SET [InstructorId] = @instructorId
                                        WHERE [Name] = @courseName";

                    cmd.Parameters.Add("@instructorId", SqlDbType.Int).Value = instructorId;
                    cmd.Parameters.Add("@courseName", SqlDbType.NVarChar).Value = courseName;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}