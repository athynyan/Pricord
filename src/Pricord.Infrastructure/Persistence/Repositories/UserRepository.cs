using Microsoft.EntityFrameworkCore;
using Pricord.Application.Authentication.Persistence;
using Pricord.Application.Common.Services;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserRepository(ApplicationDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task AddAsync(User entity)
    {
        await _context.Users.AddAsync(entity);
    }
    
    public async Task<User?> FindByIdAsync(UserId id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> FindByEmailAsync(Email email)
    {
        return await _context.Users
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> FindByNameAsync(Name name)
    {
        return await _context.Users
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<User?> FindByRefreshToken(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.Tokens)
            .FirstOrDefaultAsync(u => u.Tokens
                .Any(t => t.Value == refreshToken));
    }
}