using MeshTools.Scripts.MeshSmoothLogic;
using UnityEngine;

namespace MeshTools.Scripts{
	public class MeshSmoother:IMeshTool<MeshSmootherData>{
		public Mesh Process(MeshSmootherData data){
			return SmoothingLogic.HcFilter(data.mesh.Clone(),(int)(data.Force*10),data.Alpha,data.Beta);
		}
	}
}