
using LYZJ.HM3Shop.DAL;
using LYZJ.HM3Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.BLL
{
    public abstract class BaseService<T> where T : class,new()
    {
        //在调用这个方法的时候必须给他赋值
        public IDAL.IBaseRepository<T> CurrentRepository { get; set; }

        //为了职责单一的原则，将获取线程内唯一实例的DbSession的逻辑放到工厂里面去了
        //public IDAL.IDbSession _DbSession = DbSessionFactory.GetCurrentDbSession();

        public IDbSession _dbSession = DbSessionFactory.GetCurrentDbSession();

        //基类的构造函数
        public BaseService()
        {
            SetCurrentRepository();  //构造函数里面调用了此设置当前仓储的抽象方法
        }

        //构造方法实现赋值 
        public abstract void SetCurrentRepository();  //约束子类必须实现这个抽象方法

        //添加
        public T AddEntities(T entity)
        {
            //如果在这里操作多个表的话，实现批量的操作
            //调用T对应的仓储来添加
            var addentity=CurrentRepository.AddEntities(entity);

            _dbSession.SaveChanges();
            return addentity;
        }

        //修改
        public bool UpdateEntities(T entity)
        {
            var updateEntity=  CurrentRepository.UpdateEntities(entity);
            _dbSession.SaveChanges();
            return updateEntity;
        }


        //修改
        public bool DeleteEntities(T entity)
        {
            var deleteEntity= CurrentRepository.DeleteEntities(entity);
            _dbSession.SaveChanges();
            return deleteEntity;
        }


        //查询
        public IQueryable<T> LoadEntities(Func<T, bool> wherelambda)
        {
            return CurrentRepository.LoadEntities(wherelambda);
        }


        //分页
        public IQueryable<T> LoadPagerEntities<S>(int pageSize, int pageIndex,
             out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            return CurrentRepository.LoadPagerEntities(pageSize, pageIndex, out total, whereLambda, isAsc, orderByLambda);
        }


    }
}
