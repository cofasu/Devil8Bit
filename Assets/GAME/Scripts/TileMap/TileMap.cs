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
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				var t = GameObject.Instantiate(tile, parent, false);

				t.name = x + "," + y;

				allTiles.Add(t);
				indexedTiles[x, y] = t;

				t.transform.localPosition = new Vector3(x, y, 0);

				TileController tc = t.GetComponent<TileController>();
				tc.SetDefaultTIle();
				tc.SetViewByIndex(matrix[y,x]);				
			}
		}
	}

	public void Instantiate(string [,] matrix)
	{

	}
}
