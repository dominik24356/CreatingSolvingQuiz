using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizGenerator.Core.ViewModels.Controls;
using QuizGenerator.Database.Entities;

namespace QuizGenerator.Core.Helpers
{
    public static class QuestionViewModelMapper
    {

        public static List<QuestionViewModel> mapToViewModelList(List<Question> list)
        {
            if (list == null)
            {
                list = new List<Question>();
            }

            List<QuestionViewModel> questionViewModels = new List<QuestionViewModel>();
          
            foreach (var question in list)
            {
                questionViewModels.Add(new QuestionViewModel
                {
                    Id = question.Id,
                    IsSelected = false,
                    QuestionText = AesEncryptionHelper.DecryptString(question.QuestionText),
                    OptionA = AesEncryptionHelper.DecryptString(question.OptionA),
                    OptionB = AesEncryptionHelper.DecryptString(question.OptionB),
                    OptionC = AesEncryptionHelper.DecryptString(question.OptionC),
                    OptionD = AesEncryptionHelper.DecryptString(question.OptionD),
                    CorrectOption = AesEncryptionHelper.DecryptString(question.CorrectOption)
                });
            }


            return questionViewModels;
        }



    }
}
