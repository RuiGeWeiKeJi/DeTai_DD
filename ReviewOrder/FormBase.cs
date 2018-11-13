using System;
using System . Windows . Forms;
using DDHelper;
using System . Data;
using DevExpress . XtraEditors;
using System . Data . SqlClient;
using StudentMgr;
using System . Collections . Generic;
using DingTalk . Api;
using DingTalk . Api . Request;
using DingTalk . Api . Response;
using System . Threading;
using CarpenterBll;

namespace ReviewOrder
{
    public partial class FormBase :DevExpress . XtraEditors . XtraForm
    {
        public static AccessToken AccessToken = new AccessToken ( );
        ReviewOrderBll.Bll.FormBaseBll _bll=null;
        DataTable table=null,tableView=null;bool isOk=false;
        string strWhere="1=1";

        public FormBase ( )
        {
            InitializeComponent ( );
            _bll = new ReviewOrderBll . Bll . FormBaseBll ( );
        }

        private void FormBase_Load ( object sender ,EventArgs e )
        {
            try
            {
                table = _bll . getTable ( );
                if ( table == null || table . Rows . Count < 1 )
                    return;
                AppSetting . corpid = txtCropid . Text = table . Rows [ 0 ] [ "corpid" ] . ToString ( );
                AppSetting . corpsecret = txtCropse . Text = table . Rows [ 0 ] [ "corpsecret" ] . ToString ( );
                AppSetting . developmentID = txtDev . Text = table . Rows [ 0 ] [ "developmentID" ] . ToString ( );

                //隐藏窗体在右下角
                this . WindowState = FormWindowState . Minimized;
                this . Hide ( );
                notifyIcon1 . Visible = true;

                getData ( );

                isOk = true;
                Thread thread = new Thread ( new ThreadStart ( this . ThreadProcSafePost ) );
                thread . Start ( );

                Thread threadInvoice = new Thread ( new ThreadStart ( this . ThreadProcInvoice ) );
                threadInvoice . Start ( );
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteException ( "错误:" + ex . StackTrace + "警告：" + ex . Message ,ex );
                throw new Exception ( "load:" + ex . Message );
            }
        }

        void getData ( )
        {
            try
            {
                getDepartMentInfo ( );
                getUserInfo ( );
                getExaInfo ( );

                SqlDependency . Start ( SqlHelper . Connstr );
                UpdateGrid ( "SELECT PRO001 FROM DETPRO WHERE PRO008='P' AND (PRO010='' OR PRO010 IS NULL)" );
                UpdateInvoice ( "SELECT LCA001 FROM DETLCA WHERE LCA010='P' AND (LCA012='' OR LCA012 IS NULL)" );
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteException ( "错误:" + ex . StackTrace + "警告：" + ex . Message ,ex );
                throw new Exception ( "getdate:" + ex . Message );
            }
        }

        #region 监控数据库数据变化
        /// <summary>
        /// 生产单（订单）
        /// </summary>
        /// <returns></returns>
        public string UpdateGrid ( string sql )
        {
            string oddNum = string . Empty;

            using ( SqlConnection connection = new SqlConnection ( SqlHelper.Connstr ) )
            {
                using ( SqlCommand command = new SqlCommand ( sql ,connection ) )
                {
                    command . CommandType = CommandType . Text;
                    connection . Open ( );
                    SqlDependency dependency = new SqlDependency ( command );
                    dependency . OnChange += new OnChangeEventHandler ( dependency_OnChange );

                    using ( SqlDataAdapter adapter = new SqlDataAdapter ( command ) )
                    {
                        DataTable dt = new DataTable ( );
                        adapter . Fill ( dt );
                        try
                        {
                            LogHelperToSql . SaveLog ( sql );
                        }
                        catch ( Exception ex ) { Utility . LogHelper . WriteLog ( ex . Message ); }

                        if ( dt == null || dt . Rows . Count < 1 )
                            return string . Empty;
                        else
                        {
                            for ( int i = 0 ; i < dt . Rows . Count ; i++ )
                            {
                                if ( string . IsNullOrEmpty ( oddNum ) )
                                    oddNum = "'" + dt . Rows [ i ] [ "PRO001" ] . ToString ( ) + "'";
                                else
                                    oddNum = oddNum + "," + "'" + dt . Rows [ i ] [ "PRO001" ] . ToString ( ) + "'";
                            }
                            return oddNum;
                        }
                    }
                }
            }
        }
        private void dependency_OnChange ( object sender ,SqlNotificationEventArgs e )
        {
            strWhere = UpdateGrid ( "SELECT PRO001 FROM DETPRO WHERE PRO008='P' AND (PRO010='' OR PRO010 IS NULL)" );
            if ( !string . IsNullOrEmpty ( strWhere ) )
                SendMessageForOrder ( strWhere );
        }

