using System.Data;
using System.Text.Json.Serialization;

namespace Models.Dto
{
    public class Values
    {
        private object value;

        public DbType DbType { get; set; }
        public bool ValueBool { get; set; }
        public int ValueInt { get; set; }
        public string? ValueString { get; set; }
        public float ValueFloat { get; set; }
        public double Valuedouble { get; set; }
        public decimal Valuedecimal { get; set; }
        [JsonIgnore]
        public object? Value
        {
            get
            {
                switch (DbType)
                {
                    case DbType.AnsiString:
                        break;
                    case DbType.Binary:
                        break;
                    case DbType.Byte:
                        break;
                    case DbType.Boolean:
                        break;
                    case DbType.Currency:
                        break;
                    case DbType.Date:
                        break;
                    case DbType.DateTime:
                        break;
                    case DbType.Decimal:
                        break;
                    case DbType.Double:
                        break;
                    case DbType.Guid:
                        break;
                    case DbType.Int16:
                        break;
                    case DbType.Int32:
                        return ValueInt;
                    case DbType.Int64:
                        break;
                    case DbType.Object:
                        break;
                    case DbType.SByte:
                        break;
                    case DbType.Single:
                        break;
                    case DbType.String:
                        break;
                    case DbType.Time:
                        break;
                    case DbType.UInt16:
                        break;
                    case DbType.UInt32:
                        break;
                    case DbType.UInt64:
                        break;
                    case DbType.VarNumeric:
                        break;
                    case DbType.AnsiStringFixedLength:
                        break;
                    case DbType.StringFixedLength:
                        break;
                    case DbType.Xml:
                        break;
                    case DbType.DateTime2:
                        break;
                    case DbType.DateTimeOffset:
                        break;
                    default:
                        break;
                }
                return ValueString;
            }
        }

    }
}
