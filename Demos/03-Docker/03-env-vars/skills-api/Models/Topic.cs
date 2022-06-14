using System;
using System.ComponentModel.DataAnnotations;

namespace SkillsApi {
    public class Topic {
        public int id { get; set; }

        [RequiredAttribute]
        public string title { get; set; }

        public int sortOrder { get; set; }
    }
}