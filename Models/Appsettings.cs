namespace Models
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
        /// <summary>
        /// 密钥
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 有效年
        /// </summary>
        public int ExpiresYear { get; set; }
        /// <summary>
        /// 有效天数
        /// </summary>
        public int ExpiresMouth { get; set; }
        /// <summary>
        /// 有效天数
        /// </summary>
        public int ExpiresDay { get; set; }
        /// <summary>
        /// 有效小时数
        /// </summary>
        public int ExpiresHour { get; set; }
        /// <summary>
        /// 有效分钟
        /// </summary>
        public int ExpiresMinutes { get; set; }
    }
}
