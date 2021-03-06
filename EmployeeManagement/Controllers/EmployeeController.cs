﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeManagement.DatabaseContext.DatabaseContext;
using EmployeeManagement.Models;
using EmployeeManagement.Models.EntityModels;
using EmployeeManagement.Repositories.Contracts;
using EmployeeManagement.Repositories.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;

        private IMapper _mapper;


        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,IMapper mapper)
        {
            this._employeeRepository = employeeRepository;
            this._departmentRepository = departmentRepository;
            this._mapper = mapper;
        }
        public IActionResult Create()
        {
            var model = new EmployeeCreateViewModel();
            model.Departments = _departmentRepository
                 .GetAll()
                 .Select(c => new SelectListItem
                 {
                     Value = c.Id.ToString(),
                     Text = c.Name
                 }).ToList();
            model.Employees = _employeeRepository.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                bool isSaved = _employeeRepository.Add(employee);
                if (isSaved)
                {
                    ViewBag.Message = "Saved Succesful!";
                }
            }

            model.Departments = _departmentRepository
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            model.Employees = _employeeRepository.GetAll();
            return View(model);
        }

        //public IActionResult Edit(int Id)
        //{
        //    var employee = _employeeRepository.GetById(Id);
        //    var model = _mapper.Map<EmployeeCreateViewModel>(employee);

           

        //    model.Departments = _departmentRepository.GetAll()
        //        .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();

        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult Edit(EmployeeCreateViewModel model)
        //{

        //    var employee = _mapper.Map<Employee>(model);

        //    bool isUpdated = _employeeRepository.Update(employee);

        //    model.Departments = _departmentRepository.GetAll()
        //        .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();
        //    if (isUpdated)
        //    {
        //        ViewBag.Message = "Updated Successful";
        //        return View(model);
        //    }

        //    return View(model);
        //}


        public IActionResult Details(int Id)
        {
            var employee = _employeeRepository.GetById(Id);


            var model = _mapper.Map<EmployeeCreateViewModel>(employee);

            model.Departments = _departmentRepository.GetAll()
                .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();

            return View(model);
        }

        public IActionResult DepartmentEmployee()
        {
            var model = new DepartmentEmployeeViewModel();
            model.Departments = _departmentRepository
                .GetAll()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

            return View(model);
        }


        //[Produces("application/json")]
        public IActionResult GetEmployeeBy(int departmentId)
        {
            var employees = _employeeRepository.GetByDepartmentId(departmentId);

            return Json(employees);
        }

        [Produces("application/json")]
        public IActionResult GetAllEmployee()
        {
            var employees = _employeeRepository.GetAll();

            return Json(employees);
        }

        public IActionResult GetEmployee(int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee != null)
            {
                return PartialView("_EmployeeView", employee);
            }
            else
            {
                return null;
            }

        }
        public IActionResult Edit(int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);

            var model = _mapper.Map<EmployeeCreateViewModel>(employee);

            model.Departments = _departmentRepository.GetAll()
                .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeCreateViewModel model)
        {
            var employee = _mapper.Map<Employee>(model);

            bool isUpdated = _employeeRepository.Update(employee);

            model.Departments = _departmentRepository.GetAll()
                .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();
            if (isUpdated)
            {
                ViewBag.Message = "Updated Successful";
                return View(model);
            }

            return View(model);
        }

        public IActionResult GetEmployeeEditPartial(int employeeId)
        {
            var employee = _employeeRepository.GetById(employeeId);
            var model = _mapper.Map<EmployeeCreateViewModel>(employee);
            model.Departments = _departmentRepository.GetAll()
                .Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Name }).ToList();

            return PartialView("Employee/_EmployeeEdit", model);
        }



    }
}

