using Newtonsoft . Json;
using System;
using System . Collections . Generic;
using System . IO;
using System . Xml;
using System . Xml . Serialization;

namespace DDHelper
{
    public class XmlUtil
    {

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize ( Type type ,string xml )
        {
            try
            {
                using ( StringReader sr = new StringReader ( xml ) )
                {
                    XmlSerializer xmldes = new XmlSerializer ( type );
                    return xmldes . Deserialize ( sr );
                }
            }
            catch ( Exception ex )
            {
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static object Deserialize ( Type type ,Stream stream )
        {
            XmlSerializer xmldex = new XmlSerializer ( type );
            return xmldex . Deserialize ( stream );
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serializer ( Type type ,object obj )
        {
            MemoryStream stream = new MemoryStream ( );
            XmlSerializer xml = new XmlSerializer ( type );
            try
            {
                //序列化对象
                xml . Serialize ( stream ,obj );
            }
            catch ( Exception )
            {
                throw;
            }
            stream . Position = 0;
            StreamReader sr = new StreamReader ( stream );
            string str = sr . ReadToEnd ( );

            sr . Dispose ( );
            stream . Dispose ( );

            return str;
        }

        /// <summary>
        /// 读取xml,得到发送审批结果
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static ApprovalResult ReadXmlToApprovalResult ( string xml )
        {
            XmlDocument xmlDoc = new XmlDocument ( );
            xmlDoc . LoadXml ( xml );
            XmlNodeList xn = xmlDoc . SelectNodes ( "//result" );
            ApprovalResult appResu = new ApprovalResult ( );
            foreach ( XmlElement element in xn )
            {
                appResu . ding_open_errcode = Convert . ToInt32 ( element . GetElementsByTagName ( "ding_open_errcode" ) [ 0 ] . InnerText );
                appResu . request_id = string . Empty;
                appResu . is_success = Convert . ToBoolean ( element . GetElementsByTagName ( "is_success" ) [ 0 ] . InnerText );
                appResu . process_instance_id = element . GetElementsByTagName ( "process_instance_id" ) [ 0 ] . InnerText;
            }
            return appResu;
        }

        /// <summary>
        /// 读取xml,得到审批结果信息
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<ApprovalReceiveResult> ReadXmlToApprovalReceiveResult ( string xml )
        {
            XmlDocument xmlDoc = new XmlDocument ( );
            xmlDoc . LoadXml ( xml );
            XmlNodeList xn = xmlDoc . SelectNodes ( "//operation_records_vo" );
            List<ApprovalReceiveResult> listApp = new List<ApprovalReceiveResult> ( );

            foreach ( XmlElement element in xn )
            {
                ApprovalReceiveResult appResu = new ApprovalReceiveResult ( );
                appResu . Date = Convert . ToDateTime ( element . GetElementsByTagName ( "date" ) [ 0 ] . InnerText );
                appResu . OperationResult = element . GetElementsByTagName ( "operation_result" ) [ 0 ] . InnerText;
                appResu . OperationType = element . GetElementsByTagName ( "operation_type" ) [ 0 ] . InnerText;
                if ( element . ChildNodes . Count > 4 && element . ChildNodes . Item ( 3 ) . Name . Equals ( "remark" ) )
                    appResu . Remark = element . GetElementsByTagName ( "remark" ) [ 0 ] . InnerText;
                else
                    appResu . Remark = string . Empty;
                appResu . OperationUser = element . GetElementsByTagName ( "userid" ) [ 0 ] . InnerText;
                listApp . Add ( appResu );
            }

            return listApp;
        }

        /// <summary>
        /// 把Json转为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static List<T> JsonStringToObj<T> ( string result ) where T : class
        {
            List<T> list = new List<T> ( );
            T t = JsonConvert . DeserializeObject<T> ( result );
            list . Add ( t );
            return list;
        }

    }
}
