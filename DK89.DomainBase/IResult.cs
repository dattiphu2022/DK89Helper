namespace DK89.DomainBase
{
    public interface IResult<T>
    {
        string? ErrorCode { get; }
        string? ErrorMessage { get; }
        bool IsFailure { get; }
        bool IsSuccess { get; }
        T? Value { get; }
    }
}