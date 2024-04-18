namespace UserManagementService.Models;

public class UserUpdateModel
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
}

public class UserResponse
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
}
