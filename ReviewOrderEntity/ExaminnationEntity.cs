namespace ReviewOrderEntity
{
    public class ExaminnationEntity
    {
        private string _exa001;
        private string _exa002;

        /// <summary>
        /// 审批表单名
        /// </summary>
        public string EXA001
        {
            get
            {
                return _exa001;
            }

            set
            {
                _exa001 = value;
            }
        }

        /// <summary>
        /// 审批ID
        /// </summary>
        public string EXA002
        {
            get
            {
                return _exa002;
            }

            set
            {
                _exa002 = value;
            }
        }
    }
}
