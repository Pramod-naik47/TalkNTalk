﻿using System.ComponentModel.DataAnnotations;

namespace UserManagementService.Models;

public class SignupModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
}
