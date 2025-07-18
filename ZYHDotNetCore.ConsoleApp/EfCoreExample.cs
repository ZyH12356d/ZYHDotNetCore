using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYHDotNetCore.ConsoleApp.Model;

namespace ZYHDotNetCore.ConsoleApp
{

    public class EfCoreExample
    {

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

        public void Create(string Title , string Author , string Content)
        {
            BlogModel blogModel = new BlogModel();
            blogModel.Title = Title;
            blogModel.Author = Author;
            blogModel.Content_data = Content;

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blogModel);
            var result = db.SaveChanges();
            Console.WriteLine(result is 1 ? "Blog Create Success" : "Blog Create Fail" );
        }

        public void GetBlogById(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where(x=> x.Id == id && x.Delete_flag ==0).FirstOrDefault();
            if (item is not null)
            {
                Console.WriteLine("Blog Title :" + item.Title);
                Console.WriteLine("Blog Author :" + item.Author);
                Console.WriteLine("Blog Content :" + item.Content_data);
                Console.WriteLine("----------------------------");
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("There is no data found with Blog id " + id);
            }
        }

        public void Update(int Id , string Title, string Author, string Content)
        {
            BlogModel blogModel=new BlogModel();
            blogModel.Id = Id;
            blogModel.Title=Title;
            blogModel.Author=Author;
            blogModel.Content_data = Content;
            AppDbContext db = new AppDbContext();
            db.Update(blogModel);
            var result = db.SaveChanges();
            Console.WriteLine(result is 1 ? "Blog Update Success" : "Blog Update Fail");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where(x => x.Id == id && x.Delete_flag == 0).FirstOrDefault();
            if (item is not null)
            {
                item.Delete_flag = 1;
                db.Update(item);
                var result = db.SaveChanges();
                Console.WriteLine(result is 1 ? "Blog Delete Success" : "Blog Delete Fail");
            }
            else
            {
                Console.WriteLine("There is no data found with Blog id " + id);
            }
        }
    }
}
