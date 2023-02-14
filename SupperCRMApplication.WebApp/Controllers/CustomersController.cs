using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Entities;
using SupperCRMApplication.Models;
using SupperCRMApplication.Services;

namespace SupperCRMApplication.WebApp.Controllers
{
    public class CustomersController : ControllerBase
    {

        private readonly IClientService _clientService;

        public CustomersController(IClientService clientService)
        {
            _clientService = clientService;
        }



        // GET: CustomerController
        public ActionResult Index(string search ="")
        {
            List<Client>? clients = null;

            if (string.IsNullOrEmpty(search) || string.IsNullOrWhiteSpace(search))
            {
                clients = _clientService.List();
            }
            else
            {
                clients = _clientService.ListBySearch(search);
                ViewData["search"] = search;
            }


            
            return View(clients);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            var client = _clientService.GetById(id);
            return Json(new AjaxResponseModel<Client> { Data = client });
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(CreateCustomerModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _clientService.Create(model);

                response.Success = "Müsteri Eklendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var client=_clientService.GetById(id);
            return Json(new AjaxResponseModel<Client> { Data = client}); 
        }

        // POST: Customers/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditCustomerModel model)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();
            if (ModelState.IsValid)
            {
                _clientService.Update(id, model);

                response.Success = "Müsteri Güncellendi.";
                return Json(response);
            }

            AddModelStateErrorsToAjaxResponse(response);

            return Json(response);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            AjaxResponseModel<string> response = new AjaxResponseModel<string>();

            try
            {
                _clientService.Delete(id);
                response.Success = "Müşteri Başarılı Bir Şekilde Silindi.";
            }
            catch (Exception ex)
            {

                response.AddError("ex", ex.Message);
            }
            return Json(response);
        }
    }
}
