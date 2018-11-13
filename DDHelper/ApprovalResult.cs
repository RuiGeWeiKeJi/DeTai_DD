using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace DDHelper
{
    public class ApprovalResult
    {

        /// <summary>
        /// 接口开启或关闭状态
        /// </summary>
        public int ding_open_errcode
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 发送审批消息是否成功
        /// </summary>
        public bool is_success
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 审批实例id
        /// </summary>
        public string process_instance_id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string request_id
        {
            get;
            set;
        }

    }
}
