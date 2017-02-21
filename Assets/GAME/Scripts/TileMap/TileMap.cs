using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

	public int ID;
	[SerializeField]
	public const int width = 11;
	public const int height = 23;
	public Vector3 position;
	public Transform parent;
	public GameObject tile;

	public List<GameObject> allTiles;
	public List<TileController> tilesControllers;
	public GameObject[,] indexedTiles = new GameObject[width, height];	

	void Start()
	{
		int[,] matrix = new int[height, width] {    { 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1},
													{ 0, 1, 2, 3, 0, 1, 2, 3, 0, 1 ,1}};

		StartTileMap(matrix);
	}

	public void SetPosition(Vector3 position)
	{
		this.position = position;
		gameObject.transform.localPosition = position;
	}

	public void MoveTo(Vector3 direction)
	{
		this.position += direction;
		gameObject.transform.localPosition = position;
	}

	public void ChangeParent(Transform newParent)
	{
		parent = newParent;
		gameObject.transform.SetParent(newParent.transform);
	}
	
	public void StartTileMap(int[,] matrix)
	{
		allTiles = new List<GameObject>();
		tilesControllers = new List<TileController>();
		
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var t = GameObject.Instantiate(tile, parent, false);
				TileController tc = t.GetComponent<TileController>();
				t.name = x + "," + y;

				allTiles.Add(t);
				tilesControllers.Add(tc);
				indexedTiles[x, y] = t;

				t.transform.localPosition = new Vector3(x, y, 0);

				tc.tilemap = this;
				tc.SetDefaultTIle();
				tc.SetViewByIndex(matrix[y,x]);
				tc.view.SetController(tc);
				tc.view.SetGlow(tc.GetComponentInChildren<Glow>());
			}
		}

		for (int i = 0; i < 5; i++)
		{
			int random = UnityEngine.Random.Range(0, tilesControllers.Count);
			tilesControllers[random].definition.SetDefinition(TileModel.Kind.Droppable, false);			
		}

		ShowDroppable(false);
	}

	public void Instantiate(string [,] matrix)
	{

	}

	public void ShowDroppable(bool show)
	{
		List<TileController> droppables = GetDroppables();
		for (int i = 0; i < droppables.Count; i++)
		{
			if(droppables[i].definition.kind == TileModel.Kind.Droppable)
			{
				droppables[i].ShowGlow(show);
			}
		}
	}

	public List<TileController> GetDroppables()
	{
		List<TileController> tiles = new List<TileController>();

		for (int i = 0; i < tilesControllers.Count; i++)
		{
			TileController tc = tilesControllers[i];
			if (tc.definition.kind == TileModel.Kind.Droppable)
				tiles.Add(tc);
		}

		return tiles;
	}
}
