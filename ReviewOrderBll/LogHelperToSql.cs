using System . Text;
using StudentMgr;
using System . Data . SqlClient;
using ReviewOrderBll;

namespace CarpenterBll
{
    public static class LogHelperToSql
    {
        /// <summary>
        /// 写操作日志到数据库
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void SaveLog ( string cmdText )
        {
            if ( cmdText . Contains ( "DETLOG" ) )
                return;
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO DETLOG (" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}','{2}',GETDATE(),null) " ,Log . Review ,Log . OddNum ,cmdText . Replace ( "'" ,"''" ) );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 写操作日志到数据库
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void SaveLog ( string cmdText ,params SqlParameter [ ] parameters )
        {
            if ( cmdText . Contains ( "DETLOG" ) )
                return;
            string param = string . Empty;
            if ( parameters . Length > 0 )
            {
                for ( int i = 0 ; i < parameters . Length ; i++ )
                {
                    if ( param == string . Empty )
                        param = " [ " + parameters [ i ] . ToString ( ) + ":" + parameters [ i ] . Value + " ] ";
                    else
                        param = param + " [ " + parameters [ i ] . ToString ( ) + ":" + parameters [ i ] . Value + " ] ";
                }
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO DETLOG (" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}','{2}',GETDATE(),'{3}') " ,Log . Review ,Log . OddNum ,cmdText . Replace ( "'" ,"''" ) ,param );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 写操作日志到数据库
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        public static void SaveLog ( string cmdText ,params object [ ] parameters )
        {
            if ( cmdText . Contains ( "DETLOG" ) )
                return;
            string param = string . Empty;

            foreach ( SqlParameter parame in parameters )
            {
                if ( param == string . Empty )
                    param = " [ " + parame + ":" + parame . Value + " ] ";
                else
                    param = param + " [ " + parame + ":" + parame . Value + " ] ";
            }

            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO DETLOG (" );
            strSql . Append ( "LOG001,LOG002,LOG003,LOG004,LOG005) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}','{2}',GETDATE(),'{3}') " ,Log . Review ,Log . OddNum ,cmdText . Replace ( "'" ,"''" ) ,param );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );
        }

    }
}
