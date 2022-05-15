namespace Example.WebApi.Endpoints.Users;

public class Validator : AbstractValidator<UserRegistration>
{
    public Validator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.BirthDate).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
    }
}
