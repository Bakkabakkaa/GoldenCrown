using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GoldenCrown.Dtos.Finance;

public class TransactionHistoryRequest
{
    [FromQuery]
    [Required(ErrorMessage = "The 'token' field is required")]
    public string Token { get; set; }
    
    public DateTime? From { get; set; }
    
    public DateTime? To { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "The limit value must be at least 1.")]
    public int Limit { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "The offset value cannot be negative.")]
    public int Offset { get; set; }
}