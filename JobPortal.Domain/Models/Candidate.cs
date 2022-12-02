using System.ComponentModel.DataAnnotations;

namespace JobPortal.Domain.Models;

public class Candidate
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }

    [StringLength(20)]
    public string PlaceOfBirth { get; set; }

    public int JobId { get; set; }

    [StringLength(int.MaxValue)]
    public string Resume { get; set; }
}
