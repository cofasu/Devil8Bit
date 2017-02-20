using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class TileModel  {

	[SerializeField]
	private bool dragable;

	public enum Kind
	{
		//Map settings
		Walkable,
		Background,
		Destructable,
		Occupied,
		Buildable,
		Droppable,
		//Character
		Walker,
		Tower,
		Mage,
		Devil,
		Temporary
	}

	[SerializeField]
	public Kind kind;	

	public TileModel(Kind kind, bool dragable)
	{
		this.kind = kind;
		this.dragable = dragable;
	}

	public virtual void SetDefinition(Kind kind, bool dragable)
	{
		this.kind = kind;
		this.dragable = dragable;
	}

	public virtual void SetKind(Kind kind)
	{
		this.kind = kind;
	}

	public virtual void SetDraggable(bool dragable)
	{
		this.dragable = dragable;
	}

	public bool GetDragable { get { return dragable; } }
}
