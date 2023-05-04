using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public interface ITestData
	{
		public List<Edge> GetTest(out Vector2 start, out Vector2 end);
	}

	public class TestData7 : ITestData
	{
		private Vector2 s = new Vector2(2, 0);
		private Vector2 e = new Vector2(21, 12);

		public List<Edge> GetTest(out Vector2 start, out Vector2 end)
		{
			start = s;
			end = e;
			return new List<Edge>()
	{
		new Edge//AB
		{
			First = new Rectangle{Min = new Vector2{x = 0, y = 0},Max = new Vector2{x = 5, y = 3}},
			Second = new Rectangle{Min = new Vector2{x = 3, y = 3 }, Max = new Vector2{x = 10, y = 6 } },
			Start = new Vector2{x = 3, y = 3},
			End = new Vector2{x = 5, y = 3},
		},
		new Edge//BC
		{
			First = new Rectangle{Min = new Vector2{x = 3, y = 3},Max = new Vector2{x = 10, y = 6}},
			Second = new Rectangle{Min = new Vector2{x = 10, y = -2 }, Max = new Vector2{x = 13, y = 4 } },
			Start = new Vector2{x = 10, y = 4},
			End = new Vector2{ x = 10, y = 3},
		},
		new Edge//CD
		{
			First = new Rectangle{Min = new Vector2{x = 10, y = -2},Max = new Vector2{x = 13, y = 4}},
			Second = new Rectangle{Min = new Vector2{x = 13, y = 0 }, Max = new Vector2{x = 19, y = 2 } },
			Start = new Vector2{x = 13, y = 2},
			End = new Vector2{ x = 13, y = 0},
		},
		new Edge//DE
		{
			First = new Rectangle{Min = new Vector2{x = 13, y = 0},Max = new Vector2{x = 19, y = 2}},
			Second = new Rectangle{Min = new Vector2{x = 15, y = 2 }, Max = new Vector2{x = 21, y = 9 } },
			Start = new Vector2{x = 15, y = 2},
			End = new Vector2{ x = 19, y = 2},
		},
		new Edge//EF
		{
			First = new Rectangle{Min = new Vector2{x = 15, y = 2},Max = new Vector2{x = 21, y = 9}},
			Second = new Rectangle{Min = new Vector2{x = 12, y = 7}, Max = new Vector2{x = 15, y = 11} },
			Start = new Vector2{x = 15, y = 7},
			End = new Vector2{x = 15, y = 9},
		},
		new Edge//FG
		{
			First = new Rectangle{Min = new Vector2{x = 12, y = 7},Max = new Vector2{x = 15, y = 11}},
			Second = new Rectangle{Min = new Vector2{x = 14, y = 11}, Max = new Vector2{x = 22, y = 13} },
			Start = new Vector2{x = 14, y = 11},
			End = new Vector2{x = 15, y = 11},
		}
	};
		}
	}

	public class TestData5 : ITestData
	{
		private Vector2 s = new Vector2(2, 0);
		private Vector2 e = new Vector2(19, 7);

		public List<Edge> GetTest(out Vector2 start, out Vector2 end)
		{
			start = s;
			end = e;
			return new List<Edge>()
	{
		new Edge//AB
		{
			First = new Rectangle{Min = new Vector2{x = 0, y = 0},Max = new Vector2{x = 5, y = 3}},
			Second = new Rectangle{Min = new Vector2{x = 3, y = 3}, Max = new Vector2{x = 10, y = 6} },
			Start = new Vector2{x = 3, y = 3},
			End = new Vector2{x = 5, y = 3},
		},
		new Edge//BC
		{
			First = new Rectangle{Min = new Vector2{x = 3, y = 3},Max = new Vector2{x = 10, y = 6}},
			Second = new Rectangle{Min = new Vector2{x = 10, y = -2}, Max = new Vector2{x = 13, y = 4} },
			Start = new Vector2{x = 10, y = 4},
			End = new Vector2{x = 10, y = 3},
		},
		new Edge//CD
		{
			First = new Rectangle{Min = new Vector2{x = 10, y = -2},Max = new Vector2{x = 13, y = 4}},
			Second = new Rectangle{Min = new Vector2{x = 13, y = 0}, Max = new Vector2{x = 19, y = 2} },
			Start = new Vector2{x = 13, y = 2},
			End = new Vector2{x = 13, y = 0},
		},
		new Edge//DE
		{
			First = new Rectangle{Min = new Vector2{x = 13, y = 0},Max = new Vector2{x = 19, y = 2}},
			Second = new Rectangle{Min = new Vector2{x = 15, y = 2}, Max = new Vector2{x = 21, y = 9} },
			Start = new Vector2{x = 15, y = 2},
			End = new Vector2{x = 19, y = 2},
		}
	};
		}
	}

	public class TestData4 : ITestData
	{
		private Vector2 s = new Vector2(1, 3);
		private Vector2 e = new Vector2(18, 1);

		public List<Edge> GetTest(out Vector2 start, out Vector2 end)
		{
			start = s;
			end = e;
			return new List<Edge>()
	{
		new Edge//AB
		{
			First = new Rectangle{Min = new Vector2{x = 0, y = 0},Max = new Vector2{x = 5, y = 3}},
			Second = new Rectangle{Min = new Vector2{x = 3, y = 3}, Max = new Vector2{x = 10, y = 6} },
			Start = new Vector2{x = 3, y = 3},
			End = new Vector2{x = 5, y = 3},
		},
		new Edge//BC
		{
			First = new Rectangle{Min = new Vector2{x = 3, y = 3},Max = new Vector2{x = 10, y = 6}},
			Second = new Rectangle{Min = new Vector2{x = 10, y = -2}, Max = new Vector2{x = 13, y = 4} },
			Start = new Vector2{x = 10, y = 4},
			End = new Vector2{x = 10, y = 3},
		},
		new Edge//CD
		{
			First = new Rectangle{Min = new Vector2{x = 10, y = -2},Max = new Vector2{x = 13, y = 4}},
			Second = new Rectangle{Min = new Vector2{x = 13, y = 0}, Max = new Vector2{x = 19, y = 2} },
			Start = new Vector2{x = 13, y = 2},
			End = new Vector2{x = 13, y = 0},
		}
	};
		}
	}

	public class FromTask : ITestData
	{
		private Vector2 s = new Vector2(2, 0);
		private Vector2 e = new Vector2(12, 0);

		public List<Edge> GetTest(out Vector2 start, out Vector2 end)
		{
			start = s;
			end = e;
			return new List<Edge>()
	{
		new Edge//AB
		{
			First = new Rectangle{Min = new Vector2{x = 0, y = 0},Max = new Vector2{x = 5, y = 3}},
			Second = new Rectangle{Min = new Vector2{x = 3, y = 3}, Max = new Vector2{x = 10, y = 6} },
			Start = new Vector2{x = 3, y = 3},
			End = new Vector2{x = 5, y = 3},
		},
		new Edge//BC
		{
			First = new Rectangle{Min = new Vector2{x = 3, y = 3},Max = new Vector2{x = 10, y = 6}},
			Second = new Rectangle{Min = new Vector2{x = 10, y = -2}, Max = new Vector2{x = 13, y = 4} },
			Start = new Vector2{x = 10, y = 4},
			End = new Vector2{x = 10, y = 3},
		}
	};
		}
	}

	public class TestCyclic : ITestData
	{
		private Vector2 s = new Vector2(2, 0);
		private Vector2 e = new Vector2(21, 12);

		public List<Edge> GetTest(out Vector2 start, out Vector2 end)
		{
			start = s;
			end = e;
			return new List<Edge>()
	{
		new Edge//AB
		{
			First = new Rectangle{Min = new Vector2{x = 0, y = 0},Max = new Vector2{x = 5, y = 3}},
			Second = new Rectangle{Min = new Vector2{x = 3, y = 3}, Max = new Vector2{x = 10, y = 6} },
			Start = new Vector2{x = 3, y = 3},
			End = new Vector2{x = 5, y = 3},
		},
		new Edge//BC
		{
			First = new Rectangle{Min = new Vector2{x = 3, y = 3},Max = new Vector2{x = 10, y = 6}},
			Second = new Rectangle{Min = new Vector2{x = 10, y = -2}, Max = new Vector2{x = 13, y = 4} },
			Start = new Vector2{x = 10, y = 4},
			End = new Vector2{x = 10, y = 3},
		},
		new Edge//CD
		{
			First = new Rectangle{Min = new Vector2{x = 10, y = -2},Max = new Vector2{x = 13, y = 4}},
			Second = new Rectangle{Min = new Vector2{x = 13, y = 0}, Max = new Vector2{x = 19, y = 2} },
			Start = new Vector2{x = 13, y = 2},
			End = new Vector2{x = 13, y = 0},
		},
		new Edge//DE
		{
			First = new Rectangle{Min = new Vector2{x = 13, y = 0},Max = new Vector2{x = 19, y = 2}},
			Second = new Rectangle{Min = new Vector2{x = 15, y = 2}, Max = new Vector2{x = 21, y = 9} },
			Start = new Vector2{x = 15, y = 2},
			End = new Vector2{x = 19, y = 2},
		},
		new Edge//EF
		{
			First = new Rectangle{Min = new Vector2{x = 15, y = 2},Max = new Vector2{x = 21, y = 9}},
			Second = new Rectangle{Min = new Vector2{x = 12, y = 7}, Max = new Vector2{x = 15, y = 11} },
			Start = new Vector2{x = 15, y = 7},
			End = new Vector2{x = 15, y = 9},
		},
		new Edge//FG
		{
			First = new Rectangle{Min = new Vector2{x = 12, y = 7},Max = new Vector2{x = 15, y = 11}},
			Second = new Rectangle{Min = new Vector2{x = 14, y = 11}, Max = new Vector2{x = 22, y = 13} },
			Start = new Vector2{x = 14, y = 11},
			End = new Vector2{x = 15, y = 11},
		},
		new Edge//BH
		{
			First = new Rectangle{Min = new Vector2{x = 3, y = 3},Max = new Vector2{x = 10, y = 6}},
			Second = new Rectangle{Min = new Vector2{x = 8, y = 6}, Max = new Vector2{x = 12, y = 9} },
			Start = new Vector2{x = 8, y = 6},
			End = new Vector2{x = 10, y = 6},
		},
		new Edge//HF
		{
			First = new Rectangle{Min = new Vector2{x = 8, y = 6},Max = new Vector2{x = 12, y = 9}},
			Second = new Rectangle{Min = new Vector2{x = 12, y = 7}, Max = new Vector2{x = 15, y = 11} },
			Start = new Vector2{x = 12, y = 7},
			End = new Vector2{x = 12, y = 9},
		}
	};
		}
	}

	public class InvalidTest1 : ITestData
	{
		private Vector2 s = new Vector2(2, 0);
		private Vector2 e = new Vector2(11, 0);

		public List<Edge> GetTest(out Vector2 start, out Vector2 end)
		{
			start = s;
			end = e;
			return new List<Edge>()
	{
		new Edge//AB
		{
			First = new Rectangle{Min = new Vector2{x = 0, y = 0},Max = new Vector2{x = 5, y = 3}},
			Second = new Rectangle{Min = new Vector2{x = 3, y = 3}, Max = new Vector2{x = 10, y = 6} },
			Start = new Vector2{x = 3, y = 3},
			End = new Vector2{x = 5, y = 3},
		},
		new Edge//CD
		{
			First = new Rectangle{Min = new Vector2{x = 11, y = -2},Max = new Vector2{x = 14, y = 4}},
			Second = new Rectangle{Min = new Vector2{x = 14, y = 0}, Max = new Vector2{x = 20, y = 2} },
			Start = new Vector2{x = 14, y = 2},
			End = new Vector2{x = 14, y = 0},
		}
	};
		}
	}
}