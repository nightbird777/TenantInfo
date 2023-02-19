using Dapper;
using System.Data.SqlClient;

namespace TenantInfo.Models
{
    public class TenantDB
    {
        private string connectionString = ("server=KHALIFABUILD202; database=NanoBoutique; user id=raju; password=raju123");
        //private string connectionString = ("server=nyctotampa; database=NanoBoutique; user id=raju; password=raju123");
        public List<Tenant> viewPage()
        {
            List<Tenant> tenants = new List<Tenant>();
            string sql = "select * from tenantNew";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                tenants = conn.Query<Tenant>(sql).ToList();
            }
            return tenants;
        }

        public Tenant getTenantById(int id)
        {
            Tenant tenant = new Tenant();
            string sql = "select * from tenantNew where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                tenant = conn.QueryFirst<Tenant>(sql, new { id });
            }
            return tenant;
        }

        public void saveEditTenant(Tenant tenant)
        {
            string sql = "update tenantNew set FirstName = '" + tenant.FirstName + "', " +
            "LastName = '" + tenant.LastName + "', UnitNo = '" + tenant.UnitNo + "' where Id = " + tenant.Id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, tenant);
            }
        }

        public void removeTenant(int id)
        {
            Tenant tenant = new Tenant();
            string sql = "delete from tenantNew where Id = " + id;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, new { id });
            }
        }

        public void newTenant(Tenant tenant)
        {
            string sql = "insert into tenantNew (FirstName, LastName, UnitNo) " +
                "values ('" + tenant.FirstName + "', '" + tenant.LastName + "', '" + tenant.UnitNo + "')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(sql, tenant);
            }
        }
    }
}
