using System;
using MeshTools.Scripts.MeshEffects.MeshSimplifierLogic;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects.Modifications{
	public class MeshSimplifierR:AbstractMeshTool<MeshSemplifierData>{
		protected override Mesh DoProcessing(MeshSemplifierData data){
			MeshSimplifier simplifier = new MeshSimplifier(data.mesh);
			simplifier.SimplifyMesh(data.Force);
			return simplifier.ToMesh();

		}
	}

	[Serializable]
	public class MeshSemplifierData : MeshToolData{
		public override Mesh GetMesh()
		{
			return new MeshSimplifierR().Process(this);
		}

	}
}