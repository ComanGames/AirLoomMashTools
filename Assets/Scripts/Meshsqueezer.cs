using System.Linq;
using UnityEngine;

namespace Assets.Scripts{
	public class MeshSqueezer:IMeshTool<MeshSqueezerData>{
		public Mesh Process(MeshSqueezerData data){

			Vector3 center = GetCenter(data);
			Mesh mesh = data.OurMesh.Clone();
			Vector3[] v = (Vector3[])mesh.vertices.Clone();
			Quaternion r = data.Rotation;
			float f = data.Force;

			for (int i = 0; i < v.Length; i++){
				Vector3 tempPoint = r * (v[i] - center);
				v[i] = (Quaternion.Inverse(r) * new Vector3(tempPoint.x, tempPoint.y * f, tempPoint.z)) + center;
			}

			mesh.vertices = v;
			return mesh;
		}

		private Vector3 GetCenter(MeshSqueezerData data){

			DetectionType centerType = data.CenterType;
			Vector3[] v = data.OurMesh.Clone().vertices;
			Vector3 result = Vector3.zero;
			switch (centerType){
				case DetectionType.VerticesAverage:
					result = new Vector3(v.Average(x=>x.x),v.Average(y=>y.y),v.Average(z=>z.z));
				break;

				case DetectionType.LowestPoint:
					result =  v.OrderBy(z=>z.y).First();
				break;
					case  DetectionType.CurrentCenter:
						 result = Vector3.zero;
					break;
					case DetectionType.MixedType:
						Vector3 average = new Vector3(v.Average(x=>x.x),v.Average(y=>y.y),v.Average(z=>z.z));
						Vector3 lowest =  v.OrderBy(z=>z.y).First();
						result = ((average*0.2f) + (lowest*0.8f));
						break;
			}
			return result;
		}
	}
}