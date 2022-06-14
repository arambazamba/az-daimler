using System;
using System.Linq;

namespace SkillsApi
{
    public static class DBInitializer
    {
        public static void Initialize(SkillDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Topics.FirstOrDefault() == null)
            {
                var sk1 = new Skill { Title = "Docker Basics", Completed = true, Hours = 4, DueDate = DateTime.Now.AddMonths(-1) };
                var sk2 = new Skill { Title = "Config Injection", Completed = false, Hours = 3, DueDate = DateTime.Now.AddMonths(-2) };
                var sk3 = new Skill { Title = "Pods", Completed = false, Hours = 2, DueDate = DateTime.Now.AddMonths(2) };
                var sk4 = new Skill { Title = "Routing", Completed = true, Hours = 5, DueDate = DateTime.Now.AddDays(2)};
                var sk5 = new Skill { Title = "Monitoring", Completed = false, Hours = 1, DueDate = DateTime.Now.AddYears(1)};

                context.Skills.AddRange(sk1, sk2, sk3, sk4, sk5);
                context.SaveChanges();
            }
        }
    }
}