using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostsBoard.Models
{
    public class PostTag
    {
        [Key]
        public int TagID { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<PostTagAssignment> PostAssignments { get; set; } 
    }
}
