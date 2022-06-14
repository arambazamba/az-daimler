using System.ComponentModel.DataAnnotations;
namespace SkillsApi {
    public class Demo {

        public int id { get; set; }

        [RequiredAttribute]
        public int topicId { get; set; }

        [RequiredAttribute]
        public string title { get; set; }

        [RequiredAttribute]
        public string url { get; set; }

        [RequiredAttribute]
        public string component { get; set; }

        public string markdown { get; set; }

        public bool visible { get; set; }

        public int sortOrder { get; set; }

    }
}