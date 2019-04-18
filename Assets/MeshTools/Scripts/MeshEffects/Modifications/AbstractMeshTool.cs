using UnityEngine;

namespace MeshTools.Scripts.MeshEffects.Modifications{
	public abstract class AbstractMeshTool<T> where T:MeshToolData{
		protected abstract Mesh DoProcessing(T data);

		public Mesh Process(T data){
			
			if(data.Active)
				return	DoProcessing(data);

			return data.mesh;


		}

	}
}