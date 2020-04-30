using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Winner.Framework.Utils;
using Winner.OAuth.Entities;

namespace Winner.OAuth.Facade.Caches
{
    public class OAuthAppCache
    {
        private const string CacheKey = "tauth_app";
        public static OAuthApp Get(int id, bool infiltrate = true)
        {
            var applist = GetAll();
            var app = applist?.Find(a => a.Id == id);
            if (app == null && infiltrate)
            {
                return LoadForce(Get, id);
            }
            return app;
        }
        public static OAuthApp Get(string appid, bool infiltrate = true)
        {
            var applist = GetAll();
            var app = applist?.Find(a => a.Appid == appid);
            if (app == null && infiltrate)
            {
                return LoadForce(Get, appid);
            }
            return app;
        }
        private static OAuthApp LoadForce<T>(Func<T, bool, OAuthApp> func, T arg)
        {
            GetAll(true);
            return func(arg, false);
        }
        public static List<OAuthApp> GetAll(bool refresh = false)
        {
            var appList = CacheHelper.Default.Get<List<OAuthApp>>(CacheKey);
            if (appList == null || refresh)
            {
                var dao = DaoFactory.Tauth_AppCollection();
                dao.ListAll();
                appList = MapProvider.Map<OAuthApp>(dao.DataTable);
                if (appList != null && appList.Count > 0)
                {
                    CacheHelper.Default.Add(CacheKey, appList);
                }
            }
            return appList;
        }
    }
}
