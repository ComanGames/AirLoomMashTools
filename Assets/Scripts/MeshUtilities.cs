using System.Linq;
using UnityEngine;

namespace Assets.Scripts{
	public static class MeshUtilities{
		public static Mesh Clone(this Mesh m){
		 return	(Mesh)Object.Instantiate(m);
		}
	}
}
