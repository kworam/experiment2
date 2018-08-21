namespace Experiment.CCI_Moderate
{
	internal class NumberSwap
	{
		public static void NumberSwapArithmetic(ref int a, ref int b)
		{
			a = a - b;
			b = b + a;
			a = b - a;
		}

		public static void NumberSwapXor(ref int a, ref int b)
		{
			// remember:
			// x ^ 1 = ~x
			// x ^ 0 = x

			// for each bit a(n), b(n)
			//  assume: a(n) = = b(n)
			//   a = a ^ b = 0
			//   b = a ^ b = 0 ^ b = b
			//   a = a ^ b = 0 ^ b = b = a(see assumption)


			//  assume: a(n) != b(n)
			//   a = a ^ b = 1
			//   b = a ^ b = 1 ^ b = ~b = a(see assumption)
			//   a = a ^ b = 1 ^ b = 1 ^ a = ~a = b(see assumption)
  
			a = a ^ b;
			b = a ^ b;
			a = a ^ b;
		}
	}
}
