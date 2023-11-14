using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsDbApp.DTO;
using StudentsDbApp.Models;
using StudentsDbApp.Services;

namespace StudentsDbApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        public Error? ErrorObj { get; set; }
        public IList<StudentReadOnlyDTO> Studentdto { get; set; } = null!;
        public Student? Katsa { get; set; }

        private readonly IStudentService? _studentService;
        private readonly IMapper? _mapper;

        public IndexModel(IStudentService? studentService, IMapper? mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }


        public IActionResult OnGet()
        {

            if (Request.Query.TryGetValue("lastname", out var lastname))
            {
                if (lastname == "Katsafanas") 
                {
                     Katsa = new Student() { Id = 45, FirstName = "Giorgos", LastName = "Opaixthw" };
                }
                return Page();
            }
            else
            {


                try
                {
                    ErrorObj = null;
                    IList<Student> students = _studentService!.GetAllStudents();
                    Studentdto = new List<StudentReadOnlyDTO>();

                    foreach (Student student in students)
                    {
                        StudentReadOnlyDTO? studentDto = _mapper!.Map<StudentReadOnlyDTO>(student);
                        Studentdto.Add(studentDto);

                    }
                }
                catch (Exception e)
                {
                    ErrorObj = new Error("", e.Message, "");
                }

                return Page();

            }
        }
    }
}
