using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(page, pagesize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm User Thành Công");
                }
            }
            return View("Index");
        }
      
        public ActionResult Edit(long id)
        {
            var user = new UserDao().ViewDetail(id);

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid || user.CreateBy == null || user.ModifiedBy   == null)
            {
                var dao = new UserDao();
                var rs = dao.Update(user);
                if (rs)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            if (ModelState.IsValid || user.CreateBy != null || user.ModifiedBy != null)
            {
                var dao = new UserDao();
                var rs = dao.Update(user);
                if (rs)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            if (ModelState.IsValid || user.CreateBy == null || user.ModifiedBy != null)
            {
                var dao = new UserDao();
                var rs = dao.Update(user);
                if (rs)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            if (ModelState.IsValid || user.CreateBy != null || user.ModifiedBy == null)
            {
                var dao = new UserDao();
                var rs = dao.Update(user);
                if (rs)
                {
                    return RedirectToAction("Index", "User");
                }
            }
            return View("Edit");
        }
        //-------------------------------------
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var dao = new UserDao();
            dao.Delete(id);
            return RedirectToAction("Index", "User");
        }
    }

}

