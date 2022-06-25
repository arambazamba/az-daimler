using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Identity.Web.Resource;
using Microsoft.Extensions.Configuration;
using FoodApp;
using System.Threading.Tasks;

namespace FoodApi
{
    [Produces("application/json")] 
    [Route("[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        public FoodController(FoodDBContext context, IConfiguration config)
        {
            ctx = context;
            cfg = config.Get<FoodConfig>();
        }

        FoodDBContext ctx;
        FoodConfig cfg;

        // http://localhost:PORT/food
        [HttpGet()]
        public async Task<IEnumerable<FoodItem>> GetFood()
        {
            return await ctx.Food.ToArrayAsync();
        }

        // http://localhost:PORT/food/3
        [HttpGet("{id}")]
        public Task<FoodItem> GetById(int id)
        {
            return ctx.Food.FirstOrDefaultAsync(v => v.ID == id);
        }

        // http://localhost:PORT/food
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost()]
        public async Task<FoodItem> AddFood(FoodItem item)
        {
            verfiyScope();
            if (item.ID == 0)
            {
                await ctx.Food.AddAsync(item);
            }
            else
            {
                ctx.Food.Attach(item);
                ctx.Entry(item).State = EntityState.Modified;
            }            

            await ctx.SaveChangesAsync();
            return item;
        }

        // http://localhost:PORT/food
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            verfiyScope();
            var v = GetById(id);
            if (v != null)
            {
                ctx.Remove(v);
                await ctx.SaveChangesAsync();
            }
            return Ok();
        }
        
        //Excluded by Swagger
        [ApiExplorerSettings(IgnoreApi = true)]
        public FoodItem GetExcluded(int id)
        {
            verfiyScope();
            return ctx.Food.FirstOrDefault(v => v.ID == id);
        }

        //Excluded from Swagger
        [NonAction]
        public void verfiyScope()
        {
            if (cfg.App.AuthEnabled)
            {
                string[] scopeRequiredByApi = new string[] { "access_as_user" };
                HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            }
        }
    }
}