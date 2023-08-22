namespace Course.API.Controllers
{
    internal class SigningCredentials
    {
        private SymmetricSecurityKey symmetricSecurityKey;
        private object hmacSha256;

        public SigningCredentials(SymmetricSecurityKey symmetricSecurityKey, object hmacSha256)
        {
            this.symmetricSecurityKey = symmetricSecurityKey;
            this.hmacSha256 = hmacSha256;
        }
    }
}