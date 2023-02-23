using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CBApp.Models
{
    public class EditUserViewModel
    {
        public string? ImageSrc { get; set; }
        public string? UserName { get; set;}
    }
}
