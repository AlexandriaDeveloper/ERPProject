using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ERPProject.Controllers
{
    public class HomeController : ApiController
    {
        // GET: Home
        public IHttpActionResult GetIndex()
        {
            string title = "تنبية";
            string welcomemsg = "أهلا بكم فى صفحة كلية الطب الخاصة بالدفع الألكترونى لتسهيل عملية الدفع ";

            return Ok(new  {head=title, msg= welcomemsg});
        }
    }
}