using System . Collections . Generic;
using System . Data;
using System . Text;
using StudentMgr;

namespace ReviewOrderBll . Dao
{
    public class InvoiceOrderDao
    {
        /// <summary>
        /// 获取要送审到钉钉的审核数据头
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ReviewOrderEntity . InvoiceHeader> getInvoiceHeaders ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LCA001,LCA002,LCA003,LCA004,LCA005,LCA006,LCA007,LCA008,LCA009 FROM DETLCA WHERE LCA010='P' AND (LCA012='' OR LCA012 IS NULL) AND LCA001 IN ({0}) " ,strWhere );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            List<ReviewOrderEntity . InvoiceHeader> modelHeanders = new List<ReviewOrderEntity . InvoiceHeader> ( );
            if ( table != null && table . Rows . Count > 0 )
            {
                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    modelHeanders . Add ( getModelHeader ( table . Rows [ i ] ) );
                }
                return modelHeanders;
            }
            else
                return null;
        }

        public ReviewOrderEntity . InvoiceHeader getModelHeader ( DataRow row )
        {
            ReviewOrderEntity . InvoiceHeader model = new ReviewOrderEntity . InvoiceHeader ( );
            if ( row != null )
            {
                if ( row [ "LCA001" ] != null )
                {
                    model . LCA001 = row [ "LCA001" ] . ToString ( );
                }
                if ( row [ "LCA002" ] != null )
                {
                    model . LCA002 = row [ "LCA002" ] . ToString ( );
                }
                if ( row [ "LCA003" ] != null )
                {
                    model . LCA003 = row [ "LCA003" ] . ToString ( );
                }
                if ( row [ "LCA004" ] != null )
                {
                    model . LCA004 = row [ "LCA004" ] . ToString ( );
                }
                if ( row [ "LCA005" ] != null )
                {
                    model . LCA005 = row [ "LCA005" ] . ToString ( );
                }
                if ( row [ "LCA006" ] != null )
                {
                    model . LCA006 = row [ "LCA006" ] . ToString ( );
                }
                if ( row [ "LCA007" ] != null )
                {
                    model . LCA007 = row [ "LCA007" ] . ToString ( );
                }
                if ( row [ "LCA008" ] != null )
                {
                    model . LCA008 = row [ "LCA008" ] . ToString ( );
                }
                if ( row [ "LCA009" ] != null )
                {
                    model . LCA009 = row [ "LCA009" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取要送审到钉钉的审核数据身
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<ReviewOrderEntity . InvoiceBody> getInvoiceBodys ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LCB002,LCB005,LCB006,LCB007,LCB008 FROM DETLCA A INNER JOIN DETLCB B ON A.LCA001=B.LCB001 WHERE LCA010='P' AND (LCA012='' OR LCA012 IS NULL) AND LCA001 IN ({0})" ,strWhere );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                List<ReviewOrderEntity . InvoiceBody> modelBody = new List<ReviewOrderEntity . InvoiceBody> ( );

                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    modelBody . Add ( getModelBody ( table . Rows [ i ] ) );
                }
                return modelBody;
            }
            else
                return null;
        }

        public ReviewOrderEntity . InvoiceBody getModelBody ( DataRow row )
        {
            ReviewOrderEntity . InvoiceBody model = new ReviewOrderEntity . InvoiceBody ( );
            if ( row != null )
            {
                if ( row [ "LCB002" ] != null )
                {
                    model . LCB002 = row [ "LCB002" ] . ToString ( );
                }
                if ( row [ "LCB005" ] != null )
                {
                    model . LCB005 = row [ "LCB005" ] . ToString ( );
                }
                if ( row [ "LCB006" ] != null )
                {
                    model . LCB006 = row [ "LCB006" ] . ToString ( );
                }
                if ( row [ "LCB007" ] != null && row [ "LCB007" ] . ToString ( ) != "" )
                {
                    model . LCB007 = decimal . Parse ( row [ "LCB007" ] . ToString ( ) );
                }
                if ( row [ "LCB008" ] != null )
                {
                    model . LCB008 = row [ "LCB008" ] . ToString ( );
                }
            }
            return model;
        }

    }
    
}
