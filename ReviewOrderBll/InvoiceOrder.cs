using ReviewOrderEntity;
using StudentMgr;
using System;
using System . Collections . Generic;
using System . Data;
using System . Linq;
using System . Text;
using System . Threading . Tasks;

namespace ReviewOrderBll 
{
    public class InvoiceOrder
    {
        private readonly Dao.InvoiceOrderDao dalIn=null;
        private readonly Dao.FormBaseDao dal=null;

        List<UserEntity> userList=null;
        List<InvoiceBody> _bodyList=null;
        List<InvoiceHeader> _headerList=null;
        ExaminnationEntity modelExa=null;
        Dictionary<string ,string > strDicUser=null;
        Dictionary<string,string> strDicUserNum=null;

        string strWhere=string.Empty;

        public InvoiceOrder ( string strWhere )
        {
            dalIn = new Dao . InvoiceOrderDao ( );
            dal = new Dao . FormBaseDao ( );

            this . strWhere = strWhere;

            try
            {
                getData ( );
            }
            catch ( Exception ex )
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
            _headerList = dalIn . getInvoiceHeaders ( strWhere );
            _bodyList = dalIn . getInvoiceBodys ( strWhere );
            modelExa = dal . getExa ( "发货单(调拨单)" );
        }

        /// <summary>
        /// 获取送审申请人信息
        /// </summary>
        /// <returns></returns>
        bool getUserData ( )
        {
            if ( _headerList == null || _headerList . Count < 1 )
                return false;
            List<string> userLists = new List<string> ( );
            foreach ( InvoiceHeader header in _headerList )
            {
                userLists . Add ( header . LCA011 );
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




    }
    
}
