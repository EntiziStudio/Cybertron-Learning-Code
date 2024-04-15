namespace Allspark.Application.Exceptions;

public class AllsparkValidationException : Exception
{
    public AllsparkValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new List<string>();
    }
    public List<string> Errors { get; } = new();
    public AllsparkValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        foreach (var failure in failures)
        {
            Errors.Add(failure.ErrorMessage);
        }
    }
    public AllsparkValidationException(List<string> errors)
    {
        Errors = errors;
    }

    public AllsparkValidationException(string error)
    {
        Errors.Add(error);
    }
}