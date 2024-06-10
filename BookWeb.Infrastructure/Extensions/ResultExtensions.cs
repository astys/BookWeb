using BookWeb.Domain.ResultType;
using BookWeb.Domain.ResultType.Errors;

namespace BookWeb.Infrastructure.Extensions;

public static class ResultExtensions
{
    public static bool IsGeneralError(this Result result)
        => result.Error is not null && result.Error.Type == nameof(GeneralError);

    public static bool IsNotFound(this Result result)
        => result.Error is not null && result.Error.Type == nameof(NotFoundError);
}