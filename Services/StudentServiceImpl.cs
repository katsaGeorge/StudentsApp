using AutoMapper;
using StudentsDbApp.DAO;
using StudentsDbApp.DTO;
using StudentsDbApp.Models;
using System.Transactions;

namespace StudentsDbApp.Services
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly IStudentDAO _studentDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentServiceImpl> _logger;

        public StudentServiceImpl(IStudentDAO studentDAO, IMapper mapper, ILogger<StudentServiceImpl> logger)
        {
            _studentDAO = studentDAO;
            _mapper = mapper;
            _logger = logger;
        }

       

        public IList<Student> GetAllStudents()
        {
           try
            {
                IList<Student> students = _studentDAO.GetAll();
                return students;

            }catch (Exception e)
            {
                _logger.LogError("An error occured while fetching all students: {ErrorMessage}",e.Message);
                throw;
            }
        }

        public Student? GetStudent(int id)
        {
            try
            {
              return _studentDAO.GetById(id);

            }
            catch (Exception e)
            {
                _logger.LogError("An error occured while fetching one student: {ErrorMessage}", e.Message);
                throw;
            }
        }

        public Student? InsertStudent(StudentInsertDTO dto)
        {
            if (dto is null) return null;
            var student = _mapper.Map<Student>(dto);

            try
            {
                using TransactionScope scope = new();
                Student? insertedStudent = _studentDAO.Insert(student);
                scope.Complete();
                return insertedStudent;
            }catch(Exception e)
            {
                _logger.LogError("An error occured while inserting a student: {ErrorMessage}", e.Message);
                throw;
            }
        }

        public Student? UpdateStudent(StudentUpdateDTO dto)
        {
            if(dto is null) return null;    
            Student? student = _mapper.Map<Student>(dto);
            Student? updateStudent = null;
            try
            {
                using TransactionScope scope = new();

                updateStudent = _studentDAO.GetById(student.Id);
                if (updateStudent is null) return null;
                updateStudent = _studentDAO.Update(student);
                scope.Complete();

                
            }catch(Exception e)
            {
                _logger.LogError("An error occured while updating a student: {ErrorMessage}", e.Message);
                throw;
            }
            return updateStudent;
        }
        public Student? DeleteStuden(int id)
        {
            Student? student = null;
            try
            {
                using TransactionScope scope = new();

                student = _studentDAO.GetById(id);
                if (student is null) return null;
                _studentDAO.Delete(id);
                scope.Complete();
            }
            catch (Exception e)
            {
                _logger.LogError("An error occured while updating a student: {ErrorMessage}", e.Message);
                throw;
            }
            return student;

        }
    }
}
