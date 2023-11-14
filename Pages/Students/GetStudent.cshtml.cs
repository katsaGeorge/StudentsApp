using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsDbApp.Models;

namespace StudentsDbApp.Pages.Students
{
    public class GetStudentModel : PageModel
    {

       public Student? Student { get; set; }
        public void OnGet(int id)
        {
            Student = new() { Id = id, FirstName = "Giorgos", LastName = "Andrianopoulos" };
        
        }
    }
}
