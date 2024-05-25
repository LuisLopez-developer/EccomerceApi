namespace EccomerceApi.Model
{
    public class AddUserModel
    {
        public required string UserEmail { get; set; }
        public required string[] Roles { get; set; }
    }
}
