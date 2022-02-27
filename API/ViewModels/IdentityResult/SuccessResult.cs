namespace API.ViewModels.IdentityResult
{
    public class SuccessResult:IdentityCustomResult
    {
        public SuccessResult()
        {
            this.IsSuccessed = true;
        }

        public SuccessResult(string token)
        {
            this.Token = token;
        }
    }
}
