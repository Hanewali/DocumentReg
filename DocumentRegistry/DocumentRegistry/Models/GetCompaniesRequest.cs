namespace DocumentRegistry.Models
{
    public class GetCompaniesRequest : RequestModel
    {
        public GetCompaniesRequest(int userId, string authorizationToken) : base(userId, authorizationToken)
        {
            //todo: CheckPermissions(Permission.CompanyRead, userId)
        }
    }
}