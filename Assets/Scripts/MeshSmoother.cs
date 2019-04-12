using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts{
	public class MeshSmoother:IMeshTool<MeshSmootherData>{
		public Mesh Process(MeshSmootherData data){
			return MeshSmoothLogic.SmoothingLogic.HcFilter(data.mesh.Clone(),(int)(data.Force*10),data.Alpha,data.Beta);
		}
	}
}