using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace ReviewOrderEntity
{
    public class OrderSendForParamSon
    {
        private string _name;

        private string _value;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

    }
}
