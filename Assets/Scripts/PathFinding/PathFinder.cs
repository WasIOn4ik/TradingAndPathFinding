using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
	public class PathFinder : IPathFinder
	{
		#region HelperClasses

		private struct Connection
		{
			public Vector2 start;
			public Vector2 end;
			public RectangleHandler target;
		}

		private struct Line
		{
			public Vector2 start;
			public Vector2 end;
		}

		private class RectangleHandler
		{
			#region Variables

			public RectangleHandler previousHandler;

			public int handlerNum;

			public Rectangle rectangle;

			public List<Connection> connections = new List<Connection>();

			public List<RectangleHandler> neighbours = new List<RectangleHandler>();

			#endregion

			#region Functions

			public List<Line> GetSides()
			{
				return new List<Line>() {
					new Line {start = new Vector2 {x = rectangle.Min.x, y = rectangle.Min.y}, end = new Vector2{x = rectangle.Min.x, y = rectangle.Max.y} },
					new Line {start = new Vector2 {x = rectangle.Min.x, y = rectangle.Max.y}, end = new Vector2{x = rectangle.Max.x, y = rectangle.Max.y} },
					new Line {start = new Vector2 {x = rectangle.Max.x, y = rectangle.Max.y}, end = new Vector2{x = rectangle.Max.x, y = rectangle.Min.y} },
					new Line {start = new Vector2 {x = rectangle.Max.x, y = rectangle.Min.y}, end = new Vector2{x = rectangle.Min.x, y = rectangle.Min.y}}
					};
			}

			public void AddConnection(Connection connection)
			{
				connections.Add(connection);
			}

			public void AddNeighbour(RectangleHandler handler)
			{
				neighbours.Add(handler);
			}

			#endregion

			#region Operators

			public static bool operator ==(RectangleHandler left, RectangleHandler right)
			{
				if (left is null)
				{
					if (right is null)
						return true;
					else
						return false;
				}
				else
				{
					if (right is null)
						return false;
				}

				return left.rectangle.Min.x == right.rectangle.Min.x &&
					left.rectangle.Min.y == right.rectangle.Min.y &&
					left.rectangle.Max.x == right.rectangle.Max.x &&
					left.rectangle.Max.y == right.rectangle.Max.y;
			}

			public static bool operator !=(RectangleHandler left, RectangleHandler right)
			{
				return !(left == right);
			}

			public override bool Equals(object obj)
			{
				if (obj is RectangleHandler rh)
				{
					return this == rh;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (int)(int.MinValue + rectangle.Min.x + rectangle.Min.y * 256 + rectangle.Max.x * 65536 + rectangle.Max.y * 16777216);
			}

			#endregion
		}

		#endregion

		#region Variables

		private List<RectangleHandler> allRectangles;
		private Queue<RectangleHandler> pathQueue;
		private List<RectangleHandler> usedRectangles;

		private int rectIndexWithStart = -1;
		private int rectIndexWithEnd = -1;

		private Vector2 startPoint;
		private Vector2 endPoint;

		private bool bDebugMode;

		#endregion

		#region Functions

		public PathFinder(bool bDebug = false)
		{
			bDebugMode = bDebug;
		}

		public IEnumerable<Vector2> GetPath(Vector2 start, Vector2 end, IEnumerable<Edge> edges)
		{
			//Инициализируеем стартовое состояние
			allRectangles = new List<RectangleHandler>();
			pathQueue = new Queue<RectangleHandler>();
			usedRectangles = new List<RectangleHandler>();

			//Преобразуем "Ребра" во внутренние данные в виде списка прямоугольников и их отношений друг к другу
			TransformEdgesToData(start, end, edges);

			//Если хотя бы одна точка расположена вне всех прямоугольников, задача считается нерешаемой
			if (!CheckIfBothPointInRectangles())
			{
				Debug.Log("Точка старта или точка конца не лежат внутри прямоугольников");
				return new List<Vector2>();
			}


			//Если начало и конец расположены в одном прямоугольнике, то путь всегда будет состоять из начала и конца
			if (rectIndexWithStart == rectIndexWithEnd)
				return new List<Vector2> { start, end };

			//Проводимм поиск по графу для поиска кратчайшего пути по прямоугольникам. Минимум прямоугольников = минимум изгибов
			CalculateRectanglesQueue();

			//Создаем массив прямоугольников из которых состоит путь
			RectangleHandler[] path = GetPathAsArray();

			if (CheckPath(path))
			{
				if (bDebugMode)
					Print(path);

				//Считаем координаты векторов пути
				return CalculatePathAsVectorList(path);
			}
			else
			{
				Debug.Log("Точка конца недостижима");
				return new List<Vector2>();
			}
		}

		private void TransformEdgesToData(Vector2 start, Vector2 end, IEnumerable<Edge> edges)
		{
			startPoint = start;
			endPoint = end;

			foreach (Edge edge in edges)
			{
				RectangleHandler first = new RectangleHandler { rectangle = edge.First };
				RectangleHandler second = new RectangleHandler { rectangle = edge.Second };

				if (!allRectangles.Contains(first))
					allRectangles.Add(first);

				if (!allRectangles.Contains(second))
					allRectangles.Add(second);

				var firstIn = allRectangles.Find(x => x == first);

				var secondIn = allRectangles.Find(x => x == second);

				Connection connection1 = new Connection { start = edge.Start, end = edge.End, target = second };
				Connection connection2 = new Connection { start = edge.Start, end = edge.End, target = first };
				firstIn.AddConnection(connection1);
				secondIn.AddConnection(connection2);
				firstIn.AddNeighbour(secondIn);
				secondIn.AddNeighbour(firstIn);
			}

			for (int i = 0; i < allRectangles.Count; i++)
			{
				if (IsInRect(start, allRectangles[i].rectangle))
					rectIndexWithStart = i;

				if (IsInRect(end, allRectangles[i].rectangle))
					rectIndexWithEnd = i;

				allRectangles[i].handlerNum = i;
			}
		}

		private bool CheckIfBothPointInRectangles()
		{
			return rectIndexWithEnd != -1 && rectIndexWithStart != -1;
		}

		private void CalculateRectanglesQueue()
		{
			if (allRectangles.Count < 1)
				return;

			pathQueue.Enqueue(allRectangles[rectIndexWithStart]);

			RectangleHandler GoalValue = allRectangles[rectIndexWithEnd];

			while (pathQueue.Count != 0)
			{
				RectangleHandler node = pathQueue.Dequeue();
				List<RectangleHandler> neighbours = node.neighbours;

				if (node == GoalValue)
				{
					return;
				}

				foreach (RectangleHandler neighbour in neighbours)
				{
					if (!usedRectangles.Contains(neighbour))
					{
						neighbour.previousHandler = node;

						if (neighbour == GoalValue)
						{
							pathQueue.Enqueue(node);
							pathQueue.Enqueue(GoalValue);
							return;
						}
						pathQueue.Enqueue(neighbour);

					}

				}
				usedRectangles.Add(node);
			}
		}

		private RectangleHandler[] GetPathAsArray()
		{
			RectangleHandler handler = allRectangles[rectIndexWithEnd];
			Stack<RectangleHandler> path = new Stack<RectangleHandler>();
			while (handler != null)
			{
				path.Push(handler);
				handler = handler.previousHandler;
			}
			return path.ToArray();

		}

		private bool CheckPath(RectangleHandler[] path)
		{
			return path[0].handlerNum == rectIndexWithStart && path[path.Length - 1].handlerNum == rectIndexWithEnd;
		}

		private void Print(RectangleHandler[] path)
		{
			foreach (var el in path)
			{
				UnityEngine.Debug.Log(el.handlerNum);
			}
		}

		private List<Vector2> CalculatePathAsVectorList(RectangleHandler[] path)
		{
			if (path.Length < 2)
				return new List<Vector2>();

			List<Vector2> result = new List<Vector2>();

			result.Add(startPoint);
			Vector2 currentPoint = startPoint;

			for (int i = 0; i < path.Length - 1; i++)
			{
				Connection connection = path[i].connections.Find(x => x.target == path[i + 1]);
				Vector2 nextPoint = default;
				Vector2 edgeCenterPoint = (connection.start + connection.end) / 2;
				Vector2 directionPoint = currentPoint + (edgeCenterPoint - currentPoint) * 100;

				if (IsOnSameLine(startPoint, edgeCenterPoint, connection.start, connection.end))
				{
					currentPoint = edgeCenterPoint;
					result.Add(currentPoint);
					Debug.Log("OnSameLine");
					continue;
				}

				var lines = path[i + 1].GetSides();

				List<Vector2> hits = new List<Vector2>();

				foreach (var line in lines)
				{
					if (IsSegmentsIntersect(line.start, line.end, currentPoint, directionPoint, out var hit))
					{
						hits.Add(hit);
					}
				}
				nextPoint = (hits[0] + hits[1]) / 2;

				result.Add(nextPoint);
				currentPoint = nextPoint;
			}

			result.Add(endPoint);

			return result;

		}

		private bool IsInRect(Vector2 point, Rectangle rect)
		{
			return point.x <= rect.Max.x && point.x >= rect.Min.x && point.y <= rect.Max.y && point.y >= rect.Min.y;
		}

		#endregion

		#region Intersections

		private bool IsSegmentsIntersect(Vector2 p1start, Vector2 p1end, Vector2 p2start, Vector2 p2end, out Vector2 intersection)
		{
			var p = p1start;
			var r = p1end - p1start;
			var q = p2start;
			var s = p2end - p2start;
			var qminusp = q - p;

			float cross_rs = CrossProduct2D(r, s);

			if (Approximately(cross_rs, 0f))
			{
				if (Approximately(CrossProduct2D(qminusp, r), 0f))
				{
					float rdotr = Vector2.Dot(r, r);
					float sdotr = Vector2.Dot(s, r);
					float t0 = Vector2.Dot(qminusp, r / rdotr);
					float t1 = t0 + sdotr / rdotr;
					if (sdotr < 0)
					{
						Swap(ref t0, ref t1);
					}

					if (t0 <= 1 && t1 >= 0)
					{
						float t = Mathf.Lerp(Mathf.Max(0, t0), Mathf.Min(1, t1), 0.5f);
						intersection = p + t * r;
						return true;
					}
					else
					{
						intersection = Vector2.zero;
						return false;
					}
				}
				else
				{
					intersection = Vector2.zero;
					return false;
				}
			}
			else
			{
				float t = CrossProduct2D(qminusp, s) / cross_rs;
				float u = CrossProduct2D(qminusp, r) / cross_rs;
				if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
				{
					intersection = p + t * r;
					return true;
				}
				else
				{
					intersection = Vector2.zero;
					return false;
				}
			}
		}

		private void Swap<T>(ref T lhs, ref T rhs)
		{
			T temp = lhs;
			lhs = rhs;
			rhs = temp;
		}

		private bool Approximately(float a, float b, float tolerance = 1e-5f)
		{
			return Mathf.Abs(a - b) <= tolerance;
		}

		private float CrossProduct2D(Vector2 a, Vector2 b)
		{
			return a.x * b.y - b.x * a.y;
		}

		private bool IsOnSameLine(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
		{
			return (a1.x == a2.x && a2.x == b1.x && b1.x == b2.x) || (a1.y == a2.y && a2.y == b1.y && b1.y == b2.y);
		}

		#endregion
	}
}
