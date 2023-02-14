using Microsoft.AspNetCore.Mvc;
using SupperCRMApplication.Models;

namespace SupperCRMApplication.WebApp.Controllers
{
    public class ControllerBase:Controller
    {
        //miras alanlar bunu görebilir  new leyenler göremez.
        protected void AddModelStateErrorsToAjaxResponse(AjaxResponseModel<string> response)
        {
            foreach (var key in ModelState.Keys)
            {
                var item = ModelState.GetValueOrDefault(key);
                if (item != null && item.Errors.Count > 0)
                {
                    item.Errors.ToList().ForEach(err => response.AddError(key, err.ErrorMessage));
                }
            }
        }
    }
}
