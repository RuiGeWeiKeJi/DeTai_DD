using System . Collections . Generic;

namespace ReviewOrderEntity
{
    public class OrderSendForParam
    {
        private string _ApprovalId;

        /// <summary>
        /// 审批ID
        /// </summary>
        public string ApprovalId
        {
            get
            {
                return _ApprovalId;
            }

            set
            {
                _ApprovalId = value;
            }
        }

        private string _ProcessCode;

        /// <summary>
        /// 表单ID
        /// </summary>
        public string ProcessCode1
        {
            get
            {
                return _ProcessCode;
            }

            set
            {
                _ProcessCode = value;
            }
        }

        private string _OriginatorUserId;

        /// <summary>
        /// 发起人ID
        /// </summary>
        public string OriginatorUserId
        {
            get
            {
                return _OriginatorUserId;
            }

            set
            {
                _OriginatorUserId = value;
            }
        }

        private string _DeptId;

        /// <summary>
        /// 发起人所在部门ID
        /// </summary>
        public string DeptId
        {
            get
            {
                return _DeptId;
            }

            set
            {
                _DeptId = value;
            }
        }

        private string _Approvers;

        /// <summary>
        /// 审批人
        /// </summary>
        public string Approvers
        {
            get
            {
                return _Approvers;
            }

            set
            {
                _Approvers = value;
            }
        }

        /// <summary>
        /// 发送消息体
        /// </summary>
        public List<OrderSendForParamSon> ParamSon
        {
            get;set;
        }

    }
}
