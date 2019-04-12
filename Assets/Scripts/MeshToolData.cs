using System;
using UnityEngine;

namespace Assets.Scripts{
	[Serializable]
	public class MeshToolData{
		public Mesh mesh;
		[Range(0,1f)]
		public float Force;
	}
}