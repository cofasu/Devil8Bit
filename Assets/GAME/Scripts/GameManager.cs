using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Transform levelsParent;
	public GameObject tileMapPrefab;
	public List<TileMapController> levels = new List<TileMapController>();

	// Use this for initialization
	void Start () {

		GameObject level1 = GameObject.Instantiate(tileMapPrefab, levelsParent, false);
		TileMapController tmc = level1.GetComponent<TileMapController>();
		tmc.StartTileMap(MatrixReader.GetLevelMatrix("/Level1Background.json"), MatrixReader.GetLevelMatrix("/Level1Roads.json"));
		levels.Add(tmc);		
		level1.transform.localPosition = new Vector3(0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
