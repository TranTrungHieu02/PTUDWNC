using FluentValidation;
using TatBlog.WebApi.Models.Post;

namespace TatBlog.WebApi.Validations
{
    public class PostValidator : AbstractValidator<PostEditModel>
    {
        public PostValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Tên không được để trống")
                .MaximumLength(150)
                .WithMessage("Tên tối đa 150 ký tự");

            RuleFor(p => p.ShortDescription)
                .NotEmpty()
                .WithMessage("ShortDescription không được để trống")
                .MaximumLength(150)
                .WithMessage("ShortDesciption tối đa 150 ký tự");

            RuleFor(p => p.Desciption)
                .NotEmpty()
                .WithMessage("Description không được để trống")
                .MaximumLength(500)
                .WithMessage("Desciption tối đa 500 ký tự");

            RuleFor(p => p.Meta)
                .NotEmpty()
                .WithMessage("Meta bài không được để trống")
                .MaximumLength(500)
                .WithMessage("Meta tối đa 500 ký tự");

        }
    }
}
