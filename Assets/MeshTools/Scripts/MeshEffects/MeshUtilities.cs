using System.Linq;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects{
	public static class MeshUtilities{
		public static Mesh Clone(this Mesh m){
		 return	(Mesh)Object.Instantiate(m);
		}

		public static Vector3 GetCenter(Vector3[] vertices, DetectionType dataCenterType){
			Vector3 result = Vector3.zero;
			switch (dataCenterType){
				case DetectionType.VerticesAverage:
					result = new Vector3(vertices.Average(x=>x.x),vertices.Average(y=>y.y),vertices.Average(z=>z.z));
					break;

				case DetectionType.LowestPoint:
					result =  vertices.OrderBy(z=>z.y).First();
					break;
				case  DetectionType.CurrentCenter:
					result = Vector3.zero;
					break;
				case DetectionType.MixedType:
					Vector3 average = new Vector3(vertices.Average(x=>x.x),vertices.Average(y=>y.y),vertices.Average(z=>z.z));
					Vector3 lowest =  vertices.OrderBy(z=>z.y).First();
					result = ((average*0.2f) + (lowest*0.8f));
					break;
			}
			return result;
		}
	}
}
