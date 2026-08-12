using AutoParts.Models.Identity;
using AutoParts.ViewModels;
using AutoParts.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AutoParts.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;


    public AccountController(
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(
        LoginViewModel model,
        string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;


        if (!ModelState.IsValid)
            return View(model);


        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);


        if (result.Succeeded)
        {
            _logger.LogInformation(
                "Usuário {Email} realizou login.",
                model.UserName);


            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);


            return RedirectToAction(
                "Index",
                "Dashboard");
        }


        ModelState.AddModelError(
            string.Empty,
            "Usuário ou senha inválidos.");


        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(
            "Login",
            "Account");
    }
}