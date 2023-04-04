using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFinallyProject.Enums
{
    public enum MenuTypes : byte
    {
        AuthorAdd=1,
        AuthorEdit,
        AuthorRemove,
        AuthorGetAll,
        AuthorGetById,
        AuthorFindById,

        BookAdd,
        BookEdit,
        BookRemove,
        BookGetAll,
        BookGetById,
        BookFindById,
    }
}
