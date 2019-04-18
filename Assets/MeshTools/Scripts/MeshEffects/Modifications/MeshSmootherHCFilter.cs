using MeshTools.Scripts.MeshEffects.MeshSmoothLogic;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects.Modifications{
	public class MeshSmootherHCFilter:AbstractMeshTool<MeshSmootherData>{
		protected override Mesh DoProcessing(MeshSmootherData data){
			return SmoothingLogic.HcFilter(data.mesh.Clone(),(int)(data.Force*20),data.Alpha,data.Beta);
		}
	}
}