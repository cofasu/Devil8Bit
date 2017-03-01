using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

	public int ID;
	[SerializeField]
	public const int width = 11;
	public const int height = 23;
	public Vector3 position;
	public Transform parent;
	public Transform background;
	public Transform characters;
	public Transform road;
	public GameObject tile;

	public List<TileController> backgroundTileControllers = new List<TileController>();
	public TileController[,] backgroundIndexedTiles = new TileController[height, width];

	public List<TileController> allyCharacterTileControllers = new List<TileController>();
	public List<TileController> enemyCharacterTileControllers = new List<TileController>();

	void Start()
	{
		int[,] matrix = new int[height, width] {    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0},
													{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0}};

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

	//public void ChangeParent(Transform newParent)
	//{
	//	parent = newParent;
	//	gameObject.transform.SetParent(newParent.transform);
	//}

	public void StartTileMap(int[,] matrix)
	{
		//Spawn Background
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var t = GameObject.Instantiate(tile, background, false);
				t.name = x + "," + y;

				TileController tc = t.GetComponent<TileController>();

				backgroundTileControllers.Add(tc);
				backgroundIndexedTiles[y, x] = tc;

				t.transform.localPosition = new Vector3(x, y, 0);

				tc.position = t.transform.localPosition;

				tc.tilemap = this;
				tc.SetDefaultTIle();
				tc.SetViewByIndex(matrix[y, x]);
				tc.view.SetController(tc);
				tc.view.SetGlow(tc.GetComponentInChildren<Glow>());
				tc.view.spriteRenderer.sortingOrder = 0;
				//tc.SetCharacterAnimationController(false);
				//tc.SetCharBasicController(false);
			}
		}

		//Spawn Roads
		for (int i = 0; i < 10; i++)
		{
			//Getmatrix
			//Spawn like background
		}

		//Spawn Characters
		int allyI = 22;
		for (int i = 0; i < 10; i++)
		{
			var t = GameObject.Instantiate(tile, characters, false);
			TileController tc = t.GetComponent<TileController>();		

			if(i<5)
				t.transform.localPosition = new Vector3(i, 0, 0);
			else
				t.transform.localPosition = new Vector3(i-5, allyI, 0);

			tc.position = t.transform.localPosition;

			if (i < 5)
			{
				t.name = "Enemy:" + tc.position.x + "," + tc.position.y;
				enemyCharacterTileControllers.Add(tc);
			}
			else
			{
				t.name = "Ally:" + tc.position.x + "," + tc.position.y;
				allyCharacterTileControllers.Add(tc);
			}


			tc.SetDefaultTIle();
			tc.GetDefinition.SetDefinition(TileModel.Kind.Walker, false);
			tc.SetCharacterAnimationController(true);
			tc.SetCharBasicController(false, false, true);
			tc.view.spriteRenderer.sortingOrder = 1;
		}
		GenerateConnections(backgroundIndexedTiles);

		ShowDroppable(false);

		//AStar astar = new AStar();
		//List<AStar.Node> lista = astar.Resolve(backgroundTileControllers[0], backgroundTileControllers[25], false);
		//Debug.Log("FoundPath from: " + backgroundTileControllers[0] + " to: " + backgroundTileControllers[25]);
		//for (int i = 0; i < lista.Count; i++)
		//{
		//	if (i == lista.Count - 1)
		//		continue;
		//	Debug.Log(lista[i].tile);
		//}

		for (int i = 0; i < 5; i++)
		{
			TileController tc = allyCharacterTileControllers[i];
			TileController closestEnemy = GetClosestEnemy(tc);
			AStar a = new AStar();
			List<AStar.Node> path = a.Resolve(	backgroundIndexedTiles[(int)tc.position.y,
												(int)tc.position.x], backgroundIndexedTiles[(int)closestEnemy.position.y, 
												(int)closestEnemy.position.x], false);
			if(path != null)
			{
				for (int k = path.Count-1; k > 0; k--)
				{
					if (k == path.Count - 1)
						continue;
					tc.charBasicController.AddCharPosition = path[k].tile.transform.localPosition;
					//Debug.Log("Added to: " + tc + " position: " + path[k].tile.transform.localPosition);
				}
			}
			else
			{
				Debug.Log("Path NUll");
			}
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

	public void ShowDroppable(bool show)
	{
		List<TileController> droppables = GetDroppables();
		for (int i = 0; i < droppables.Count; i++)
		{
			if (droppables[i].definition.kind == TileModel.Kind.Droppable)
			{
				droppables[i].ShowGlow(show);
			}
		}
	}

	public List<TileController> GetDroppables()
	{
		List<TileController> tiles = new List<TileController>();

		for (int i = 0; i < backgroundTileControllers.Count; i++)
		{
			TileController tc = backgroundTileControllers[i];
			if (tc.definition.kind == TileModel.Kind.Droppable)
				tiles.Add(tc);
		}

		return tiles;
	}

	public List<TileController> FilterByDistance(List<TileController> toFilter, TileController with, float distance)
	{
		for (int i = 0; i < toFilter.Count; i++)
		{
			if (Vector3.Distance(with.transform.position, toFilter[i].transform.position) > distance)
			{
				toFilter.RemoveAt(i);
				i--;
			}
		}

		return toFilter;
	}

	float kDistance = 1;
	TileController lastClosest = null;
	public TileController GetClosestEnemy(TileController current)
	{
		lastClosest = null;
		List<TileController> enemyList;
		if(allyCharacterTileControllers.Contains(current))
		{
			enemyList = enemyCharacterTileControllers;
		}
		else
		{
			enemyList = allyCharacterTileControllers;
		}
		
		for (int i = 0; i < enemyList.Count; i++)
		{
			if (lastClosest != null)
			{
				if (Vector3.Distance(current.transform.localPosition, lastClosest.transform.localPosition) < Vector3.Distance(current.transform.localPosition, enemyList[i].transform.localPosition))
					continue;

				lastClosest = enemyList[i];
			}
			else
			{
				lastClosest = enemyList[i];
			}
		}

		//Debug.Log("LastClosest returned: " + lastClosest.position + " To: " + current.position);
		return lastClosest;
	}


}
