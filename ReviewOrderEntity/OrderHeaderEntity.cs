using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace ReviewOrderEntity
{
    public class OrderHeaderEntity
    {
        #region Model
        private int _id;
        private string _pro001;
		private string _pro002;
        private string _pro003;
		private string _pro004;
        private string _pro005;
        private string _pro006;
        private string _pro007;
        private string _pro008;
        private string _pro009;
        private string _pro010;
        private string _pro011;
        private string _pro012;
        private bool _pro013;
        private string _pro014;
        private string _pro015;
        private string _pro016;
        private bool _pro017;
        private string _pro018;
        private string _pro019;
        private string _pro020;
        private bool _pro021;
        private string _pro022;
        private string _pro023;
        private string _pro024;
        private bool _pro025;
        private string _pro026;
        private string _pro027;
        private string _pro028;
        private bool _pro029;
        private string _pro030;
        private string _pro031;
        private string _pro032;
        private bool _pro033;
        private string _pro034;
        private string _pro035;
        private string _pro036;
        private bool _pro037;
        private string _pro038;

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string PRO001
        {
            set
            {
                _pro001 = value;
            }
            get
            {
                return _pro001;
            }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string PRO002
        {
            set
            {
                _pro002 = value;
            }
            get
            {
                return _pro002;
            }
        }
        /// <summary>
        /// 填表日期
        /// </summary>
        public string PRO003
        {
            set
            {
                _pro003 = value;
            }
            get
            {
                return _pro003;
            }
        }
        /// <summary>
        /// 签订单位
        /// </summary>
        public string PRO004
        {
            set
            {
                _pro004 = value;
            }
            get
            {
                return _pro004;
            }
        }
        /// <summary>
        /// 申请人
        /// </summary>
        public string PRO005
        {
            set
            {
                _pro005 = value;
            }
            get
            {
                return _pro005;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string PRO006
        {
            set
            {
                _pro006 = value;
            }
            get
            {
                return _pro006;
            }
        }
        /// <summary>
        /// 交货日期
        /// </summary>
        public string PRO007
        {
            set
            {
                _pro007 = value;
            }
            get
            {
                return _pro007;
            }
        }
        /// <summary>
        /// 送审状态   P:送审  T：审核  E：审核完毕 N：撤审 X:审核不通过
        /// </summary>
        public string PRO008
        {
            set
            {
                _pro008 = value;
            }
            get
            {
                return _pro008;
            }
        }
        /// <summary>
        /// 申请人编号
        /// </summary>
        public string PRO009
        {
            set
            {
                _pro009 = value;
            }
            get
            {
                return _pro009;
            }
        }
        /// <summary>
        /// 钉钉审核单据ID
        /// </summary>
        public string PRO010
        {
            set
            {
                _pro010 = value;
            }
            get
            {
                return _pro010;
            }
        }
        /// <summary>
		/// 审批人编号
		/// </summary>
		public string PRO011
        {
            set
            {
                _pro011 = value;
            }
            get
            {
                return _pro011;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO012
        {
            set
            {
                _pro012 = value;
            }
            get
            {
                return _pro012;
            }
        }
        /// <summary>
        /// 审批结果  通过：1  不通过：2
        /// </summary>
        public bool PRO013
        {
            set
            {
                _pro013 = value;
            }
            get
            {
                return _pro013;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO014
        {
            set
            {
                _pro014 = value;
            }
            get
            {
                return _pro014;
            }
        }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string PRO015
        {
            set
            {
                _pro015 = value;
            }
            get
            {
                return _pro015;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO016
        {
            set
            {
                _pro016 = value;
            }
            get
            {
                return _pro016;
            }
        }
        /// <summary>
        /// 审批结果  通过：1  不通过：2
        /// </summary>
        public bool PRO017
        {
            set
            {
                _pro017 = value;
            }
            get
            {
                return _pro017;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO018
        {
            set
            {
                _pro018 = value;
            }
            get
            {
                return _pro018;
            }
        }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string PRO019
        {
            set
            {
                _pro019 = value;
            }
            get
            {
                return _pro019;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO020
        {
            set
            {
                _pro020 = value;
            }
            get
            {
                return _pro020;
            }
        }
        /// <summary>
        /// 审批结果  通过：1  不通过：2
        /// </summary>
        public bool PRO021
        {
            set
            {
                _pro021 = value;
            }
            get
            {
                return _pro021;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO022
        {
            set
            {
                _pro022 = value;
            }
            get
            {
                return _pro022;
            }
        }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string PRO023
        {
            set
            {
                _pro023 = value;
            }
            get
            {
                return _pro023;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO024
        {
            set
            {
                _pro024 = value;
            }
            get
            {
                return _pro024;
            }
        }
        /// <summary>
        /// 审批结果  通过：1  不通过：2
        /// </summary>
        public bool PRO025
        {
            set
            {
                _pro025 = value;
            }
            get
            {
                return _pro025;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO026
        {
            set
            {
                _pro026 = value;
            }
            get
            {
                return _pro026;
            }
        }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string PRO027
        {
            set
            {
                _pro027 = value;
            }
            get
            {
                return _pro027;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO028
        {
            set
            {
                _pro028 = value;
            }
            get
            {
                return _pro028;
            }
        }
        /// <summary>
        /// 审批结果  通过：1  不通过：2
        /// </summary>
        public bool PRO029
        {
            set
            {
                _pro029 = value;
            }
            get
            {
                return _pro029;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO030
        {
            set
            {
                _pro030 = value;
            }
            get
            {
                return _pro030;
            }
        }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string PRO031
        {
            set
            {
                _pro031 = value;
            }
            get
            {
                return _pro031;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO032
        {
            set
            {
                _pro032 = value;
            }
            get
            {
                return _pro032;
            }
        }
        /// <summary>
        /// 审批结果  通过：1  不通过：2
        /// </summary>
        public bool PRO033
        {
            set
            {
                _pro033 = value;
            }
            get
            {
                return _pro033;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO034
        {
            set
            {
                _pro034 = value;
            }
            get
            {
                return _pro034;
            }
        }
        /// <summary>
        /// 审批人编号
        /// </summary>
        public string PRO035
        {
            get
            {
                return _pro035;
            }

            set
            {
                _pro035 = value;
            }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string PRO036
        {
            get
            {
                return _pro036;
            }

            set
            {
                _pro036 = value;
            }
        }
        /// <summary>
        /// 审批结果 通过：1  不通过：2
        /// </summary>
        public bool PRO037
        {
            get
            {
                return _pro037;
            }

            set
            {
                _pro037 = value;
            }
        }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string PRO038
        {
            get
            {
                return _pro038;
            }

            set
            {
                _pro038 = value;
            }
        }
        #endregion Model
    }
}
