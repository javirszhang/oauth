using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;

namespace Winner.OAuth.Facade.Caches
{
    public class ScopeCache
    {
        private const string CacheKey = "tauth_scope";
        public static List<Scope> Get(string[] scopes, bool infiltrate = true)
        {
            var collection = GetAll();
            var tmp = collection?.FindAll(s => scopes.Contains(s.Code));

            if ((tmp == null || tmp.Count <= 0) && infiltrate)
            {
                return LoadForce(Get, scopes);
            }
            return tmp;
        }
        public static Scope Get(string scope, bool infiltrate = true)
        {
            var scopes = GetAll();
            var ins = scopes?.Find(s => s.Code == scope);
            if (ins == null && infiltrate)
            {
                return LoadForce(Get, scope);
            }
            return ins;
        }
        public static Scope Get(int id, bool infiltrate = true)
        {
            var scopes = GetAll();
            var ins = scopes.Find(s => s.Id == id);
            if (ins == null && infiltrate)
            {
                return LoadForce(Get, id);
            }
            return ins;
        }
        private static TResult LoadForce<T, TResult>(Func<T, bool, TResult> func, T arg)
        {
            GetAll(true);
            return func(arg, false);
        }
        public static List<Scope> GetAll(bool refresh = false)
        {
            var collection = CacheHelper.Default.Get<List<Scope>>(CacheKey);
            if (collection == null || refresh)
            {
                var dao = DaoFactory.Tauth_ScopeCollection();
                dao.ListAll();
                collection = MapProvider.Map<Scope>(dao.DataTable);
                if (collection != null && collection.Count > 0)
                {
                    CacheHelper.Default.Add(CacheKey, collection, TimeSpan.FromHours(2));
                }
            }
            return collection;
        }
    }
}
