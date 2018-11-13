using DDHelper;
using System . Collections . Generic;
using System . Data;

namespace ReviewOrderBll . Bll
{
    public class FormBaseBll
    {
        private readonly Dao.FormBaseDao dal=null;
        private readonly Dao.OrderDao dalOrder=null;

        public FormBaseBll ( )
        {
            dal = new Dao . FormBaseDao ( );
            dalOrder = new Dao . OrderDao ( );
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public DataTable getTable ( )
        {
            return dal . getTable ( );
        }

        /// <summary>
        /// 保存钉钉基础数据
        /// </summary>
        /// <returns></returns>
        public bool Save ( )
        {
            return dal . Save ( );
        }

        /// <summary>
        /// 保存部门信息
        /// </summary>
        /// <param name="departList"></param>
        /// <returns></returns>
        public bool SaveDepart ( List<Depart> departList )
        {
            return dal . SaveDepart ( departList );
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <returns></returns>
        public bool DeleteUser ( )
        {
            return dal . DeleteUser ( );
        }

        /// <summary>
        /// 保存人员信息
        /// </summary>
        /// <param name="userList"></param>
        /// <returns></returns>
        public bool SaveUser ( List<User> userList,string departmentId )
        {
            return dal . SaveUser ( userList,departmentId );
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <returns></returns>
        public DataTable getDepart ( )
        {
            return dal . getDepart ( );
        }

        /// <summary>
        /// 获取审批表单相关数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableExa ( )
        {
            return dal . getTableExa ( );
        }

        /// <summary>
        /// 保存审批表单相关
        /// </summary>
        /// <param name="tableView"></param>
        /// <returns></returns>
        public bool SaveTableView ( DataTable tableView )
        {
            return dal . SaveTableView ( tableView );
        }

        /// <summary>
        /// 获取待送审的订单单头内容
        /// </summary>
        /// <returns></returns>
        public List<ReviewOrderEntity . OrderHeaderEntity> getOrderHeander ( )
        {
            return dalOrder . getOrderHeander ( "''");
        }

        /// <summary>
        /// 获取待送审的订单单身内容
        /// </summary>
        /// <returns></returns>
        public List<ReviewOrderEntity . OrderBodyEntity> getOrderBody ( )
        {
            return dalOrder . getOrderBody ( "''" );
        }

    }
}
