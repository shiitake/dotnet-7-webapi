﻿using System;
using System.Collections.Generic;

namespace Core.Domain.Entities;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int Credits { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual OnlineCourse? OnlineCourse { get; set; }

    public virtual OnsiteCourse? OnsiteCourse { get; set; }

    public virtual ICollection<StudentGrade> StudentGrades { get; set; } = new List<StudentGrade>();

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
