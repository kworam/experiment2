using System;
using System.Collections;

namespace Experiment.HackerRank
{
	public class Primality
	{
		public static string primality(int n)
		{
			// Sieve of erastothenes
			// if # is 1, 2
			if (n == 1)
			{
				return "Not prime";
			}

			if (n == 2)
			{
				return "Prime";
			}

			// for all numbers n > 2
			//   create a bit vector of size 0..sqrt(n)
			//   for each number x from 2..sqrt(n), set all multiples of x to false
			//   if x is prime, sqrt(x) is not an integer
			//   any numbers > sqrt(x) will not divide the prime evenly
			//int numInts = (int) Math.Ceiling((double)n/32);
			BitArray bitVector = new BitArray(n+1);
			int stop = (int)Math.Ceiling(Math.Sqrt(n));
			for (int i = 2; i <= stop; i++)
			{
				if (n%i == 0) return "Not prime";
				//EliminateMultiplesOf(i, bitVector);
			}

			//return bitVector[n] ? "Not prime" : "Prime";
			return "Prime";
		}
		static void EliminateMultiplesOf(int i, BitArray bitVector)
		{
			int multiple = 2;
			while (i * multiple < bitVector.Length)
			{
				bitVector[i * multiple] = true;
				multiple++;
			}
		}
	}
}
