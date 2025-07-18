using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYHDotNetCore.ConsoleApp.Model
{
    [Table("Tbl_Blog")]
    public class BlogModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Author")]
        public string Author { get; set; }
        [Column("Content_data")]
        public string Content_data { get; set; }
        [Column("Delete_flag")]
        public byte Delete_flag { get; set; }
    }
}
