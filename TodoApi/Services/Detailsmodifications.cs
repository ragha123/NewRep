using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class Detailsmodifications : Idetails
    {
        private readonly TodoContext _context;
        public Detailsmodifications(TodoContext context)
        {
            _context = context;
        }
        public void Deleted(int ID)
        {
            TodoItem ti = GetByID(ID);
            _context.Remove(ti);
            //throw new NotImplementedException();
        }

        public List<TodoItem> GetAll()
        {

            return _context.TodoItems.ToList();
            //throw new NotImplementedException();
        }

        public TodoItem GetByID(int id)
        {
            return _context.TodoItems.Where(a => a.Id == id).SingleOrDefault();
            //throw new NotImplementedException();
        }

        public void insert(TodoItem todoItem)
        {
            _context.Add(todoItem);
           // throw new NotImplementedException();
        }

        public void save()
        {
            _context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void update(TodoItem todoItem)
        {
            _context.TodoItems.Update(todoItem);
            //throw new NotImplementedException();
        }
    }
}
