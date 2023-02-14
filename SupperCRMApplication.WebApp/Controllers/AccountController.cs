using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Common;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;

namespace SupperCRMApplication.WebApp.Controllers
{
    public class AccountController : ControllerBase 
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        //GET
        public IActionResult Login()
        {
            return View();
        }      

        //POST İşlemi
        [HttpPost]
        public IActionResult Login(AuthenticateModel model )
        {
            AjaxResponseModel<string> response =new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                var user = _userService.Authenticate(model);
                if(user != null)
                {
                    response.Success = "Giriş işlemi başarılı bir şekilde yapıldı.";
                    HttpContext.Session.SetString(Constants.Session_Name, user.Name);
                    HttpContext.Session.SetString(Constants.Session_Role, user.Role);
                    HttpContext.Session.SetInt32(Constants.Session_Id, user.Id);
                }
                else
                {
                    response.AddError(nameof(model.Username), "Hatalı Kullanıcı Adı ve/veya Şifre.");
                }
                return Json(response);
            }
            AddModelStateErrorsToAjaxResponse(response);
            return Json(response);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult CreateFakeUser()
        {
            _userService.Create(new CreateUserModel
            {
                Name = "Kubilay Bayindir",
                Email ="kubişaybayindirr@gmail.com",
                Password ="123456",
                Locked=false,
                RePassword = "123456",
                Role="admin",
                Username="kubilayb"
            });
            return Json("ok");
        }    
    }
}
