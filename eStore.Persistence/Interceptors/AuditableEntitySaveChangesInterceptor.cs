using eStore.Application.Common.Interfaces;
using eStore.Domain.Common;
using eStore.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eStore.Infrastructure.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor(
        ICurrentUserService currentUserService,
        IDateTime dateTime
        ) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result
        )
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default
        )
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null)
                return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.UserId;
                    entry.Entity.CreatedOn = dateTime.UtcNow;
                    entry.Entity.Status = Status.Active;
                }

                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.ModifiedBy = currentUserService.UserId;
                    entry.Entity.ModifiedOn = dateTime.UtcNow;
                    entry.Entity.Status = Status.Modified;
                }
            }
        }
    }

    public static class Extensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(
                r =>
                    r.TargetEntry != null
                    && r.TargetEntry.Metadata.IsOwned()
                    && (
                        r.TargetEntry.State == EntityState.Added
                        || r.TargetEntry.State == EntityState.Modified
                    )
            );
    }
}
