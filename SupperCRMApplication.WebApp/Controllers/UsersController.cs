using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Entities;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;

namespace SupperCRMApplication.WebApp.Controllers
{
    public class UsersController:ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: UserController
        public ActionResult Index(string search = "")
        {
            List<User>? users = null;

            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                users = _userService.List();
            }
            else
            {
                users = _userService.ListBySearch(search);
                ViewData["search"] = search;
            }
            return View(users);
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            return Json(new AjaxResponseModel<User> { Data = user });
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.Create(model);

                    response.Success = "Kullanıcı Eklendi.";
                    return Json(response);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(nameof(model.Username), ex.Message);
                }
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            return Json(new AjaxResponseModel<User> { Data = user });
        }

        // POST: Users/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditUserModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _userService.Update(id, model);

                response.Success = "Müsteri Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            try
            {
                _userService.Delete(id);
                response.Success = "Müşteri Başarılı Bir Şekilde Silindi.";
            }
            catch (Exception ex)
            {

                response.AddError("ex", ex.Message);
            }
            return Json(response);
        }

        // POST: Users/ChangeUsername/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangeUsername(int id, ChangeUsernameModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _userService.ChangeUsername(id,model);

                response.Success = "Müsteri Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }
        // POST: Users/ChangeUsername/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int id, ChangePasswordModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _userService.ChangePassword(id, model);

                response.Success = "Müsteri Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

    }

}
