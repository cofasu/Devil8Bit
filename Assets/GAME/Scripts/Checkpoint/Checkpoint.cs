using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Checkpoint : MonoBehaviour {
		
	public abstract void OnEnter(TileController tc);
	public abstract void OnExit(TileController tc);		
}
