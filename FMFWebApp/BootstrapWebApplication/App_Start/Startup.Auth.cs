using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using BootstrapWebApplication.Models;

namespace BootstrapWebApplication
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            
            

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            // Release auth info
            var googleAuth = new
            {
                Id = "385585572512-ho4vi6hgv0780blkg4egklu1qmubmkp7.apps.googleusercontent.com",
                Secret = "P_4B6q9KL-eKITq4E1Rljb9U"
            };
            var fbAuth = new
            {
                Id = "520500291413229",
                Secret = "db10dab9d4e6b680a323f43df437f130"
            };

            // Debug auth info
            if (Kthusid.Helpers.Debug.IsDebugMode)
            {
                googleAuth = new
                {
                    Id = "385585572512-atk08dp0qcna1fu4q9iek556e8s1lcb1.apps.googleusercontent.com",
                    Secret = "lGkpQA9CpL3LuxZTxte0FS7t"
                };
                fbAuth = new
                {
                    Id = "261016004099052",
                    Secret = "15f511eb240996bafba2c62102bc2065"
                };
            }

            // https://console.developers.google.com/project/apps~valid-banner-664
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = googleAuth.Id,
                ClientSecret = googleAuth.Secret
            });

            // https://developers.facebook.com/apps/261016004099052/dashboard/
            app.UseFacebookAuthentication(appId: fbAuth.Id, appSecret: fbAuth.Secret);
        }
    }
}