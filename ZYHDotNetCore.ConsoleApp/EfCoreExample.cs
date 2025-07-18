using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYHDotNetCore.ConsoleApp
{

    public class EfCoreExample
    {
        //public AppDbContext _context;

        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var list = db.Blogs.Where(x=> x.Delete_flag == 0).ToList();
            foreach (var item in list)
            {
                Console.WriteLine("Blog Title :" + item.Title);
                Console.WriteLine("Blog Author :" + item.Author);
                Console.WriteLine("Blog Content :" + item.Content_data);
                Console.WriteLine("----------------------------");
                Console.WriteLine("");
                
            }

        }
    }
}
