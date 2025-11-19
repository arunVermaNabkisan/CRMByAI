using Dapper;
using SambhandhCRM.Core.Data;
using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Models.Enums;
using SambhandhCRM.Core.Repositories.Interfaces;

namespace SambhandhCRM.Core.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CustomerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM Customers
            WHERE IsDeleted = 0
            ORDER BY CreatedAt DESC";

        return await connection.QueryAsync<Customer>(sql);
    }

    public async Task<Customer?> GetByIdAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM Customers
            WHERE Id = @Id AND IsDeleted = 0";

        return await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
    }

    public async Task<Customer?> GetByIdWithDetailsAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();

        var customerSql = @"
            SELECT * FROM Customers
            WHERE Id = @Id AND IsDeleted = 0";

        var customer = await connection.QueryFirstOrDefaultAsync<Customer>(customerSql, new { Id = id });

        if (customer == null)
            return null;

        // Load business segments
        var businessSegmentsSql = @"
            SELECT cbs.*, bs.*
            FROM CustomerBusinessSegments cbs
            INNER JOIN BusinessSegments bs ON cbs.BusinessSegmentId = bs.Id
            WHERE cbs.CustomerId = @CustomerId AND cbs.IsDeleted = 0";

        var businessSegments = await connection.QueryAsync<CustomerBusinessSegment, BusinessSegment, CustomerBusinessSegment>(
            businessSegmentsSql,
            (cbs, bs) =>
            {
                cbs.BusinessSegment = bs;
                return cbs;
            },
            new { CustomerId = id },
            splitOn: "Id");

        // Note: BusinessSegments loaded but not assigned to model as it uses BusinessSegment (singular) property
        // TODO: Consider refactoring Customer model to include BusinessSegments collection if needed

        return customer;
    }

    public async Task<IEnumerable<Customer>> SearchAsync(string searchTerm)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM Customers
            WHERE IsDeleted = 0
            AND (
                EntityName LIKE @SearchTerm
                OR PANNumber LIKE @SearchTerm
                OR PrimaryPhone LIKE @SearchTerm
                OR PrimaryEmail LIKE @SearchTerm
            )
            ORDER BY CreatedAt DESC";

        return await connection.QueryAsync<Customer>(sql, new { SearchTerm = $"%{searchTerm}%" });
    }

    public async Task<IEnumerable<Customer>> GetByStatusAsync(CustomerStatus status)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT * FROM Customers
            WHERE Status = @Status AND IsDeleted = 0
            ORDER BY CreatedAt DESC";

        return await connection.QueryAsync<Customer>(sql, new { Status = status });
    }

    public async Task<Guid> CreateAsync(Customer customer)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            INSERT INTO Customers (
                Id, LegalConstitution, EntityName, RegistrationNumber, CIN, PANNumber,
                AadhaarNumber, DateOfIncorporation, DateOfBirth, PrimaryBusinessActivity,
                Occupation, AnnualTurnover, EmployeeCountRange, AnnualIncomeRange,
                RegisteredAddress, OfficeAddress, CorrespondenceAddress,
                RegisteredPinCode, OfficePinCode, CorrespondencePinCode,
                PrimaryPhone, MobileNumber, AlternativePhone, PrimaryEmail, SecondaryEmail,
                Website, LinkedInProfile, TwitterProfile,
                PrimaryBankName, BankingSinceYear, IsExistingNABKISANCustomer,
                ExistingProductType, OutstandingAmount, OtherLenderRelationships,
                Status, AssignedToUserId,
                AuthorizedCapital, PaidUpCapital, MCALastFetchedAt,
                CreatedAt, CreatedBy, IsDeleted
            )
            VALUES (
                @Id, @LegalConstitution, @EntityName, @RegistrationNumber, @CIN, @PANNumber,
                @AadhaarNumber, @DateOfIncorporation, @DateOfBirth, @PrimaryBusinessActivity,
                @Occupation, @AnnualTurnover, @EmployeeCountRange, @AnnualIncomeRange,
                @RegisteredAddress, @OfficeAddress, @CorrespondenceAddress,
                @RegisteredPinCode, @OfficePinCode, @CorrespondencePinCode,
                @PrimaryPhone, @MobileNumber, @AlternativePhone, @PrimaryEmail, @SecondaryEmail,
                @Website, @LinkedInProfile, @TwitterProfile,
                @PrimaryBankName, @BankingSinceYear, @IsExistingNABKISANCustomer,
                @ExistingProductType, @OutstandingAmount, @OtherLenderRelationships,
                @Status, @AssignedToUserId,
                @AuthorizedCapital, @PaidUpCapital, @MCALastFetchedAt,
                @CreatedAt, @CreatedBy, @IsDeleted
            )";

        if (customer.Id == 0)
            customer.Id = 0; // Let database generate ID

        await connection.ExecuteAsync(sql, customer);
        return customer.Id;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        using var connection = _connectionFactory.CreateConnection();

        var sql = @"
            UPDATE Customers SET
                LegalConstitution = @LegalConstitution,
                EntityName = @EntityName,
                RegistrationNumber = @RegistrationNumber,
                CIN = @CIN,
                PANNumber = @PANNumber,
                AadhaarNumber = @AadhaarNumber,
                DateOfIncorporation = @DateOfIncorporation,
                DateOfBirth = @DateOfBirth,
                PrimaryBusinessActivity = @PrimaryBusinessActivity,
                Occupation = @Occupation,
                AnnualTurnover = @AnnualTurnover,
                EmployeeCountRange = @EmployeeCountRange,
                AnnualIncomeRange = @AnnualIncomeRange,
                RegisteredAddress = @RegisteredAddress,
                OfficeAddress = @OfficeAddress,
                CorrespondenceAddress = @CorrespondenceAddress,
                RegisteredPinCode = @RegisteredPinCode,
                OfficePinCode = @OfficePinCode,
                CorrespondencePinCode = @CorrespondencePinCode,
                PrimaryPhone = @PrimaryPhone,
                MobileNumber = @MobileNumber,
                AlternativePhone = @AlternativePhone,
                PrimaryEmail = @PrimaryEmail,
                SecondaryEmail = @SecondaryEmail,
                Website = @Website,
                LinkedInProfile = @LinkedInProfile,
                TwitterProfile = @TwitterProfile,
                PrimaryBankName = @PrimaryBankName,
                BankingSinceYear = @BankingSinceYear,
                IsExistingNABKISANCustomer = @IsExistingNABKISANCustomer,
                ExistingProductType = @ExistingProductType,
                OutstandingAmount = @OutstandingAmount,
                OtherLenderRelationships = @OtherLenderRelationships,
                Status = @Status,
                AssignedToUserId = @AssignedToUserId,
                AuthorizedCapital = @AuthorizedCapital,
                PaidUpCapital = @PaidUpCapital,
                MCALastFetchedAt = @MCALastFetchedAt,
                UpdatedAt = @UpdatedAt,
                UpdatedBy = @UpdatedBy
            WHERE Id = @Id AND IsDeleted = 0";

        var rowsAffected = await connection.ExecuteAsync(sql, customer);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            UPDATE Customers
            SET IsDeleted = 1, UpdatedAt = @UpdatedAt
            WHERE Id = @Id";

        var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id, UpdatedAt = DateTime.UtcNow });
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsByPANAsync(string panNumber)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"
            SELECT COUNT(1) FROM Customers
            WHERE PANNumber = @PANNumber AND IsDeleted = 0";

        var count = await connection.ExecuteScalarAsync<int>(sql, new { PANNumber = panNumber });
        return count > 0;
    }
}
