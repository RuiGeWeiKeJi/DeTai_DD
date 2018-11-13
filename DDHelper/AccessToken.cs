using System;

namespace DDHelper
{
    /// <summary>
    /// 访问票据
    /// </summary>
    public class AccessToken
    {
        /// <summary>
        /// 票据的值
        /// </summary>
        public string Value
        {
            get; set;
        }

        /// <summary>
        /// 票据的开始时间
        /// </summary>
        public DateTime Begin
        {
            get;
            set;
        } = DateTime . Parse ( "1970-01-01" );
    }
}
