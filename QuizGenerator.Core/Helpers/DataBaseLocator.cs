using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGenerator.Database;

namespace QuizGenerator.Core.Helpers
{
    class DataBaseLocator
    {

        public static QuizGeneratorDbContext Database { get; set; }

    }
}
