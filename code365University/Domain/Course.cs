
namespace code365University.Domain;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CourseId { get; set; }

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public int Credits { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
