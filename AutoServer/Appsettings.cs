namespace AutoServer
{
    public class Appsettings
    {
        public bool IsDebug { get; set; }
        public SqlSugarOption SqlSugarOption { get; set; }
        public JwtOption JwtOption { get; set; }
    }
    public class SqlSugarOption
    {
        public string? ConnectString { get; set; }
        public string? Type { get; set; }
    }
    public class JwtOption
    {
        public string SecurityKey { get; set; }
    }
}
