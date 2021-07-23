using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
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


        public TodoItemsController(Idetails idetails, ILogger<TodoItemsController> logger, IOptions<jsonproperty> json)
        {
            _idetails = idetails;
            jsonprop = json.Value;
            this.logger = logger;
            
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoList()
        {
            logger.LogInformation("Getting all the Employee");
            return _idetails.GetAll();
        }


        // GET: TodoItems/Details/5

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
                return Json(new { message = "This id is not found to get the data Successfully" });

            }


        }
        
        [HttpPut]
        //put:to update the data of the todolist

        public IActionResult Edit(TodoItem todoItem)
        {

            _idetails.update(todoItem);

            _idetails.save();
            return Json(new { message = "updated Successfully" });



        }
        //post:to create a new todolist
       
        [HttpPost]

        public ActionResult<TodoItem> Create(TodoItem todoItem)
        {

            _idetails.insert(todoItem);
            _idetails.save();
            return Json(new { message = "inserted  Successfully" });

        }
        //Delete:to delete the todolist
        
        [HttpDelete("{id}")]


        public ActionResult<TodoItem> Delete(int id)
        {
            var todoItem = _idetails.GetByID(id);
            if (todoItem != null)
            {
                _idetails.Deleted(id);
                _idetails.save();
                return Json(new { message = "deleted Successfully" });

            }
            else
            {
                return Json(new { message = "This id is not found to deleted the data Successfully" });
            }

        }
        //Get:to get and show  the values in json file
        [Route("Json")]
        [HttpGet]
        

        public ActionResult<IEnumerable<jsonproperty>> Details()
        {

            return Content(JsonConvert.SerializeObject(jsonprop));
        }
        


    }
}
