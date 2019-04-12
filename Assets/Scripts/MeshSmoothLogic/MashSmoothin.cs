using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MeshSmoothLogic
{
	public class SmoothingLogic {

		public static Mesh LaplacianFilter (Mesh mesh, int times = 1) {
			mesh.vertices = LaplacianFilter(mesh.vertices, mesh.triangles, times);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			return mesh;
		}

		public static Vector3[] LaplacianFilter(Vector3[] vertices, int[] triangles, int times) {
			var network = VertexConnection.BuildNetwork(triangles);
			for(int i = 0; i < times; i++) {
				vertices = LaplacianFilter(network, vertices);
			}
			return vertices;
		}

		static Vector3[] LaplacianFilter(Dictionary<int, VertexConnection> network, Vector3[] origin) {
			Vector3[] vertices = new Vector3[origin.Length];
			for(int i = 0, n = origin.Length; i < n; i++) {
				var connection = network[i].connection;
				var v = Vector3.zero;
				foreach(int adj in connection) {
					v += origin[adj];
				}
				vertices[i] = v / connection.Count;
			}
			return vertices;
		}

		/*
		 * HC (Humphrey’s Classes) Smooth Algorithm - Reduces Shrinkage of Laplacian Smoother
		 * alpha 0.0 ~ 1.0
		 * beta  0.0 ~ 1.0
		*/
		public static Mesh HcFilter (Mesh mesh, int times = 5, float alpha = 0.5f, float beta = 0.75f) {
			mesh.vertices = HcFilter(mesh.vertices, mesh.triangles, times, alpha, beta);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			return mesh;
		}

		 private static Vector3[] HcFilter(Vector3[] vertices, int[] triangles, int times, float alpha, float beta) {
			alpha = Mathf.Clamp01(alpha);
			beta = Mathf.Clamp01(beta);

			var network = VertexConnection.BuildNetwork(triangles);

			Vector3[] origin = new Vector3[vertices.Length];
			Array.Copy(vertices, origin, vertices.Length);
			for(int i = 0; i < times; i++) {
				vertices = HcFilter(network, origin, vertices, triangles, alpha, beta);
			}
			return vertices;
		}
			
		public static Vector3[] HcFilter(Dictionary<int, VertexConnection> network, Vector3[] o, Vector3[] q, int[] triangles, float alpha, float beta) {
			Vector3[] p = LaplacianFilter(network, q);
			Vector3[] b = new Vector3[o.Length];

			for(int i = 0; i < p.Length; i++) {
				b[i] = p[i] - (alpha * o[i] + (1f - alpha) * q[i]);
			}

			for(int i = 0; i < p.Length; i++) {
				var set = network[i].connection;
				var bs = Vector3.zero;
				foreach(int adj in set) {
					bs += b[adj];
				}
				p[i] = p[i] - (beta * b[i] + (1 - beta) / set.Count * bs);
			}

			return p;
		}

	}

}