        /// <summary>
        /// 发货单（调拨单）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string UpdateInvoice ( string sql )
        {
            string oddNum = string . Empty;

            using ( SqlConnection connection = new SqlConnection ( SqlHelper . Connstr ) )
            {
                using ( SqlCommand command = new SqlCommand ( sql ,connection ) )
                {
                    command . CommandType = CommandType . Text;
                    connection . Open ( );
                    SqlDependency dependency = new SqlDependency ( command );
                    dependency . OnChange += new OnChangeEventHandler ( dependencyInvoice_OnChange );

                    using ( SqlDataAdapter adapter = new SqlDataAdapter ( command ) )
                    {
                        DataTable dt = new DataTable ( );
                        adapter . Fill ( dt );
                        try
                        {
                            LogHelperToSql . SaveLog ( sql );
                        }
                        catch ( Exception ex ) { Utility . LogHelper . WriteLog ( ex . Message ); }

                        if ( dt == null || dt . Rows . Count < 1 )
                            return string . Empty;
                        else
                        {
                            for ( int i = 0 ; i < dt . Rows . Count ; i++ )
                            {
                                if ( string . IsNullOrEmpty ( oddNum ) )
                                    oddNum = "'" + dt . Rows [ i ] [ "LCA001" ] . ToString ( ) + "'";
                                else
                                    oddNum = oddNum + "," + "'" + dt . Rows [ i ] [ "LCA001" ] . ToString ( ) + "'";
                            }
                            return oddNum;
                        }
                    }
                }
            }
        }
        private void dependencyInvoice_OnChange ( object sender ,SqlNotificationEventArgs e )
        {
            strWhere = UpdateGrid ( "SELECT LCA001 FROM DETLCA WHERE LCA010='P' AND (LCA012='' OR LCA012 IS NULL)" );
            if ( !string . IsNullOrEmpty ( strWhere ) )
                SendMessageForInvoice ( strWhere );
        }
        #endregion

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnConfig_Click ( object sender ,EventArgs e )
        {
            Connection . Form1 form = new Connection . Form1 ( );
            form . StartPosition =  FormStartPosition . CenterScreen;
            form . ShowDialog ( );
        }

