using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostsBoard.Models
{
    public class PostComment 
    {   
        [Key]
        public int CommentID { get; set; } 

        [Required]
        public string Content { get; set; } 
        public int Likes { get; set; }
        public DateTime CommentedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }

        [ForeignKey("PostID")]
        [JsonIgnore]
        public virtual Post Post { get; set; }
        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual User User { get; set; }//need to be check for relationship with User model
    }
}
