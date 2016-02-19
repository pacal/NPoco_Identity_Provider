using System;
using System.Collections.Generic;
using System.Linq;
using NPoco;
using NPoco.Linq;

namespace Pacal.NPoco_Idenity_Provider
{
    public class DataProvider : IDisposable
    {
        private IDatabase _connection;

        public delegate List<TResult> QueryMethod<TResult, in T1, in T2>(T1 sql, T2 args);
        public delegate int ActionMethod<in T1, in T2>(T1 sql, T2 args);

        public DataProvider(string connectionStringName, DatabaseType dbType)
        {
            _connection = new Database(connectionStringName, dbType);
        }
        
        public TPoco GetPocobyId<TPoco>(string id)
        {
            TPoco ret;
            using (_connection)
            {
                ret = _connection.SingleOrDefaultById<TPoco>(id);
            }

            return ret;
        }

        public TPoco GetPocoWhereSingle<TPoco>(Dictionary<string, string> Props)
        {
            TPoco ret = default(TPoco);            
            var pocos = GetPocoWhere<TPoco>(Props);
            if (pocos.Count > 1)
            {
                throw new InvalidOperationException("The input sequence contains more than one element.");
            }
            else
            {
                ret = pocos.FirstOrDefault();
            }
            return ret;
        }

        public List<TPoco> GetPocoWhere<TPoco>(Dictionary<string, string> Props)
        {
            List<TPoco> ret = null;
            var pocos = QueryPocoWhere<TPoco>(Props, _connection.Fetch<TPoco>);
            ret = pocos.ToList();            
            return ret;
        }

        private List<TPoco> QueryPocoWhere<TPoco>(Dictionary<string, string> Props, QueryMethod<TPoco, string, object[]> qry)
        {
            List<TPoco> ret;
            object[] p;
            var sql = SQLBuilder(Props, out p);

            using (_connection)
            {
                //ret = _connection.Fetch<TPoco>(sql, p);
                ret = qry(sql, p);
            }
         

            return ret;
        }

        public int DeletePocoWhere<TPoco>(Dictionary<string, string> Props)
        {
            return ActionPocoWhere<TPoco>(Props, _connection.Delete<TPoco>);
        }

        private int ActionPocoWhere<TPoco>(Dictionary<string, string> Props, ActionMethod<string, object[]> qry )
        {
            int ret;
            object[] p;
            var sql = SQLBuilder(Props, out p);

            using (_connection)
            {
           
                ret = qry(sql, p);
            }


            return ret;
        }

        private static string SQLBuilder(Dictionary<string, string> Props, out object[] p)
        {
            string sql = string.Empty;

            int cnt = 0;
            p = new object[Props.Count];

            foreach (var prop in Props)
            {
                if (cnt == 0)
                {
                    sql = string.Format("WHERE {0} = @{1}", prop.Key, cnt);
                }
                else
                {
                    sql += string.Format(" AND {0} = @{1}", prop.Key, cnt);
                }

                p[cnt] = prop.Value;
                cnt++;
            }
            return sql;
        }       

        public bool Delete<TPoco>(string id)
        {
            bool ret = false;
            using (_connection)
            {
               var cnt =  _connection.Delete<TPoco>(id);
                if (cnt > 0)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool Delete<TPoco>(TPoco poco)
        {

            bool ret = false;
            using (_connection)
            {
                var cnt = _connection.Delete<TPoco>(poco);
                if (cnt > 0)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool Update<TPoco>(TPoco poco)
        {
            bool ret = false;
            using (_connection)
            {
                var cnt = _connection.Update(poco);
                if (cnt > 0)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public bool Update<TPoco>(TPoco poco, Snapshot<TPoco> snapshot)
        {
            bool ret = false;
            using (_connection)
            {
                var cnt = _connection.Update(poco, snapshot.UpdatedColumns());
                if (cnt > 0)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public TVal Insert<TVal, TPoco>(TPoco poco)
        {
            TVal ret;
            using (_connection)
            {
                ret =  (TVal) _connection.Insert(poco);               
            }

            return ret;
        }
 
        public List<Dictionary<string, object>> ExecutRawFetch(string sql, params object[] args)
        {
            var ret = new List<Dictionary<string, object>>();
            using (_connection)
            {
                 ret = _connection.Fetch<Dictionary<string, object>>(sql, args);
            }

            return ret;
        }

        public int ExecuteRawScalar(string sql, params object[] args)
        {
            int ret;
            using (_connection)
            {
                ret = _connection.Execute(sql, args);
            }

            return ret;
        }

        public IQueryProviderWithIncludes<TPoco> GetNPocoIqProviderWithIncludes<TPoco>()
        { 
                using (_connection)
                {
                    var iq = _connection.Query<TPoco>();
                    return iq;
                }           
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.CloseSharedConnection();
                _connection.Dispose();
                _connection = null;
            }
            
        }
    }
}
