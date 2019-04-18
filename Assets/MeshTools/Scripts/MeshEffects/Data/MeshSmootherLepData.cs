using System;
using MeshTools.Scripts.MeshEffects.Modifications;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects{
	[Serializable]
	public class MeshSmootherLepData : MeshToolData{
		[SerializeField]

		[Range(0,10f)]
		public float _step; 

		public int Step{
			get { return (int) _step; }
		}

		public override Mesh GetMesh()
		{
			return new MeshSmootherLaplacianFilter().Process(this);
		}
	}
}