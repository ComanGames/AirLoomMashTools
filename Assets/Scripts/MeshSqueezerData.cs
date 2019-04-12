using System;
using UnityEngine;

namespace Assets.Scripts{
	[Serializable]
	public class MeshSqueezerData : MeshToolData{

		[SerializeField]
		private Vector3 _rotation;
		[HideInInspector]
		public Quaternion Rotation => Quaternion.Euler(_rotation);

		public DetectionType CenterType;
		}
}