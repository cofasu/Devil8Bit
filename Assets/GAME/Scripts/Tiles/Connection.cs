using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[SerializeField]
public class Connection {
	public TileController tileA;
	public TileController tileB;
	public bool removable = false;
	public bool revealed = true;

	public void MarkAsRevealed() {
		revealed = true;
	}

	public bool IsRevealed() {
		return revealed;
	}

	public float GetDistance() {
		Debug.Log(Vector3.Distance(tileA.position, tileB.position));
		return Vector3.Distance(tileA.position, tileB.position);
	}

	public TileController GetOpposedSite(TileController source)
	{
		if (tileA == source)
			return tileB;
		return tileA;
	}	
}
