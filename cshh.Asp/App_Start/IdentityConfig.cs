using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using cshh.Asp.Models;
using System.Web.Configuration;

namespace cshh.Asp
{
    //public class EmailService : IIdentityMessageService
    //{
    //    public Task SendAsync(IdentityMessage message)
    //    {
    //        // Plug in your email service here to send an email.
    //        return Task.FromResult(0);
    //    }
    //}


    public class EmailService : IIdentityMessageService
    {
        string _host;
        int _port;
        string _mailFrom;
        Func<string> _getPassword;

        public EmailService(string host, int port, string mailFrom, Func<string> passwordGetter)
        {
            //System.Configuration.AppSettingsSection

            _host = host;
            _port = port;
            _mailFrom = mailFrom;
            _getPassword = passwordGetter;
        }
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Send(message.Body, message.Subject, message.Destination);
        }

        public Task Send(string body, string subject, string recipients)
        {
            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient(_host, _port);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(_mailFrom, _getPassword());
            var message = new System.Net.Mail.MailMessage(_mailFrom, recipients)
            {
                IsBodyHtml = true,
                Body = body,
                Subject = subject
            };
            return smtpClient.SendMailAsync(message);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Код, полученный по телефону", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Ваш код безопасности: {0}"
            });
            manager.RegisterTwoFactorProvider("Код из сообщения", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Код безопасности",
                BodyFormat = "Ваш код безопасности: {0}"
            });

            var section = WebConfigurationManager.GetSection("appSettingsEncrypted") as System.Collections.Specialized.NameValueCollection;


            // todo check encode section if in real environment
            //#if !DEBUG
            //            // todo убрать кудато, надо чтоб запускалось на той машине где будет жить, или через утилиту Aspnet_regiis.exe после публикации
            //            //Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
            //            //ConfigurationSection appSettings = config.GetSection("appSettingsEncrypted");
            //            //{
            //            //    appSettings.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            //            //    config.Save();
            //            //}
            //#endif

            manager.EmailService = new EmailService(section["MailHost"], Convert.ToInt32(section["MailPort"]), section["Mail"], () => section["MailP"]);

            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
