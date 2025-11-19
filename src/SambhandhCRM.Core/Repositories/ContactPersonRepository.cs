using Dapper;
using SambhandhCRM.Core.Data;
using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Repositories.Interfaces;

namespace SambhandhCRM.Core.Repositories;

public class ContactPersonRepository : IContactPersonRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ContactPersonRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<ContactPerson>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM ContactPersons
            WHERE IsDeleted = 0
            ORDER BY FullName";

        return await connection.QueryAsync<ContactPerson>(sql);
    }

    public async Task<ContactPerson?> GetByIdAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM ContactPersons
            WHERE Id = @Id AND IsDeleted = 0";

        return await connection.QueryFirstOrDefaultAsync<ContactPerson>(sql, new { Id = id });
    }

    public async Task<IEnumerable<ContactPerson>> GetByCustomerIdAsync(long customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT cp.*
            FROM ContactPersons cp
            INNER JOIN CustomerContactPersons ccp ON cp.Id = ccp.ContactPersonId
            WHERE ccp.CustomerId = @CustomerId AND cp.IsDeleted = 0
            ORDER BY ccp.IsPreferredContact DESC, cp.FullName";

        return await connection.QueryAsync<ContactPerson>(sql, new { CustomerId = customerId });
    }

    public async Task<long> CreateAsync(ContactPerson contactPerson)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO ContactPersons (
                Id, FullName, MobileNumber, Email, PANNumber, DIN, LinkedInProfile,
                CreatedAt, CreatedBy, IsDeleted
            )
            VALUES (
                @Id, @FullName, @MobileNumber, @Email, @PANNumber, @DIN, @LinkedInProfile,
                @CreatedAt, @CreatedBy, @IsDeleted
            )";

        if (contactPerson.Id == 0)
            contactPerson.Id = 0; // Let database generate ID

        await connection.ExecuteAsync(sql, contactPerson);
        return contactPerson.Id;
    }

    public async Task<bool> UpdateAsync(ContactPerson contactPerson)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE ContactPersons SET
                FullName = @FullName,
                MobileNumber = @MobileNumber,
                Email = @Email,
                PANNumber = @PANNumber,
                DIN = @DIN,
                LinkedInProfile = @LinkedInProfile,
                UpdatedAt = @UpdatedAt,
                UpdatedBy = @UpdatedBy
            WHERE Id = @Id AND IsDeleted = 0";

        var rowsAffected = await connection.ExecuteAsync(sql, contactPerson);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            UPDATE ContactPersons
            SET IsDeleted = 1, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UpdatedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }

    public async Task<bool> LinkToCustomerAsync(long contactPersonId, long customerId, bool isPrimary = false)
    {
        using var connection = _connectionFactory.CreateConnection();

        // If this is to be the preferred contact, first unset any existing preferred contacts
        if (isPrimary)
        {
            var unsetPreferredSql = @"
                UPDATE CustomerContactPersons
                SET IsPreferredContact = 0
                WHERE CustomerId = @CustomerId";

            await connection.ExecuteAsync(unsetPreferredSql, new { CustomerId = customerId });
        }

        // Check if relationship already exists
        var checkSql = @"
            SELECT COUNT(1)
            FROM CustomerContactPersons
            WHERE ContactPersonId = @ContactPersonId AND CustomerId = @CustomerId";

        var exists = await connection.ExecuteScalarAsync<int>(checkSql, new
        {
            ContactPersonId = contactPersonId,
            CustomerId = customerId
        }) > 0;

        if (exists)
        {
            // Update existing relationship
            var updateSql = @"
                UPDATE CustomerContactPersons
                SET IsPreferredContact = @IsPreferred
                WHERE ContactPersonId = @ContactPersonId AND CustomerId = @CustomerId";

            var rowsAffected = await connection.ExecuteAsync(updateSql, new
            {
                ContactPersonId = contactPersonId,
                CustomerId = customerId,
                IsPreferred = isPrimary
            });

            return rowsAffected > 0;
        }
        else
        {
            // Create new relationship
            var insertSql = @"
                INSERT INTO CustomerContactPersons (
                    CustomerId, ContactPersonId, RoleInOrganization, RoleStartDate,
                    IsStillActive, IsDecisionMaker, IsPreferredContact
                )
                VALUES (
                    @CustomerId, @ContactPersonId, @RoleInOrganization, @RoleStartDate,
                    @IsStillActive, @IsDecisionMaker, @IsPreferredContact
                )";

            await connection.ExecuteAsync(insertSql, new
            {
                CustomerId = customerId,
                ContactPersonId = contactPersonId,
                RoleInOrganization = string.Empty,
                RoleStartDate = DateTime.UtcNow,
                IsStillActive = true,
                IsDecisionMaker = false,
                IsPreferredContact = isPrimary
            });

            return true;
        }
    }
}
