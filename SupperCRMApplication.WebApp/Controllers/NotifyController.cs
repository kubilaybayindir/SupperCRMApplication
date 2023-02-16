using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Common;
using SupperCRMApplication.Entities;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;
using SupperCRMApplication.WebApp.Filters;
using System.Runtime.InteropServices;

namespace SupperCRMApplication.WebApp.Controllers
{
    [Auth]
    public class NotifyController : ControllerBase
    {
        private readonly INotifyService _notifyService;

        public NotifyController(INotifyService notifyService)
        {
            _notifyService = notifyService;
        }

        // GET: Notify
        public ActionResult Index(string search = "")
        {
            List<Notify> notifies = null;

            int userid = HttpContext.Session.GetInt32(Constants.Session_Id).Value;

            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                notifies = _notifyService.ListByUserId(userid, null);
            }
            else
            {
                notifies = _notifyService.ListBySearch(search, userid);
                ViewData["search"] = search;
            }

            return View(notifies);
        }

        // GET: Notify/Edit/5
        public ActionResult Edit(int id, bool read)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            _notifyService.Update(id, read);

            response.Success = "Bildirim güncellendi.";
            return Json(response);
        }
    }
}









//using Microsoft.AspNetCore.Mvc;
//using SupperCRMApplication.Common;
//using SupperCRMApplication.Entities;
//using SupperCRMApplication.Models;
//using SupperCRMApplication.Services;
//using System.Runtime.InteropServices;

//namespace SupperCRMApplication.WebApp.Controllers
//{
//    public class NotifyController:ControllerBase
//    {
//        private readonly INotifyService _notifyService;

//        public NotifyController(INotifyService notifyService)
//        {
//            _notifyService = notifyService;
//        }

//        //GET : Notify
//        public ActionResult Index(string search = "")
//        {
//            List<Notify> notifies = null;
//            //Kullanıcı id sini sessiondan alıyoruz.
//            int userid = HttpContext.Session.GetInt32(Constants.Session_Id).Value;
//            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
//            {
//                notifies = _notifyService.ListByUserId(userid, null);
//            }
//            else
//            {
//                notifies = _notifyService.ListBySearch(search,userid);
//                ViewData["search"] = search;
//            }
//            return View(notifies);
//        }
//        //GET : Notify/Edit/5
//        public ActionResult Index(int id, bool read)
//        {
//            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

//            _notifyService.Update(id, read);

//            response.Success = "Bildirim Güncellendi.";
//            return Json(response);
//        }
//    }
//}
