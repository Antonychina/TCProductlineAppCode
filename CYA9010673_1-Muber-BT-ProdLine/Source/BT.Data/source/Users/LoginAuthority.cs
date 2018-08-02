using System;
using System.Collections.Generic;
using System.Text;

namespace Mubea.AutoTest
{
    /// <summary>
    /// Edit Status
    /// </summary>
    public enum LoginAuthority
    {
        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 0,

        /// <summary>
        /// 操作员
        /// </summary>
        Operator,

        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdmin,

        /// <summary>
        /// 未知
        /// </summary>
        Unkown,
    }
}
