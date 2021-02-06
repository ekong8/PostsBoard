using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostsBoard.Models
{
    public class PostTagAssignment
    {
        public int TagID { set; get; }
        public int PostID { set; get; }

        [ForeignKey("TagID")]
        public virtual PostTag Tag { get; set; }
        [ForeignKey("PostID")]
        public virtual Post Post { get; set; }
    }
}
