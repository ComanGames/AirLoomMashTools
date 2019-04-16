using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MeshTools.Scripts.VisualEffects{
	[RequireComponent(typeof(RawImage))]
	public class EdgeSmoothingManager:MonoBehaviour{

		public float ScaleMultiply=0.05f;
		public LayerMask VisibleLayer;
		public LayerMask MaskLayer;
		public Material OneColorMaterial;
		public static bool _init = false;
		public RawImage Target;

		#region  Blur Params
        private List<Material> createdMaterials = new List<Material> ();
        private RenderTexture _maskTargetTexture;
        private RenderTexture _visualTargetTexture;
		

		[Range(0, 2)]
		public int downsample = 1;

		public enum BlurType
		{
			StandardGauss = 0,
			SgxGauss = 1,
		}

		[Range(0.0f, 10.0f)]
		public float blurSize = 3.0f;

		[Range(1, 4)]
		public int blurIterations = 2;

		public BlurType blurType = BlurType.StandardGauss;

		public Shader blurShader = null;
		private Material blurMaterial = null;
		#endregion


		private void Update(){

			RenderTexture newText=			BlurTexture(_maskTargetTexture);

			Target.material.SetTexture("_AlphaTex",newText);
		}
		private void Awake(){
			
			Target = GetComponent<RawImage>();
			Target.enabled = true;
			Camera main = Camera.main;
			Transform t = main.transform;

			GameObject visualCam = SetCloneToParent(new GameObject("Visual Camera"),t.gameObject);

			GameObject maskCam = SetCloneToParent(new GameObject("Mask Camera"),t.gameObject);
			

			Camera visual = CopyCamera(main, visualCam);
			Camera mask = CopyCamera(main, maskCam);

			visual.cullingMask = VisibleLayer;
			mask.cullingMask = MaskLayer;

			_maskTargetTexture = new RenderTexture(Screen.width,Screen.height,32,RenderTextureFormat.ARGBInt);
			_visualTargetTexture = new RenderTexture(Screen.width,Screen.height,32,RenderTextureFormat.ARGBInt);

			mask.backgroundColor = Color.black;
			visual.targetTexture = _visualTargetTexture;
			mask.targetTexture = _maskTargetTexture;

			Target.texture = _visualTargetTexture;
			Target.material.SetTexture("_AlphaTex",_maskTargetTexture);
	
		}

		public void Start(){
			
			MadeHiddenObjects();
		}

		private void MadeHiddenObjects(){

			if (_init)
				return;
			_init = true;

			GameObject[] toHide = GameObject.FindGameObjectsWithTag("Mask");

			foreach (GameObject g in toHide){
				HideObject(g);
			}
		}

		private void HideObject(GameObject current){

			GameObject clone = new GameObject("Mask");
			SetCloneToParent(clone, current);

			current.layer = LayerToInt(VisibleLayer);
			clone.layer = LayerToInt(MaskLayer);

			clone.transform.localScale = current.transform.localScale * (1 - ScaleMultiply);
			MeshFilter f = clone.AddComponent<MeshFilter>();
			MeshRenderer r = clone.AddComponent<MeshRenderer>();
			r.material = OneColorMaterial;
			f.mesh = current.GetComponent<MeshFilter>().mesh;
		}

		private static GameObject SetCloneToParent( GameObject clone, GameObject current){
			clone.transform.parent = current.transform;
			clone.transform.position = current.transform.position;
			clone.transform.rotation = current.transform.rotation;
			return clone;
		}

		public static int LayerToInt(int input){
				int result = 0;
				if (input < 2)
					return 1;

				while (input > 1){
					input = input >> 1;
					result++;
				}

				return result;
			}

		Camera CopyCamera(Camera original, GameObject destination) 
		{
			Camera copy = destination.AddComponent<Camera>();
			copy.CopyFrom(original);
			return copy;
		}


        protected Material CheckShaderAndCreateMaterial ( Shader s, Material m2Create)
		{
            if (!s)
			{
                Debug.Log("Missing shader in " + ToString ());
                enabled = false;
                return null;
            }

            if (s.isSupported && m2Create && m2Create.shader == s)
                return m2Create;

            if (!s.isSupported)
			{
                Debug.Log("The shader " + s.ToString() + " on effect "+ToString()+" is not supported on this platform!");
                return null;
            }

            m2Create = new Material (s);
            createdMaterials.Add (m2Create);
            m2Create.hideFlags = HideFlags.DontSave;

            return m2Create;
		}
		public RenderTexture BlurTexture (RenderTexture source) {


            blurMaterial = CheckShaderAndCreateMaterial (blurShader, blurMaterial);

            float widthMod = 1.0f / (1.0f * (1<<downsample));

            blurMaterial.SetVector ("_Parameter", new Vector4 (blurSize * widthMod, -blurSize * widthMod, 0.0f, 0.0f));
            source.filterMode = FilterMode.Bilinear;

            int rtW = source.width >> downsample;
            int rtH = source.height >> downsample;

            // downsample
            RenderTexture rt = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);

            rt.filterMode = FilterMode.Bilinear;
            Graphics.Blit (source, rt, blurMaterial, 0);

            var passOffs= blurType == BlurType.StandardGauss ? 0 : 2;

            for(int i = 0; i < blurIterations; i++) {
                float iterationOffs = (i*1.0f);
                blurMaterial.SetVector ("_Parameter", new Vector4 (blurSize * widthMod + iterationOffs, -blurSize * widthMod - iterationOffs, 0.0f, 0.0f));

                // vertical blur
                RenderTexture rt2 = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
                rt2.filterMode = FilterMode.Bilinear;
                Graphics.Blit (rt, rt2, blurMaterial, 1 + passOffs);
                RenderTexture.ReleaseTemporary (rt);
                rt = rt2;

                // horizontal blur
                rt2 = RenderTexture.GetTemporary (rtW, rtH, 0, source.format);
                rt2.filterMode = FilterMode.Bilinear;
                Graphics.Blit (rt, rt2, blurMaterial, 2 + passOffs);
                RenderTexture.ReleaseTemporary (rt);
                rt = rt2;
            }

            return rt;

		}
	}
}