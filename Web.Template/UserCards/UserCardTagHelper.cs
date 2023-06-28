using BaseProject.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.Template.UserCards
{
    public class UserCardTagHelper : TagHelper
    {
        //<user-card app-user= />  örn.
        public AppUser AppUser { get; set; }

        private readonly IHttpContextAccessor _contextAccessor;

        public UserCardTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            UserCardTemplate userCardTemplate;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userCardTemplate = new PrimeUserCardTemplate();

            }
            else
            {
                userCardTemplate = new DefaultUserCardTemplate();
            }
            userCardTemplate.SetUser(AppUser);

            output.Content.SetHtmlContent(userCardTemplate.Build());
        }
    }
}
