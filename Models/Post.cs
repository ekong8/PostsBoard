using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using System.Threading.Tasks;

namespace PostsBoard.Models
{
    public class Post
    {
        public int PostID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PostedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime LastUpdatedDate { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int CategoryID { get; set; }

        //navigation properties     
        [ForeignKey("UserID")]
        public virtual User User { get; set; } 
        [ForeignKey("CategoryID")]
        public virtual PostCategory Category { get; set; } //reference for CategoryID
        [JsonIgnore]
        public virtual ICollection<PostTagAssignment> TagAssignment { get; set; }
        public virtual ICollection<PostComment> Comments { get; set; }
    }
}
