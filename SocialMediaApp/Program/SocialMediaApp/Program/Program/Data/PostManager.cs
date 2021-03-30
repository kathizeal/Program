using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.Model;

namespace Program.Data
{
    public sealed class PostManager
    {
        private List<Post> _Posts = new List<Post>();


        //private List<Comment> _Comments = new List<Comment>();
        //private List<Comment> _Replys = new List<Comment>();
        private static PostManager _Instance = null;
        private PostManager() { }


        public static PostManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new PostManager();

            }
            return _Instance;
        }



        public void AddPost(Post post)
        {
            _Posts.Add(post);

        }
        public void CreatePost(Post post)
        {
            _Posts.Add(post);
        }
        public void DeletePost(Post post)
        {
            _Posts.Remove(post);
        }

        public List<Post> ViewAllPost() { return _Posts; }
        public List<Post> ViewPost(long postid)
        {
            return _Posts.Where(post => post.PostId == postid).ToList();
        }

        public List<Post> ViewMyPost(long userid)
        {
            return _Posts.Where(post => post.PostCreatedByUserId == userid).ToList();
        }
        public void AddComment(Post post, Comment comment)
        {
            //_Comments.Add(comment);
            post.Comments.Add(comment);



        }
        public List<Comment> ViewPostComment(Post post, long postid)
        {
            return post.Comments.Where(comments => comments.PostId == postid).ToList();
            //return _Posts.
            //.Where(comments => comments.PostId==postid).ToList(); }


            /*public void addReply(Comment comment,Comment reply)
            {
                comment.Replys.Add(reply);
            }*/

        }
        public void  AddReply(Post post,Comment comment,Comment reply)
        {

            reply.ParentCommentId = comment.CommenterId;
            post.Replys.Add(reply);
            
        }
        public void LikePost(Post post,User user)
        {
            if (!post.LikedId.Contains(user.UserId))
            {
                ++post.Likes;
                post.LikedId.Add(user.UserId);
            }
        }
    }

}
