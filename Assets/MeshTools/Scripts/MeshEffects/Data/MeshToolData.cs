using System;
using MeshTools.Scripts.MeshEffects.Modifications;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects{
	[Serializable]
	public abstract class MeshToolData{

		[SerializeField]
		public bool Active = false;
		public Mesh mesh;
		[Range(0,1f)]
		public float Force;
		public abstract Mesh GetMesh();
	}
}