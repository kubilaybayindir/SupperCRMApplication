using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Common;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;
using SupperCRMApplication.WebApp.Filters;

namespace SupperCRMApplication.WebApp.Controllers
{
    public class AccountController : ControllerBase 
    {
        private readonly IUserService _userService;
        private readonly ILogService _logService;

        public AccountController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
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

                    _logService.Create("Sistem girişi yapıldı.", Entities.LogType.SystemEntry, user.Id);
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
        [Auth]
        public IActionResult LogOut()
        {
            int? userId=HttpContext.Session.GetInt32(Constants.Session_Id);
            if(userId != null)
            {
                _logService.Create("Sistem çıkışı yapıldı.",Entities.LogType.SystemEntry, userId.Value);
            }

            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }

        [Auth]
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
