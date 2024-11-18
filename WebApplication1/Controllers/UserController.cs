using AutoMapper;
using BLL.IServices;
using DTO.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IServiceSuperAdmin _service;
        private readonly IMapper _mapper;

        public UserController(IServiceSuperAdmin service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // Побачити всіх  
        [Authorize(Roles ="Admin,SuperAdmin,Client")]
        public IActionResult Index()
        {
            var users = _mapper.Map<List<UserDto>>(_service.GetProducts());
            return View(users);
        }

        // Створення нового
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Create()
        {
            return View(new UserDto());
        }
        [HttpPost]
        public IActionResult Create(UserDto dto)
        {
            if(ModelState.IsValid)
            {

                dto.StatusId = 1;

                var user = _mapper.Map<User>(dto);
                _service.Add(user);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // Зміна імені
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Edit(int id)
        {
            var user = _mapper.Map<UserDto>(_service.GetById(id));
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Edit(int id,UserDto dto)
        {
            if (ModelState.IsValid)
            {
                //var user = _mapper.Map<User>(dto);
                _service.UpdateName(id, dto.FirstName);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        // Видалення
        [Authorize(Roles = "Admin,SuperAdmin")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }


        // Окремо для клієнтів
        [Authorize(Roles = "Client")]
        public IActionResult ViewUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_service.GetProducts());
            return View(users);
        }
    }
}
