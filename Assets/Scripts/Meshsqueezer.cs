using System.Linq;
using UnityEngine;

namespace Assets.Scripts{
	public class MeshSqueezer:IMeshTool<MeshSqueezerData>{
		public Mesh Process(MeshSqueezerData data){

			Vector3 center = MeshUtilities.GetCenter(data.mesh.Clone().vertices, data.CenterType);
			Mesh mesh = data.mesh.Clone();
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
	}
}