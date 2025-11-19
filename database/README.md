# Database Setup Guide

This guide will help you set up the SQL Server database for Sambandh CRM.

## Prerequisites

- SQL Server 2019 or later (Express Edition is fine for development)
- SQL Server Management Studio (SSMS) or Azure Data Studio

## Setup Steps

### 1. Create the Database

You can either:

**Option A: Using SSMS**
1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Right-click on "Databases" and select "New Database"
4. Name it `SambhandhCRM`
5. Click OK

**Option B: Using T-SQL**
```sql
CREATE DATABASE SambhandhCRM;
GO
```

### 2. Run the Schema Script

1. Open the `schema.sql` file in SSMS or Azure Data Studio
2. Make sure you're connected to your SQL Server instance
3. Execute the script (F5 or click Execute)

This will create all the necessary tables:
- Customers (Party Master)
- BusinessSegments
- CustomerBusinessSegments
- ContactPersons
- CustomerContactPersons
- Leads
- LeadStatusHistory
- CommunicationLogs

### 3. Configure Connection String

Update the connection string in your `appsettings.json` or `appsettings.Development.json`:

**For Windows Authentication (Recommended for local development):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SambhandhCRM;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**For SQL Server Authentication:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SambhandhCRM;User Id=your_username;Password=your_password;TrustServerCertificate=True;"
  }
}
```

**For Azure SQL Database:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=SambhandhCRM;Persist Security Info=False;User ID=your_username;Password=your_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

### 4. Verify the Setup

1. Run the Blazor application
2. Navigate to the Customers page (/customers) or Leads page (/leads)
3. If there are no errors, the database is connected successfully!

## Database Architecture

### Core Tables

#### Customers (Party Master)
Central repository for all customer and prospect information including:
- Basic entity information (name, PAN, CIN, etc.)
- Contact details
- Banking relationships
- MCA integration data

#### Leads
Tracks business opportunities with:
- Lead tracking and status management
- Document checklist
- Assignment and follow-up tracking
- Conversion tracking

#### ContactPersons
Individual profiles for key personnel in customer organizations

#### CommunicationLogs
Complete audit trail of all customer interactions

## Sample Data

The schema script automatically inserts sample business segments:
- FPO (Farmer Producer Organizations)
- Agri-Startup
- MFI (Microfinance Institutions)
- SHG (Self Help Groups)
- Agri-SME
- Dairy Cooperatives
- Input Dealers
- Others

## Technology Stack

- **ORM**: Dapper (lightweight, high-performance)
- **Database**: SQL Server 2019+
- **Connection Management**: Microsoft.Data.SqlClient

## Troubleshooting

### Connection Issues

1. **Cannot connect to SQL Server**
   - Ensure SQL Server is running
   - Check if SQL Server Browser service is running
   - Verify firewall settings
   - For named instances, use: `Server=localhost\SQLEXPRESS`

2. **Login failed for user**
   - Verify username and password
   - Check if SQL Server authentication is enabled
   - Ensure the user has access to the database

3. **Certificate validation errors**
   - Add `TrustServerCertificate=True` to connection string
   - Only use in development; for production, use proper SSL certificates

### Performance Tips

1. The schema includes indexes on frequently queried columns
2. Consider adding additional indexes based on your query patterns
3. Regularly update statistics: `EXEC sp_updatestats`
4. Monitor query performance and add indexes as needed

## Migration from Mock Data

If you have been using the application with mock data and want to migrate:

1. Export any test data you want to keep
2. Run the schema script to create tables
3. Import your data using SQL INSERT statements or bulk import tools
4. Update repository implementations if needed

## Next Steps

1. Implement API controllers to expose data endpoints
2. Add authentication and authorization
3. Implement data validation
4. Add audit logging
5. Set up backup strategies
