using UnityEngine;

namespace Assets.Scripts{
	public interface IMeshTool<T> where T:MeshToolData{
		Mesh Process(T data);
	}

}