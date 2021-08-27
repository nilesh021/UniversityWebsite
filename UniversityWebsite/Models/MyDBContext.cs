using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UniversityWebsite.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        {

        }
 
        public DbSet<Club> Clubs { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<News> News { get; set;  }

        public DbSet<User> Users { get; set; }

        public DbSet<Club_User_Relation> Club_User_Relations { get; set; }

        public DbSet<Idea> Ideas { get; set; }

        public DbSet<Idea_User_Relation> Idea_User_Relations { get; set; }

        public DbSet<IdeaCategory> IdeaCategories { get; set; }

        public DbSet<Share> Shares { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Grievance> Grievances { get; set; }

        public DbSet<Help> Helps { get; set; }

        public DbSet<Events_User_Relation> Events_User_Relations { get; set; }

        public System.Data.Entity.DbSet<UniversityWebsite.Models.CreateHelpTicketViewModel> CreateHelpTicketViewModels { get; set; }
    }
}