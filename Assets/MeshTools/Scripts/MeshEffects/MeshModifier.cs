using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshTools.Scripts.MeshEffects.Modifications{
	[Serializable]
	public class Modeirs:IEnumerable<MeshToolData>{
		public MeshSemplifierData semplifierData;
		public MeshSqueezerData squeezerData;
		public MeshSmootherLepData smootherDataLap;
		public MeshSmootherData smootherDataClasic;
		public MeshSemplifierData FinalSimplifier;
		public IEnumerator<MeshToolData> GetEnumerator(){
			yield return semplifierData;
			yield return squeezerData;
			yield return smootherDataLap;
			yield return smootherDataClasic;
			yield return FinalSimplifier;
		}

		IEnumerator IEnumerable.GetEnumerator(){
			return GetEnumerator();
		}
	}

	public class MeshModifier : MonoBehaviour{
		private MeshFilter _meshFilter;
		public Mesh Original;


		// Start is called before the first frame update

		protected bool _init;

		[Range(1,5)]
		public int Repeat = 2;

		[SerializeField]
		public Modeirs _modeirs ;

		public void Awake(){
			if (_init)
				return;
			_init = true;
			_meshFilter = GetComponent<MeshFilter>();
			Original = _meshFilter.sharedMesh.Clone();
			MadeModifications();
		}

		#if UNITY_EDITOR
		private void OnDrawGizmos(){
		}

		private void OnValidate(){
				_meshFilter = GetComponent<MeshFilter>();
				MadeModifications();
		}

#endif
		private void MadeModifications(){
			Mesh mesh = Original;
			for (int i = 0; i < Repeat; i++){
				
			}

			_meshFilter.mesh = mesh;
		}
	}
}