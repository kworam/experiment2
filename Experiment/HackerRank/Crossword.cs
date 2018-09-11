using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Experiment.HackerRank
{
	public class Crossword
	{

		// Complete the crosswordPuzzle function below.
		public static string[] crosswordPuzzle(string[] crossword, string words)
		{
			Puzzle p = GetPuzzle(crossword, words);
			if (SolveCrossword(p))
			{
				return p.GetPuzzleRows();
			}

			return null;
		}


		private class Puzzle
		{
			public string[] words;
			public string[] rows;
			public Dictionary<int, List<string>> wordsByLength;
			public Dictionary<int, List<WordSpace>> remainingSpaces;
			public readonly Dictionary<string, WordSpace> wordToSpaceMap = new Dictionary<string, WordSpace>();
			public readonly Dictionary<string, string> spaceToWordMap = new Dictionary<string, string>();

			public string GetLongestRemainingWord()
			{
				int maxLen = int.MinValue;
				foreach (int len in wordsByLength.Keys)
				{
					if (wordsByLength[len].Count > 0 && len > maxLen)
					{
						maxLen = len;
					}
				}

				return wordsByLength[maxLen].First();
			}

			public int GetRemainingSpaces()
			{
				return words.Length - wordToSpaceMap.Count;
			}

			public IEnumerable<WordSpace> GetPossibleSpaces(string word)
			{
				return new List<WordSpace>(remainingSpaces[word.Length]);
			}

			public bool PlaceWord(string word, WordSpace ws)
			{
				if (IsConflict(word, ws))
				{
					return false;
				}

				wordToSpaceMap[word] = ws;
				spaceToWordMap[ws.ToString()] = word;
				wordsByLength[word.Length].Remove(word);
				remainingSpaces[word.Length].Remove(ws);
				return true;
			}

			private bool IsConflict(string word, WordSpace ws)
			{
				foreach (WordSpaceIntersection wsi in ws.intersections)
				{
					string spaceKey = wsi.ws.ToString();
					if (!spaceToWordMap.ContainsKey(spaceKey))
					{
						continue;
					}

					string intersectingWord = spaceToWordMap[spaceKey];
					if (IsConflict(word, ws, intersectingWord, wsi))
					{
						return true;
					}
				}

				return false;
			}

			private bool IsConflict(
				string word1, WordSpace ws1, string word2, WordSpaceIntersection wsi)
			{
				int indexOfIntersectionInWord1;
				int indexOfIntersectionInWord2;
				if (ws1.orientation == Orientation.Vertical)
				{
					indexOfIntersectionInWord1 = wsi.ip.row - ws1.start.row;
					indexOfIntersectionInWord2 = wsi.ip.col - wsi.ws.start.col;
				}
				else
				{
					indexOfIntersectionInWord1 = wsi.ip.col - ws1.start.col;
					indexOfIntersectionInWord2 = wsi.ip.row - wsi.ws.start.row;
				}

				return word1[indexOfIntersectionInWord1] != word2[indexOfIntersectionInWord2];
			}

			public void RemoveWord(string word, WordSpace ws)
			{
				wordToSpaceMap.Remove(word);
				spaceToWordMap.Remove(ws.ToString());
				wordsByLength[word.Length].Add(word);
				remainingSpaces[word.Length].Add(ws);
			}

			public string[] GetPuzzleRows()
			{
				StringBuilder[] resultHolder = GetResultHolder(rows);
				foreach (string word in words)
				{
					WriteWordIntoSpace(word, wordToSpaceMap[word], resultHolder);
				}
				string[] result = new string[resultHolder.Length];
				for (int i = 0; i < resultHolder.Length; i++)
				{
					result[i] = resultHolder[i].ToString();
				}
				return result;
			}

			private StringBuilder[] GetResultHolder(string[] rows)
			{
				StringBuilder[] holder = new StringBuilder[rows.Length];
				for (int i = 0; i < rows.Length; i++)
				{
					holder[i] = new StringBuilder(rows[i]);
				}
				return holder;
			}

			public void WriteWordIntoSpace(string word, WordSpace ws, StringBuilder[] resultHolder)
			{
				if (ws.orientation == Orientation.Vertical)
				{
					WriteVerticalWord(word, ws, resultHolder);
				}
				else
				{
					WriteHorizontalWord(word, ws, resultHolder);
				}
			}

			public void WriteVerticalWord(string word, WordSpace ws, StringBuilder[] resultHolder)
			{
				for (int i = 0; i < word.Length; i++)
				{
					resultHolder[ws.start.row + i][ws.start.col] = word[i];
				}
			}

			public void WriteHorizontalWord(string word, WordSpace ws, StringBuilder[] resultHolder)
			{
				for (int i = 0; i < word.Length; i++)
				{
					resultHolder[ws.start.row][ws.start.col + i] = word[i];
				}
			}
		}

		enum Orientation
		{
			Horizontal,
			Vertical
		}

		private class Point
		{
			public int row;
			public int col;

			public Point(int row, int col)
			{
				this.row = row;
				this.col = col;
			}

			public override string ToString()
			{
				return string.Format("{0}_{1}", row, col);
			}
		}

		private class WordSpace
		{
			public Point start;
			public List<WordSpaceIntersection> intersections;
			public int length;
			public Orientation orientation;


			public override string ToString()
			{
				return string.Format("{0}_{1}", orientation, start);
			}
		}

		private static bool SolveCrossword(Puzzle p)
		{
			if (p.GetRemainingSpaces() == 0)
			{
				return true;
			}

			string lw = p.GetLongestRemainingWord();
			IEnumerable<WordSpace> possibleSpaces = p.GetPossibleSpaces(lw);
			foreach (WordSpace possibleSpace in possibleSpaces)
			{
				if (p.PlaceWord(lw, possibleSpace))
				{
					if (SolveCrossword(p))
					{
						return true;
					}
					p.RemoveWord(lw, possibleSpace);
				}				
			}

			return false;
		}

		private static Puzzle GetPuzzle(string[] rows, string words)
		{
			Puzzle p = new Puzzle();
			p.rows = rows;
			p.words = words.Split(';');
			p.wordsByLength = GetWordsByLength(p.words);
			p.remainingSpaces = GetSpaces(rows);
			return p;
		}

		private static Dictionary<int, List<string>> GetWordsByLength(string[] words)
		{
			Dictionary<int, List<string>> wbl = new Dictionary<int, List<string>>();
			foreach (string word in words)
			{
				if (!wbl.ContainsKey(word.Length))
				{
					wbl[word.Length] = new List<string>();
				}
				wbl[word.Length].Add(word);
			}
			return wbl;
		}

		private static Dictionary<int, HashSet<int>> GetAllPoints(string[] rows)
		{
			Dictionary<int, HashSet<int>> allPoints = new Dictionary<int, HashSet<int>>();
			for (int i=0; i<rows.Length; i++)
			{
				allPoints[i] = new HashSet<int>();
				for (int j = 0; j < rows[i].Length; j++)
				{
					allPoints[i].Add(j);
				}
			}
			return allPoints;
		}

		private static void RemoveFromRemainingPoints(Dictionary<int, HashSet<int>> remainingPoints, WordSpace ws)
		{
			if (ws.orientation == Orientation.Horizontal)
			{
				for (int i = 0; i < ws.length; i++)
				{
					RemoveFromRemainingPoints(remainingPoints, ws.start.row, ws.start.col + i);
				}
			}
			else // ws.orientation == Orientation.Vertical
			{
				for (int i = 0; i < ws.length; i++)
				{
					RemoveFromRemainingPoints(remainingPoints, ws.start.row + i, ws.start.col);
				}
			}
		}

		private static Dictionary<int, List<WordSpace>> GetSpaces(string[] rows)
		{
			Dictionary<int, List<WordSpace>> wsbl = new Dictionary<int, List<WordSpace>>();
			Dictionary<int, HashSet<int>> remainingPoints = GetAllPoints(rows);

			while (remainingPoints.Count > 0)
			{
				WordSpace firstWordSpace = GetStartWordSpace(rows, remainingPoints);
				if (firstWordSpace == null)
				{
					break;
				}

				HashSet<string> discovered = new HashSet<string>();
				discovered.Add(firstWordSpace.ToString());
				Queue<WordSpace> toExplore = new Queue<WordSpace>();
				toExplore.Enqueue(firstWordSpace);
				while (toExplore.Count > 0)
				{
					WordSpace cws = toExplore.Dequeue();
					RemoveFromRemainingPoints(remainingPoints, cws);

					if (!wsbl.ContainsKey(cws.length))
					{
						wsbl[cws.length] = new List<WordSpace>();
					}
					wsbl[cws.length].Add(cws);

					foreach (WordSpaceIntersection wsi in GetIntersectingWordSpaces(cws, rows))
					{
						string wsKey = wsi.ws.ToString();
						if (!discovered.Contains(wsKey))
						{
							discovered.Add(wsKey);
							toExplore.Enqueue(wsi.ws);
						}
					}
				}
			}

			return wsbl;
		}

		private class WordSpaceIntersection
		{
			public WordSpace ws;
			public Point ip;
		}

		private static List<WordSpaceIntersection> GetIntersectingWordSpaces(
			WordSpace ws, string[] rows)
		{
			if (ws.intersections != null)
			{
				return ws.intersections;
			}

			if (ws.orientation == Orientation.Horizontal)
			{
				ws.intersections = GetVerticalIntersections(ws, rows);
			}
			else
			{
				ws.intersections = GetHorizontalIntersections(ws, rows);
			}

			return ws.intersections;
		}

		private static List<WordSpaceIntersection> GetVerticalIntersections(
			WordSpace ws, string[] rows)
		{
			List<WordSpaceIntersection> result = new List<WordSpaceIntersection>();
			for (int i = 0; i < ws.length; i++)
			{
				if (IsVerticalIntersection(ws.start.row, ws.start.col + i, rows))
				{
					result.Add(GetVerticalIntersection(ws.start.row, ws.start.col + i, rows));
				}
			}
			return result;
		}

		private static bool IsVerticalIntersection(int row, int col, string[] rows)
		{
			return
			(row > 0 && rows[row - 1][col] == '-')
			||
			(row < rows.Length - 1 && rows[row + 1][col] == '-');
		}

		private static WordSpaceIntersection GetVerticalIntersection(
			int row, int col, string[] rows)
		{
			int start = row;
			while (start >= 0 && rows[start][col] == '-') start--;
			if (start < 0 || rows[start][col] != '-') start++;
			int end = row;
			while (end < rows.Length && rows[end][col] == '-') end++;
			if (end == rows.Length || rows[end][col] != '-') end--;

			WordSpaceIntersection wsi = new WordSpaceIntersection();

			WordSpace ws = new WordSpace();
			ws.start = new Point(start, col);
			ws.length = (end - start) + 1;
			ws.orientation = Orientation.Vertical;
			wsi.ws = ws;

			wsi.ip = new Point(row, col);

			return wsi;
		}

		private static List<WordSpaceIntersection> GetHorizontalIntersections(
			WordSpace ws, string[] rows)
		{
			List<WordSpaceIntersection> result = new List<WordSpaceIntersection>();
			for (int i = 0; i < ws.length; i++)
			{
				if (IsHorizontalIntersection(ws.start.row + i, ws.start.col, rows))
				{
					result.Add(GetHorizontalIntersection(ws.start.row + i, ws.start.col, rows));
				}
			}
			return result;
		}

		private static bool IsHorizontalIntersection(int row, int col, string[] rows)
		{
			return
				(col > 0 && rows[row][col - 1] == '-')
				||
				(col < rows[row].Length - 1 && rows[row][col + 1] == '-');
		}

		private static WordSpaceIntersection GetHorizontalIntersection(
			int row, int col, string[] rows)
		{
			int start = col;
			while (start >= 0 && rows[row][start] == '-') start--;
			if (start < 0 || rows[row][start] != '-') start++;
			int end = col;
			while (end < rows[row].Length && rows[row][end] == '-') end++;
			if (end == rows[row].Length || rows[row][end] != '-') end--;

			WordSpaceIntersection wsi = new WordSpaceIntersection();

			WordSpace ws = new WordSpace();
			ws.start = new Point(row, start);
			ws.length = (end - start) + 1;
			ws.orientation = Orientation.Horizontal;

			wsi.ws = ws;
			wsi.ip = new Point(row, col);
			return wsi;
		}

		private static WordSpace GetStartWordSpace(string[] rows, Dictionary<int, HashSet<int>> remainingPoints)
		{
			for (int row = 0; row < rows.Length; row++)
			{
				for (int col = 0; col < rows[row].Length; col++)
				{
					if (!remainingPoints.ContainsKey(row))
					{
						break;
					}

					if (!remainingPoints[row].Contains(col))
					{
						continue;
					}

					RemoveFromRemainingPoints(remainingPoints, row, col);

					char c = rows[row][col];
					if (c == '-')
					{
						WordSpace ws = GetHorizontalWordspace(row, col, rows);
						if (ws == null)
						{
							ws = GetVerticalWordspace(row, col, rows);
						}

						return ws;
					}
				}
			}

			return null;
		}

		private static void RemoveFromRemainingPoints(Dictionary<int, HashSet<int>> remainingPoints, int row, int col)
		{
			remainingPoints[row].Remove(col);
			if (remainingPoints[row].Count == 0)
			{
				remainingPoints.Remove(row);
			}
		}

		private static WordSpace GetHorizontalWordspace(int row, int col, string[] rows)
		{
			int len = 0;
			int c = col;
			while (c < rows[row].Length && rows[row][c] == '-')
			{
				c++;
				len++;
			}

			if (len == 1)
			{
				return null;
			}

			WordSpace ws = new WordSpace();
			ws.start = new Point(row, col);
			ws.length = len;
			ws.orientation = Orientation.Horizontal;
			return ws;
		}

		private static WordSpace GetVerticalWordspace(int row, int col, string[] rows)
		{
			int len = 0;
			int r = row;
			while (rows[r][col] == '-' && r < rows.Length)
			{
				r++;
				len++;
			}

			if (len == 1)
			{
				return null;
			}

			WordSpace ws = new WordSpace();
			ws.start = new Point(row, col);
			ws.length = len;
			ws.orientation = Orientation.Vertical;
			return ws;
		}
	}
}
