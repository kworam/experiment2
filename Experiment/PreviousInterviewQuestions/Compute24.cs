using System;
using System.Collections.Generic;

namespace Experiment.PreviousInterviewQuestions
{
    public class Compute24
    {
        private class Op
        {
            public int index1;
            public int index2;
            public string op;

            public override string ToString()
            {
                return string.Format("{0}{1}{2}", index1, op, index2);
            }
        }

        public static bool CanCompute24(List<int> nums)
        {
            List<Op> opQueue = new List<Op>();
            return CanCompute24(nums, opQueue);
        }

        private static bool CanCompute24(List<int> nums, List<Op> opList)
        { 
            // using only the operations +, -, *, and /,
            // is it possible to compute24 with the specified numbers?
            if (nums.Count == 1)
            {
                return nums[0] == 24;
            }

            Op op = new Op();
            opList.Add(op);
            for (int i = 0; i < nums.Count - 1; i++)
            {
                for (int j = i+1; j < nums.Count; j++)
                {
                    int firstNum = nums[i];
                    int secondNum = nums[j];
                    List<int> newList = CopyAndRemove(nums, i, j);

                    newList.Add(0);
                    int index = newList.Count - 1;

                    newList[index] = firstNum + secondNum;
                    if (CanCompute24(newList, UpdateOp(opList, i, j, "+"))) return true;

                    newList[index] = firstNum - secondNum;
                    if (CanCompute24(newList, UpdateOp(opList, i, j, "-"))) return true;
                    newList[index] = secondNum - firstNum;
                    if (CanCompute24(newList, UpdateOp(opList, j, i, "-"))) return true;

                    newList[index] = firstNum * secondNum;
                    if (CanCompute24(newList, UpdateOp(opList, i, j, "*"))) return true;

                    if (secondNum != 0)
                    {
                        newList[index] = firstNum / secondNum;
                        if (CanCompute24(newList, UpdateOp(opList, i, j, "/"))) return true;
                    }

                    if (firstNum != 0)
                    {
                        newList[index] = secondNum / firstNum;
                        if (CanCompute24(newList, UpdateOp(opList, j, i, "/"))) return true;
                    }
                }
            }
            opList.Remove(op);

            return false;
        }

        private static List<Op> UpdateOp(List<Op> list, int index1, int index2, string opstring)
        {
            Op op = list[list.Count - 1];
            op.index1 = index1;
            op.index2 = index2;
            op.op = opstring;
            return list;
        }

        private static List<int> CopyAndRemove(List<int> l, int i, int j)
        {
            List<int> result = new List<int>(l);
            int minIndex = Math.Min(i, j);
            int maxIndex = Math.Max(i, j);
            result.RemoveAt(maxIndex);
            result.RemoveAt(minIndex);
            return result;
        }
    }
}
