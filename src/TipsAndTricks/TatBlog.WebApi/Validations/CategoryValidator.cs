using FluentValidation;
using TatBlog.WebApi.Models.Category;

namespace TatBlog.WebApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator() 
        {
            RuleFor(b => b.Name )
                .NotEmpty()
                .WithMessage("Tên không được để trống!!!!!")
                .MaximumLength(100)
                .WithMessage("Tên tối đa 100 ký tự :3");

            RuleFor(b => b.UrlSlug)
                .NotEmpty()
                .WithMessage("UrlSlug không được để trống!!!!!")
                .MaximumLength(100)
                .WithMessage("UrlSlug tối đa 100 ký tự :C");
 
            RuleFor(b => b.Description)
                .NotEmpty()
                .WithMessage("Mô tả không được để trống!!!!!")
                .MaximumLength(100)
                .WithMessage("Mô tả tối đa 100 ký tự :D");

            RuleFor(b => b.ShowOnMenu);
               
        }

    }
}
