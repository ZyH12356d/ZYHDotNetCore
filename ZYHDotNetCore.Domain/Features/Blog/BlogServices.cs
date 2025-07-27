using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYHDotNetCore.Database.AppDbContextModels;

namespace ZYHDotNetCore.Domain.Features.Blog
{
    public class BlogServices
    {
        private readonly AppDbContext _dbContext = new AppDbContext();
        public List<TblBlog> GetBlog()
        {
            var lst = _dbContext.TblBlogs.ToList();
            return lst;
        }
        public TblBlog GetBlogById(int id)
        {
            var item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            return item;
        }
        public TblBlog CreateBlog(TblBlog blog)
        {
            _dbContext.TblBlogs.Add(blog);
            var result = _dbContext.SaveChanges();
            if (result > 0)
            {
                return blog;
            }
            else
            {
                throw new Exception("Failed to add blog.");
            }
        }
        public TblBlog EditBlog(int id, TblBlog blog)
        {
            var item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            if (item is null)
            {
                throw new Exception("Blog not found or already deleted.");
            }
            item.Title = blog.Title;
            item.Author = blog.Author;
            item.ContentData = blog.ContentData;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return item;
        }
        public bool DeleteBlog(int id)
        {
            var item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(x => x.Id == id && x.DeleteFlag == 0);
            if (item is null)
            {
                return false;
            }
            item.DeleteFlag = 1; // Mark as deleted
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
