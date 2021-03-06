﻿using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Orchard.Security;
using Orchard.Environment.Extensions;
using Orchard.Themes;

namespace MC.Auth.Controllers {
    [Themed]
    [OrchardSuppressDependency("Orchard.Users.Controllers.AccountController")]
    public class AccountController : Controller {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }

        public void LogOn() {
            if (Request.IsAuthenticated) {
                return;
            }

            var redirectUri = Url.Content("~/");

            Request.GetOwinContext().Authentication.Challenge(new AuthenticationProperties {RedirectUri = redirectUri}, OpenIdConnectAuthenticationDefaults.AuthenticationType);
        }

        public void LogOff() {
            Request.GetOwinContext().Authentication.SignOut(
              OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType); //OpenID Connect sign-out request.
        }

        public ActionResult AccessDenied() {
            return View();
        }
    }
}
