using System.ComponentModel.DataAnnotations;

namespace LinqAssessment.API.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;
    
    [Required]
    public string PasswordHash { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
} 