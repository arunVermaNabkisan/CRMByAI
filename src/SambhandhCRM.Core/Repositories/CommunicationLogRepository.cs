using Dapper;
using SambhandhCRM.Core.Data;
using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Repositories.Interfaces;

namespace SambhandhCRM.Core.Repositories;

public class CommunicationLogRepository : ICommunicationLogRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CommunicationLogRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<CommunicationLog>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM CommunicationLogs
            WHERE IsDeleted = 0
            ORDER BY CommunicationDate DESC";

        return await connection.QueryAsync<CommunicationLog>(sql);
    }

    public async Task<CommunicationLog?> GetByIdAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM CommunicationLogs
            WHERE Id = @Id AND IsDeleted = 0";

        return await connection.QueryFirstOrDefaultAsync<CommunicationLog>(sql, new { Id = id });
    }

    public async Task<IEnumerable<CommunicationLog>> GetByCustomerIdAsync(long customerId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM CommunicationLogs
            WHERE CustomerId = @CustomerId AND IsDeleted = 0
            ORDER BY CommunicationDate DESC";

        return await connection.QueryAsync<CommunicationLog>(sql, new { CustomerId = customerId });
    }

    public async Task<IEnumerable<CommunicationLog>> GetByLeadIdAsync(long leadId)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM CommunicationLogs
            WHERE LeadId = @LeadId AND IsDeleted = 0
            ORDER BY CommunicationDate DESC";

        return await connection.QueryAsync<CommunicationLog>(sql, new { LeadId = leadId });
    }

    public async Task<long> CreateAsync(CommunicationLog communicationLog)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO CommunicationLogs (
                Id, CustomerId, LeadId, CommunicationDate, CommunicationType,
                Direction, Subject, Summary, NextActionRequired, LoggedByUserId,
                IsBulkCommunication, CampaignId, DeliveryStatus,
                CreatedAt, CreatedBy, IsDeleted
            )
            VALUES (
                @Id, @CustomerId, @LeadId, @CommunicationDate, @CommunicationType,
                @Direction, @Subject, @Summary, @NextActionRequired, @LoggedByUserId,
                @IsBulkCommunication, @CampaignId, @DeliveryStatus,
                @CreatedAt, @CreatedBy, @IsDeleted
            )";

        if (communicationLog.Id == 0)
            communicationLog.Id = 0; // Let database generate ID

        await connection.ExecuteAsync(sql, communicationLog);
        return communicationLog.Id;
    }

    public async Task<bool> UpdateAsync(CommunicationLog communicationLog)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE CommunicationLogs SET
                CommunicationDate = @CommunicationDate,
                CommunicationType = @CommunicationType,
                Direction = @Direction,
                Subject = @Subject,
                Summary = @Summary,
                NextActionRequired = @NextActionRequired,
                DeliveryStatus = @DeliveryStatus,
                UpdatedAt = @UpdatedAt,
                UpdatedBy = @UpdatedBy
            WHERE Id = @Id AND IsDeleted = 0";

        var rowsAffected = await connection.ExecuteAsync(sql, communicationLog);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            UPDATE CommunicationLogs
            SET IsDeleted = 1, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UpdatedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }
}
