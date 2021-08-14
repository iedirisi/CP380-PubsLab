using System.Linq;
using System;

namespace CP380_PubsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbcontext = new Models.PubsDbContext())
            {
                if (dbcontext.Database.CanConnect())
                {
                    Console.WriteLine("Yes, I can connect");
                }

                // 1:Many practice
                //
                // TODO: - Loop through each employee
                //       - For each employee, list their job description (job_desc, in the jobs table)

                // TODO: - Loop through all of the jobs
                //       - For each job, list the employees (first name, last name) that have that job
                               
                var employees = dbcontext.Employee.ToList();
                var jobs = dbcontext.Jobs.ToList();

                Console.WriteLine("Employee List");
                foreach (var employee in employees)
                {
                    Console.WriteLine("\t> " + employee.fname + " " + employee.lname + " (" + dbcontext.Jobs.First(j => j.job_id == employee.job_id).job_desc + ")");
                }

                Console.WriteLine("\n\nJob List");
                foreach (var job in jobs)
                {
                    Console.WriteLine("\t" + job.job_desc);
                    var tempEmployees = dbcontext.Employee.Where(e => e.job_id == job.job_id).ToList();
                    foreach (var employee in tempEmployees)
                    {
                        Console.WriteLine("\t\t" + employee.fname + " " + employee.lname);
                    }
                }

                // Many:many practice
                //
                // TODO: - Loop through each Store
                //       - For each store, list all the titles sold at that store
                //
                // e.g.
                //  Bookbeat -> The Gourmet Microwave, The Busy Executive's Database Guide, Cooking with Computers: Surreptitious Balance Sheets, But Is It User Friendly?
                
                // TODO: - Loop through each Title
                //       - For each title, list all the stores it was sold at
                //
                // e.g.
                //  The Gourmet Microwave -> Doc-U-Mat: Quality Laundry and Books, Bookbeat
                           var stores = dbcontext.Stores.ToList();
                var titles = dbcontext.Titles.ToList();
                var sales = dbcontext.Sales.ToList();

                Console.WriteLine("\n\nStores");
                foreach (var store in stores)
                {
                    Console.Write("\t" + store.stor_name + " => ");
                    var tempSales = sales.Where(s => s.stor_id == store.stor_id).ToList();
                    foreach(var sale in tempSales)
                    {
                        Console.Write(titles.First(t => t.title_id == sale.title_id).title + ", ");
                    }
                    Console.WriteLine("\n");
                }

                Console.WriteLine("\n\nBooks");
                foreach (var title in titles)
                {
                    Console.Write("\t" + title.title + " => ");
                    var tempSales = sales.Where(s => s.title_id == title.title_id).ToList();
                    foreach (var sale in tempSales)
                    {
                        Console.Write(stores.First(t => t.stor_id == sale.stor_id).stor_name + ", ");
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}
