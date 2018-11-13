using System . Collections . Generic;
using System . Data;
using System . Text;
using StudentMgr;

namespace ReviewOrderBll . Dao
{
    public class OrderDao
    {
        /// <summary>
        /// 获取待送审的订单单头内容
        /// </summary>
        /// <returns></returns>
        public List<ReviewOrderEntity . OrderHeaderEntity> getOrderHeander (string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRO001,PRO002,PRO003,PRO004,PRO005,PRO006,PRO007,PRO008,PRO009 FROM DETPRO WHERE PRO008='P' AND (PRO010='' OR PRO010 IS NULL) AND PRO001 IN ({0})" ,strWhere );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            List<ReviewOrderEntity . OrderHeaderEntity> modelList = new List<ReviewOrderEntity . OrderHeaderEntity> ( );
            if ( table != null && table . Rows . Count > 0 )
            {
                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    modelList . Add ( getModel ( table . Rows [ i ] ) );
                }
                return modelList;
            }
            else
                return null;
        }

        public ReviewOrderEntity . OrderHeaderEntity getModel ( DataRow row )
        {
            ReviewOrderEntity . OrderHeaderEntity model = new ReviewOrderEntity . OrderHeaderEntity ( );
            if ( row != null )
            {
                if ( row [ "PRO001" ] != null )
                {
                    model . PRO001 = row [ "PRO001" ] . ToString ( );
                }
                if ( row [ "PRO002" ] != null )
                {
                    model . PRO002 = row [ "PRO002" ] . ToString ( );
                }
                if ( row [ "PRO003" ] != null )
                {
                    model . PRO003 = row [ "PRO003" ] . ToString ( );
                }
                if ( row [ "PRO004" ] != null )
                {
                    model . PRO004 = row [ "PRO004" ] . ToString ( );
                }
                if ( row [ "PRO005" ] != null )
                {
                    model . PRO005 = row [ "PRO005" ] . ToString ( );
                }
                if ( row [ "PRO006" ] != null )
                {
                    model . PRO006 = row [ "PRO006" ] . ToString ( );
                }
                if ( row [ "PRO007" ] != null )
                {
                    model . PRO007 = row [ "PRO007" ] . ToString ( );
                }
                if ( row [ "PRO008" ] != null )
                {
                    model . PRO008 = row [ "PRO008" ] . ToString ( );
                }
                if ( row [ "PRO009" ] != null )
                {
                    model . PRO009 = row [ "PRO009" ] . ToString ( );
                }
            }
            return model;
        }

        /// <summary>
        /// 获取待送审的订单单身内容
        /// </summary>
        /// <returns></returns>
        public List<ReviewOrderEntity . OrderBodyEntity> getOrderBody ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT PRP001,PRP002,PRP003,PRP004,PRP005,PRP006,PRP007 FROM DETPRP A INNER JOIN DETPRO B ON A.PRP001=B.PRO001 AND PRO008='P' AND (PRO010='' OR PRO010 IS NULL) AND PRP001 IN ({0})" ,strWhere );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            List<ReviewOrderEntity . OrderBodyEntity> modelList = new List<ReviewOrderEntity . OrderBodyEntity> ( );
            if ( table != null && table . Rows . Count > 0 )
            {
                for ( int i = 0 ; i < table . Rows . Count ; i++ )
                {
                    modelList . Add ( getModelBody ( table . Rows [ i ] ) );
                }
                return modelList;
            }
            else
                return null;
        }

        public ReviewOrderEntity . OrderBodyEntity getModelBody ( DataRow row )
        {
            ReviewOrderEntity . OrderBodyEntity model = new ReviewOrderEntity . OrderBodyEntity ( );
            if ( row != null )
            {
                if ( row [ "PRP001" ] != null )
                {
                    model . PRP001 = row [ "PRP001" ] . ToString ( );
                }
                if ( row [ "PRP002" ] != null )
                {
                    model . PRP002 = row [ "PRP002" ] . ToString ( );
                }
                if ( row [ "PRP003" ] != null )
                {
                    model . PRP003 = row [ "PRP003" ] . ToString ( );
                }
                if ( row [ "PRP004" ] != null )
                {
                    model . PRP004 = row [ "PRP004" ] . ToString ( );
                }
                if ( row [ "PRP005" ] != null && row [ "PRP005" ] . ToString ( ) != "" )
                {
                    model . PRP005 = decimal . Parse ( row [ "PRP005" ] . ToString ( ) );
                }
                if ( row [ "PRP006" ] != null )
                {
                    model . PRP006 = row [ "PRP006" ] . ToString ( );
                }
                if ( row [ "PRP007" ] != null )
                {
                    model . PRP007 = row [ "PRP007" ] . ToString ( );
                }
            }
            return model;
        }

    }
}
