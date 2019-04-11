using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Realizzer : MonoBehaviour
{
    // Start is called before the first frame update
    public MeshSqueezerData SquizerData;
    private MeshFilter _meshFilter;
    private Mesh _original;
	IMeshTool<MeshSqueezerData> MeshSquzer = new MeshSqueezer();
    void Start()
    {
	    _meshFilter = GetComponent<MeshFilter>();
	    _original = _meshFilter.sharedMesh.Clone();
    }

    // Update is called once per frame
    void Update(){
	    SquizerData.OurMesh = _original;
	    _meshFilter.mesh = MeshSquzer.Process(SquizerData);
    }
}
