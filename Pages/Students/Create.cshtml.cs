using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using StudentsDbApp.DTO;
using StudentsDbApp.Models;
using StudentsDbApp.Services;

namespace StudentsDbApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = new();
        public StudentInsertDTO StudentInsertDto { get; set; } = new();

        private readonly IStudentService? _studentService;
        private readonly IValidator<StudentInsertDTO> studentInsertValidator;

        public CreateModel(IStudentService? studentService, IValidator<StudentInsertDTO> studentInsertValidator)
        {
            _studentService = studentService;
            this.studentInsertValidator = studentInsertValidator;
        }

        /*public void OnGet()
        {
        }*/

        public void OnPost(StudentInsertDTO dto) 
        {
            //When form refresh the text boxes retain the same
            //retain old values
            StudentInsertDto = dto;
           
            var validationResult = studentInsertValidator.Validate(dto);
            if (!validationResult.IsValid)
            {
                foreach(var error in validationResult.Errors) 
                {
                    ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Student student = _studentService!.InsertStudent(dto)!;
                Response.Redirect("/Students/getall");
            }catch(Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }


    }
}
