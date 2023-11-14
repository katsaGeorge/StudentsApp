using StudentsDbApp.Models;
using StudentsDbApp.Services.DBHelper;
using System.Data.SqlClient;

namespace StudentsDbApp.DAO
{
    public class StudentDAOImpl : IStudentDAO
    {
        public Student? Insert(Student student)
        {
            if (student == null) return null;

            string sql = "INSERT INTO STUDENTS (FIRSTNAME, LASTNAME) VALUES (@firstname, @lastname); " +
                "SELECT SCOPE_IDENTITY();";
            
            Student? StudentToReturn = null;
            
            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand insertedCommand= new(sql, conn);

            insertedCommand.Parameters.AddWithValue("@firstname", student.FirstName);
            insertedCommand.Parameters.AddWithValue("@lastname", student.LastName);

            object insertedObj = insertedCommand.ExecuteScalar();
            int insertedId = 0;
            if(insertedObj is not  null) 
            { 
                if(!int.TryParse(insertedObj.ToString(), out insertedId))
                {
                    Console.WriteLine("Error in inserted id");
                }
            }

            string sqlInsertedStudent = "SELECT * FROM STUDENTS WHERE ID = @id";

            using SqlCommand selectCommand = new(sqlInsertedStudent, conn);
            selectCommand.Parameters.AddWithValue("@id", insertedId);

            using SqlDataReader reader = selectCommand.ExecuteReader();
            if(reader.Read()) 
            {
                StudentToReturn = new Student()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
                    LastName = reader.GetString(reader.GetOrdinal("LASTNAME"))
                };
            }
            reader.Close();
            return StudentToReturn;
        }

        public Student? Update(Student student)
        {
            if (student is null) return null;

            string sql = "UPDATE STUDENTS SET FIRSTNAME = @firstname, LASTNAME = @lastname WHERE ID = @id";
            Student? studentToReturn = null;

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand updateCommand = new(sql, conn);
            updateCommand.Parameters.AddWithValue("@firstname", student.FirstName);
            updateCommand.Parameters.AddWithValue("@lastname", student.LastName);
            updateCommand.Parameters.AddWithValue("@id", student.Id);
            updateCommand.ExecuteNonQuery();
            studentToReturn = student;
            return studentToReturn;


        }
        public Student? GetById(int id)
        {
            string sql = "SELECT * FROM STUDENTS WHERE ID = @id";
            Student? student = null;

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);
            using SqlDataReader reader = command.ExecuteReader();

            if(reader.Read()) 
            {
                student = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
                    LastName = reader.GetString(reader.GetOrdinal("LASTNAME"))
                };
            }
            return student;
        }

        public IList<Student> GetAll()
        {
            string sql = "SELECT * FROM STUDENTS";
            var students = new List<Student>();

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand command = new(sql,conn);

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                Student student = new Student()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FIRSTNAME")),
                    LastName = reader.GetString(reader.GetOrdinal("LASTNAME"))
                };

                students.Add(student);

            }
            return students;
        }
        
        public void Delete(int id)
        {
            string? sql = "DELETE FROM STUDENTS WHERE ID = @id";

            using SqlConnection? conn = DBUtil.GetConnection();
            conn!.Open();

            using SqlCommand deleteCommand = new(sql, conn);

            deleteCommand.Parameters.AddWithValue("@id", id);
            deleteCommand.ExecuteNonQuery();

        }
    }
}
