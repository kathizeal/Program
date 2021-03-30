using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program.Data;
using Program.Model;

namespace Program
{
    class Program
    {
        static int count = 0;
        public static void DisplayPost(Post post)
        {
            TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(post.CreatedTime, localZoneId);
            Console.WriteLine(count + ". Post Title" + post.PostTitle + "\n   Post Content" + post.PostContent + "\n   Created By: " + post.PostCreatedByUserName +
                ", and posted time " + localTime.ToString("dddd,dd-MMMM-yyyy") + "\n");
            count++;
            Console.WriteLine("   post likes : " + post.Likes);
            UserManager userManager = UserManager.GetInstance();
            foreach (long userid in post.LikedId)
            {
                Console.WriteLine("   liked by :" + userManager.FindUser(userid) + "\n");
            }
            int commentcount = 0;
            foreach (var comment in post.Comments)
            {
                if (comment.ParentCommentId == null)
                {
                    DateTime localTimeComment = TimeZoneInfo.ConvertTimeFromUtc(comment.CreatedTime, localZoneId);
                    Console.WriteLine("     " + commentcount + ". comment: " + comment.CommentContent + " commented by: " + comment.CommenterName + ", and commented time "
                        + localTime.ToString("dddd,dd-MMMM-yyyy") + "\n");
                    commentcount++;
                    int replyCount = 0;
                    foreach (var reply in post.Comments)
                    {
                        if (reply.ParentCommentId == comment.CommentId)
                        {
                            DateTime localTimeReply = TimeZoneInfo.ConvertTimeFromUtc(reply.CreatedTime, localZoneId);
                            Console.WriteLine("         " + replyCount + ". reply: " + reply.CommentContent + " replayed by: " + reply.CommenterName + ", and replayed time "
                            + localTime.ToString("dddd,dd-MMMM-yyyy") + "\n");
                            replyCount++;
                        }
                    }
                }

            }

        }
        public static void Main(string[] args)
        {
            UserManager userManager = UserManager.GetInstance();
            userManager.AddUser("user1", "1234");
            userManager.AddUser("user2", "2345");
            PostManager postManager = PostManager.GetInstance();
            //postManager.AddPost(new Post("User1 Post", "This is my First Post", "user1",12345689));
            //postManager.AddPost(new Post("User1 Post", "This is my Second Post", "user1", 12345689));
            //postManager.AddPost(new Post("User2 Post", "This is my first Post", "user2", 234567890));
            while (true)
            {
                Console.Write("Username : ");
                string inputUser = Console.ReadLine();
                Console.Write("Password : ");
                string inputPassword = Console.ReadLine();
                if (userManager.LoginUser(inputUser, inputPassword))
                {
                    Console.WriteLine("User: " + inputUser + " has login.");                 

                }
                while (userManager.CurrentUser != null)
                {
                    Console.WriteLine("\n\nPress 1 to view all post\nPress 2 to view my post\nPress 3 to Create a post\nPress 4 To Add Comment\nPress 5 to add reply\nPress 6 to like a post\nPress 7 to remove a post\nPress 8 to logout\n");
                    Console.WriteLine("\nChoose your option ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    List<Post> _posts;
                    Post post;
                    int innerchoice = 0;
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("All Post");
                            _posts = postManager.ViewAllPost();
                            if (_posts.Count != 0)
                            {
                                count = 0;
                                _posts.ForEach(singlepost => DisplayPost(singlepost));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("NO post found");
                                break;
                            }
                        case 2:
                            Console.WriteLine("Users Post");
                            _posts = postManager.ViewMyPost(userManager.CurrentUser.UserId);
                            if (_posts.Count != 0)
                            {
                                count = 0;
                                _posts.ForEach(singlepost => DisplayPost(singlepost));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("NO post found from {0}", userManager.CurrentUser.UserName);
                                break;
                            }
                        case 3:
                            Console.Write("Enter Title : ");
                            string title = Console.ReadLine();
                            Console.Write("Enter Content : ");
                            string content = Console.ReadLine();
                            postManager.CreatePost(new Post(title, content, userManager.CurrentUser.UserName, userManager.CurrentUser.UserId));
                            break;
                        case 4:
                            _posts = postManager.ViewAllPost();
                            count = 0;
                            if (_posts.Count != 0)
                            {
                                _posts.ForEach(singlepost => DisplayPost(singlepost));
                                Console.Write("choose which post you want comment : ");
                                innerchoice = Convert.ToInt32(Console.ReadLine());
                                post = _posts.ElementAt(innerchoice);
                                long postid = post.PostId;
                                Console.Write("Enter your Comment : ");
                                string Commentcontent = Console.ReadLine();
                                postManager.AddComment(post, new Comment(postid, Commentcontent, userManager.CurrentUser.UserName, userManager.CurrentUser.UserId, null));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("No post to comment");
                                break;
                            }
                        case 5:
                            _posts = postManager.ViewAllPost();
                            count = 0;
                            _posts.ForEach(singlepost => DisplayPost(singlepost));
                            if (_posts.Count == 0)
                            {
                                Console.WriteLine("No post is available");
                                break;
                            }
                            count = 0;
                            Console.Write("Choose which post you want comment : ");
                            innerchoice = Convert.ToInt32(Console.ReadLine());
                            post = _posts.ElementAt(innerchoice);
                            DisplayPost(post);
                            if (post.Comments.Count != 0)
                            {
                                Console.Write("which comment you want reply : ");
                                int choiceOfComment = Convert.ToInt32(Console.ReadLine());
                                Comment comment = postManager.ViewPostComment(post, post.PostId).ElementAt(choiceOfComment);
                                Console.Write("Enter your reply : ");
                                string replyComment = Console.ReadLine();                                
                                postManager.AddReply(post, comment.CommentId, new Comment(post.PostId, replyComment, userManager.CurrentUser.UserName, userManager.CurrentUser.UserId, comment.CommentId));
                                break;
                            }
                            else
                            {
                                Console.WriteLine("No comment to reply ");
                                break;
                            }
                        case 6:
                            _posts = postManager.ViewAllPost();
                            count = 0;
                            _posts.ForEach(singlepost => DisplayPost(singlepost));
                            if (_posts.Count != 0)
                            {
                                count = 0;
                                Console.Write("Choose which post you want like : ");
                                innerchoice = Convert.ToInt32(Console.ReadLine());
                                post = _posts.ElementAt(innerchoice);
                                postManager.LikePost(post, userManager.CurrentUser);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("no post is there to like");
                                break;
                            }
                        case 7:
                            _posts = postManager.ViewMyPost(userManager.CurrentUser.UserId);
                            if (_posts.Count == 0)
                            {
                                Console.WriteLine("no post is available from user");
                                break;
                            }
                            _posts.ForEach(singlepost => DisplayPost(singlepost));
                            Console.Write("which post you want  remove : ");
                            innerchoice = Convert.ToInt32(Console.ReadLine());
                            postManager.DeletePost(_posts.ElementAt(innerchoice));
                            Console.WriteLine("Post is removed ");
                            break;
                        case 8:
                            userManager.Logout();
                            Console.WriteLine("User: " + inputUser + " has logout.");
                            break;
                    }
                }
            }
        }
    }
}
