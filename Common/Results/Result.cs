namespace Common.Results;

public class Result
{
	public Result() => this.Errors = [];

	internal Result(bool succeeded, string error)
	{
		this.Succeeded = succeeded;

		this.Errors =
		[
			error
		];
	}

	internal Result(bool succeeded, List<object> errors)
	{
		this.Succeeded = succeeded;
		this.Errors = errors;
	}

	internal Result(bool succeeded, List<string> errors)
	{
		this.Succeeded = succeeded;
		this.Errors = errors.ToList<object>();
	}

	public bool Succeeded { get; set; }

	public List<object> Errors { get; set; }

	public static Result Success() => new(true, new List<object> { });

	public static Result Failure(List<object> errors) => new(false, errors);

	public static Result Failure(List<string> errors) => new(false, errors);

	public static Result Failure(string error) => new(false, error);
}

public class Result<T>
{
	internal Result(bool succeeded, string error)
	{
		this.Succeeded = succeeded;
		this.Errors =
		[
			error
		];
		this.Data = default!;
	}

	internal Result(bool succeeded, List<object> errors)
	{
		this.Succeeded = succeeded;
		this.Errors = errors;
		this.Data = default!;
	}

	internal Result(bool succeeded, T data, List<object> errors)
	{
		this.Succeeded = succeeded;
		this.Errors = errors;
		this.Data = data;
	}

	internal Result(T data, int count)
	{
		this.Succeeded = true;
		this.Data = data;
		this.Count = count;
		this.Errors = [];
	}
	public bool Succeeded { get; set; }

	public T Data { get; set; }

	public int Count { get; set; }

	public List<object> Errors { get; set; }

	public static Result<T> Success(T data) => new(true, data, []);

	public static Result<T> Success(T data, int count) => new(data, count);

	public static Result<T> Failure(string error) => new(false, error);

	public static Result<T> Failure(List<object> errors) => new(false, errors);

	
}

public class ErrorResult : Result
{
	public ErrorResult() => this.Succeeded = false;

	[DefaultValue(false)]
	public new bool Succeeded { get; set; }
}

public class PagedResults<T>
{
    internal PagedResults(T search, int totalCount, int pageNumber, int pageSize)
    {
        this.Search = search;
        this.TotalCount = totalCount;
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
    }

    internal PagedResults(T search, int count)
    {
        this.Succeeded = true;
        this.Search = search;
        this.TotalCount = count;
    }
    public bool Succeeded { get; set; }

    public T Search { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public static PagedResults<T> Success(T search, int totalCount, int pageNumber, int pageSize) => new(search, totalCount, pageNumber, pageSize);
    public static PagedResults<T> Success(T data, int count) => new(data, count);

}
