using MeshTools.Scripts.MeshEffects.MeshSmoothLogic;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects.Modifications{
	public class MeshSmootherLaplacianFilter : AbstractMeshTool<MeshSmootherLepData>{
		protected override Mesh DoProcessing(MeshSmootherLepData data){
			return SmoothingLogic.LaplacianFilter(data.mesh.Clone(), (int) (data.Force * 10));
		}
	}
}