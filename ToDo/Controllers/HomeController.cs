using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            ToDoView viewModel = new ToDoView();
            string sql = @"select Id,Text,Done from dbo.Tasks";
            using (IDbConnection cnn = new SqlConnection(connString("db1")))
            {
                viewModel.list = cnn.Query<ToDoItem>(sql).ToList();
            }
            return View(viewModel);
        }

        private string connString(string str) => ConfigurationManager.ConnectionStrings[str].ConnectionString;

        public int getId()
        {
            List<int> list = new List<int>();
            string sql = @"select Id from dbo.Tasks order by Id";
            using (IDbConnection cnn = new SqlConnection(connString("db1")))
            {
                list = cnn.Query<int>(sql).ToList();
            }

            for (int x = 0; x < 100; x++) if (!list.Contains(x)) return x;
            return 99;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ToDoItem item)
        {
            if (item.Text != null)
            {
                ViewBag.Title = "Index";
                ToDoView item1 = new ToDoView();
                item1.Id = getId();
                item1.Text = item.Text;
                item1.Done = item.Done;
                string sql = @"insert into dbo.Tasks (Id, Text, Done) values (@Id, @Text, @Done)";
                using (IDbConnection cnn = new SqlConnection(connString("db1")))
                {
                    cnn.Execute(sql, item1);
                }
            }
            else
            {
                string sql = @"DELETE FROM dbo.Tasks WHERE Id="+item.Id;
                using (IDbConnection cnn = new SqlConnection(connString("db1")))
                {
                    cnn.Execute(sql);
                }
            }

            return Index();
        }

        public void Delete(int id)
        {
            using (IDbConnection cnn = new SqlConnection(connString("db1")))
            {
                string sql = @"DELETE FROM dbo.Tasks where Id='" + id + "'";
                cnn.Execute(sql);
            }
        }
    }
}