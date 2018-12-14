using System;
using System.Text;

namespace Experiment.PreviousInterviewQuestions
{
    class PrintDiamond
    {
        public static void Print(int size)
        {
            StringBuilder sb = new StringBuilder();
            int numLines = 0;
            int numSpaces = size / 2;
            int increment = -1;
            while (numLines < size)
            {
                sb.Clear();
                for (int i = 0; i < numSpaces; i++)
                {
                    sb.Append(' ');
                }

                int numDiamonds = size - numSpaces * 2;
                for (int i = 0; i < numDiamonds; i++)
                {
                    sb.Append('x');
                }

                for (int i = 0; i < numSpaces; i++)
                {
                    sb.Append(' ');
                }

                Console.WriteLine(sb.ToString());

                if (numSpaces == 0)
                {
                    increment = -increment;
                }
                numSpaces += increment;

                numLines++;
            }
        }
    }
}
