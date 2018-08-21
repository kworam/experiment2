namespace Experiment.CCI_Hard
{
	internal class AddWithoutArithmeticOperators
	{
		public static int AddWithoutPlusXorAnd(int a, int b)
		{
			if (b == 0)
			{
				return a;
			}

			// do add without carry:

			// for each bit a(n) and b(n)
			//   if a==b result is 0
			//   if a<>b result is 1
			// this yields the right 'add without carry' result for each bit.
			int addWithoutCarries = a ^ b;

			// do carry only:

			// for each bit a(n) and b(n)
			//  if a==1 and b==1, result is 1
			//  else result is 0
			// this yields the right 'carry' result for each bit.
			// Must then shift the carries left by one so they can be added to the proper bit.
			int carries = (a & b) << 1;

			return AddWithoutPlusXorAnd(addWithoutCarries, carries);
		}
	}
}
