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
	public TileController[,] indexedTiles = new TileController[height, width];	

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
				indexedTiles[y, x] = tc;

				t.transform.localPosition = new Vector3(x, y, 0);

				tc.tilemap = this;
				tc.SetDefaultTIle();
				tc.SetViewByIndex(matrix[y,x]);
				tc.view.SetController(tc);
				tc.view.SetGlow(tc.GetComponentInChildren<Glow>());
				tc.SetCharacterAnimationController(false);
				tc.SetCharBasicController(false);
			}
		}

		for (int i = 0; i < 5; i++)
		{
			int random = UnityEngine.Random.Range(0, tilesControllers.Count);
			tilesControllers[random].GetDefinition.SetDefinition(TileModel.Kind.Droppable, false);
			tilesControllers[random].AddCheckpointFighter();
		}	

		GenerateConnections(indexedTiles);

		ShowDroppable(false);

		AStar astar = new AStar();
		List<AStar.Node> lista = astar.Resolve(tilesControllers[0], tilesControllers[25], false);
		Debug.Log("FoundPath from: " + tilesControllers[0] + " to: " + tilesControllers[25]);
		for (int i = 0; i < lista.Count; i++)
		{
			if (i == lista.Count-1)
				continue;
			Debug.Log(lista[i].tile);
		}		
	}

	public void GenerateConnections(TileController[,] matrix)
	{
		int leftConecction, topConnection, rightConnection, bottomConnection = 0;

		for (int w = 0; w < width; w++)
		{
			for (int h = 0; h < height; h++)
			{
				leftConecction = w;
				leftConecction--;
				topConnection = h;
				topConnection++;
				rightConnection = w;
				rightConnection++;
				bottomConnection = h;
				bottomConnection--;

				if (leftConecction >= 0)
				{
					Connection c = new Connection();
					c.tileA = matrix[h, w];
					c.tileB = matrix[h, leftConecction];
					matrix[h, w].connections.Add(c);
				}

				if (rightConnection < width)
				{
					Connection c = new Connection();
					c.tileA = matrix[h, w];
					c.tileB = matrix[h, rightConnection];
					matrix[h, w].connections.Add(c);
				}

				if (bottomConnection >= 0)
				{
					Connection c = new Connection();
					c.tileA = matrix[h, w];
					c.tileB = matrix[bottomConnection, w];
					matrix[h, w].connections.Add(c);
				}

				if (topConnection < height)
				{
					Connection c = new Connection();
					c.tileA = matrix[h, w];
					c.tileB = matrix[topConnection, w];
					matrix[h, w].connections.Add(c);
				}
			}
		}
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
