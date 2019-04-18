using System;
using MeshTools.Scripts.MeshEffects.Modifications;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects{
	[Serializable]
	public class MeshSqueezerData : MeshToolData{

		[SerializeField]
		private Vector3 _rotation;
		[HideInInspector]
		public Quaternion Rotation => Quaternion.Euler(_rotation);

		public DetectionType CenterType;
		public override Mesh GetMesh()
		{
			return new MeshSqueezer().Process(this);
		}

	}
}