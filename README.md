<h1>Driving & Vehicle License Department (DVLD) System</h1>
A comprehensive, enterprise-grade management system designed to automate the workflows of a Driving License Department. This project serves as a full-scale implementation of Object-Oriented Programming (OOP), Relational Database Design, and 3-Tier Software Architecture.

<img width="880" height="708" alt="Screenshot 2026-03-11 212515" src="https://github.com/user-attachments/assets/52f53426-ab87-4b41-8c5b-629d65843844" />

<img width="1919" height="1021" alt="Screenshot 2026-03-11 212533" src="https://github.com/user-attachments/assets/50bcc67d-5833-4ec5-b098-ee1ba501d914" />

________________________________________________________________________________________________________________
<h3>(IMPORTANT SECTION)</h3>
Testing Credentials
To access the system with full administrative privileges:

Username: zoo

Password: 1111
________________________________________________________________________________________________________________

<h3>Architectural Overview</h3>
The system is architected into three distinct layers to ensure a clean Separation of Concerns (SoC), making it highly maintainable and scalable:

Data Access Layer (DAL): Handles all communication with the SQL Server database using optimized ADO.NET queries and secure data handling patterns.
Business Logic Layer (BLL): The "Brain" of the system. It enforces all business rules (e.g., age validation, test prerequisites, and license expiration logic) through C# classes and inheritance.
Presentation Layer (PL): A modular Windows Forms interface utilizing User Controls to provide a consistent and reusable UI/UX across different modules.

<h3>Tech Stack & Skills</h3>
Programming Language: C# (.NET Framework)
Database: Microsoft SQL Server
Persistence: ADO.NET (Disconnected Architecture)
UI/UX: WinForms with custom User Controls
Architecture: 3-Tier Design Pattern
Security: Multi-attempt Login System, Bitwise Permissions, and Password Persistence.

<h3>Key Modules & Functionalities</h3>
<h4>Person & User Management</h4>
Unified Person Registry: Centralized management for all individuals (Drivers, Users, Applicants).
Role-Based Security: Advanced user management with granular permissions and account status tracking (Active/Inactive).
Session Management: "Remember Me" login persistence using local file caching.

<h4>Driving License Lifecycle</h4>
Local Licenses: Comprehensive workflow for issuing new licenses, including application submission and status tracking.
International Licenses: Logic to issue global licenses based on active local credentials.
Renewals & Replacements: Automated handling for license renewals and replacements for lost or damaged cards.
Detain & Release: Full module for detaining licenses with fine management and release workflows.

<h4>Integrated Testing System</h4>
Appointment Scheduling: Manage slots for Vision, Written, and Street tests.
Tiered Prerequisites: The system strictly enforces that applicants pass tests in a specific order before a license is granted.
Trial Tracking: Automatic tracking of test attempts and "Retake Test" fee management.

<h3>Logic Highlights</h3>
Component Reusability: Developed a ctrlPersonCardWithFilter User Control, which is reused in nearly every module, significantly reducing code duplication.
Resource Management: Automated profile image handling that copies files to a project directory and renames them using GUIDs to prevent naming collisions.
Data Integrity: Used SQL Transactions and Parameters to ensure data consistency and prevent SQL injection.

<h3>Getting Started</h3>

Prerequisites<br>
Windows OS<br>
.NET Framework 4.7.2+<br>
Microsoft SQL Server<br>
Database Setup<br>
Open SQL Server Management Studio (SSMS)<br>

restore the DVLD database that will find in the database folder.
Update the connection string in DataLayer/clsDataAccessSettings.cs.
