using System;
using UnityEngine;

namespace Assets.Scripts{
	[Serializable]
	public class MeshSqueezerData : MeshToolData{

		[SerializeField]
		private Vector3 _rotation;
		[HideInInspector]
		public Quaternion Rotation => Quaternion.Euler(_rotation);

		[Range(0,1f)]
		public float Force;
		public DetectionType CenterType;
	}

	public enum DetectionType{

		CurrentCenter,
		VerticesAverage,
		LowestPoint,
		MixedType,
		GeometryCenter
	}
}