using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsDbApp.DTO;
using StudentsDbApp.Models;
using StudentsDbApp.Services;

namespace StudentsDbApp.Pages.Students
{
    public class UpdateModel : PageModel
    {

        public StudentUpdateDTO studentUpdateDto { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = new();

         private readonly IStudentService _studentService;
         private readonly IMapper _mapper;
        private readonly IValidator<StudentUpdateDTO> _studentUpdateValidator;

        public UpdateModel(IStudentService studentService,
            IMapper mapper, IValidator<StudentUpdateDTO> studentUpdateValidator)
        {
            _studentService = studentService;
            _mapper = mapper;
            _studentUpdateValidator = studentUpdateValidator;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Student? student = _studentService?.GetStudent(id);
                studentUpdateDto = _mapper.Map<StudentUpdateDTO>(student);
            }catch(Exception e) 
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
            return Page();
        }

        public void OnPost(StudentUpdateDTO dto)
        {
            //Refresh
            studentUpdateDto  = dto;

            var validationResult = _studentUpdateValidator.Validate(dto);
            if(!validationResult.IsValid) 
            {
                ErrorArray = new();
                    foreach(var error in validationResult.Errors)
                {
                    ErrorArray.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }
            try
            {
                Student? student = _studentService.UpdateStudent(dto);
                Response.Redirect("/Students/getall");
            }catch(Exception e) 
            {
                ErrorArray.Add(new Error("Upadate Error", e.Message, ""));
            }
        }
    }
}
