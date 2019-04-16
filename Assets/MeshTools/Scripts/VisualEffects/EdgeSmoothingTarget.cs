using UnityEngine;

namespace MeshTools.Scripts.VisualEffects{
	public class EdgeSmoothingTarget : MonoBehaviour{

		public float ScaleMultiply=0.05f;
		public LayerMask VisibleLayer;
		public LayerMask MaskLayerNumber;
		public Material OneColorMaterial;
		public static bool _init = false;
		private void Start(){

			if(_init)
				return;

			GameObject clone = Instantiate(new GameObject("Mask"),transform);
			gameObject.layer =  LayerToInt(VisibleLayer);
			clone.layer =  LayerToInt(MaskLayerNumber);
			clone.transform.localScale = transform.localScale*(1-ScaleMultiply);
			transform.localScale *= (1+ScaleMultiply);
			MeshFilter f= clone.AddComponent<MeshFilter>(); 
			MeshRenderer r= clone.AddComponent<MeshRenderer>(); 
			r.material = OneColorMaterial;
			f.mesh = GetComponent<MeshFilter>().mesh;
			_init = true;
		}

		public int LayerToInt(int input){

			int result=0;
			if (input < 2)
				return 1;

			while (input>1){
				input= input >> 1;
				result++;
			}
			return result;
		}
	}
}