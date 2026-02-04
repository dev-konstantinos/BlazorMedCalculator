# BlazorMedCalculator

**BlazorMedCalculator** is a Blazor-based medical calculator web application built with **ASP.NET**.  
The project is intended as a **demo and learning project**, focusing on realistic backend architecture, infrastructure concerns (authentication, email, PDF generation), and extensibility rather than polished UI.

The application is under active development, and its functionality grows incrementally.

---

## ✨ Features (Current State)

- 🔍 Content search (under construction)
- 🧮 Online medical calculations
- 📄 PDF report generation (for registered users)
- 📧 PDF report delivery via email (for registered users)
- 📝 Simple article/content editing using Markdown files
- 🔐 User authentication (ASP.NET Identity)
- ⚙️ Environment-based infrastructure (DEV / PROD email switch)

---

## 🎯 Project Goals

- Choose realistic Blazor backend architecture
- Apply clean separation between logic and infrastructure
- Solve practical problems (auth, email, PDF, content)
- Serve as a foundation for future modularization and expansion

## 🧱 Project Structure (Overview)

BlazorMedCalculator.Web
│
├── Calculators      // Medical calculation logic
├── Components       // Blazor UI components
├── Content          // Markdown-based articles
├── Data             // EF Core, DbContext, migrations
├── Endpoints        // Application endpoints
├── Interfaces       // Service abstractions
├── Models           // Domain and DTO models
│
├── Services         // Concrete service implementations
│   ├── Content      // Markdown loading & processing (Markdig)
│   ├── Email        // Email services (SMTP / Fake for testing)
│   ├── Identity     // Authentication & user management
│   └── Pdf          // PDF generation (QuestPDF)
│
├── wwwroot          // Static assets
├── appsettings.json
├── Program.cs

---

## 🛠️ Technologies Used

- ASP.NET / Blazor
- Entity Framework Core
- Microsoft SQL Server
- Markdig – Markdown processing
- QuestPDF – PDF generation
- ASP.NET Identity – Authentication and email flows

## 📧 Email Infrastructure (DEV / PROD Switch)

This project implements a real email infrastructure with a safe development mode.
Real email sending can be disabled locally while preserving the full ASP.NET Identity email confirmation flow.
Environment Switch (DEV vs PROD): Email behavior is controlled by a single configuration flag.

🔴 Production (appsettings.json):
"Email": { "Enabled": true }

SmtpEmailService:

- Sends real emails with SMTP server
- SMTP options can be easily adjusted
- Used in Production

🟢 Development (appsettings.Development.json):
"Email": { "Enabled": false }

FakeEmailService:

- Does not send emails
- Logs email content to application logs
- Used only in Development mode

## Medical Disclaimer

This project is intended for informational and educational purposes only.  
It is **not** intended to provide medical advice, diagnosis, or treatment.
