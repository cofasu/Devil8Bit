using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

	[SerializeField]
	private Vector3 position;
	[SerializeField]
	private TileView view;
	private TileModel definition;
	private int spriteID;

	void Start () {

		if (view == null)
		{
			view = gameObject.GetComponent<TileView>();
			Debug.LogWarning("Te olvidaste de poner el TileView. Lo puse por vos");
		}

		view.ChangeMaterial(gameObject.GetComponent<SpriteRenderer>().material);

		SetDefaultTIle();
	}

	public void SetDefaultTIle()
	{
		definition = new TileModel(TileModel.Kind.Background, false);
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
}
