using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public int Done { get; set; }

        [Required]
        public string Text { get; set; }
    }
}