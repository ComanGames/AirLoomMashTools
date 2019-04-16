using System;
using UnityEngine;

namespace MeshTools.Scripts{
	[Serializable]
	public class MeshSmootherData : MeshToolData{
		[Range(0, 1)] 
		public float Alpha = 0.5f;
		[Range(0,1)]
		public float Beta = 0.5f;
	}
}