using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LYZJ.HM3Shop.DAL;
using LYZJ.HM3Shop.Model;

namespace LYZJ.HM3Shop.UnitTest
{
    [TestClass]
    public class UserInfoRepositoryTest
    {
        //添加的测试
        [TestMethod]
        public void ShowTest()
        {
            //TODO:初始化为适当的值
            UserInfoRepository target = new UserInfoRepository();
            UserInfo userinfo = new UserInfo();
            userinfo.Pwd = "citsoft";
            userinfo.UName = "hyl";
            var addUserInfo = target.AddEntities(userinfo);
            Assert.AreEqual(true, addUserInfo.ID > 0);
        }

        //修改的测试
        [TestMethod]
        public void UpdateTest()
        {
            UserInfoRepository updateuserInfo = new UserInfoRepository();
            UserInfo userinfo = new UserInfo();
            userinfo.ID = 23;
            userinfo.UName = "韩迎龙";
            userinfo.Pwd = "shit";
            bool isTure = updateuserInfo.UpdateEntities(userinfo);
            Assert.AreEqual(true, isTure);
        }

        //删除的测试
        [TestMethod]
        public void DeleteTest()
        {
            UserInfoRepository deleteuserinfo = new UserInfoRepository();
            UserInfo userinfo = new UserInfo();
            userinfo.ID = 23;
            bool isTrue = deleteuserinfo.DeleteEntities(userinfo);
            Assert.AreEqual(true, isTrue);
        }
    }
}
