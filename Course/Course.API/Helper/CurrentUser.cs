namespace Course.API.Helper
{
    public static class CurrentUser
    {
        public static string Get(HttpContext httpContext)
        {
			try
			{
				var ccl = httpContext.User.Claims.Where(k=>k.Type.Contains
				("nameidentifier")).FirstOrDefault ();
				if(ccl !=null)
				{
					return ccl.Value;
				}
				else
				{
					return null;
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }
    }
}
