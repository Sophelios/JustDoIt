using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustDoIt.Models
{
    public class TaskList
    {
        [Key]
        public int ToDoListID { get; set; }
        public string Title { get; set; }

        //nav property
        public virtual IEnumerable<ToDoItem> ToDoItems { get; set; }
    }
}