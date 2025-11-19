# Sambandh CRM API Documentation

## Overview

This document describes the REST API endpoints for the NABKISAN Sambandh CRM System - a comprehensive customer database management system.

## Base URL

- Development: `https://localhost:5001/api`
- API Documentation: `https://localhost:5001/api-docs` (Swagger UI)

## API Structure

### Authentication
Currently, the API does not implement authentication. You should add authentication/authorization middleware before deploying to production.

## API Endpoints

### 1. Customers API (`/api/customers`)

Customer/Party Master operations for managing customer and prospect information.

#### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/customers` | Get all customers with pagination and filtering |
| GET | `/api/customers/{id}` | Get customer by ID with full details |
| POST | `/api/customers` | Create a new customer |
| PUT | `/api/customers/{id}` | Update an existing customer |
| DELETE | `/api/customers/{id}` | Delete a customer (soft delete) |
| POST | `/api/customers/fetch-mca-data` | Fetch company data from MCA/Probe42 API |
| POST | `/api/customers/check-duplicate` | Check for duplicate customers |
| GET | `/api/customers/statistics` | Get customer statistics for dashboard |

#### Query Parameters for GET `/api/customers`

- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 20)
- `searchTerm` (string, optional)
- `status` (CustomerStatus enum, optional)
- `legalConstitution` (LegalConstitution enum, optional)
- `businessSegmentId` (Guid, optional)

#### Request Body Example (POST)

```json
{
  "legalConstitution": "Company",
  "entityName": "ABC Private Limited",
  "cin": "U74999DL2020PTC123456",
  "panNumber": "ABCDE1234F",
  "dateOfIncorporation": "2020-01-15",
  "primaryBusinessActivity": "Agricultural Services",
  "annualTurnover": 5000000,
  "registeredAddress": "123 Business Park, New Delhi",
  "registeredPinCode": "110001",
  "primaryPhone": "9876543210",
  "primaryEmail": "contact@abc.com",
  "isExistingNABKISANCustomer": false,
  "businessSegmentIds": ["guid1", "guid2"]
}
```

---

### 2. Leads API (`/api/leads`)

Lead tracking and management operations.

#### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/leads` | Get all leads with pagination and filtering |
| GET | `/api/leads/{id}` | Get lead by ID |
| POST | `/api/leads` | Create a new lead |
| PUT | `/api/leads/{id}` | Update an existing lead |
| PATCH | `/api/leads/{id}/status` | Update lead status |
| GET | `/api/leads/{id}/status-history` | Get lead status history |
| GET | `/api/leads/pipeline-summary` | Get lead pipeline summary |
| GET | `/api/leads/follow-ups-due` | Get leads due for follow-up |
| PATCH | `/api/leads/{id}/assign` | Assign lead to user |
| GET | `/api/leads/my-leads` | Get my leads (for logged-in user) |

#### Query Parameters for GET `/api/leads`

- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 20)
- `searchTerm` (string, optional)
- `status` (LeadStatus enum, optional)
- `priority` (Priority enum, optional)
- `assignedToUserId` (string, optional)
- `fromDate` (DateTime, optional)
- `toDate` (DateTime, optional)

#### Request Body Example (POST)

```json
{
  "customerId": "guid",
  "leadSource": "Direct Walk-in",
  "productInterest": "Term Loan",
  "loanAmountRange": "10L-50L",
  "priority": "High",
  "assignedToUserId": "user-guid",
  "nextFollowUpDate": "2024-11-20",
  "notes": "Initial discussion about agricultural expansion"
}
```

---

### 3. Contact Persons API (`/api/contactpersons`)

Relationship management and contact person operations.

#### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/contactpersons` | Get all contact persons with pagination |
| GET | `/api/contactpersons/{id}` | Get contact person by ID |
| POST | `/api/contactpersons` | Create a new contact person |
| PUT | `/api/contactpersons/{id}` | Update an existing contact person |
| POST | `/api/contactpersons/link-to-customer` | Link contact person to customer |
| DELETE | `/api/contactpersons/unlink/{customerId}/{contactPersonId}` | Unlink contact person from customer |
| GET | `/api/contactpersons/by-customer/{customerId}` | Get all contact persons for a customer |
| GET | `/api/contactpersons/{contactPersonId}/customers` | Get all customers for a contact person |
| PUT | `/api/contactpersons/relationship` | Update relationship details |
| POST | `/api/contactpersons/check-duplicate` | Check for duplicate contact person |

#### Request Body Example (POST)

```json
{
  "fullName": "John Doe",
  "mobileNumber": "9876543210",
  "email": "john@example.com",
  "panNumber": "ABCDE1234F",
  "linkedInProfile": "https://linkedin.com/in/johndoe"
}
```

#### Link to Customer Example

```json
{
  "customerId": "customer-guid",
  "contactPersonId": "contact-guid",
  "roleInOrganization": "CEO",
  "isDecisionMaker": true,
  "isPreferredContact": true
}
```

---

### 4. Communications API (`/api/communications`)

Communication log and campaign management operations.

#### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/communications` | Get all communication logs with pagination |
| GET | `/api/communications/{id}` | Get communication log by ID |
| POST | `/api/communications` | Create a new communication log entry |
| GET | `/api/communications/customer/{customerId}/history` | Get communication history for a customer |
| GET | `/api/communications/lead/{leadId}/history` | Get communication history for a lead |
| GET | `/api/communications/summary` | Get communication summary/statistics |
| POST | `/api/communications/campaigns/sms` | Send bulk SMS campaign |
| POST | `/api/communications/campaigns/email` | Send bulk email campaign |
| GET | `/api/communications/campaigns/{campaignId}/status` | Get campaign delivery status |

#### Query Parameters for GET `/api/communications`

- `pageNumber` (int, default: 1)
- `pageSize` (int, default: 20)
- `customerId` (Guid, optional)
- `leadId` (Guid, optional)
- `communicationType` (CommunicationType enum, optional)
- `fromDate` (DateTime, optional)
- `toDate` (DateTime, optional)

#### Request Body Example (POST)

```json
{
  "customerId": "customer-guid",
  "communicationDate": "2024-11-19T10:30:00Z",
  "communicationType": "PhoneCall",
  "direction": "Outbound",
  "subject": "Follow-up on loan inquiry",
  "summary": "Discussed loan requirements and documentation needed",
  "nextActionRequired": "Send document checklist via email"
}
```

---

### 5. Reports API (`/api/reports`)

Reports and analytics operations.

#### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/reports/dashboard/management` | Get management dashboard data |
| GET | `/api/reports/dashboard/rm` | Get relationship manager dashboard |
| GET | `/api/reports/customers/summary` | Get customer summary report |
| GET | `/api/reports/leads/pipeline` | Get lead pipeline report |
| GET | `/api/reports/activity` | Get activity report |
| GET | `/api/reports/leads/conversion-rate` | Get conversion rate report |
| GET | `/api/reports/leads/aging-analysis` | Get aging analysis report |
| POST | `/api/reports/export/excel` | Export report to Excel |
| POST | `/api/reports/export/pdf` | Export report to PDF |
| POST | `/api/reports/schedule` | Schedule a report |

#### Query Parameters

- `fromDate` (DateTime, optional)
- `toDate` (DateTime, optional)
- `userId` (string, optional)
- `assignedToUserId` (string, optional)

---

### 6. Master Data API (`/api/masterdata`)

System administration and master data management operations.

#### Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/masterdata/business-segments` | Get all business segments |
| POST | `/api/masterdata/business-segments` | Create a new business segment |
| PUT | `/api/masterdata/business-segments/{id}` | Update a business segment |
| DELETE | `/api/masterdata/business-segments/{id}` | Delete a business segment |
| GET | `/api/masterdata/categories/{category}` | Get master data by category |
| GET | `/api/masterdata/categories` | Get all master data categories |
| POST | `/api/masterdata` | Create master data entry |
| PUT | `/api/masterdata/{id}` | Update master data entry |
| DELETE | `/api/masterdata/{id}` | Delete master data entry |
| GET | `/api/masterdata/settings` | Get system settings |
| PUT | `/api/masterdata/settings` | Update system settings |
| POST | `/api/masterdata/import/excel` | Bulk import data from Excel |
| POST | `/api/masterdata/export` | Export data |
| GET | `/api/masterdata/health` | Get system health and statistics |

#### Master Data Categories

- `LeadSource`
- `ProductCategory`
- `DocumentType`
- `RejectionReason`
- `CommunicationTemplate`
- `RoleInOrganization`

---

## Enumerations

### LegalConstitution
- `Company`
- `Society`
- `TrustNGO`
- `PartnershipLLP`
- `IndividualProprietor`

### CustomerStatus
- `Prospect`
- `ActiveLead`
- `Customer`
- `Dormant`
- `Archived`

### LeadStatus
- `New`
- `InProgress`
- `Documentation`
- `SubmittedToCredit`
- `Dropped`
- `Converted`

### Priority
- `Low`
- `Medium`
- `High`

### CommunicationType
- `PhoneCall`
- `Email`
- `Meeting`
- `SiteVisit`
- `WhatsApp`
- `SMS`

### CommunicationDirection
- `Inbound`
- `Outbound`

---

## Response Format

All API responses follow a standard format:

### Success Response
```json
{
  "success": true,
  "message": "Operation successful",
  "data": { /* response data */ },
  "errors": []
}
```

### Error Response
```json
{
  "success": false,
  "message": "Error message",
  "data": null,
  "errors": ["Error detail 1", "Error detail 2"]
}
```

### Paginated Response
```json
{
  "success": true,
  "message": "Operation successful",
  "data": {
    "items": [ /* array of items */ ],
    "totalCount": 100,
    "pageNumber": 1,
    "pageSize": 20,
    "totalPages": 5,
    "hasPreviousPage": false,
    "hasNextPage": true
  },
  "errors": []
}
```

---

## HTTP Status Codes

- `200 OK` - Successful GET, PUT, PATCH requests
- `201 Created` - Successful POST requests
- `400 Bad Request` - Invalid request data
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server error

---

## Running the API

1. Restore packages:
   ```bash
   dotnet restore
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --project src/SambhandhCRM.Web
   ```

4. Access Swagger UI at: `https://localhost:5001/api-docs`

---

## Implementation Notes

### TODO Items

The current implementation includes controller scaffolding with TODO comments for:

1. **Data Access Layer**: Implement Entity Framework Core with SQL Server
2. **Business Logic Layer**: Add service classes for business logic
3. **Authentication**: Implement JWT-based authentication
4. **Authorization**: Add role-based access control
5. **Validation**: Implement FluentValidation for request validation
6. **MCA Integration**: Integrate with Probe42/MCA API for company data
7. **SMS Gateway**: Integrate SMS service provider
8. **Email Service**: Configure SMTP for email sending
9. **File Upload**: Implement document storage (Azure Blob/local file system)
10. **Logging**: Add structured logging with Serilog
11. **Caching**: Implement Redis caching for frequently accessed data
12. **Background Jobs**: Use Hangfire for scheduled reports and campaigns

### Database Setup

You need to:
1. Configure connection string in `appsettings.json`
2. Create Entity Framework Core DbContext
3. Add migrations and create database
4. Seed initial master data

### Security Considerations

Before production deployment:
1. Add authentication middleware (JWT/OAuth)
2. Implement authorization policies
3. Add rate limiting
4. Configure CORS properly (restrict origins)
5. Enable HTTPS only
6. Add input validation and sanitization
7. Implement audit logging
8. Add API versioning
9. Configure proper error handling

---

## Contact

For questions or support, contact NABKISAN Finance Limited IT Team.

---

**Version:** 1.0
**Last Updated:** November 19, 2024
