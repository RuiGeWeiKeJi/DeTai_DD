namespace ReviewOrderEntity
{
    public class UserEntity
    {
        #region Model
        private int _id;
        private string _dbb001;
        private string _dbb002;
        private string _dbb003;
        private string _dbb004;
        private string _dbb005;
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
        /// 人员id
        /// </summary>
        public string DBB001
        {
            set
            {
                _dbb001 = value;
            }
            get
            {
                return _dbb001;
            }
        }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string DBB002
        {
            set
            {
                _dbb002 = value;
            }
            get
            {
                return _dbb002;
            }
        }
        /// <summary>
        /// 成员所属部门id列表
        /// </summary>
        public string DBB003
        {
            set
            {
                _dbb003 = value;
            }
            get
            {
                return _dbb003;
            }
        }
        /// <summary>
        /// 员工工号
        /// </summary>
        public string DBB004
        {
            set
            {
                _dbb004 = value;
            }
            get
            {
                return _dbb004;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DBB005
        {
            set
            {
                _dbb005 = value;
            }
            get
            {
                return _dbb005;
            }
        }
        #endregion Model
    }
}
