using Assets.Scripts;
using UnityEngine;

public class MeshModifier : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshSqueezerData squeezerData;
    public MeshSmootherData smootherData;
    private MeshFilter _meshFilter;
    private Mesh _original;
    readonly IMeshTool<MeshSqueezerData> _meshSqueezer = new MeshSqueezer();
	readonly IMeshTool<MeshSmootherData> _meshSmoother = new MeshSmoother();
    public  void Start()
    {
	    _meshFilter = GetComponent<MeshFilter>();
	    _original = _meshFilter.sharedMesh.Clone();
    }

    public void Update(){
	    squeezerData.mesh = _original;

	     Mesh middleMesh = _meshSqueezer.Process(squeezerData);
	     smootherData.mesh = middleMesh;
	    _meshFilter.mesh = _meshSmoother.Process(smootherData);

    }
}
