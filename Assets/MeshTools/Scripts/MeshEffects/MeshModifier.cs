using UnityEngine;

namespace MeshTools.Scripts{
	public class MeshModifier : MonoBehaviour{
		private readonly IMeshTool<MeshSqueezerData> _meshSqueezer = new MeshSqueezer();
		private readonly IMeshTool<MeshSmootherData> _meshSmoother = new MeshSmoother();
		private MeshFilter _meshFilter;
		public Mesh Original;

		public MeshSmootherData smootherData;

		// Start is called before the first frame update
		public MeshSqueezerData squeezerData;

		public bool _init;
		public void Awake(){
			if (_init)
				return;
			_init = true;
			_meshFilter = GetComponent<MeshFilter>();
			Original = _meshFilter.sharedMesh.Clone();
			MadeModifications();
		}


		private void MadeModifications(){

			squeezerData.mesh = Original;
			var middleMesh = _meshSqueezer.Process(squeezerData);
			smootherData.mesh = middleMesh;
			_meshFilter.mesh = _meshSmoother.Process(smootherData);
		}
	}
}