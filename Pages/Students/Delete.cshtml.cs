using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsDbApp.Models;
using StudentsDbApp.Services;

namespace StudentsDbApp.Pages.Students
{
    public class DeleteModel : PageModel
    {
      
        public List<Error> ErrorArray { get; set; } = new();

        private readonly IStudentService? _studentService;

        public DeleteModel(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public void OnGet(int id)
        {
            try
            {
                Student? student = _studentService?.DeleteStuden(id);
                Response.Redirect("/Students/getall");
            }catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, "")); 
            }

        }
    }
}
