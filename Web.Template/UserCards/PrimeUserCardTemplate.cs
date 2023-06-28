using System.Text;

namespace Web.Template.UserCards
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<a href=\"#\" class=\"card-link\">Send Message</a>");
            sb.Append("<a href=\"#\" class=\"card-link\">Details</a>");

            return sb.ToString();
        }

        protected override string SetPictures()
        {
            return $"<img class=\"card-img-top\" src='{AppUser.PictureUrl}'>";
        }
    }
}
