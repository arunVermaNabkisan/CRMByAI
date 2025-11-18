# Sambandh CRM - NABKISAN Finance Limited

## Overview
Sambandh CRM is a customer database management system designed for NABKISAN Finance Limited to maintain comprehensive records of existing and potential clients, track leads, manage relationships, and enable targeted communication.

## Technology Stack
- **Framework**: .NET 9
- **UI**: Blazor Server
- **Database**: SQL Server (SSDT Project)
- **Language**: C#

## Solution Structure

```
CRMByAI/
├── src/
│   ├── SambhandhCRM.Web/           # Blazor Server Application
│   ├── SambhandhCRM.Core/          # Core Business Logic & Models
│   └── SambhandhCRM.Database/      # SSDT Database Project
│       ├── Tables/                 # Database Tables
│       ├── Views/                  # Database Views
│       ├── StoredProcedures/       # Stored Procedures
│       └── Scripts/                # Deployment Scripts
└── SambhandhCRM.sln               # Solution File
```

## Database Schema

### Core Tables

#### 1. Party Master & Related Tables
- **PartyMaster**: Main customer/organization repository
  - Supports 5 legal constitutions: Company, Society, Trust/NGO, Partnership/LLP, Individual/Proprietor
  - Business segment classification (FPO, Agri-Startup, MFI, NBFC, etc.)
  - MCA integration support for company verification
  - Customer status tracking (Prospect, Active Lead, Customer, Dormant, Archived)

- **PartyAddress**: Multiple addresses per party (Registered, Office, Correspondence, Residence)
- **PartyContact**: Multiple contact methods (Phone, Mobile, Email, WhatsApp)
- **BankingInfo**: Banking relationships and facility details

#### 2. Individual & Relationship Management
- **Individual**: Key personnel and contact persons
  - Personal and professional details
  - Identification (PAN, DIN, Aadhaar)
  - Social media profiles

- **PartyIndividualRelationship**: Links individuals to organizations
  - Roles: Chairman, CEO/MD, Director, CFO, Authorized Signatory, etc.
  - Decision-maker flags
  - Contact preferences

#### 3. Lead Management
- **Lead**: Lead tracking and pipeline management
  - Auto-generated Lead ID (LEAD-YYYYMM-9999)
  - Lead sources (Walk-in, Website, Referral, Campaign)
  - Product interest and loan amount
  - Status workflow (New → In-Progress → Documentation → Submitted → Dropped/Converted)
  - Document checklist tracking
  - Aging analysis

- **LeadActivity**: Activity log for leads
  - Status changes
  - Follow-up tracking
  - Next actions

#### 4. Communication Management
- **CommunicationLog**: All customer interactions
  - Types: Phone Call, Email, Meeting, Site Visit, WhatsApp, SMS
  - Direction tracking (Inbound/Outbound)
  - Bulk communication support
  - Delivery status for SMS/Email
  - Follow-up scheduling

#### 5. User Management
- **User**: System users and employees
  - Authentication and security
  - Reporting hierarchy
  - Session management
  - User preferences and notifications

- **Role**: Role-based access control
  - Role hierarchy
  - Permission management (JSON format)

- **UserRole**: User-to-role mapping (many-to-many)

#### 6. Master Data Configuration
- **MasterDataCategory**: Configurable dropdown categories
  - Business Segments
  - Lead Sources
  - Product Types
  - Communication Types
  - Document Types

- **MasterDataValue**: Actual dropdown values
  - Parent-child relationships for dependent dropdowns
  - System-defined vs user-defined values
  - Display order and UI customization

#### 7. Document Management
- **Document**: Document repository
  - Multi-entity support (Party, Individual, Lead, Communication)
  - Version control
  - Verification workflow
  - Multiple storage backends (Local, Azure, AWS)
  - Security levels and access control

#### 8. Audit & Compliance
- **AuditLog**: Comprehensive audit trail
  - All CRUD operations
  - Login/logout tracking
  - Change history (old vs new values in JSON)
  - IP address and session tracking
  - Performance metrics

## Key Features

### Data Quality Controls
- **Duplicate Prevention**:
  - Hard checks: PAN, CIN, Registration Number, Mobile+Name
  - Soft warnings: Similar names, addresses, phone numbers

- **Validation**:
  - PAN format: AAAAA9999A
  - CIN format: 21 alphanumeric characters
  - Mobile: 10 digits starting with 6-9
  - PIN Code: 6 digits

### Security Features
- Role-based access control
- Audit trails for all operations
- Data encryption support (Aadhaar, Bank Account)
- Session management
- Password policies

### Business Capabilities
- Customer 360-degree view
- Lead pipeline management
- Relationship mapping
- Communication tracking
- Document management
- Configurable master data
- Comprehensive reporting

## Database Design Principles

1. **Soft Delete**: All tables use `IsDeleted` flag instead of physical deletion
2. **Audit Fields**: Every table has CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
3. **Indexing**: Strategic indexes for performance on frequently queried columns
4. **Constraints**: Check constraints for data integrity
5. **Unique Constraints**: Filtered unique constraints (excluding deleted records)
6. **Foreign Keys**: Proper referential integrity
7. **Computed Columns**: For calculated fields (e.g., FullName, DaysInPipeline)

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server 2019 or later
- Visual Studio 2022 or JetBrains Rider

### Building the Solution
```bash
dotnet restore
dotnet build
```

### Running the Application
```bash
cd src/SambhandhCRM.Web
dotnet run
```

### Database Deployment
The SSDT project can be deployed using:
1. Visual Studio (Publish Database Project)
2. SqlPackage.exe command-line tool
3. Azure DevOps CI/CD pipeline

## Integration Points

### Planned Integrations
1. **MCA/Probe42 API**: Company verification and director information
2. **SMS Gateway**: Bulk SMS and delivery tracking (DLT compliant)
3. **Email Server**: SMTP for email campaigns
4. **WhatsApp Business API** (Phase 2)
5. **Credit Bureau/CIBIL** (Phase 2)
6. **GST Verification** (Phase 2)

## Module Breakdown

### Module 1: Customer Database (Party Master)
- Organization and individual management
- Multi-address and multi-contact support
- Banking relationship tracking
- MCA integration for companies

### Module 2: Lead Tracking
- Simple lead management without complex workflows
- Lead source tracking
- Pipeline status management
- Document checklist
- Conversion tracking

### Module 3: Relationship Management
- Individual profiles
- Organization-person mapping
- Role and designation tracking
- Decision-maker identification

### Module 4: Communication Log
- Interaction tracking
- Bulk communication (SMS/Email)
- Campaign management
- Follow-up scheduling

### Module 5: Reports & Analytics
- Customer reports (segment-wise, geographic)
- Lead pipeline reports
- Activity reports
- Management dashboards
- RM dashboards

### Module 6: System Administration
- User management
- Role management
- Master data configuration
- System settings

## Business Segments Supported
- FPO (Farmer Producer Organization)
- Agri-Startup
- MFI (Microfinance Institution)
- NBFC (Non-Banking Financial Company)
- HFC (Housing Finance Company)
- CSR Partner
- PACS (Primary Agricultural Credit Society)
- Others (configurable)

## Success Metrics
- Data accuracy rate: >95%
- User adoption rate: >80%
- System uptime: >99%
- Lead conversion tracking
- Customer data completeness

## Version
- **Document Version**: 2.0
- **Date**: November 17, 2024
- **Status**: Initial Database Schema Implementation

## License
Proprietary - NABKISAN Finance Limited

## Contact
For technical queries, contact the IT Team at NABKISAN Finance Limited.
