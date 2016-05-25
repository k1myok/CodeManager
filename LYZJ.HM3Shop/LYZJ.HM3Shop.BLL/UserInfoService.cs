using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LYZJ.HM3Shop.DAL;    //
using LYZJ.HM3Shop.IDAL;   //
using LYZJ.HM3Shop.Model;
using LYZJ.HM3Shop.IBLL;
using LYZJ.HM3Shop.Model.Enum;
using System.Collections.Generic;
using LYZJ.HM3Shop.Model.Compare;  //


namespace LYZJ.HM3Shop.BLL
{
    /// <summary>
    /// UserInfo相关的业务封装
    /// </summary>
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {

        #region -----重点介绍  备注-----
        ////访问DAL实现CRUD，记得这里使用接口编程：IUserInfoRepository,不要写成UserInfoRepository
        ////依赖接口编程
        ////private IUserInfoRepository _userInfoRepository = new UserInfoRepository();
        ////当UserInfoRepository实例变化的时候，在BLL很多地方都用到了此实例，

        ////简单工厂：创建实例，不再依赖具体的实例的类型
        //private IUserInfoRepository _userInfoRepository = RepositoryFactory.UserInfoRepository;

        //public UserInfo AddUserInfo(UserInfo userInfo)
        //{
        //    return _userInfoRepository.AddEntities(userInfo);
        //}

        //public bool UpdateUserInfo(UserInfo userInfo)
        //{
        //    return _userInfoRepository.UpdateEntities(userInfo);
        //}

        #endregion

        //public override void SetCurrentRepository()
        //{
        //    CurrentRepository = _dbSession.UserInfoRepository;
        //}

        //public UserInfo Login()
        //{
        //    //操作用户表

        //    //操作积分表

        //    //用户单位关联表

        //    //操作用户角色表

        //    //_dbSession.SaveChanges();
        //}


        /// <summary>
        /// 检验用户是否登录成功
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public UserInfo checkUserLogin(UserInfo userinfo)
        {
            //判断用户的用户名密码是否正确
            return _dbSession.UserInfoRepository.LoadEntities(u => u.UName == userinfo.UName && u.Pwd == userinfo.Pwd)
                .FirstOrDefault();
        }

        /// <summary>
        /// List集合实现多选删除数据
        /// </summary>
        /// <param name="DeleteUserInfoID"></param>
        /// <returns></returns>
        public int DeleteUserInfo(List<int> DeleteUserInfoID)
        {
            foreach (var ID in DeleteUserInfoID)
            {
                _dbSession.UserInfoRepository.DeleteEntities(new UserInfo() { ID = ID });
            }
            return _dbSession.SaveChanges();
        }

        /// <summary>
        /// 加载用户模糊查询的数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IQueryable<UserInfo> LoadSearchData(GetModelQuery query)
        {
            //拿到所有的数据
            var temp = _dbSession.UserInfoRepository.LoadEntities(u => true);

            //进行过滤姓名
            if (!string.IsNullOrEmpty(query.Name))
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(query.Name));
            }
            //进行邮箱的过滤
            if (!string.IsNullOrEmpty(query.Mail))
            {
                temp = temp.Where<UserInfo>(u => u.Mail.Contains(query.Mail));
            }
            //返回总数
            query.total = temp.Count();

