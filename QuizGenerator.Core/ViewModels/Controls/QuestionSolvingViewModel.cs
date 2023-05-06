using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGenerator.Core.ViewModels.Controls
{
    public class QuestionSolvingViewModel : QuestionViewModel
    {
        public String SelectedOption;

        private bool _selectA;
        public bool SelectA
        {
            get { return _selectA; }
            set
            {
                _selectA = value;
                if (value)
                {
                    SelectedOption = "A";
                }
            }
        }

        private bool _selectB;
        public bool SelectB
        {
            get { return _selectB; }
            set
            {
                _selectB = value;
                if (value)
                {
                    SelectedOption = "B";
                }
            }
        }

        private bool _selectC;
        public bool SelectC
        {
            get { return _selectC; }
            set
            {
                _selectC = value;
                if (value)
                {
                    SelectedOption = "C";
                }
            }
        }

        private bool _selectD;
        public bool SelectD
        {
            get { return _selectD; }
            set
            {
                _selectD = value;
                if (value)
                {
                    SelectedOption = "D";
                }
            }
        }
    }
}
