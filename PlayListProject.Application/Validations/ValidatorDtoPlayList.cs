using FluentValidation;
using PlayListProject.Application.Dtos;

namespace PlayListProject.Application.Validations
{
    public class ValidatorDtoPlayList : AbstractValidator<DtoPlayList>
    {
        public ValidatorDtoPlayList(bool validateId)
        {
            if (validateId)
            {
                RuleFor(s => s.Id)
                .NotEmpty();
            }

            RuleFor(s => s.Name)
                .NotEmpty();

            RuleFor(s => s.Description)
                .NotEmpty();

            RuleFor(s => s.Songs)
                .Must(s =>
                {
                    if(s != null && s.Count > 0)
                    {
                        return s.All(s => s.Id > 0);
                    }
                    return true;
                });
        }
    }
}
