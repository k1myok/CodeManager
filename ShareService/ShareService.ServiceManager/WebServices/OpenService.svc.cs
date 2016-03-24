using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ShareService.Service.ARR;
using ShareService.ServiceManager.DAL;

namespace ShareService.ServiceManager.WebServices
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“OpenService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 OpenService.svc 或 OpenService.svc.cs，然后开始调试。
    public class OpenService : IOpenService
    {
        private ShareServiceContext context = new ShareServiceContext();
        public bool Login(string userName, string passwords)
        {
            // 在此处添加操作实现
            return true;
        }

        public string[] GetServices(string token)
        {
            var tokenModel = context.ServiceToken.Find(Guid.Parse(token));
            if (tokenModel == null || tokenModel.IsPaused || tokenModel.ExpiredDate < DateTime.Now)
                return null;
            var userCode = tokenModel.UserCode;

            var services = new List<string>();
            if (tokenModel.SingleService)
            {
                services = new List<string> { context.Service.Find(tokenModel.ServiceCode).URL };
            }
            else
            {
                //获取用户具有访问权限的服务列表
                var servicesOfUser = from a in context.Service
                                     join b in context.UFServicesOfUser.Where(p => p.UserCode == userCode)
                                     on a.Code equals b.ServiceCode
                                     select a.URL;

                //获取用户具有的所有角色
                var roleCodesOfUser = context.UFUserInRole.Where(p => p.UserCode == userCode);
               
                //获取用户角色具有访问权限的所有服务编号列表
                var serviceCodesOfRole = from a in context.UFServicesOfRole
                                     join b in roleCodesOfUser
                                     on a.RoleCode equals b.RoleCode
                                     select a;
                //获取角色具有访问权限的所有服务资源
                var servicesOfRole = from a in context.Service
                                     join b in serviceCodesOfRole
                                     on a.Code equals b.ServiceCode
                                     select a.URL;

                //获取用户所在的组列表
                var groupCodesOfUser = context.UFUserInGroup.Where(p => p.UserCode == userCode);

                //获取用户所在组具备的所有角色
                var rolesCodesOfGroup = from a in context.UFGroupInRole
                                        join b in groupCodesOfUser
                                        on a.GroupCode equals b.GroupCode
                                        select a;

                //获取用户组具有访问权限的所有服务编号
                var serviceCodesOfGroup = from a in context.UFServicesOfRole
                                         join b in rolesCodesOfGroup
                                         on a.RoleCode equals b.RoleCode
                                         select a;

                //获取所有组具有访问权限的所有服务资源
                var servicesOfGroup = from a in context.Service
                                     join b in serviceCodesOfGroup
                                     on a.Code equals b.ServiceCode
                                     select a.URL;

                //将用户直接具有访问权限的服务资源、用户具有的角色所包含的服务资源、用户所在组具备访问权限的服务资源进行合并，即为用户具有访问权限的所有服务资源
                services = servicesOfUser.Union(servicesOfRole).Union(servicesOfGroup).ToList();
            }

            var host = ConfigurationManager.AppSettings["Host"];
            var port = ConfigurationManager.AppSettings["Port"];
            var result = services.Select(p => {
                var url = new Uri(p);
                var proxyURL = string.Format("http://{0}:{1}{2}", host, port, url.PathAndQuery);
                return proxyURL;
            });
            return result.ToArray();
        }
    }
}
