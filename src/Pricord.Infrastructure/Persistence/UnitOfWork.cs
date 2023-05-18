using Microsoft.EntityFrameworkCore;
using Pricord.Application.Common.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Common.Interfaces;

namespace Pricord.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{   
    private readonly ApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UnitOfWork(ApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        SetCreatedAtAndLastModified();
        await _context.SaveChangesAsync(cancellationToken);
    }

    private void SetCreatedAtAndLastModified()
	{
		var entries = _context.ChangeTracker.Entries();
		var now = _dateTimeProvider.UtcNow;

		foreach (var entry in entries)
		{
			if (entry.Entity is not IAuditable entity) continue;

			switch (entry)
			{
				case { State: EntityState.Added }:
					entity.CreatedAt = now;
					entity.LastModified = now;
					break;
				case { State: EntityState.Modified }:
					entity.LastModified = now;
					break;
			}
		}
	}
}