namespace DDHelper
{
    public static  class AppSetting
    {
        private static string _corpid;
        private static string _corpsecret;
        private static string _developmentID;
        private static string _approvers;

        /// <summary>
        /// 企业id
        /// </summary>
        public static string corpid
        {
            get
            {
                return _corpid;
            }

            set
            {
                _corpid = value;
            }
        }

        /// <summary>
        /// 企业秘钥
        /// </summary>
        public static string corpsecret
        {
            get
            {
                return _corpsecret;
            }

            set
            {
                _corpsecret = value;
            }
        }

        /// <summary>
        /// 开发者ID
        /// </summary>
        public static string developmentID
        {
            get
            {
                return _developmentID;
            }

            set
            {
                _developmentID = value;
            }
        }

        /// <summary>
        /// 审批流ID
        /// </summary>
        public static string Approvers
        {
            get
            {
                return _approvers;
            }

            set
            {
                _approvers = value;
            }
        }

    }
}
