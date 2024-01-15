using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistapi.Models;

namespace todolistapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyListController : Controller
    {
        private readonly MyListContext _ctx;
        public MyListController(MyListContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public async Task<IEnumerable<MyList>> GetLists()
        {
            var lists = await _ctx.MyList.ToListAsync();
            return lists;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetList(int Id)
        {

            var foundList = await _ctx.MyList.FirstOrDefaultAsync(x => x.Id == Id);
                  if (foundList == null)
                return NotFound("Data with that Id was not found");

            return Ok(foundList);
            
        }


        [HttpPost]
        public async Task<IActionResult> AddList(MyList listData)
        {
            if(listData.Status.Equals("New") || listData.Status.Equals("In progress") || listData.Status.Equals("Completed"))
            {
            _ctx.MyList.Add(listData);
            await _ctx.SaveChangesAsync();
            return Ok(listData);

            }
            else
            {
                return BadRequest("Status must either be 'New', 'In progress' or 'Completed'");
            }
       }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateList(int Id, MyList listData)
        {
            var foundList = await _ctx.MyList.FirstOrDefaultAsync(x => x.Id == Id);
            if (foundList == null) 
            {
                return NotFound("Data with that Id was not found");
            } 

            foundList.Title = listData.Title;
            foundList.Description = listData.Description;

            if (foundList.Status == "New" || foundList.Status == "In progress")
            {
                foundList.Status = listData.Status;
            } 
            else if(foundList.Status == "Completed")
            {
                foundList.Status = "Completed";
               
            }
            await _ctx.SaveChangesAsync();
            return Ok("Data has been successfully updated!");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteList(int Id)
        {
            var foundList = await _ctx.MyList.FindAsync(Id);
            if (foundList == null) return NotFound("Data with that Id was not found");

            _ctx.MyList.Remove(foundList);
            await _ctx.SaveChangesAsync();
            return Ok("Data was successfully deleted.");
        }
    
    }
}
