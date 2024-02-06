using Microsoft.AspNetCore.Mvc;

namespace test_bdd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController(TodoContext context) : ControllerBase
    {
        [HttpGet(Name = "GetTodoItems")]
        public IEnumerable<TodoItem> Get()
        {
            return [.. context.TodoItems];
        }

        [HttpGet("{id}", Name = "GetTodoItem")]
        public ActionResult<TodoItem> GetById(string id)
        {
            var item = context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost(Name = "CreateTodoItem")]
        public ActionResult<TodoItem> Create(string todoText)
        {
            var item = new TodoItem(
                Id: Guid.NewGuid().ToString(),
                Text: todoText,
                IsComplete: false
            );

            context.TodoItems.Add(item);
            context.SaveChanges();

            return CreatedAtRoute("GetTodoItem", new { id = item.Id }, item);
        }

        [HttpDelete("{id}", Name = "DeleteTodoItem")]
        public IActionResult Delete(string id)
        {
            var item = context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            context.TodoItems.Remove(item);
            context.SaveChanges();

            return NoContent();
        }
    }
}
