using UnityEngine;
using System.Collections;
/*
Place this on an agent so it knows the possible
agents available. This circumvents having to use
FindObjectWithTag. Using this enum, many objects 
can have more than one "name" or tag.
*/
public class Aspect : MonoBehaviour {
	public enum aspect {
        None,
		Player,
		Enemy,
		Civilian
	}
	public aspect aspectName;
}
