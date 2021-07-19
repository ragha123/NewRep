using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    //todocontroller activities
    public class TodoItemsController : Controller
    {
        private readonly Idetails _idetails;
        private readonly ILogger<TodoItemsController> logger;
        private readonly jsonproperty jsonprop;

        public TodoItemsController(Idetails idetails,ILogger<TodoItemsController> logger,IOptions<jsonproperty> json)
        {
            _idetails = idetails;
            jsonprop = json.Value;
            this.logger = logger;
        }

        [Route("Json")]
        public ActionResult<IEnumerable<jsonproperty>> GetSettings()
        {

            return Content(JsonConvert.SerializeObject(jsonprop));
        }
       
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetEmployees()
        {
            logger.LogInformation("Getting all the Employee");
            return _idetails.GetAll();
        }


       // GET: Todo
        // Items/Details/5
        
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Details(int id)
        {



            logger.LogDebug("getting by id......");//logger activities

            var todoItem = _idetails.GetByID(id);
            if (todoItem != null)
            {
                return Ok(todoItem);
            }
            else
            {
                logger.LogWarning($"{id} not found ");
                logger.LogWarning("this is a error");
                return NotFound();

            }


        }

        [HttpPut]
        //put

        public IActionResult Edit(TodoItem todoItem)
        {

            _idetails.update(todoItem);
            
                _idetails.save();
                return Json(new {message = "updated Successfully"});
            


        }
        
        [HttpPost]
        
        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {
            
                _idetails.insert(todoItem);
                _idetails.save();
            return Json(new {message = "inserted  Successfully" });

        }
        

        [HttpDelete("{id}")]


        public ActionResult<TodoItem> Delete(int id)
        {
            var todoItem = _idetails.GetByID(id);
            if (todoItem != null)
            {
                _idetails.Deleted(id);
                _idetails.save();
                return Json(new {message = "deleted Successfully" });

            }
            else
            {
                return NotFound();
            }
            
        }

        
       



    }
}
