using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GoldenCrown.Dtos.Finance;

public class TransferRequest
{
   [Required(ErrorMessage = "The 'ReceiverLogin' field is required")]
    public string ReceiverLogin { get; set; }
    
    [Range(0.01, double.MaxValue, ErrorMessage = "The amount must be greater than 0")]
    public decimal Amount { get; set; }
}