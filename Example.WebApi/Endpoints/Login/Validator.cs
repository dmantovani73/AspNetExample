namespace Example.WebApi.Endpoints.Login;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(p => p.UserName).NotEmpty();
        RuleFor(p => p.Password).NotEmpty();
    }
}
