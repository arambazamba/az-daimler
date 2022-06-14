namespace SkillsApi
{
    public class ConnectionStrings
    {
        public string SQLServerDBConnection { get; set; }
        public string SQLiteDBConnection { get; set; }
    }

    public class SkillsConfig
    {
        public string ApplicationName { get; set; }
        public bool UseSQLite { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}