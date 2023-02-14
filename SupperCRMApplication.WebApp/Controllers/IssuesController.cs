using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Common;
using SupperCRMApplication.Entities;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;
using Microsoft.AspNetCore.Http;

namespace SupperCRMApplication.WebApp.Controllers
{
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly IUserService _userService;

        public IssuesController(IIssueService issueService, IUserService userService)
        {
            _issueService = issueService;
            _userService = userService;
        }

        // GET: GetUserList
        public ActionResult GetUserList()
        {
            AjaxResponseModel<List<User>> response = new AjaxResponseModel<List<User>>();

            string role = HttpContext.Session.GetString(Constants.Session_Role);

            if (role == Constants.Role_Admin)
            {//adminse bütün kullanıcıları userServise 'den getir.
                response.Data = _userService.List();
            }
            else
            {
                //admin değilse sadece kendini  userServise 'den getir.
                int userid = HttpContext.Session.GetInt32(Constants.Session_Id).GetValueOrDefault();

                var user = _userService.GetById(userid);
                //servis referans döngüsünde aldığı bir hatadan dolayı issueleri nulla çektik.
                //Geriye bu datayı dönerken sorun yaşamaması için 
                user.Issues = null;

                response.Data = new List<User> { user };
            }
            //ada göre alfabetik sıralanıp döndürüyoruz.
            response.Data = response.Data.OrderBy(x => x.Name).ToList();
            return Json(response);
        }

        // GET: IssuesController
        public ActionResult Index(string search = "")
        {
            List<Issue>? issues = null;
            string role = HttpContext.Session.GetString(Constants.Session_Role);
            int userid = HttpContext.Session.GetInt32(Constants.Session_Id).GetValueOrDefault();
            //search yapılmıyorsa
            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                //adminse
                if (role == Constants.Role_Admin)
                {
                    //bütün kayıtları çek
                    issues = _issueService.List();
                }
                else
                {
                    //aksi halse belli id deki issue leri getirir.
                    issues = _issueService.ListByUserId(userid);
                }
            }
            else
            {
                //adminse
                if (role == Constants.Role_Admin)
                {
                    //bütün kayıtları çek
                    issues = _issueService.ListBySearch(search);
                }
                else
                {
                    //aksi halse belli id deki issue leri getirir.
                    issues = _issueService.ListBySearch(search, userid);
                }
                ViewData["search"] = search;
            }
            //tamamlaanlar aşağıda kalacak şekilde (tarih sırasıda var ) sıralanır.
            //Son oluşturan en yukarıda olaccak şekilde
            return View(issues.OrderBy(x => x.Completed).ThenByDescending(x => x.CreatedAt).ToList());
        }

        // POST: Issues/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreateIssueModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            if (ModelState.IsValid)
            {
                var issue = _issueService.Create(model);

                //todo: burada görev eklendiği için kullanıcıya bildirim eklenmeli.

                response.Success = "Görev Eklendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // GET: Issues/Details/5
        public ActionResult Details(int id)
        {
            var issue = _issueService.GetById(id);
            return Json(new AjaxResponseModel<Issue> { Data = issue });
        }

        // POST: Issues/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditIssueModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                var issue = _issueService.Update(id, model);

                //todo : burada görev güncellendi için kullanıcıya bildirim eklenmeli

                response.Success = "Görev Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // POST: Issues/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            try
            {
                _issueService.Delete(id);
                response.Success = "Görev Başarılı Bir Şekilde Silindi.";
            }
            catch (Exception ex)
            {

                response.AddError("ex", ex.Message);
            }
            return Json(response);
        }
    }
}
