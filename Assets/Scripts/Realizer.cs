using Assets.Scripts;
using UnityEngine;

public class Realizer : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshSqueezerData SquizerData;
    public MeshSmootherData SmootherData;
    private MeshFilter _meshFilter;
    private Mesh _original;
	IMeshTool<MeshSqueezerData> MeshSquzer = new MeshSqueezer();
	IMeshTool<MeshSmootherData> Meshsmoother = new MeshSmoother();
    void Start()
    {
	    _meshFilter = GetComponent<MeshFilter>();
	    _original = _meshFilter.sharedMesh.Clone();
    }

    // Update is called once per frame
    void Update(){
	    SquizerData.mesh = _original;

	     Mesh middleMesh = MeshSquzer.Process(SquizerData);
	     SmootherData.mesh = middleMesh;
	    _meshFilter.mesh = Meshsmoother.Process(SmootherData);

    }
}
