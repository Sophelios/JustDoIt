using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JustDoIt.Models
{
    public class ToDoItem
    {
        [Key]
        public int ToDoItemID { get; set; }

        [Required]
        public string ItemName { get; set; }
        public string ItemNote { get; set; }
        public int Priority { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }

        [ForeignKey("TaskList")]
        public int ToDoListID { get; set; }

        //Nav Property
        public virtual TaskList TaskList { get; set; }
    }
}