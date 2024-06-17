using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace UnitOfWork.Interfaces;

/// <summary>
/// Defines the interface(s) for generic unit of work.
/// </summary>
public interface IUnitOfWork<out TContext> : IUnitOfWork
    where TContext : DbContext 
{
    /// <summary>
    /// Gets the db context.
    /// </summary>
    /// <returns>The instance of type <typeparamref name="TContext"/>.</returns>
    TContext DbContext { get; }

    /// <summary>
    /// Saves all changes made in this context to the database with distributed transaction.
    /// </summary>
    /// <param name="unitOfWorks">An optional <see cref="IUnitOfWork{TUser}"/> array.</param>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
    Task<int> SaveChangesAsync(params IUnitOfWork[] unitOfWorks);

}

/// <summary>
/// Defines the interface(s) for unit of work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Asynchronously saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous save operation. The task result contains the number of state entities written to database.</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Uses Track Graph Api to attach disconnected entities
    /// </summary>
    /// <param name="rootEntity"> Root entity</param>
    /// <param name="callback">Delegate to convert Object's State properties to Entities entry state.</param>
    void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback);

    /// <summary>
    /// Returns Transaction 
    /// </summary>
    /// <returns></returns>
    Task<IDbContextTransaction> BeginTransactionAsync(bool useIfExists = false);

    /// <summary>
    /// DbContext disable/enable auto detect changes
    /// </summary>
    /// <param name="value"></param>
    void SetAutoDetectChanges(bool value);

    /// <summary>
    /// Last error after SaveChanges operation executed
    /// </summary>
    SaveChangesResult LastSaveChangesResult { get; }
}