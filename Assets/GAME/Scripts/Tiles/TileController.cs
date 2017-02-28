using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileController : MonoBehaviour
{
	[SerializeField]
	public Vector3 position;
	[SerializeField]
	public TileView view;
	public TileModel definition;
	private int spriteID;
	private Transform parent;

	public TileModel GetDefinition { get { return definition; } }
	public TileView GetView { get { return view; } }

	public DragAndDroppable dragAndDroppable;
	private Vector2 mousePosition;
	public static TileController itemBeingDragged;

	public TileMap tilemap;
	public List<Connection> connections = new List<Connection>();
	public List<Checkpoint> checkpoints = new List<Checkpoint>();

	public CharBasicController charBasicController;
	public CharAnimationController charAnimationController;

	void Start()
	{
		if (view == null)
		{
			view = gameObject.GetComponent<TileView>();
			Debug.LogWarning("Te olvidaste de poner el TileView. Lo puse por vos");
		}

		parent = gameObject.GetComponentInParent<Transform>();
		view.ChangeMaterial(gameObject.GetComponent<SpriteRenderer>().material);

		dragAndDroppable = gameObject.AddComponent<DragAndDroppable>();
		dragAndDroppable.tileController = this;
	}

	public void SetDefaultTIle()
	{
		definition = new TileModel(TileModel.Kind.Background, true);
	}

	public virtual void MoveTo(Vector3 direction)
	{
		position += direction;
		gameObject.transform.localPosition += direction;
	}

	public virtual void SetPosition(Vector3 position)
	{
		this.position = position;
		gameObject.transform.localPosition = position;
	}

	public void SetViewByIndex(int ID)
	{
		Sprite s = SpriteManager.self.GetSprite(ID);
		view.ChangeSprite(s);
	}

	public void ShowGlow(bool show)
	{		
		view.GetGlow().TurnGlow(show);
	}

	public void OnEnter(TileController tc)
	{
		for (int i = 0; i < checkpoints.Count; i++)
		{
			checkpoints[i].OnEnter(tc);
		}
	}
}