        /// <summary>
        /// 保存钉钉设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click ( object sender ,EventArgs e )
        {
            dxErrorProvider1 . ClearErrors ( );
            if ( string . IsNullOrEmpty ( txtCropid . Text ) )
            {
                dxErrorProvider1 . SetError ( txtCropid ,"不可为空" );
                return;
            }
            if ( string . IsNullOrEmpty ( txtCropse . Text ) )
            {
                dxErrorProvider1 . SetError ( txtCropse ,"不可为空" );
                return;
            }
            if ( string . IsNullOrEmpty ( txtDev . Text ) )
            {
                dxErrorProvider1 . SetError ( txtDev ,"不可为空" );
                return;
            }
            AppSetting . corpid = txtCropid . Text;
            AppSetting . corpsecret = txtCropse . Text;
            AppSetting . developmentID = txtDev . Text;

            bool result = _bll . Save ( );
            if ( result == false )
                XtraMessageBox . Show ( "保存失败" ,"提示" );
            else
                XtraMessageBox . Show ( "成功保存" ,"提示" );
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick ( object sender ,EventArgs e )
        {
            this . Visible = true;
            this . WindowState = FormWindowState . Normal;
        }

        /// <summary>
        /// 获取部门信息并保存
        /// </summary>
        private void getDepartMentInfo ( )
        {
            string apiurl = FormatApiUrlWithToken ( Urls . department_list );
            var result = Analyze . Get<DepartResultSet> ( apiurl );
            List<DepartResultSet> list = XmlUtil . JsonStringToObj<DepartResultSet> ( result . Json );
            if ( list . Count > 0 )
            {
                foreach ( DepartResultSet depart in list )
                {
                    if ( depart . department . Count > 0 )
                    {
                        //保存部门信息到数据库
                        isOk = _bll . SaveDepart ( depart . department );
                    }
                }
            }

            if ( isOk == false )
            {
                Utility . LogHelper . WriteLog ( "部门信息保存失败" );
            }
        }

        /// <summary>
        /// 获取人员信息并保存
        /// </summary>
        private void getUserInfo ( )
        {
            DataTable depart = _bll . getDepart ( );
            if ( depart == null || depart . Rows . Count < 1 )
                return;

            UpdateAccessToken ( false );
            IDingTalkClient client = new DefaultDingTalkClient ( Urls . department_user );
            List<UserResultSet> list = new List<UserResultSet> ( );

            _bll . DeleteUser ( );

            for ( int i = 0 ; i < depart . Rows . Count ; i++ )
            {
                OapiUserSimplelistRequest req = new OapiUserSimplelistRequest ( );
                req . Lang = "zh_CN";
                req . DepartmentId = string . IsNullOrEmpty ( depart . Rows [ i ] [ "DBA001" ] . ToString ( ) ) == true ? 1L : Convert . ToInt64 ( depart . Rows [ i ] [ "DBA001" ] . ToString ( ) );
                req . Offset = 0L;
                req . Size = 100L;
                req . Order = "custom";
                req . SetHttpMethod ( "GET" );
                OapiUserSimplelistResponse rsp = client . Execute ( req ,AccessToken . Value );
                list = XmlUtil . JsonStringToObj<UserResultSet> ( rsp .Body );
                if ( list . Count > 0 )
                {
                    foreach ( UserResultSet user in list )
                    {
                        if ( user . userlist . Count > 0 )
                        {
                            isOk = _bll . SaveUser ( user . userlist ,req . DepartmentId . ToString ( ) );
                        }
                    }
                }
            }
            if ( isOk == false )
            {
                Utility . LogHelper . WriteLog ( "人员信息保存失败" );
            }

            //string apiurl = FormatApiUrlWithToken ( Urls . user_list );
            //var result = Analyze . Get<UserResultSet> ( apiurl );
            //List<UserResultSet> list = XmlUtil . JsonStringToObj<UserResultSet> ( result . Json );
            //if ( list . Count > 0 )
            //{
            //    foreach ( UserResultSet user in list )
            //    {
            //        if ( user . department . Count > 0 )
            //        {
            //            isOk = _bll . SaveUser ( user . department );
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="forced"></param>
        private static void UpdateAccessToken (bool forced = false )
        {
            //ConstVars.CACHE_TIME是缓存时间(常量，也可放到配置文件中)，这样在有效期内则直接从缓存中获取票据，不需要再向服务器中获取。
            if ( !forced && AccessToken . Begin . AddSeconds ( ConstVars . CACHE_TIME ) >= DateTime . Now )
            {
                return;
            }

            string CorpID = AppSetting . corpid;
            string CorpSecret = AppSetting . corpsecret;
            string TokenUrl = Urls . gettoken;
            string apiurl = $"{TokenUrl}?{DDHelper . Keys . corpid}={CorpID}&{DDHelper . Keys . corpsecret}={CorpSecret}";
            TokenResult tokenResult = Analyze . Get<TokenResult> ( apiurl );

            if ( tokenResult . ErrCode == ErrCodeEnum . OK )
            {
                AccessToken . Value = tokenResult . Access_token;
                AccessToken . Begin = DateTime . Now;
            }
        }

        /// <summary>
        /// 获取url
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="forceUpdate"></param>
        /// <returns></returns>
        private static string FormatApiUrlWithToken ( string Url ,bool forceUpdate = false )
        {
            //获取token
            UpdateAccessToken ( forceUpdate );
            string apiurl = $"{Url}?{DDHelper . Keys . access_token}={AccessToken . Value}";

            return apiurl;
        }

        /// <summary>
        /// 审批表单相关信息设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveBase_Click ( object sender ,EventArgs e )
        {
            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
                return;
            isOk = _bll . SaveTableView ( tableView );
            if ( isOk == false )
            {
                Utility . LogHelper . WriteLog ( "审批相关保存失败" );
            }
        }

        /// <summary>
        /// 获取审批表单相关数据
        /// </summary>
        private void getExaInfo ( )
        {
            tableView = _bll . getTableExa ( );
            gridControl1 . DataSource = tableView;
        }

        #region 发送审批信息
        /// <summary>
        /// 发送订单审核消息
        /// </summary>
        private void SendMessageForOrder ( string strWhere )
        {
            ReviewOrderBll . SendInfoForOrder sender = new ReviewOrderBll . SendInfoForOrder ( strWhere );
            UpdateAccessToken ( false );
            sender . SendMessageForOrder ( AccessToken . Value );

        }
        private void ThreadProcSafePost ( )
        {
            while ( true )
            {
                //休眠一分钟
                Thread . Sleep ( 1000 * 60 );
                ReviewOrderBll . SendInfoForOrder sender = new ReviewOrderBll . SendInfoForOrder ( "''" );
                UpdateAccessToken ( false );
                isOk = sender . ReceiveResult ( AccessToken . Value );
            }
        }

        /// <summary>
        /// 发送发货单审核消息
        /// </summary>
        /// <param name="strWhere"></param>
        private void SendMessageForInvoice ( string strWhere )
        {

        }
        private void ThreadProcInvoice ( )
        {

        }
        #endregion

    }
}

