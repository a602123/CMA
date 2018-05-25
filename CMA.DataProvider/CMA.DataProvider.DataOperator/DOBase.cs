using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CMA.DataProvider.DataOperator
{
    /// <summary>
    /// 对所有表CURD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DOBase<T>
        where T : class
    {
        /// <summary>
        /// 上下文对象
        /// </summary>
        private DbContext _dbContex = null;

        public DOBase(db_cmaEntities dbtity)
        {
            _dbContex = dbtity;
        }


        #region 1.0 增加
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="Model">实体</param>
        public void Add(T Model)
        {
            _dbContex.Set<T>().Add(Model);
        }
        #endregion

        #region 2.0 查询
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="lamWhere">查询lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> lamWhere)
        {
            return _dbContex.Set<T>().Where(lamWhere);
        }
        #endregion

        #region 3.0连表查询
        /// <summary>
        /// 连表查询
        /// </summary>
        ///  <param name="lamWhere">查询lambda表达式</param>
        /// <param name="TableName">连表表名</param>
        /// <returns></returns>
        public IQueryable<T> QueryJion(Expression<Func<T, bool>> lamWhere, string[] TableName)
        {
            if (TableName == null || TableName.Any() == false)
            {
                throw new Exception("连表至少一个");
            }
            DbQuery<T> query = _dbContex.Set<T>();
            foreach (var TbName in TableName)
            {
                query = query.Include(TbName);
            }
            return query.Where(lamWhere);
        }
        #endregion

        #region 4.0SQL查询
        /// <summary>
        /// SQL查询
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="prams">参数</param>
        /// <returns></returns>
        public DbSqlQuery<T> SqlQuery(string sql, params object[] prams)
        {
            return _dbContex.Set<T>().SqlQuery(sql, prams);
        }
        #endregion

        #region 5.0 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="propretyNames">要编辑的属性名称</param>
        public void Edit(T model, string[] propretyNames)
        {
            if (model == null)
            {
                return;

            }
            if (propretyNames.Any() == false)
            {
                return;
            }
            DbEntityEntry entry = _dbContex.Entry(model);
            entry.State = System.Data.EntityState.Unchanged;
            foreach (var item in propretyNames)
            {
                entry.Property(item).IsModified = true;
            }
            _dbContex.Configuration.ValidateOnSaveEnabled = false;//关闭EF 验证合法性

        }
        #endregion

        #region 6.0 删除
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="Model">实体</param>
        /// <param name="IsAdd">判断是否追到EF容器 如果没有就追加</param>
        public void Delete(T Model, bool IsAdd = false)
        {

            if (!IsAdd)//判断是否追到EF容器 如果没有就追加
            {
                _dbContex.Set<T>().Attach(Model);

            }
            _dbContex.Set<T>().Remove(Model);

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">要删除主键</param>
        public void Delete(object id)
        {
            T model = _dbContex.Set<T>().Find(id);
            _dbContex.Set<T>().Remove(model);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        public void Delete(object[] ids)
        {
            int Length = ids.Length;
            foreach (var Id in ids)
            {
                T Model = _dbContex.Set<T>().Find(Id);
                _dbContex.Set<T>().Remove(Model);
            }
        }
        #endregion

        #region 7.0得到实体对象
        /// <summary>
        /// 得到实体对象
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T GetModel(object Id)
        {
            T Model = _dbContex.Set<T>().Find(Id);
            return Model;
        }

        #endregion

        #region 8.0 排序查询
        /// <summary>
        /// 对数据进行升序排列
        /// </summary>
        /// <param name="lamWhere">查询lambda表达式</param>
        /// <param name="Orderlam">排列lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> OrderAsc<Keys>(Expression<Func<T, bool>> lamWhere, Expression<Func<T, Keys>> Orderlam)
        {
            return _dbContex.Set<T>().Where(lamWhere).OrderBy(Orderlam);
        }
        /// <summary>
        /// 对数据进行降序排列
        /// </summary>
        /// <param name="lamWhere">查询lambda表达式</param>
        /// <param name="Orderlam">排列lambda表达式</param>
        /// <returns></returns>
        public IQueryable<T> OrderDesc<Keys>(Expression<Func<T, bool>> lamWhere, Expression<Func<T, Keys>> Orderlam)
        {
            return _dbContex.Set<T>().Where(lamWhere).OrderByDescending(Orderlam);
        }
        #endregion

        public IQueryable<T> ListPage<K>(Expression<Func<T, bool>> lamWhere, Expression<Func<T, K>> Orderlam, out int Count, int PageSize, int PageIndex)
        {
            Count = _dbContex.Set<T>().Where(lamWhere).Count();
            return _dbContex.Set<T>().Where(lamWhere).OrderByDescending(Orderlam).Skip(PageSize * (PageIndex - 1))
                  .Take(PageSize);
        }
        #region 9.0保存到数据库
        public int SaveChang()
        {
            return _dbContex.SaveChanges();
        }

        #endregion
    }
}
