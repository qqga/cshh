using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Asp.JqGrid
{
    public enum SortDirection { asc, desc }

    public enum GroupOperator { AND, OR }

    public enum SearchOperatot
    {
        eq,
        /// <summary>
        /// not equal
        /// </summary>
        ne,
        /// <summary>
        /// less
        /// </summary>
        lt,
        /// <summary>
        ///less or equal
        /// </summary>
        le,
        /// <summary>
        ///greater
        /// </summary>
        gt,
        /// <summary>
        ///greater or equal
        /// </summary>
        ge,
        /// <summary>
        /// begins with
        /// </summary>
        bw,
        /// <summary>
        /// does not begin with
        /// </summary>
        bn,
        /// <summary>
        /// is in
        /// </summary>
        @in,
        /// <summary>
        /// is not in
        /// </summary>
        ni,
        /// <summary>
        /// ends with
        /// </summary>
        ew,
        /// <summary>
        /// does not end with
        /// </summary>
        en,
        /// <summary>
        /// contains
        /// </summary>
        cn,
        /// <summary>
        /// does not contain
        /// </summary>
        nc,
        /// <summary>
        /// is null
        /// </summary>
        nu,
        /// <summary>
        /// is not null
        /// </summary>
        nn
    }


}