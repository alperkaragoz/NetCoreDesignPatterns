﻿using BaseProject.Models;
using System.Text;

namespace Web.Template.UserCards
{
    public abstract class UserCardTemplate
    {
        protected AppUser AppUser { get; set; }
        public void SetUser(AppUser appUser)
        {
            AppUser = appUser;
        }
        public string Build()
        {
            if (AppUser == null)
                new ArgumentNullException(nameof(AppUser));

            StringBuilder sb = new StringBuilder();
            sb.Append("<div class='card'>");

            sb.Append(SetPictures());
            sb.Append($@"<div class='card-body'>
                        <h5>{AppUser.UserName}</h5>
                        <p>{AppUser.Description}</p>");
            sb.Append(SetFooter());
            sb.Append("</div>");

            sb.Append("</div>");

            return sb.ToString();
        }
        protected abstract string SetFooter();
        protected abstract string SetPictures();
    }
}
