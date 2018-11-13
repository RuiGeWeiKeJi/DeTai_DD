using System . Collections . Generic;
using System . Data;
using System . Text;
using StudentMgr;
using DDHelper;
using System . Collections;
using System . Data . SqlClient;

namespace ReviewOrderBll . Dao
{
    public class FormBaseDao
    {
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTable ( )
        {
            StringBuilder strSql = strSql = new StringBuilder ( );
            strSql . Append ( "select corpid,corpsecret,developmentID from DETDDB " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 保存钉钉基础数据
        /// </summary>
        /// <returns></returns>
        public bool Save ( )
        {
            ArrayList SQLString = new ArrayList ( );
            StringBuilder strSql = strSql = new StringBuilder ( );
            strSql . Append ( "delete from DETDDB" );
            SQLString . Add ( strSql );
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "insert into DETDDB (corpid,corpsecret,developmentID) values ('{0}','{1}','{2}')" ,AppSetting . corpid ,AppSetting . corpsecret ,AppSetting . developmentID );
            SQLString . Add ( strSql );

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="departList"></param>
        /// <returns></returns>
        public bool SaveDepart ( List<Depart> departList )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = strSql = new StringBuilder ( );

            strSql . Append ( "DELETE FROM DETDBA" );

            SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );

            foreach ( Depart depart in departList )
            {
                if ( ExistsDepart ( depart . id ) )
                    EditDepart ( SQLString ,strSql ,depart );
                else
                    AddDepart ( SQLString ,strSql ,depart );
            }

            DataTable dt = getDepart ( );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                ReviewOrderEntity . DepartEntity depart = new ReviewOrderEntity . DepartEntity ( );
                for ( int i = 0 ; i < dt . Rows . Count ; i++ )
                {
                    depart . DBA001 = dt . Rows [ i ] [ "DBA001" ] . ToString ( );
                    depart . DBA002 = dt . Rows [ i ] [ "DBA002" ] . ToString ( );
                    depart . DBA003 = dt . Rows [ i ] [ "DBA003" ] . ToString ( );
                    if ( depart . DBA003 == null || string . IsNullOrEmpty ( depart . DBA003 ) )
                        return false;
                    if ( departList . Exists ( ( Depart d ) => d . id . Equals ( depart . DBA001 ) && d . name . Equals ( depart . DBA002 ) && d . parentId . Equals ( depart . DBA003 ) ) == false )
                        DeleteDepart ( SQLString ,strSql ,depart );
                }
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        /// <summary>
        /// 是否存在此部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistsDepart ( string id )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM DETDBA WHERE DBA001='{0}'" ,id );

            return SqlHelper . Exists ( strSql . ToString ( ) ); 
        }

        void AddDepart ( Hashtable SQLString ,StringBuilder strSql ,Depart depart )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into DETDBA(" );
            strSql . Append ( "DBA001,DBA002,DBA003)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@DBA001,@DBA002,@DBA003)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DBA001", SqlDbType.NVarChar,200),
                    new SqlParameter("@DBA002", SqlDbType.NVarChar,200),
                    new SqlParameter("@DBA003", SqlDbType.NVarChar,200)
            };
            parameters [ 0 ] . Value = depart . id;
            parameters [ 1 ] . Value = depart . name;
            parameters [ 2 ] . Value = depart . parentId;

            SQLString . Add ( strSql ,parameters );
        }

        void EditDepart ( Hashtable SQLString ,StringBuilder strSql ,Depart depart )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update DETDBA set " );
            strSql . Append ( "DBA002=@DBA002," );
            strSql . Append ( "DBA003=@DBA003 " );
            strSql . Append ( " where DBA001=@DBA001" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DBA001", SqlDbType.NVarChar,200),
                    new SqlParameter("@DBA002", SqlDbType.NVarChar,200),
                    new SqlParameter("@DBA003", SqlDbType.NVarChar,200)
            };
            parameters [ 0 ] . Value = depart . id;
            parameters [ 1 ] . Value = depart . name;
            parameters [ 2 ] . Value = depart . parentId;

