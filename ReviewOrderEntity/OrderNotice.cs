using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace ReviewOrderEntity
{
    //生产单通知
    public class OrderNotice
    {
        private string _bna001;
        private string _bna002;
        private string _bna003;
        private string _bna007;

        /// <summary>
        /// 单号
        /// </summary>
        public string BNA001
        {
            get
            {
                return _bna001;
            }

            set
            {
                _bna001 = value;
            }
        }
        /// <summary>
        /// 发出者
        /// </summary>
        public string BNA002
        {
            get
            {
                return _bna002;
            }

            set
            {
                _bna002 = value;
            }
        }
        /// <summary>
        /// 接收者
        /// </summary>
        public string BNA003
        {
            get
            {
                return _bna003;
            }

            set
            {
                _bna003 = value;
            }
        }
        /// <summary>
        /// 讯息标题  或  文本讯息内容
        /// </summary>
        public string BNA007
        {
            get
            {
                return _bna007;
            }

            set
            {
                _bna007 = value;
            }
        }
    }
}
