using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services
{
    public interface Idetails
    {
        List<TodoItem> GetAll();
        void save();
        void insert(TodoItem todoItem);
        void Deleted(int ID);
        void update(TodoItem todoItem);
        TodoItem GetByID(int id);
    }
}
