﻿namespace UserManagementSystem.Application.DTOs.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
