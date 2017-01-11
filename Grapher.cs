using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Grapher : MonoBehaviour {

	public Transform[] points;

	private void Update () {
		
		for (int i = 0; i < points.Length; i++) {
			Vector3 p = points[i].transform.position;
			p.y = Sine(p.z);
			points[i].position = p;
		}
	}
		
	private static float Sine (float x){
		return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x + Time.timeSinceLevelLoad*5);
	}
}