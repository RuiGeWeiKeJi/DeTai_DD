namespace DDHelper
{
    public class ResultPackage
    {

        public ErrCodeEnum ErrCode
        {
            get; set;
        } = ErrCodeEnum . Unknown;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrMsg
        {
            get; set;
        }

        /// <summary>
        /// 结果的Json形式
        /// </summary>
        public string Json
        {
            get;
            set;
        }

        public bool IsOK ( )
        {
            return ErrCode == ErrCodeEnum . OK;
        }

        public override string ToString ( )
        {
            string info = $"{nameof ( ErrCode )}:{ErrCode},{nameof ( ErrMsg )}:{ErrMsg}";

            return info;
        }

    }
}
