using Microsoft.EntityFrameworkCore;
using Pricord.Application.Authentication.Persistence;
using Pricord.Domain.Authentication;
using Pricord.Domain.Authentication.ValueObjects;
using Pricord.Domain.Common.ValueObjects;

namespace Pricord.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
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
}