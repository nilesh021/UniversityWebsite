using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityWebsite.Models
{
    public class FeedViewModel
    {
        public int Idea_Id { get; set; }
        public Idea Idea { get; set; }

        public Club Club { get; set; }

        public User User { get; set; }

        public IdeaCategory IdeaCategory { get; set; }
    }
}