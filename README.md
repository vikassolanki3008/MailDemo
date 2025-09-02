# 📧 Quartz.NET Email Scheduler with MailKit/MimeKit (Console App)

A simple **.NET console app** demonstrating how to use **Quartz.NET** to schedule background jobs.  
This project runs a scheduled task every **1 minute** (using a cron expression).  
The scheduled job shows how to **send emails using MailKit + MimeKit**.  

---

## 🚀 Features
- Background job scheduling using **Quartz.NET**  
- Configurable **cron expressions** (run every 1 min, daily at 6 PM, etc.)  
- Email sending using **MailKit + MimeKit**   

---

## 🛠️ Tech Stack
- [.NET Console App](https://dotnet.microsoft.com/)  
- [Quartz.NET](https://www.quartz-scheduler.net/) — job scheduling  
- [MailKit](https://github.com/jstedfast/MailKit) — SMTP client  
- [MimeKit](https://github.com/jstedfast/MimeKit) — MIME message creation  

---

## 📦 Install Dependencies
```bash
dotnet add package Quartz
dotnet add package MailKit
dotnet add package MimeKit
```

More details: [Quartz Cron Documentation](https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html)
---
## Note
- For demonstrate you can hardcode the email but its worst pratice
- For sending email use email password not your personal gmail password

## Feedback
-Improvement in repo you are openly can change 
