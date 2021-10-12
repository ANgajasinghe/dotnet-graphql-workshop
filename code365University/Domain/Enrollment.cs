
namespace code365University.Domain;

public class Enrollment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EnrollmentId { get; set; }

    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public Grade? Grade { get; set; }

    public virtual Course Course { get; set; } = default!;

    public virtual Student Student { get; set; } = default!;
}

public enum Grade
{
    A, B, C, D, F
}
