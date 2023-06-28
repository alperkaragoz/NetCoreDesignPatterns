namespace Web.Template.UserCards
{
    public class DefaultUserCardTemplate : UserCardTemplate
    {

        protected override string SetFooter()
        {
            return string.Empty;
        }

        protected override string SetPictures()
        {
            return $"<img class=\"card-img-top\" src='/userPictures/anonymoususer.png'>";
        }
    }
}
