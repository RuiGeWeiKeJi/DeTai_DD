using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace ReviewOrderEntity
{
    public class InvoiceBody
    {
        private string _lcb001;
        private string _lcb002;
        private string _lcb003;
		private string _lcb004;
        private string _lcb005;
		private string _lcb006;
        private decimal? _lcb007;
		private string _lcb008;

        /// <summary>
        /// ERP单号
        /// </summary>
        public string LCB001
        {
            set
            {
                _lcb001 = value;
            }
            get
            {
                return _lcb001;
            }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string LCB002
        {
            set
            {
                _lcb002 = value;
            }
            get
            {
                return _lcb002;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LCB003
        {
            set
            {
                _lcb003 = value;
            }
            get
            {
                return _lcb003;
            }
        }
        /// <summary>
        /// 交货日期
        /// </summary>
        public string LCB004
        {
            set
            {
                _lcb004 = value;
            }
            get
            {
                return _lcb004;
            }
        }
        /// <summary>
        /// 生产设备名称
        /// </summary>
        public string LCB005
        {
            set
            {
                _lcb005 = value;
            }
            get
            {
                return _lcb005;
            }
        }
        /// <summary>
        /// 型号
        /// </summary>
        public string LCB006
        {
            set
            {
                _lcb006 = value;
            }
            get
            {
                return _lcb006;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? LCB007
        {
            set
            {
                _lcb007 = value;
            }
            get
            {
                return _lcb007;
            }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public string LCB008
        {
            set
            {
                _lcb008 = value;
            }
            get
            {
                return _lcb008;
            }
        }
    }
}
