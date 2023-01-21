using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yu3zx.DapperExtend
{
    public abstract class IEntity
    {

    }

    public enum GetColumnType
    {
        Add,
        Update,
        Select
    }
    /// <summary>
    /// Sql查询类型
    /// </summary>
    public enum SqlType
    {
        Select,
        Insert,
        Update,
        Delete,
        Page,
        Count,
        Where
    }
}
