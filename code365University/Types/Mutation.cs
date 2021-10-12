

using code365University.Persistence;

namespace code365University.Types;

public record EnrollStudentInput(int CourseId, string FirstMidName, string LastName);

public class EnrollStudentPayload
{
    public EnrollStudentPayload(int studentId, int courseId)
    {
        StudentId = studentId;
        CourseId = courseId;
    }

    public int StudentId { get; }

    public int CourseId { get; }

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Student> GetStudent(SchoolContext context)
        => context.Students.Where(t => t.Id == StudentId);

    [UseFirstOrDefault]
    [UseProjection]
    public IQueryable<Course> GetCourse(SchoolContext context)
        => context.Courses.Where(t => t.CourseId == CourseId);
}

public class Mutation
{

    // one logic 

    /// <summary>
    /// Enroll new student into a course.
    /// </summary>
    public async Task<EnrollStudentPayload> EnrollStudentAsync(
        EnrollStudentInput input,
        SchoolContext context,
        CancellationToken cancellationToken)
    {
        var student = new Student
        {
            FirstMidName = input.FirstMidName,
            LastName = input.LastName,
            EnrollmentDate = DateTime.UtcNow,
            Enrollments = { new() { CourseId = input.CourseId } }
        };

        context.Students.Add(student);
        await context.SaveChangesAsync();

        return new EnrollStudentPayload(student.Id, input.CourseId);
    }
}


