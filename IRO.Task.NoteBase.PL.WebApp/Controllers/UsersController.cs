using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.Entities;
using IRO.Task.NoteBase.PL.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRO.Task.NoteBase.PL.WebApp.Controllers
{
    public class UsersController : Controller
    {
        IUserService userService;
        IMapper mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            userService = service;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = userService.GetAll();
            var usersVM = mapper.Map<IEnumerable<User>, List<UserViewModel>>(users);
            return View(usersVM);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckUserName(string name)
        {
            var userNames = userService.GetAll().Select(x => x.Name).ToList();
            if (userNames.Contains(name))
                return Json(false);
            return Json(true);
        }

        public IActionResult AddUser()
        {
            return PartialView("_AddUser", new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserViewModel model)
        {
            var user = mapper.Map<UserViewModel, User>(model);
            if (ModelState.IsValid)
            {
                userService.AddUser(user);
            }
            if (user.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View("_AddUser", model);
        }


        [HttpGet]
        public IActionResult DeleteUser(long? id)
        {
            UserViewModel model = new UserViewModel();
            if (id.HasValue && id != 0)
            {
                User user = userService.GetById(id.Value);
                model = mapper.Map<User, UserViewModel>(user);
            }
            return PartialView("_DeleteUser", model.Name);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(long id)
        {
            userService.DeleteUser(id);
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}