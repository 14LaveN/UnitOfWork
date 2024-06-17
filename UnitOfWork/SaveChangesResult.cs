namespace UnitOfWork;

/// <summary>
/// Represents the operation result for SaveChanges.
/// </summary>
public sealed class SaveChangesResult
{
    /// <summary>
    /// Ctor.
    /// </summary>
    public SaveChangesResult() =>
        Messages = new List<string>();

    /// <inheritdoc />
    public SaveChangesResult(string message) 
        : this() =>
        AddMessage(message);

    /// <summary>
    /// Last Exception you can find here.
    /// </summary>
    public Exception? Exception { get; internal set; }

    /// <summary>
    /// Is Exception occupied while last operation execution.
    /// </summary>
    public bool IsOk => Exception == null;

    /// <summary>
    /// Adds new message to result.
    /// </summary>
    /// <param name="message">The string of message.</param>
    public void AddMessage(string message) =>
        Messages.Add(message);

    /// <summary>
    /// List of the error should appear there.
    /// </summary>
    private List<string> Messages { get; }
}