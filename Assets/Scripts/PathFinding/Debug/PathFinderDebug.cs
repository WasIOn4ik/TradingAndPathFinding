using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using R = PathFinding.Rectangle;
using V2 = UnityEngine.Vector2;

namespace PathFinding
{
	public enum TestType
	{
		FromTask,
		R4,
		R5,
		R7,
		Cyclic1,
		Invalid1
	}

	public class PathFinderDebug : MonoBehaviour
	{
		#region Variables

		[SerializeField] private bool bDebug;
		[SerializeField] bool bOverrideTest;
		[SerializeField] private V2 startOverride = new V2(2, 0);
		[SerializeField] private V2 endOverride = new V2(11, 0);
		[SerializeField] private TestType testType;
		private V2 start;
		private V2 end;

		private Dictionary<TestType, ITestData> tests = new Dictionary<TestType, ITestData>()
		{
			{TestType.FromTask, new FromTask()},
			{TestType.R4, new TestData4() },
			{TestType.R5, new TestData5() },
			{TestType.R7, new TestData7() },
			{TestType.Cyclic1, new TestCyclic()},
			{TestType.Invalid1, new InvalidTest1() }
		};

		private PathFinder pathFinder = new PathFinder();

		#endregion

		#region Functions

		private void Awake()
		{
			var list = tests[testType].GetTest(out start, out end);

			if (bOverrideTest)
			{
				start = startOverride;
				end = endOverride;
			}

			if (bDebug)
			{
				foreach (var r in list)
				{
					DrawRect(r.First);
					DrawRect(r.Second);
					DrawLine(r.Start, r.End, Color.blue);
				}
				DrawX(start, Color.cyan);
				DrawX(end, Color.green);
			}

			var path = pathFinder.GetPath(start, end, list).ToArray();

			if (bDebug)
			{
				for (int i = 0; i < path.Length - 1; i++)
				{
					if (i > 0 && i < path.Length - 1)
						DrawX(path[i], Color.black, 0.2f);

					DrawLine(path[i], path[i + 1], Color.magenta);
				}
			}
		}

		private void DrawRect(R rect, float duration = 10000f)
		{
			V2 LD = rect.Min;
			V2 LU = new V2(rect.Min.x, rect.Max.y);
			V2 RD = new V2(rect.Max.x, rect.Min.y);
			V2 RU = rect.Max;

			Vector3 LD3 = new Vector3(LD.x, 0, LD.y);
			Vector3 LU3 = new Vector3(LU.x, 0, LU.y);
			Vector3 RD3 = new Vector3(RD.x, 0, RD.y);
			Vector3 RU3 = new Vector3(RU.x, 0, RU.y);


			Debug.DrawLine(LD3, LU3, Color.red, duration);
			Debug.DrawLine(LU3, RU3, Color.red, duration);
			Debug.DrawLine(RU3, RD3, Color.red, duration);
			Debug.DrawLine(RD3, LD3, Color.red, duration);
		}

		private void DrawLine(V2 s, V2 e, Color color, float duration = 10000f)
		{
			Vector3 start = new Vector3(s.x, 0.1f, s.y);
			Vector3 end = new Vector3(e.x, 0.1f, e.y);
			Debug.DrawLine(start, end, color, duration);
		}

		private void DrawX(V2 point, Color color, float size = 0.3f, float duration = 10000f)
		{
			Vector3 LD3 = new Vector3(point.x - size, 0.2f, point.y - size);
			Vector3 LU3 = new Vector3(point.x - size, 0.2f, point.y + size);
			Vector3 RD3 = new Vector3(point.x + size, 0.2f, point.y - size);
			Vector3 RU3 = new Vector3(point.x + size, 0.2f, point.y + size);

			Debug.DrawLine(LD3, RU3, color, duration);
			Debug.DrawLine(RD3, LU3, color, duration);
		}

		#endregion
	}
}
