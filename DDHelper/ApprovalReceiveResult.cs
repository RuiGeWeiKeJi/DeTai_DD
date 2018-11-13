using System;

namespace DDHelper
{
    public class ApprovalReceiveResult
    {
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime Date
        {
            get;
            set;
        }

        /// <summary>
        /// 审批结果
        /// </summary>
        public string OperationResult
        {
            get;
            set;
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType
        {
            get;
            set;
        }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 审批人
        /// </summary>
        public string OperationUser
        {
            get;
            set;
        }
    }
}
