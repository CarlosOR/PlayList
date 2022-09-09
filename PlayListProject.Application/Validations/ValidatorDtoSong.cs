﻿using FluentValidation;
using PlayListProject.Application.Dtos;

namespace PlayListProject.Application.Validations
{
    public class ValidatorDtoSong : AbstractValidator<DtoSong>
    {
        public ValidatorDtoSong(bool validateId)
        {
            if (validateId)
            {
                RuleFor(s => s.Id)
                .NotEmpty();
            }
            RuleFor(s => s.Name)
                .NotEmpty();

            RuleFor(s => s.AuthorId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(s => s.PlayList)
                .Must(pl =>
                {
                    if (pl != null && pl.Count > 0)
                    {
                        return pl.All(s => s.Id > 0);
                    }
                    return true;
                });
        }
    }
}
