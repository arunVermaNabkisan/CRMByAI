using Dapper;
using SambhandhCRM.Core.Data;
using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Models.Enums;
using SambhandhCRM.Core.Repositories.Interfaces;

namespace SambhandhCRM.Core.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public LeadRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Lead>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT l.*, c.*
            FROM Leads l
            LEFT JOIN Customers c ON l.CustomerId = c.Id
            WHERE l.IsDeleted = 0
            ORDER BY l.CreatedAt DESC";

        var leadDictionary = new Dictionary<long, Lead>();

        await connection.QueryAsync<Lead, Customer, Lead>(
            sql,
            (lead, customer) =>
            {
                if (!leadDictionary.TryGetValue(lead.Id, out var existingLead))
                {
                    existingLead = lead;
                    existingLead.Customer = customer;
                    leadDictionary.Add(lead.Id, existingLead);
                }
                return existingLead;
            },
            splitOn: "Id");

        return leadDictionary.Values;
    }

    public async Task<Lead?> GetByIdAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM Leads
            WHERE Id = @Id AND IsDeleted = 0";

        return await connection.QueryFirstOrDefaultAsync<Lead>(sql, new { Id = id });
    }

    public async Task<Lead?> GetByIdWithDetailsAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT l.*, c.*
            FROM Leads l
            LEFT JOIN Customers c ON l.CustomerId = c.Id
            WHERE l.Id = @Id AND l.IsDeleted = 0";

        Lead? lead = null;

        await connection.QueryAsync<Lead, Customer, Lead>(
            sql,
            (l, c) =>
            {
                if (lead == null)
                {
                    lead = l;
                    lead.Customer = c;
                }
                return lead;
            },
            new { Id = id },
            splitOn: "Id");

        return lead;
    }

    public async Task<IEnumerable<Lead>> GetByCustomerIdAsync(long customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM Leads
            WHERE CustomerId = @CustomerId AND IsDeleted = 0
            ORDER BY CreatedAt DESC";

        return await connection.QueryAsync<Lead>(sql, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT l.*, c.*
            FROM Leads l
            LEFT JOIN Customers c ON l.CustomerId = c.Id
            WHERE l.Status = @Status AND l.IsDeleted = 0
            ORDER BY l.CreatedAt DESC";

        var leadDictionary = new Dictionary<long, Lead>();

        await connection.QueryAsync<Lead, Customer, Lead>(
            sql,
            (lead, customer) =>
            {
                if (!leadDictionary.TryGetValue(lead.Id, out var existingLead))
                {
                    existingLead = lead;
                    existingLead.Customer = customer;
                    leadDictionary.Add(lead.Id, existingLead);
                }
                return existingLead;
            },
            new { Status = status },
            splitOn: "Id");

        return leadDictionary.Values;
    }

    public async Task<IEnumerable<Lead>> GetByAssignedUserAsync(string userId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT l.*, c.*
            FROM Leads l
            LEFT JOIN Customers c ON l.CustomerId = c.Id
            WHERE l.AssignedToUserId = @UserId AND l.IsDeleted = 0
            ORDER BY l.CreatedAt DESC";

        var leadDictionary = new Dictionary<long, Lead>();

        await connection.QueryAsync<Lead, Customer, Lead>(
            sql,
            (lead, customer) =>
            {
                if (!leadDictionary.TryGetValue(lead.Id, out var existingLead))
                {
                    existingLead = lead;
                    existingLead.Customer = customer;
                    leadDictionary.Add(lead.Id, existingLead);
                }
                return existingLead;
            },
            new { UserId = userId },
            splitOn: "Id");

        return leadDictionary.Values;
    }

    public async Task<long> CreateAsync(Lead lead)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO Leads (
                Id, LeadNumber, CustomerId, LeadSource, ReferralSource, ProductInterest,
                LoanAmountRange, Priority, Status, AssignedToUserId, AssignedAt,
                LastContactDate, NextFollowUpDate, Notes,
                HasKYCDocuments, HasFinancialStatements, HasBusinessDocuments, HasOtherDocuments,
                ConvertedAt, DropReason,
                CreatedAt, CreatedBy, IsDeleted
            )
            VALUES (
                @Id, @LeadNumber, @CustomerId, @LeadSource, @ReferralSource, @ProductInterest,
                @LoanAmountRange, @Priority, @Status, @AssignedToUserId, @AssignedAt,
                @LastContactDate, @NextFollowUpDate, @Notes,
                @HasKYCDocuments, @HasFinancialStatements, @HasBusinessDocuments, @HasOtherDocuments,
                @ConvertedAt, @DropReason,
                @CreatedAt, @CreatedBy, @IsDeleted
            )";

        if (lead.Id == 0)
            lead.Id = 0; // Let database generate ID

        if (string.IsNullOrEmpty(lead.LeadNumber))
            lead.LeadNumber = await GenerateLeadNumberAsync();

        await connection.ExecuteAsync(sql, lead);
        return lead.Id;
    }

    public async Task<bool> UpdateAsync(Lead lead)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE Leads SET
                LeadSource = @LeadSource,
                ReferralSource = @ReferralSource,
                ProductInterest = @ProductInterest,
                LoanAmountRange = @LoanAmountRange,
                Priority = @Priority,
                Status = @Status,
                AssignedToUserId = @AssignedToUserId,
                AssignedAt = @AssignedAt,
                LastContactDate = @LastContactDate,
                NextFollowUpDate = @NextFollowUpDate,
                Notes = @Notes,
                HasKYCDocuments = @HasKYCDocuments,
                HasFinancialStatements = @HasFinancialStatements,
                HasBusinessDocuments = @HasBusinessDocuments,
                HasOtherDocuments = @HasOtherDocuments,
                ConvertedAt = @ConvertedAt,
                DropReason = @DropReason,
                UpdatedAt = @UpdatedAt,
                UpdatedBy = @UpdatedBy
            WHERE Id = @Id AND IsDeleted = 0";

        var rowsAffected = await connection.ExecuteAsync(sql, lead);
        return rowsAffected > 0;
    }

    public async Task<bool> UpdateStatusAsync(long id, LeadStatus status)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE Leads SET
                Status = @Status,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id AND IsDeleted = 0";

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            Id = id,
            Status = status,
            UpdatedAt = DateTime.UtcNow
        });

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            UPDATE Leads
            SET IsDeleted = 1, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UpdatedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }

    public async Task<string> GenerateLeadNumberAsync()
    {
        using var connection = _connectionFactory.CreateConnection();

        // Get the count of leads created this month
        var sql = @"
            SELECT COUNT(*)
            FROM Leads
            WHERE YEAR(CreatedAt) = @Year AND MONTH(CreatedAt) = @Month";

        var now = DateTime.UtcNow;
        var count = await connection.ExecuteScalarAsync<int>(sql, new
        {
            Year = now.Year,
            Month = now.Month
        });

        // Format: LEAD-YYYYMM-9999
        return $"LEAD-{now:yyyyMM}-{(count + 1):D4}";
    }
}