            SQLString . Add ( strSql ,parameters );
        }

        void DeleteDepart ( Hashtable SQLString ,StringBuilder strSql ,ReviewOrderEntity . DepartEntity depart )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM DETDBA " );
            strSql . AppendFormat ( "WHERE DBA001='{0}' AND DBA002='{1}' AND DBA003='{2}'" ,depart . DBA001 ,depart . DBA002 ,depart . DBA003 );

            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DBA001,DBA002,DBA003 FROM DETDBA" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <returns></returns>
        public bool DeleteUser ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM DETDBB" );

            int rows = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) );

            return rows > 0 ? true : false;
        }

        /// <summary>
        /// 保存人员信息
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        public bool SaveUser ( List<User> userList ,string departmentId )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = strSql = new StringBuilder ( );
            foreach ( User user in userList )
            {
                user . departmentID = departmentId;
                if ( ExistsUser ( user . userid ,user.departmentID) )
                    EditUser ( SQLString ,strSql ,user );
                else
                    AddUser ( SQLString ,strSql ,user );
            }

            //DataTable dt = getUser ( );
            //if ( dt != null && dt . Rows . Count > 0 )
            //{
            //    ReviewOrderEntity . UserEntity user = new ReviewOrderEntity . UserEntity ( );
            //    for ( int i = 0 ; i < dt . Rows . Count ; i++ )
            //    {
            //        user . DBB001 = dt . Rows [ i ] [ "DBB001" ] . ToString ( );
            //        user . DBB002 = dt . Rows [ i ] [ "DBB002" ] . ToString ( );
            //        user . DBB003 = dt . Rows [ i ] [ "DBB003" ] . ToString ( );
            //        if ( userList . Exists ( ( User u ) => u . userid . Equals ( user . DBB001 . ToString ( ) ) && u . name . Equals ( user . DBB002 ) && u .departmentID . Equals ( user . DBB003 ) ) == false )
            //            DeleteUser ( SQLString ,strSql ,user );
            //    }
            //}

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }
        
        /// <summary>
        /// 是否存在此员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistsUser ( string id ,string departId)
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM DETDBB WHERE DBB001='{0}' AND DBB003='{1}'" ,id ,departId );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        void AddUser ( Hashtable SQLString ,StringBuilder strSql ,User user )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into DETDBB(" );
            strSql . Append ( "DBB001,DBB002,DBB003)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@DBB001,@DBB002,@DBB003)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DBB001", SqlDbType.VarChar,200),
                    new SqlParameter("@DBB002", SqlDbType.VarChar,200),
                    new SqlParameter("@DBB003", SqlDbType.VarChar,200)
            };
            parameters [ 0 ] . Value = user . userid;
            parameters [ 1 ] . Value = user . name;
            parameters [ 2 ] . Value = user . departmentID;
            SQLString . Add ( strSql ,parameters );
        }

        void EditUser ( Hashtable SQLString ,StringBuilder strSql ,User user )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update DETDBB set " );
            strSql . Append ( "DBB002=@DBB002 " );
            strSql . Append ( " where DBB001=@DBB001 AND DBB003=@DBB003" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DBB001", SqlDbType.VarChar,200),
                    new SqlParameter("@DBB002", SqlDbType.VarChar,200),
                    new SqlParameter("@DBB003", SqlDbType.VarChar,200)
            };
            parameters [ 0 ] . Value = user . userid;
            parameters [ 1 ] . Value = user . name;
            parameters [ 2 ] . Value = user . departmentID;
            SQLString . Add ( strSql ,parameters );
        }

        void DeleteUser ( Hashtable SQLString ,StringBuilder strSql ,ReviewOrderEntity . UserEntity user )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM DETDBB " );
            strSql . AppendFormat ( "WHERE DBB001='{0}' AND DBB002='{1}' AND DBB003='{2}'" ,user . DBB001 ,user . DBB002 ,user . DBB003 );

            SQLString . Add ( strSql ,null );
        }


        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <returns></returns>
        DataTable getUser ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DBB001,DBB002,DBB003 FROM DETDBB " );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }


        /// <summary>
        /// 保存审批表单相关
        /// </summary>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool SaveTableView ( DataTable tableView )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            DataTable dt = getTableExa ( );
            bool result = false;
            if ( dt != null && dt . Rows . Count > 0 )
                result = true;
            ReviewOrderEntity . ExaminnationEntity model = new ReviewOrderEntity . ExaminnationEntity ( );
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                model . EXA001 = tableView . Rows [ i ] [ "EXA001" ] . ToString ( );
                model . EXA002 = tableView . Rows [ i ] [ "EXA002" ] . ToString ( );
                if ( result == false )
                    AddExa ( SQLString ,strSql ,model );
                else
                {
                    if ( dt . Select ( "EXA001='" + model . EXA001 + "' AND EXA002='" + model . EXA002 + "'" ) . Length < 1 )
                        AddExa ( SQLString ,strSql ,model );
                }
            }

            if ( result )
            {
                for ( int i = 0 ; i < dt . Rows . Count ; i++ )
                {
                    model . EXA001 = dt . Rows [ i ] [ "EXA001" ] . ToString ( );
                    model . EXA002 = dt . Rows [ i ] [ "EXA002" ] . ToString ( );
                    if ( tableView . Select ( "EXA001='" + model . EXA001 + "' AND EXA002='" + model . EXA002 + "'" ) . Length < 1 )
                    {
                        DeleteExa ( SQLString ,strSql ,model );
                    }
                }
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }
        
        /// <summary>
        /// 获取审批表单相关数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableExa ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT EXA001,EXA002 FROM DETEXA" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        void AddExa ( Hashtable SQLString ,StringBuilder strSql ,ReviewOrderEntity . ExaminnationEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO DETEXA (" );
            strSql . Append ( "EXA001,EXA002)" );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@EXA001,@EXA002)" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@EXA001",SqlDbType.NVarChar,200),
                new SqlParameter("@EXA002",SqlDbType.NVarChar,200)
            };
            parameter [ 0 ] . Value = model . EXA001;
            parameter [ 1 ] . Value = model . EXA002;
            SQLString . Add ( strSql ,parameter );
        }

        void DeleteExa ( Hashtable SQLString ,StringBuilder strSql ,ReviewOrderEntity . ExaminnationEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "DELETE FROM DETEXA " );
            strSql . AppendFormat ( "WHERE EXA001='{0}' AND EXA002='{1}'" ,model . EXA001 ,model . EXA002 );

            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 获取审批ID相关
        /// </summary>
        /// <param name="nameOf"></param>
        /// <returns></returns>
        public ReviewOrderEntity . ExaminnationEntity getExa ( string nameOf )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT EXA001,EXA002 FROM DETEXA WHERE EXA001='{0}' " ,nameOf );

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                return getModel ( dt . Rows [ 0 ] );
            }
            else
                return null;
        }

        public ReviewOrderEntity . ExaminnationEntity getModel ( DataRow row )
        {
            ReviewOrderEntity . ExaminnationEntity model = new ReviewOrderEntity . ExaminnationEntity ( );
            if ( row != null )
            {
                if ( row [ "EXA001" ] != null )
                {
                    model . EXA001 = row [ "EXA001" ] . ToString ( );
                }
                if ( row [ "EXA002" ] != null )
                {
                    model . EXA002 = row [ "EXA002" ] . ToString ( );
                }
            }

            return model;
        }

        /// <summary>
        /// 获取审核申请人信息
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        public List<ReviewOrderEntity . UserEntity> getUser ( List<string> userList )
        {
            string users = string . Empty;
            foreach ( string user in userList )
            {
                if ( string . IsNullOrEmpty ( users ) )
                    users = "'" + user + "'";
                else
                    users = users + "," + "'" + user + "'";
            }
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT D.DBA001 DBB005,DBB001,DBB002,A.DBA002,A.DBA001 FROM DETDBA A INNER JOIN DETDBB B ON A.DBA001=B.DBB003 INNER JOIN TPADBA D ON D.DBA002=B.DBB002 INNER JOIN TPADAA E ON A.DBA002=E.DAA002 INNER JOIN DETPRO C ON D.DBA001=C.PRO009 " );
            strSql . AppendFormat ( "WHERE C.PRO009 IN ({0})" ,users );
            
            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            List<ReviewOrderEntity . UserEntity> modelList = new List<ReviewOrderEntity . UserEntity> ( );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                for ( int i = 0 ; i < dt . Rows . Count ; i++ )
                {
                    modelList . Add ( getModelUser ( dt . Rows [ i ] ) );
                }
                return modelList;
            }
            else
                return null;
        }

        public ReviewOrderEntity . UserEntity getModelUser ( DataRow row )
        {
            ReviewOrderEntity . UserEntity model = new ReviewOrderEntity . UserEntity ( );

            if ( row != null )
            {
                if ( row [ "DBB001" ] != null )
                {
                    model . DBB001 = row [ "DBB001" ] . ToString ( );
                }
                if ( row [ "DBB002" ] != null )
                {
                    model . DBB002 = row [ "DBB002" ] . ToString ( );
                }
                if ( row [ "DBA001" ] != null )
                {
                    model . DBB003 = row [ "DBA001" ] . ToString ( );
                }
                if ( row [ "DBA002" ] != null )
                {
                    model . DBB004 = row [ "DBA002" ] . ToString ( );
                }
                if ( row [ "DBB005" ] != null )
                {
                    model . DBB005 = row [ "DBB005" ] . ToString ( );
                }
            }

            return model;
        }

    }
}
