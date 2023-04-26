using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGen.Model.Question;

namespace QuizGen.Database
{
        public class QuestionRepository
        {
            private readonly MyDbContext _dbContext;

            public QuestionRepository(MyDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public List<Question> GetAllQuestions()
            {
                return _dbContext.questions.ToList();
            }

            public Question GetQuestionById(int id)
            {
                return _dbContext.questions.FirstOrDefault(q => q.Id == id);
            }


        public Question saveQuestion(Question question)
        {
            _dbContext.questions.Add(question);
            _dbContext.SaveChanges();
            return question;
        }




    }
}