            //做分页查询
            return temp.Skip(query.pageSize * (query.pageIndex - 1)).Take(query.pageSize).AsQueryable();

        }

        /// <summary>
        /// 实现添加用户角色的信息，先删除所有的数据，然后再次的添加新数据就行了
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDList"></param>
        /// <returns></returns>
        public bool SetUserInfoRole(int userID, List<int> roleIDList)
        {
            //首先根据用户ID，查询出用户的信息
            var currentUser = _dbSession.UserInfoRepository.LoadEntities(c => c.ID == userID).FirstOrDefault();
            if (currentUser == null)
            {
                return false;
            }
            //得到角色表中的数据全部返回
            var listRoles = currentUser.R_UserInfo_Role.ToList();
            ///处理清空原来的数据，用户的和角色的中间表信息
            for (int i = 0; i < listRoles.Count; i++)
            {
                _dbSession.R_UserInfo_RoleRepository.DeleteEntities(listRoles[i]);
            }
            //真正的删除了所有的数据
            _dbSession.SaveChanges();

            //在此重新将数据加载会数据库
            foreach (var roleID in roleIDList)
            {
                R_UserInfo_Role rUserInfoRole = new R_UserInfo_Role();
                rUserInfoRole.RoleID = roleID;
                rUserInfoRole.UserInfoID = userID;
                rUserInfoRole.SubTime = DateTime.Now;
                _dbSession.R_UserInfo_RoleRepository.AddEntities(rUserInfoRole);
            }
            //实现添加功能
            _dbSession.SaveChanges();
            return true;
        }

        public bool SetActionInfoRole(int userID, List<int> ActionIDS)
        {
            //根据ID,查询出用户的所有的信息
            var currentUser = _dbSession.UserInfoRepository.LoadEntities(c => c.ID == userID).FirstOrDefault();
            if (currentUser == null)
            {
                return false;
            }
            //得到权限表中的所有数据返回
            var actionList = currentUser.R_UserInfo_ActionInfo.ToList();
            //循环遍历删除所有的用户的权限信息
            for (int i = 0; i < actionList.Count; i++)
            {
                _dbSession.R_UserInfo_ActionInfoRepository.DeleteEntities(actionList[i]);
            }
            _dbSession.SaveChanges();
            //将所有的新的数据在此的加入到表中
            foreach (var actionID in ActionIDS)
            {
                R_UserInfo_ActionInfo rUserInfoActionInfo = new R_UserInfo_ActionInfo();
                rUserInfoActionInfo.UserInfoID = userID;
                rUserInfoActionInfo.ActionInfoID = actionID;
                rUserInfoActionInfo.HasPermation = true;
                _dbSession.R_UserInfo_ActionInfoRepository.AddEntities(rUserInfoActionInfo);
            }
            _dbSession.SaveChanges();
            return true;
        }

        /// <summary>
        /// 添加用户特殊权限的设置
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ListActionIDs"></param>
        /// <returns></returns>
        public bool SetAddActionInfoRole(int userID, List<int> ListActionIDs)
        {
            //首先根据用户ID查询到用户的信息
            var currentUser = _dbSession.UserInfoRepository.LoadEntities(c => c.ID == userID).FirstOrDefault();
            if (currentUser == null)
            {
                return false;
            }
            //根据用户信息得到权限表的信息显示出来
            var actionInfo = currentUser.R_UserInfo_ActionInfo.ToList();

            //循环遍历删除所有的信息
            for (int i = 0; i < actionInfo.Count; i++)
            {
                _dbSession.R_UserInfo_ActionInfoRepository.DeleteEntities(actionInfo[i]);
            }
            _dbSession.SaveChanges();
            //然后将选择的数据在添加到信息中
            foreach (var actionID in ListActionIDs)
            {
                R_UserInfo_ActionInfo userActionInfo = new R_UserInfo_ActionInfo();
                userActionInfo.UserInfoID = userID;
                userActionInfo.ActionInfoID = actionID;
                userActionInfo.HasPermation = true;
                _dbSession.R_UserInfo_ActionInfoRepository.AddEntities(userActionInfo);
            }
            _dbSession.SaveChanges();
            return true;
        }

        /// <summary>
        /// 加载所有的菜单数据，显示在表单上面
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public IQueryable<MenuData> LoadMenuData(int UserID)
        {
            //先获取到UserID的用户信息
            var CurrentUser = _dbSession.UserInfoRepository.LoadEntities(c => c.ID == UserID).FirstOrDefault();
            //判断是否为空
            if (CurrentUser == null)
            {
                return null;
            }
            //根据用户拿到对应的角色
            var userRoleList = from r in CurrentUser.R_UserInfo_Role
                               select r.Role;
            //根据角色对应的分组
            var groups = from n in userRoleList
                         from g in n.ActionGroup
                         select g;

            //获取选中的是菜单项的选择
            short actionTypeMenu = (short)ActionTypeEnum.MenuItem;

            //实现过滤重复的数据，引用不同
            //默认的就是引用类型，对比的时候用的是引用类型，如果我们不想使用引用地址，而人为指定表的属性，那么可以自己写一个比较起，重写Equals和GethashCode方法就行了
            groups.Distinct(new EntityCompare());

            //把所有的信息封装MenuData数据传递给控制器，Json格式
            var menuData = from g in groups
                           select new MenuData()
                           {
                               GroupID = g.ID,
                               GroupName = g.GroupName,
                               MenuItems = (from a in g.ActionInfo
                                            where a.ActionType == actionTypeMenu
                                            select new MenuItem
                                            {
                                                Id = a.ID,
                                                MenuName = a.ActionName,
                                                Url = a.RequestUrl
                                            })
                           };
            return menuData.AsQueryable();
        }
    }
}