using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpriteManager : MonoBehaviour
{
	public static SpriteManager self;
	[SerializeField]
	private Sprite[] indexer;

	void Awake()
	{
		if(self == null)
			self = this;
		else
		{
			Destroy(this);
		}
	}

	public Sprite GetSprite(string name)
	{
		var sprite = indexer.Where(s => s.name == name).ToList();

		if(sprite.Count > 1)
		{
			Debug.LogError("Pediste un nombre del que hay mas de uno en el manager");
		}
		
		return sprite[0];
	}

	public Sprite GetSprite(int id)
	{
		if(indexer.Length < id)
		{
			Debug.LogError("El array es mas chico que el ID que pediste");
		}

		if(indexer[id] == null)
		{
			Debug.LogError("Pediste un ID que es nulo");
		}	

		return indexer[0];
	}
}
