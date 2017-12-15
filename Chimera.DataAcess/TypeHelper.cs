using System;
using Microsoft.SqlServer.Management.Smo;

namespace Chimera.DataAcess
{
    public static class TypeHelper
    {
        public static DataType GetSqlType(Type type, int? length = null)
        {
            if (type == typeof(Guid))
            {
                return DataType.UniqueIdentifier;
            }
            if (type == typeof(Boolean))
            {
                return DataType.Bit;
            }
            if (type == typeof(Byte))
            {
                return DataType.TinyInt;
            }
            if (type == typeof(SByte))
            {
                return DataType.SmallInt;
            }
            if (type == typeof(Int16))
            {
                return DataType.SmallInt;
            }
            if (type == typeof(Int32))
            {
                return DataType.Int;
            }
            if (type == typeof(Int64))
            {
                return DataType.BigInt;
            }
            if (type == typeof(UInt64))
            {
                return DataType.Decimal(20,0);
            }
            if (type == typeof(Decimal))
            {
                return DataType.Decimal(20, 4);
            }
            if (type == typeof(Single))
            {
                return DataType.Real;
            }
            if (type == typeof(Double))
            {
                return DataType.Float;
            }
            // String Types
            if (type == typeof(String))
            {
                return (length.HasValue ? DataType.NVarChar(length.Value) : DataType.NVarChar(100));
            }

            // Datetime types
            if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                return DataType.DateTime2(7);
            }
            if (type == typeof(DateTimeOffset))
            {
                return DataType.DateTimeOffset(7);
            }
            return DataType.Int;
        }
    }
}