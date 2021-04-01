using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDo.Models
{
    public class ToDoView
    {
        
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a Task")]
        public string Text { get; set; }
        public int Done { get; set; }
        public List<ToDoItem> list { get; set; }
        public int toDel { get; set; }
        public int toFin { get; set; }
    }
}