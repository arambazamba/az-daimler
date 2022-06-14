using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SkillsApi
{
    // [Authorize]
    [Route("skills")]
    public class SkillsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private SkillDBContext ctx;

        public SkillsController(SkillDBContext dbctx)
        {
            ctx = dbctx;
        }

        // http://localhost:5000/api/skills
        [HttpGet]
        public ActionResult<Skill[]> Get()
        {
            return this.ctx.Skills.ToArray();
        }

        // http://localhost:5000/api/skills/1
        [HttpGet("{id}")]
        public ActionResult<Skill> Get(int id)
        {
            return ctx.Skills.FirstOrDefault(v => v.Id == id);
        }

        [HttpGet]
        [Route("init")]
        public IActionResult Init()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Skill m)
        {
            if (m.Id == 0)
            {
                ctx.Skills.Add(m);
            }
            else
            {
                ctx.Skills.Attach(m);
                ctx.Entry(m).State = EntityState.Modified;
            }

            ctx.SaveChanges();
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var v = ctx.Skills.FirstOrDefault(m => m.Id == id);
            if (v != null)
            {
                ctx.Remove(v);
                ctx.SaveChanges();
            }
            return Ok();
        }
    }
}