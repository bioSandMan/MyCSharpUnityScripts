using UnityEngine;
using System.Collections;

namespace HydraGOAP {

	// need a layer manager to keep track of all the possible shootable objects
	public class LayerManager {

		private static int layerCreature=30;
		private static int layerCiv=29;
		private static int layerShootObj=28;
		private static int layerTerrain=27;

		public static int LayerCreature(){ return layerCreature; }
		public static int LayerCiv(){ return layerCiv; }
		public static int LayerShootObject(){ return layerShootObj; }

		public static int LayerTerrain(){ return layerTerrain; }
		public static int LayerUI(){ return 5; }	//layer5 is named UI by Unity's default

	}

}
