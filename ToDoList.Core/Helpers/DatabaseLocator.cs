using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Core.Helpers
{
    public class DatabaseLocator
    {

        public static ToDoListDbContext Database { get; set; }

    }
}
