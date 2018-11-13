namespace ReviewOrderEntity
{
    public class DepartEntity
    {
        #region Model
        private int _id;
        private string _dba001;
        private string _dba002;
        private string _dba003;
        private string _dba004;
        private string _dba005;
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
        /// 部门ID
        /// </summary>
        public string DBA001
        {
            set
            {
                _dba001 = value;
            }
            get
            {
                return _dba001;
            }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DBA002
        {
            set
            {
                _dba002 = value;
            }
            get
            {
                return _dba002;
            }
        }
        /// <summary>
        /// 上级ID
        /// </summary>
        public string DBA003
        {
            set
            {
                _dba003 = value;
            }
            get
            {
                return _dba003;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DBA004
        {
            set
            {
                _dba004 = value;
            }
            get
            {
                return _dba004;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DBA005
        {
            set
            {
                _dba005 = value;
            }
            get
            {
                return _dba005;
            }
        }
        #endregion Model

    }
}
