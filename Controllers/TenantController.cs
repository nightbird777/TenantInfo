using Microsoft.AspNetCore.Mvc;
using System.Text;
using TenantInfo.Models;

namespace TenantInfo.Controllers
{
    public class TenantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult homePage()
        {
            TenantDB tenantDB = new TenantDB();
            List<Tenant> tenants = tenantDB.viewPage();
            ViewBag.Tenant = tenants;
            return View();
        }

        public IActionResult editTenant(int id)
        {
            TenantDB tenantDB = new TenantDB();
            Tenant tenant = tenantDB.getTenantById(id);
            ViewBag.Tenant = tenant;
            return View();
        }

        public IActionResult editTenantSave(Tenant tenant)
        {
            TenantDB tenantDB = new TenantDB();
            tenantDB.saveEditTenant(tenant);
            List<Tenant> tenants = tenantDB.viewPage();
            return RedirectToAction("homePage");
        }

        public IActionResult DeleteTenant(int id)
        {
            TenantDB tenantDB = new TenantDB();
            tenantDB.removeTenant(id);
            List<Tenant> tenants = tenantDB.viewPage();
            return RedirectToAction("homePage");
        }

        public IActionResult CreateNew()
        {
            return View();
        }

        public IActionResult saveNewTenant(Tenant tenant)
        {
            TenantDB tenantDB = new TenantDB();
            tenantDB.newTenant(tenant);
            List<Tenant> tenants = tenantDB.viewPage();
            return RedirectToAction("CreateNew");
        }

        public IActionResult ExportAsCSV()
        {
            TenantDB tenantDB = new TenantDB();
            List<Tenant> tenants = tenantDB.viewPage();

            var builder = new StringBuilder();
            builder.AppendLine("Type, Color, Price");

            foreach (var tenant in tenants)
            {
                builder.AppendLine($"{tenant.FirstName}, {tenant.LastName}, {tenant.UnitNo}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Tenant.csv");
        }

        public IActionResult Print()
        {
            TenantDB tenantDB = new TenantDB();
            List<Tenant> tenants = tenantDB.viewPage();
            ViewBag.Tenant = tenants;
            return View();
        }
    }
}
