using UnityEngine;

namespace MeshTools.Scripts{
	public interface IMeshTool<in T>  where T:MeshToolData{
		Mesh Process(T data);
	}

}