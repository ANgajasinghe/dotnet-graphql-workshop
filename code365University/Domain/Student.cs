
namespace code365University.Domain;

public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string FirstMidName { get; set; } = default!;

    [Required]
    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}


