// See https://aka.ms/new-console-template for more information
using System;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
Console.WriteLine("Hello, World!");
try
{
    Console.WriteLine("Please enter your email address");
    string senderEmail = Console.ReadLine()!;
    if (senderEmail is null)
    {
        Console.WriteLine("Empty email address are not allowed");
        return;
    }
    if (!senderEmail.Contains("@"))
    {
        Console.WriteLine("Invalid email address format");
        return;
    }
    string domain = senderEmail.Split('@')[1].ToLower();
    string smtpServer = domain switch
    {
        "gmail.com" => "smtp@gmail.com",
        "yahoo.com" => "smtp.mail.yahoo.com",
        "outlook.com" or "hotmail.com" => "smtp.office365.com",
        "zoho.com" => "smtp.zoho.com",
        _ => null!
    };
    if (string.IsNullOrEmpty(smtpServer))
    {
        Console.WriteLine("You have entered custom email, so please provide smtp server");
        smtpServer = Console.ReadLine()!;
        if (string.IsNullOrEmpty(smtpServer))
        {
            Console.WriteLine("Smtp server cannot be null");
            return ;
        }

    }
    Console.WriteLine("Please enter receiver email address");
    string receiverEmail = Console.ReadLine()!;
    if (receiverEmail is null)
    {
        Console.WriteLine("Empty receive email address are not allowed");
        return;
    }
    if (!receiverEmail.Contains("@"))
    {
        Console.WriteLine("Invalid email address format");
        return;
    }
    Console.WriteLine("Please enter your app password \n(Note dont provide email password.\n" +
        "              App password usually provided by your mail provider)");
    string password = Console.ReadLine()!;
    if (password is null)
    {
        Console.WriteLine("Empty password are not allowed");
        return;
    }
    var email = new MimeMessage();
    email.From.Add(new MailboxAddress("Sender Name", senderEmail)); 
    email.To.Add(new MailboxAddress("Receiver Address", receiverEmail));
    email.Subject = "Testing out email sending";
    var textPart = new TextPart("plain")
    {
        Text = "Hello, this email has an attachment!"
    };
    string basePath = AppDomain.CurrentDomain.BaseDirectory;
    string attachmentPath = Path.Combine(basePath, "Resources","QR.pdf");
    if (!File.Exists(attachmentPath))
    {
        Console.WriteLine("⚠️ Attachment not found: " + attachmentPath);
        return;
    }
    var attachment = new MimePart("application", "pdf")
    {
        Content = new MimeContent(File.OpenRead(attachmentPath)),
        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
        ContentTransferEncoding = ContentEncoding.Base64,
        FileName = Path.GetFileName(attachmentPath)
    };

    var multipart = new Multipart("mixed");
    multipart.Add(textPart);
    multipart.Add(attachment);

    email.Body = multipart;
    using (var smtp = new SmtpClient())
    {
        try
        {
            // Try Gmail's preferred submission port (587 + STARTTLS)
            smtp.Connect(smtpServer, 587, SecureSocketOptions.StartTls);
            Console.WriteLine("✅ Connected via port 587 (STARTTLS).");
        }
        catch (Exception ex1)
        {
            Console.WriteLine("⚠️ Port 587 failed: " + ex1.Message);

            // Fall back to port 465 (SSL)
            smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
            Console.WriteLine("✅ Connected via port 465 (SSL).");
        }
        //smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
        smtp.Authenticate(senderEmail, password); 
        smtp.Send(email);
        smtp.Disconnect(true);  
    }
    Console.WriteLine("EMail send");
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
}
