using System . Collections . Generic;
using DDHelper;
using ReviewOrderEntity;
using DingTalk . Api;
using DingTalk . Api . Request;
using System;
using DingTalk . Api . Response;
using System . Collections;
using System . Text;
using StudentMgr;
using System . Data;
using System . Data . SqlClient;
using System . Linq;

namespace ReviewOrderBll
{
    public class SendInfoForOrder
    {
        private readonly Dao.OrderDao dalOrder=null;
        private readonly Dao.FormBaseDao dal=null;

        List<OrderHeaderEntity> headerList=null;
        List<OrderBodyEntity> bodyList=null;
        List<UserEntity> userList=null;
        ExaminnationEntity modelExa=null;
        List<ApprovalResult> appResult=null;
        Dictionary<string ,string > strDicUser=null;
        Dictionary<string,string> strDicUserNum=null;
        
        string strWhere="1=1";

        public SendInfoForOrder ( string strWhere )
        {
            dalOrder = new Dao . OrderDao ( );
            dal = new Dao . FormBaseDao ( );

            this . strWhere = strWhere;

            //Task ts = new Task ( getData );
            //ts . Start ( );

            try
            {
                getData ( );
            }catch(Exception ex )
            {
                Utility . LogHelper . WriteException ( "错误:" + ex . StackTrace + "警告:" + ex . Message ,ex );
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        void getData ( )
        {
            headerList = dalOrder . getOrderHeander ( strWhere );
            bodyList = dalOrder . getOrderBody ( strWhere );
            modelExa = dal . getExa ( "生产单(订单)" );
        }

        /// <summary>
        /// 获取送审申请人信息
        /// </summary>
        /// <returns></returns>
        bool getUserData ( )
        {
            if ( headerList == null || headerList . Count < 1 )
                return false;
            List<string> userLists = new List<string> ( );
            foreach ( OrderHeaderEntity header in headerList )
            {
                userLists . Add ( header . PRO009 );
            }

            if ( userLists == null || userLists . Count < 1 )
                return false;
            userList = dal . getUser ( userLists );
            if ( userList == null || userList . Count < 1 )
                return false;
            return true;
        }

        /// <summary>
        /// 获取审核人  有顺序的
        /// </summary>
        /// <returns></returns>
        string getUserForAppro ( string oddNum )
        {
            strDicUser = new Dictionary<string ,string> ( );
            strDicUserNum = new Dictionary<string ,string> ( );

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT IBA912,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA912=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( "WHERE IBA001='{0}'" ,oddNum );
            strSql . Append ( " UNION ALL " );
            strSql . Append ( "SELECT IBA915,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA915=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( "WHERE IBA001='{0}'" ,oddNum );
            strSql . Append ( " UNION ALL " );
            strSql . Append ( "SELECT IBA918,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA918=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( "WHERE IBA001='{0}'" ,oddNum );
            strSql . Append ( " UNION ALL " );
            strSql . Append ( "SELECT IBA921,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA921=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( "WHERE IBA001='{0}'" ,oddNum );
            strSql . Append ( " UNION ALL " );
            strSql . Append ( "SELECT IBA924,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA924=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( "WHERE IBA001='{0}'" ,oddNum );
            strSql . Append ( " UNION ALL " );
            strSql . Append ( "SELECT IBA927,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA927=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( "WHERE IBA001='{0}'" ,oddNum );
            strSql . Append ( " UNION ALL " );
            strSql . Append ( "SELECT IBA930,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA930=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( " WHERE IBA001 = '{0}'" ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                string userList = string . Empty;
                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    if ( string . IsNullOrEmpty ( userList ) )
                        userList = table . Rows [ i ] [ "DBB001" ] . ToString ( );
                    else
                    {
                        if ( !userList . Contains ( table . Rows [ i ] [ "DBB001" ] . ToString ( ) ) )
                            userList = userList + "," + table . Rows [ i ] [ "DBB001" ] . ToString ( );
                    }
                    if ( !strDicUser . ContainsKey ( table . Rows [ i ] [ "DBB001" ] . ToString ( ) ) )
                        strDicUser . Add ( table . Rows [ i ] [ "DBB001" ] . ToString ( ) ,table . Rows [ i ] [ "DBA002" ] . ToString ( ) );
                    if ( !strDicUserNum . ContainsKey ( table . Rows [ i ] [ "DBB001" ] . ToString ( ) ) )
                        strDicUserNum . Add ( table . Rows [ i ] [ "DBB001" ] . ToString ( ) ,table . Rows [ i ] [ "IBA912" ] . ToString ( ) );
                }
                return userList;
            }
            else
                return string . Empty;
        }

        /// <summary>
        /// 获取终审人
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        string getUserForApproLastOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT IBA930,B.DBA002,DAA002,E.DBB001 FROM DCSIBA A INNER JOIN TPADBA B ON A.IBA930=B.DBA001 INNER JOIN TPADAA C ON DBA005=DAA001 INNER JOIN DETDBA D ON C.DAA002=D.DBA002 INNER JOIN DETDBB E ON D.DBA001=E.DBB003 AND E.DBB002=B.DBA002 " );
            strSql . AppendFormat ( " WHERE IBA001 = '{0}'" ,oddNum );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
                return table . Rows [ 0 ] [ "DBB001" ] . ToString ( );
            else
                return string . Empty;
        }

        /// <summary>
        /// 消息送审，并得到送审的消息
        /// </summary>
        void getOrderForPara ( string accessToken )
        {
            try
            {
                if ( string . IsNullOrEmpty ( AppSetting . corpid ) )
                    return;
                if ( modelExa == null )
                    return;
                if ( getUserData ( ) == false )
                    return;

                appResult = new List<ApprovalResult> ( );

                foreach ( OrderHeaderEntity header in headerList )
                {
                    if ( header . PRO009 != null || header . PRO009 != string . Empty )
                    {

                        UserEntity user = userList . Find ( ( k ) =>
                        {
                            return k . DBB005 == header . PRO009;
                        } );

                        if ( user != null )
                        {

                            //撤审已经发出的审核单据
                            if ( header . PRO008 . Equals ( "N" ) )
                            {
                                string msg = "单号:" + header . PRO001 + "\r\n" + "项目名称：" + header . PRO002 + "\r\n" + "申请人：" + header . PRO005 + "\r\n" + "的单据,已经在ERP中撤审,请知晓。";
                                // 通过机器人发送消息到钉钉群
                                string WEB_HOOK = "https://oapi.dingtalk.com/robot/send?access_token=8dbbb3da6a40b246811c1a5cd24c47e2358338143f0bb3881644355c9b049153";
                                string textMsg = "{\"msgtype\":\"text\",\"text\":{\"content\": \"" + msg + "\"} }";
                                var result = Analyze . POST<SendMessageResult> ( WEB_HOOK ,textMsg ,null );
                            }
                            //删除已经发出的审核单据
                            else if ( header . PRO008 . Equals ( "E" ) )
                            {
                                string msg = "单号:" + header . PRO001 + "\r\n" + "项目名称：" + header . PRO002 + "\r\n" + "申请人：" + header . PRO005 + "\r\n" + "的单据,已经在ERP中审核通过,请知晓.";
                                string WEB_HOOK = "https://oapi.dingtalk.com/robot/send?access_token=8dbbb3da6a40b246811c1a5cd24c47e2358338143f0bb3881644355c9b049153";
                                string textMsg = "{\"msgtype\":\"text\",\"text\":{\"content\": \"" + msg + "\"} }";
                                var result = Analyze . POST<SendMessageResult> ( WEB_HOOK ,textMsg ,null );
                            }
                            //送审
                            else if ( header . PRO008 . Equals ( "P" ) )
                            {
                                #region
                                IDingTalkClient client = new DefaultDingTalkClient ( Urls . get_Examination_and_approval );
                                SmartworkBpmsProcessinstanceCreateRequest req = new SmartworkBpmsProcessinstanceCreateRequest ( );

                                //审批ID
                                req . AgentId = string . IsNullOrEmpty ( AppSetting . Approvers ) == true ? 0 : Convert . ToInt64 ( AppSetting . Approvers );
                                //审批表单ID
                                req . ProcessCode = modelExa . EXA002;
                                //发起人ID
                                req . OriginatorUserId = user . DBB001;
                                //发起人所在部门ID
                                req . DeptId = string . IsNullOrEmpty ( user . DBB003 ) == true ? 0 : Convert . ToInt64 ( user . DBB003 );
                                //审批人  多人用逗号隔开  按顺序审批
                                req . Approvers = getUserForAppro ( header . PRO001 );

                                List<SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain> list = new List<SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain> ( );

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj1 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj1 . Name = "ERP单号";
                                obj1 . Value = header . PRO001;
                                list . Add ( obj1 );

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj2 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj2 . Name = "项目名称";
                                obj2 . Value = header . PRO002;
                                list . Add ( obj2 );

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj3 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj3 . Name = "填表日期";
                                obj3 . Value = header . PRO003;
                                list . Add ( obj3 );


                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj4 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj4 . Name = "签订单位";
                                obj4 . Value = header . PRO004;
                                list . Add ( obj4 );

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj5 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj5 . Name = "申请人";
                                obj5 . Value = header . PRO005;
                                list . Add ( obj5 );

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj6 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj6 . Name = "备注";
                                obj6 . Value = header . PRO006;
                                list . Add ( obj6 );

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj7 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj7 . Name = "交货日期";
                                obj7 . Value = header . PRO007;
                                list . Add ( obj7 );


                                object [ ] obj = new object [ bodyList . FindAll ( ( k ) =>
                                {
                                    return k . PRP001 == header . PRO001;
                                } ) . Count ];

                                int l = 0;

                                foreach ( OrderBodyEntity body in bodyList . FindAll ( ( k ) =>
                                      {
                                          return k . PRP001 == header . PRO001;
                                      } ) )
                                {
                                    //SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj8 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                    //obj8 . Name = "序号:";
                                    //obj8 . Value = body . PRP002;

                                    SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj9 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                    obj9 . Name = "生产设备名称";
                                    obj9 . Value = body . PRP003;


                                    SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj10 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                    obj10 . Name = "型号";
                                    obj10 . Value = body . PRP004;

                                    SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj11 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                    obj11 . Name = "数量";
                                    obj11 . Value = body . PRP005 . ToString ( );

                                    SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj12 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                    obj12 . Name = "生产类型";
                                    obj12 . Value = body . PRP006;

                                    SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj13 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                    obj13 . Name = "方向";
                                    obj13 . Value = body . PRP007;

                                    obj [ l ] = new object [ ] { obj9 ,obj10 ,obj11 ,obj12 ,obj13 };
                                    l++;
                                }

                                SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain obj14 = new SmartworkBpmsProcessinstanceCreateRequest . FormComponentValueVoDomain ( );
                                obj14 . Name = "序号：00";
                                obj14 . Value = FastJSON . JSON . ToJSON ( obj );
                                list . Add ( obj14 );

                                req . FormComponentValues_ = list;

                                SmartworkBpmsProcessinstanceCreateResponse rsp = client . Execute ( req ,accessToken );
                                ApprovalResult result = XmlUtil . ReadXmlToApprovalResult ( rsp . Body );
                                result . request_id = header . PRO001;
                                appResult . Add ( result );

                                #endregion
                            }
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteException ( "错误:" + ex . StackTrace + "警告:" + ex . Message ,ex );
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 得到送审钉钉单据ID，回写到送审表中
        /// </summary>
        /// <param name="accessToken"></param>
        public bool SendMessageForOrder ( string accessToken  )
        {
            getOrderForPara ( accessToken );
            if ( appResult == null || appResult . Count < 1 )
                return false;
            return SaveResult ( );
        }

        bool SaveResult ( )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            foreach ( ApprovalResult result in appResult )
            {
                EditBody ( SQLString ,strSql ,result );
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void EditBody ( Hashtable SQLString ,StringBuilder strSql,ApprovalResult result )
        {
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE DETPRO SET PRO010='{0}' WHERE PRO001='{1}'" ,result . process_instance_id ,result . request_id );

            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 接收审核结果并回写到数据库
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool ReceiveResult ( string accessToken )
        {
            DataTable table = tableProcessId ( );
            if ( table == null )
                return false;

            string userForAppro = string . Empty;
            string userForApproLase = string . Empty;
            string [ ] strArr = null;
            headerList = new List<OrderHeaderEntity> ( );

            #region
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                userForAppro = getUserForAppro ( table . Rows [ i ] [ "PRO001" ] . ToString ( ) );
                userForApproLase = getUserForApproLastOne ( table . Rows [ i ] [ "PRO001" ] . ToString ( ) );
                IDingTalkClient client = new DefaultDingTalkClient ( Urls . get_Examination_and_approval );
                SmartworkBpmsProcessinstanceGetRequest req = new SmartworkBpmsProcessinstanceGetRequest ( );
                req . ProcessInstanceId = table . Rows [ i ] [ "PRO010" ] . ToString ( );
                SmartworkBpmsProcessinstanceGetResponse rsp = client . Execute ( req ,accessToken );
                List<ApprovalReceiveResult> appResult = XmlUtil . ReadXmlToApprovalReceiveResult ( rsp . Body );

                OrderHeaderEntity header = new OrderHeaderEntity ( );
                header . PRO001 = table . Rows [ i ] [ "PRO001" ] . ToString ( );
                header . PRO011 = string . Empty;
                header . PRO012 = string . Empty;
                header . PRO013 = false;
                header . PRO014 = string . Empty;
                header . PRO015 = string . Empty;
                header . PRO016 = string . Empty;
                header . PRO017 = false;
                header . PRO018 = string . Empty;
                header . PRO019 = string . Empty;
                header . PRO020 = string . Empty;
                header . PRO021 = false;
                header . PRO022 = string . Empty;
                header . PRO023 = string . Empty;
                header . PRO024 = string . Empty;
                header . PRO025 = false;
                header . PRO026 = string . Empty;
                header . PRO027 = string . Empty;
                header . PRO028 = string . Empty;
                header . PRO029 = false;
                header . PRO030 = string . Empty;
                header . PRO031 = string . Empty;
                header . PRO032 = string . Empty;
                header . PRO033 = false;
                header . PRO034 = string . Empty;
                header . PRO035 = string . Empty;
                header . PRO036 = string . Empty;
                header . PRO037 = false;
                header . PRO038 = string . Empty;
                foreach ( ApprovalReceiveResult app in appResult )
                {
                    if ( app . OperationResult . Equals ( "AGREE" ) || app . OperationResult . Equals ( "REFUSE" ) )
                    {
                        if ( userForAppro . Contains ( app . OperationUser ) )
                        {
                            strArr = userForAppro . Split ( ',' );
                            for ( int k = 0 ; k < strArr . Length ; k++ )
                            {
                                if ( strArr [ k ] . Equals ( app . OperationUser ) )
                                {
                                    switch ( k )
                                    {
                                        case 0:
                                        if ( strArr . Length - 1 == 0 )
                                        {
                                            header . PRO035 = app . OperationUser;
                                            header . PRO036 = strDicUser [ header . PRO035 ];
                                            header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO038 = app . Remark;
                                            if ( header . PRO037 )
                                                header . PRO008 = "E";
                                            else
                                                header . PRO008 = "X";
                                        }
                                        else
                                        {
                                            header . PRO011 = app . OperationUser;
                                            header . PRO012 = strDicUser [ header . PRO011 ];
                                            header . PRO013 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO014 = app . Remark;
                                            if ( header . PRO008 == null || !header . PRO008 . Equals ( "E" ) )
                                            {
                                                if ( header . PRO013 )
                                                    header . PRO008 = "T";
                                                else
                                                    header . PRO008 = "X";
                                            }
                                        }
                                        break;
                                        case 1:
                                        if ( strArr . Length - 1 == 1 )
                                        {
                                            header . PRO035 = app . OperationUser;
                                            header . PRO036 = strDicUser [ header . PRO035 ];
                                            header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO038 = app . Remark;
                                            if ( header . PRO037 )
                                                header . PRO008 = "E";
                                            else
                                                header . PRO008 = "X";
                                        }
                                        else
                                        {
                                            header . PRO015 = app . OperationUser;
                                            header . PRO016 = strDicUser [ header . PRO015 ];
                                            header . PRO017 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO018 = app . Remark;
                                            if ( header . PRO008 == null || !header . PRO008 . Equals ( "E" ) )
                                            {
                                                if ( header . PRO017 )
                                                    header . PRO008 = "T";
                                                else
                                                    header . PRO008 = "X";
                                            }
                                        }
                                        break;
                                        case 2:
                                        if ( strArr . Length - 1 == 2 )
                                        {
                                            header . PRO035 = app . OperationUser;
                                            header . PRO036 = strDicUser [ header . PRO035 ];
                                            header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO038 = app . Remark;
                                            if ( header . PRO037 )
                                                header . PRO008 = "E";
                                            else
                                                header . PRO008 = "X";
                                        }
                                        else
                                        {
                                            header . PRO019 = app . OperationUser;
                                            header . PRO020 = strDicUser [ header . PRO019 ];
                                            header . PRO021 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO022 = app . Remark;
                                            if ( header . PRO008 == null || !header . PRO008 . Equals ( "E" ) )
                                            {
                                                if ( header . PRO021 )
                                                    header . PRO008 = "T";
                                                else
                                                    header . PRO008 = "X";
                                            }
                                        }
                                        break;
                                        case 3:
                                        if ( strArr . Length - 1 == 3 )
                                        {
                                            header . PRO035 = app . OperationUser;
                                            header . PRO036 = strDicUser [ header . PRO035 ];
                                            header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO038 = app . Remark;
                                            if ( header . PRO037 )
                                                header . PRO008 = "E";
                                            else
                                                header . PRO008 = "X";
                                        }
                                        else
                                        {
                                            header . PRO023 = app . OperationUser;
                                            header . PRO024 = strDicUser [ header . PRO023 ];
                                            header . PRO025 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO026 = app . Remark;
                                            if ( header . PRO008 == null || !header . PRO008 . Equals ( "E" ) )
                                            {
                                                if ( header . PRO025 )
                                                    header . PRO008 = "T";
                                                else
                                                    header . PRO008 = "X";
                                            }
                                        }
                                        break;
                                        case 4:
                                        if ( strArr . Length - 1 == 4 )
                                        {
                                            header . PRO035 = app . OperationUser;
                                            header . PRO036 = strDicUser [ header . PRO035 ];
                                            header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO038 = app . Remark;
                                            if ( header . PRO037 )
                                                header . PRO008 = "E";
                                            else
                                                header . PRO008 = "X";
                                        }
                                        else
                                        {
                                            header . PRO027 = app . OperationUser;
                                            header . PRO028 = strDicUser [ header . PRO027 ];
                                            header . PRO029 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO030 = app . Remark;
                                            if ( header . PRO008 == null || !header . PRO008 . Equals ( "E" ) )
                                            {
                                                if ( header . PRO029 )
                                                    header . PRO008 = "T";
                                                else
                                                    header . PRO008 = "X";
                                            }
                                        }
                                        break;
                                        case 5:
                                        if ( strArr . Length - 1 == 5 )
                                        {
                                            header . PRO035 = app . OperationUser;
                                            header . PRO036 = strDicUser [ header . PRO035 ];
                                            header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO038 = app . Remark;
                                            if ( header . PRO037 )
                                                header . PRO008 = "E";
                                            else
                                                header . PRO008 = "X";
                                        }
                                        else
                                        {
                                            header . PRO031 = app . OperationUser;
                                            header . PRO032 = strDicUser [ header . PRO031 ];
                                            header . PRO033 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                            header . PRO034 = app . Remark;
                                            if ( header . PRO008 == null || !header . PRO008 . Equals ( "E" ) )
                                            {
                                                if ( header . PRO033 )
                                                    header . PRO008 = "T";
                                                else
                                                    header . PRO008 = "X";
                                            }
                                        }
                                        break;
                                        case 6:
                                        header . PRO035 = app . OperationUser;
                                        header . PRO036 = strDicUser [ header . PRO035 ];
                                        header . PRO037 = app . OperationResult . Equals ( "AGREE" ) == true ? true : false;
                                        header . PRO038 = app . Remark;
                                        if ( header . PRO037 )
                                            header . PRO008 = "E";
                                        else
                                            header . PRO008 = "X";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if ( header . PRO008 != null && !string . IsNullOrEmpty ( header . PRO008 ) )
                    headerList . Add ( header );
            }
            #endregion

            if ( headerList == null || headerList . Count < 1 )
                return false;

            if ( EditProcessAppro ( ) )
                return false;
            else
                return true;
        }

        /// <summary>
        /// 获取未审核通过  撤审  审核完毕之外的其他内容
        /// </summary>
        /// <returns></returns>
        DataTable tableProcessId ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT PRO010,PRO001 FROM DETPRO WHERE (PRO010!='' AND PRO010 IS NOT NULL) AND PRO008!='E' AND PRO008!='N' AND PRO008!='X' " );

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt == null || dt . Rows . Count < 1 )
                return null;
            else
                return dt;
        }

        /// <summary>
        /// 更新钉钉返回的审核状态
        /// </summary>
        /// <returns></returns>
        bool EditProcessAppro ( )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            foreach ( OrderHeaderEntity header in headerList )
            {
                EditHeader ( SQLString ,strSql ,header );
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void EditHeader ( Hashtable SQLString ,StringBuilder strSql ,OrderHeaderEntity model )
        {
            model . PRO011 = strDicUserNum . ContainsKey ( model . PRO011 ) == true ? strDicUserNum [ model . PRO011 ] : "";
            model . PRO015 = strDicUserNum . ContainsKey ( model . PRO015 ) == true ? strDicUserNum [ model . PRO015 ] : "";
            model . PRO019 = strDicUserNum . ContainsKey ( model . PRO019 ) == true ? strDicUserNum [ model . PRO019 ] : "";
            model . PRO023 = strDicUserNum . ContainsKey ( model . PRO023 ) == true ? strDicUserNum [ model . PRO023 ] : "";
            model . PRO027 = strDicUserNum . ContainsKey ( model . PRO027 ) == true ? strDicUserNum [ model . PRO027 ] : "";
            model . PRO031 = strDicUserNum . ContainsKey ( model . PRO031 ) == true ? strDicUserNum [ model . PRO031 ] : "";
            model . PRO035 = strDicUserNum . ContainsKey ( model . PRO035 ) == true ? strDicUserNum [ model . PRO035 ] : "";

            strSql = new StringBuilder ( );
            strSql . Append ( "update DETPRO set " );
            strSql . Append ( "PRO008=@PRO008," );
            strSql . Append ( "PRO011=@PRO011," );
            strSql . Append ( "PRO012=@PRO012," );
            strSql . Append ( "PRO013=@PRO013," );
            strSql . Append ( "PRO014=@PRO014," );
            strSql . Append ( "PRO015=@PRO015," );
            strSql . Append ( "PRO016=@PRO016," );
            strSql . Append ( "PRO017=@PRO017," );
            strSql . Append ( "PRO018=@PRO018," );
            strSql . Append ( "PRO019=@PRO019," );
            strSql . Append ( "PRO020=@PRO020," );
            strSql . Append ( "PRO021=@PRO021," );
            strSql . Append ( "PRO022=@PRO022," );
            strSql . Append ( "PRO023=@PRO023," );
            strSql . Append ( "PRO024=@PRO024," );
            strSql . Append ( "PRO025=@PRO025," );
            strSql . Append ( "PRO026=@PRO026," );
            strSql . Append ( "PRO027=@PRO027," );
            strSql . Append ( "PRO028=@PRO028," );
            strSql . Append ( "PRO029=@PRO029," );
            strSql . Append ( "PRO030=@PRO030," );
            strSql . Append ( "PRO031=@PRO031," );
            strSql . Append ( "PRO032=@PRO032," );
            strSql . Append ( "PRO033=@PRO033," );
            strSql . Append ( "PRO034=@PRO034," );
            strSql . Append ( "PRO035=@PRO035," );
            strSql . Append ( "PRO036=@PRO036," );
            strSql . Append ( "PRO037=@PRO037," );
            strSql . Append ( "PRO038=@PRO038 " );
            strSql . Append ( " WHERE PRO001=@PRO001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@PRO008", SqlDbType.VarChar,1),
                    new SqlParameter("@PRO011", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO012", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO013", SqlDbType.Bit,1),
                    new SqlParameter("@PRO014", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO015", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO016", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO017", SqlDbType.Bit,1),
                    new SqlParameter("@PRO018", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO019", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO020", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO021", SqlDbType.Bit,1),
                    new SqlParameter("@PRO022", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO023", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO024", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO025", SqlDbType.Bit,1),
                    new SqlParameter("@PRO026", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO027", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO028", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO029", SqlDbType.Bit,1),
                    new SqlParameter("@PRO030", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO031", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO032", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO033", SqlDbType.Bit,1),
                    new SqlParameter("@PRO034", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO035", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO036", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO037", SqlDbType.Bit,1),
                    new SqlParameter("@PRO038", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO001", SqlDbType.VarChar,14)
            };
            parameters [ 0 ] . Value = model . PRO008;
            parameters [ 1 ] . Value = model . PRO011;
            parameters [ 2 ] . Value = model . PRO012;
            parameters [ 3 ] . Value = model . PRO013;
            parameters [ 4 ] . Value = model . PRO014;
            parameters [ 5 ] . Value = model . PRO015;
            parameters [ 6 ] . Value = model . PRO016;
            parameters [ 7 ] . Value = model . PRO017;
            parameters [ 8 ] . Value = model . PRO018;
            parameters [ 9 ] . Value = model . PRO019;
            parameters [ 10 ] . Value = model . PRO020;
            parameters [ 11 ] . Value = model . PRO021;
            parameters [ 12 ] . Value = model . PRO022;
            parameters [ 13 ] . Value = model . PRO023;
            parameters [ 14 ] . Value = model . PRO024;
            parameters [ 15 ] . Value = model . PRO025;
            parameters [ 16 ] . Value = model . PRO026;
            parameters [ 17 ] . Value = model . PRO027;
            parameters [ 18 ] . Value = model . PRO028;
            parameters [ 19 ] . Value = model . PRO029;
            parameters [ 20 ] . Value = model . PRO030;
            parameters [ 21 ] . Value = model . PRO031;
            parameters [ 22 ] . Value = model . PRO032;
            parameters [ 23 ] . Value = model . PRO033;
            parameters [ 24 ] . Value = model . PRO034;
            parameters [ 25 ] . Value = model . PRO035;
            parameters [ 26 ] . Value = model . PRO036;
            parameters [ 27 ] . Value = model . PRO037;
            parameters [ 28 ] . Value = model . PRO038;
            parameters [ 29 ] . Value = model . PRO001;

            SQLString . Add ( strSql ,parameters );

            string state = string . Empty;
            if ( model . PRO008 . Equals ( "P" ) || model . PRO008 . Equals ( "T" ) || model . PRO008 . Equals ( "N" ) )
                state = "P";
            else if ( model . PRO008 . Equals ( "E" ) )
                state = "T";
            else if ( model . PRO008 . Equals ( "X" ) )
                state = "F";
            else
                state = "";

            strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE DCSIBA SET " );
            //strSql . Append ( "IBA912=@PRO011, " );
            strSql . Append ( "IBA913=@PRO013," );
            strSql . Append ( "IBA914=@PRO014," );
            //strSql . Append ( "IBA915=@PRO015," );
            strSql . Append ( "IBA916=@PRO017," );
            strSql . Append ( "IBA917=@PRO018," );
            //strSql . Append ( "IBA918=@PRO019," );
            strSql . Append ( "IBA919=@PRO021," );
            strSql . Append ( "IBA920=@PRO022," );
            //strSql . Append ( "IBA921=@PRO023," );
            strSql . Append ( "IBA922=@PRO025," );
            strSql . Append ( "IBA923=@PRO026," );
            //strSql . Append ( "IBA924=@PRO027," );
            strSql . Append ( "IBA925=@PRO029," );
            strSql . Append ( "IBA926=@PRO030," );
            //strSql . Append ( "IBA927=@PRO031," );
            strSql . Append ( "IBA928=@PRO033," );
            strSql . Append ( "IBA929=@PRO034," );
            //strSql . Append ( "IBA930=@PRO035," );
            strSql . Append ( "IBA045=@PRO035," );
            strSql . Append ( "IBA931=@PRO037," );
            strSql . Append ( "IBA932=@PRO038," );
            strSql . Append ( "IBA910=@PRO008," );
            strSql . Append ( "IBA022=@PRO08 " );
            strSql . Append ( "WHERE IBA001=@PRO001 " );
            SqlParameter [ ] parameter = {
                    new SqlParameter("@PRO008", SqlDbType.VarChar),
                    new SqlParameter("@PRO08", SqlDbType.VarChar),
                    //new SqlParameter("@PRO011", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO013", SqlDbType.VarChar),
                    new SqlParameter("@PRO014", SqlDbType.NVarChar,200),
                    //new SqlParameter("@PRO015", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO017", SqlDbType.VarChar),
                    new SqlParameter("@PRO018", SqlDbType.NVarChar,200),
                    //new SqlParameter("@PRO019", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO021", SqlDbType.VarChar),
                    new SqlParameter("@PRO022", SqlDbType.NVarChar,200),
                    //new SqlParameter("@PRO023", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO025", SqlDbType.VarChar),
                    new SqlParameter("@PRO026", SqlDbType.NVarChar,200),
                    //new SqlParameter("@PRO027", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO029", SqlDbType.VarChar),
                    new SqlParameter("@PRO030", SqlDbType.NVarChar,200),
                    //new SqlParameter("@PRO031", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO033", SqlDbType.VarChar),
                    new SqlParameter("@PRO034", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO035", SqlDbType.VarChar,50),
                    new SqlParameter("@PRO037", SqlDbType.VarChar),
                    new SqlParameter("@PRO038", SqlDbType.NVarChar,200),
                    new SqlParameter("@PRO001", SqlDbType.VarChar,14)
            };
            parameter [ 0 ] . Value = state;
            parameter [ 1 ] . Value = model . PRO008 == "E" ? "T" : "F";
            //parameter [ 2 ] . Value = model . PRO011;
            parameter [ 2 ] . Value = model . PRO013 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO011 ) == true ? "" : "F" );
            parameter [ 3 ] . Value = model . PRO014;
            //parameter [ 5 ] . Value = model . PRO015;
            parameter [ 4 ] . Value = model . PRO017 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO015 ) == true ? "" : "F" );
            parameter [ 5 ] . Value = model . PRO018;
            //parameter [ 8 ] . Value = model . PRO019;
            parameter [ 6 ] . Value = model . PRO021 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO019 ) == true ? "" : "F" );
            parameter [ 7 ] . Value = model . PRO022;
            //parameter [ 11 ] . Value = model . PRO023;
            parameter [ 8 ] . Value = model . PRO025 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO023 ) == true ? "" : "F" );
            parameter [ 9 ] . Value = model . PRO026;
            //parameter [ 14 ] . Value = model . PRO027;
            parameter [ 10 ] . Value = model . PRO029 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO027 ) == true ? "" : "F" );
            parameter [ 11 ] . Value = model . PRO030;
            //parameter [ 17 ] . Value = model . PRO031;
            parameter [ 12 ] . Value = model . PRO033 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO031 ) == true ? "" : "F" );
            parameter [ 13 ] . Value = model . PRO034;
            parameter [ 14 ] . Value = model . PRO035;
            parameter [ 15 ] . Value = model . PRO037 == true ? "T" : ( string . IsNullOrEmpty ( model . PRO035 ) == true ? "" : "F" );
            parameter [ 16 ] . Value = model . PRO038;
            parameter [ 17 ] . Value = model . PRO001;

            SQLString . Add ( strSql ,parameter );

            //model . PRO035  终审人
            if ( !string . IsNullOrEmpty ( model . PRO035 ) && model . PRO037 )
            {
                List<string> strList = new List<string> ( );
                strSql = new StringBuilder ( );
                strSql . AppendFormat ( "SELECT IBA911,IBA933,IBA934,IBA935 FROM DCSIBA WHERE IBA001='{0}'" ,model . PRO001 );
                DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
                if ( dt == null || dt . Rows . Count < 1 )
                    return;

                List<OrderNotice> orList = new List<OrderNotice> ( );

                OrderNotice orN = new OrderNotice ( );

                //送审人
                orN . BNA003 = model . PRO002 = dt . Rows [ 0 ] [ "IBA911" ] . ToString ( );
                if ( string . IsNullOrEmpty ( model . PRO002 ) )
                    return;
                //通知单单号
                model . PRO003 = getOddNumForNotice ( );
                if ( strList . Contains ( model . PRO003 ) )
                {
                    model . PRO003 = strList . Max ( );
                    model . PRO003 = ( Convert . ToInt64 ( model . PRO003 ) + 1 ) . ToString ( );
                    strList . Add ( model . PRO003 );
                }
                else
                    strList . Add ( model . PRO003 );

                orN . BNA001 = model . PRO003;
                orN . BNA002 = model . PRO035;
                orN . BNA007 = "请审批单据：订单" + model . PRO001;
                orList . Add ( orN );

                //送审人
                orN . BNA003 = model . PRO002 = dt . Rows [ 0 ] [ "IBA933" ] . ToString ( );
                if ( string . IsNullOrEmpty ( model . PRO002 ) )
                    return;
                orList . Add ( orN );

                //送审人
                orN . BNA003 = model . PRO002 = dt . Rows [ 0 ] [ "IBA934" ] . ToString ( );
                if ( string . IsNullOrEmpty ( model . PRO002 ) )
                    return;
                orList . Add ( orN );

                //送审人
                orN . BNA003 = model . PRO002 = dt . Rows [ 0 ] [ "IBA935" ] . ToString ( );
                if ( string . IsNullOrEmpty ( model . PRO002 ) )
                    return;
                orList . Add ( orN );

                if ( orList == null || orList . Count < 1 )
                    return;
                foreach ( OrderNotice orNo in orList )
                {
                    strSql = new StringBuilder ( );
                    strSql . Append ( "INSERT INTO TPABNA ( " );
                    strSql . Append ( "BNA001,BNA002,BNA003,BNA004,BNA005,BNA006,BNA007,BNA008,BNA009,BNA010,BNA011,BNA012,BNA901,BNA902)" );
                    strSql . Append ( "VALUES (" );
                    strSql . Append ( "@BNA001,@BNA002,@BNA003,'F','T',GETDATE(),@BNA007,@BNA007,'1','T','COPDC02','T',@BNA002,GETDATE())" );
                    SqlParameter [ ] paramet = {
                        new SqlParameter("@BNA001",SqlDbType.NVarChar),
                        new SqlParameter("@BNA002",SqlDbType.NVarChar),
                        new SqlParameter("@BNA003",SqlDbType.NVarChar),
                        new SqlParameter("@BNA007",SqlDbType.NVarChar)
                    };
                    paramet [ 0 ] . Value = orNo . BNA001;
                    paramet [ 1 ] . Value = orNo . BNA002;
                    paramet [ 2 ] . Value = orNo . BNA003;
                    paramet [ 3 ] . Value = orNo . BNA007;

                    SQLString . Add ( strSql ,paramet );
                }
            }
            
            //IBA910: P审批中 T已审批  F打回退件  未送审或撤送审时空白
            //IBA022: T已审核 F未审核
            //IBA913 审批结果 F不同意 T同意
        }

        /// <summary>
        /// 获取服务器系统日期
        /// </summary>
        /// <returns></returns>
        DateTime getDt ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT GETDATE() t;" );

            DataTable da = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( da != null && da . Rows . Count > 0 )
            {
                if ( string . IsNullOrEmpty ( da . Rows [ 0 ] [ "t" ] . ToString ( ) ) )
                    return DateTime . Now;
                else
                    return Convert . ToDateTime ( da . Rows [ 0 ] [ "t" ] . ToString ( ) );
            }
            else
                return DateTime . Now;
        }

        /// <summary>
        /// 获取通知单单号
        /// </summary>
        /// <returns></returns>
        string getOddNumForNotice ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(BNA001) BNA001 FROM TPABNA WHERE BNA001 LIKE '{0}%'" ,getDt ( ) . ToString ( "yyyyMMdd" ) );

            DataTable de = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( de != null && de . Rows . Count > 0 )
            {
                string oddNum = de . Rows [ 0 ] [ "BNA001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( oddNum ) )
                    return getDt ( ) . ToString ( "yyyyMMdd" ) + "0001";
                else
                    return ( Convert . ToInt64 ( oddNum ) + 1 ) . ToString ( );
            }
            else
                return getDt ( ) . ToString ( "yyyyMMdd" ) + "0001";
        }

    }
}
