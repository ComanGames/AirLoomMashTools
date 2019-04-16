using UnityEngine;

namespace MeshTools.Scripts{
	public class MeshModifier : MonoBehaviour{
		private readonly IMeshTool<MeshSqueezerData> _meshSqueezer = new MeshSqueezer();
		private readonly IMeshTool<MeshSmootherData> _meshSmoother = new MeshSmoother();
		private MeshFilter _meshFilter;
		private Mesh _original;

		public MeshSmootherData smootherData;

		// Start is called before the first frame update
		public MeshSqueezerData squeezerData;

		public void Awake(){
			MadeModifications();
		}

#if UNITY_EDITOR
		private bool _guiInit = false;
		private void OnDrawGizmosSelected(){
			if (!_guiInit){
				_meshFilter = GetComponent<MeshFilter>();
				_original = _meshFilter.sharedMesh.Clone();
				_guiInit = true;
			}
			squeezerData.mesh = _original;
			var middleMesh = _meshSqueezer.Process(squeezerData);
			smootherData.mesh = middleMesh;
			_meshFilter.mesh = _meshSmoother.Process(smootherData);
		}
#endif


		private void MadeModifications(){
			_meshFilter = GetComponent<MeshFilter>();
			_original = _meshFilter.sharedMesh.Clone();
			squeezerData.mesh = _original;
			var middleMesh = _meshSqueezer.Process(squeezerData);
			smootherData.mesh = middleMesh;
			_meshFilter.mesh = _meshSmoother.Process(smootherData);
		}
	}
}