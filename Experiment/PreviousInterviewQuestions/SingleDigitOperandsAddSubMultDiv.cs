using System.Collections.Generic;
using System.Text;

namespace Experiment.PreviousInterviewQuestions
{
    public class SingleDigitOperandsAddSubMultDiv
    {
        public static int Evaluate(string exp)
        {
            if (string.IsNullOrEmpty(exp))
            {
                return 0;
            }

            int prevResult = DigitToInt(exp[0]);
            List<char> operators = new List<char>();
            List<int> operands = new List<int>();
            for (int i = 1; i < exp.Length; i += 2)
            {
                if (exp[i] == '*')
                {
                    prevResult = prevResult * DigitToInt(exp[i + 1]);
                }
                else if (exp[i] == '/')
                {
                    prevResult = prevResult / DigitToInt(exp[i + 1]);
                }
                else
                {
                    operands.Add(prevResult);
                    operators.Add(exp[i]);
                    prevResult = DigitToInt(exp[i + 1]);
                }
            }

            if (operands.Count == 0)
            {
                return prevResult;
            }

            operands.Add(prevResult);

            int result = operands[0];
            for (int i = 0; i < operators.Count; i++)
            {
                if (operators[i] == '+')
                {
                    result += operands[i + 1];
                }
                else
                {
                    result -= operands[i + 1];
                }
            }

            return result;
        }

        private static int DigitToInt(char c)
        {
            return c - '0';
        }
    }
}
