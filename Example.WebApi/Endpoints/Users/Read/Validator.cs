namespace Example.WebApi.Endpoints.Users.Read;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}
