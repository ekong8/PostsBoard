using PostsBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsBoard.ViewModel
{
    public class FilterViewModel
    {
        public Post post { get; set; }
        public IEnumerable<PostCategory> Categories { get; set; }
    }
}
