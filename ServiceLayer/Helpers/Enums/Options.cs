using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers.Enums
{
    public enum Options
    {
        TeacherOptions =1,
        GroupOptions,
        CreateTeacher = 1,
        Deleteteacher,
        GetAllTeacher,
        GetTeacherById,
        SearchNameAndSurnameByTeacher,
        UpdateTeacher,
        CreateGroup =1,
        UpdateGroup,
        GetGroupById,
        DeleteGroup,
        GetAllGroupsByCapacity,
        GetAllGroupsByTeacherId,
        GetAllGroupsByTeacherName,
        SearchForGroupByName,
        GetAllGroupsByCount
    }
}
