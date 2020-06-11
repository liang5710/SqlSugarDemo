﻿using SqlSugarDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SqlSugarDemo.IService
{
    public interface IStudentService
    {
        int Sum(int i, int j);

        int Add(Student student);
        bool Delete(Student student);
        bool Update(Student student);
        List<Student> Query(Expression<Func<Student, bool>> whereExpression);
    }
}
