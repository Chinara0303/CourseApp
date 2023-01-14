using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers.Enums
{
    public enum Options
    {
        CreateTeacher=1,
        Deleteteacher,
        GetAllTeacher,
        GetTeacherById,
        SearchNameAndSurnameByTeacher,
        UpdateTeacher,
        CreateGroup,
        UpdateGroup,
        GetGroupById,
        DeleteGroup,
        GetAllGroupsByCapacity,
        GetAllGroupsByTeacherId,
        GetAllGroupsByteacherName,
        SearchForGroupByName,
        GetAllGroupsByCount
    }
}
