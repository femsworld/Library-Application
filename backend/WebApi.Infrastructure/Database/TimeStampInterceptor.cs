using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Database
{
    public class TimeStampInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var addedEntries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                if (entry.Entity is TimeStamp hasTimestamp)
                {
                    var now = DateTime.UtcNow;
                    hasTimestamp.CreatedAt = now;
                    hasTimestamp.UpdatedAt = now;
                }
            }

            var modifiedEntries = eventData.Context.ChangeTracker.Entries()
           .Where(e => e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is TimeStamp hasTimestamp)
                {
                    var now = DateTime.UtcNow;
                    hasTimestamp.UpdatedAt = now;
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}