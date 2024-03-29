﻿using FluentValidation;
using Yoli.WebApi.Contracts.Requests;

namespace Yoli.WebApi.Validations;

public class YoliSignInRequestValidator : AbstractValidator<YoliSignInRequest>
{
    public YoliSignInRequestValidator()
    {
        RuleFor(r => r.Password)
            .NotNull().NotEmpty()
            .WithMessage("Password required");
    }
}