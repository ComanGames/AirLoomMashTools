using System;
using MeshTools.Scripts.MeshEffects.Modifications;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects{
	[Serializable]
	public class MeshSmootherData : MeshToolData{
		[Range(0, 1)] 
		public float Alpha = 0.5f;
		[Range(0,1)]
		public float Beta = 0.5f;

		public override Mesh GetMesh(){
			return new MeshSmootherHCFilter().Process(this);
		}
	}
}