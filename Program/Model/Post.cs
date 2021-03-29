using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program.Model
{
    public class Post
    {
        
        public Post(string title, string content, string username,long userid)
        {
            PostTitle = title;
            PostContent = content;
            PostCreatedByUserName = username;
            PostCreatedByUserId = userid;
            PostId = DateTime.Now.Ticks;
            CreatedTime = DateTime.UtcNow;
           

        }

        public string PostCreatedByUserName { get; set; }
        public long PostCreatedByUserId { get; set; }

        public long PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public DateTime CreatedTime { get; set; }
        private List<Comment> _Comments =new List<Comment>();
        public List<Comment> Comments { get { return _Comments; } set { _Comments = value; } }
        private List<Comment> _Replys = new List<Comment>();
        public List<Comment> Replys { get { return _Replys; } set { _Replys = value; } }
        public int Likes { get; set; }
        private List<long> _LikedId = new List<long>();
        public List<long> LikedId { get{ return _LikedId; } set { _LikedId = value; } }



    }
}
