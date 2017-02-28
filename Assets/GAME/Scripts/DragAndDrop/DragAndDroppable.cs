using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class DragAndDroppable : MonoBehaviour
{
	void Update()
	{
		UpdateDrag();
	}

	public TileModel.Kind droppableKind;

	Vector2 mousePosition;
	Vector3 originalPosition;
	public TileController tileController;

	public void UpdateDrag()
	{
		if (!tileController.GetDefinition.GetDragable)
			return;

		if (TileController.itemBeingDragged == null)
		{
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//Checks whether the mouse is over the sprite
			bool overSprite = tileController.GetView.spriteRenderer.bounds.Contains(mousePosition);
			//If it's over the sprite
			if (overSprite)
			{
				//If we've pressed down on the mouse (or touched on the iphone)
				if (Input.GetButton("Fire1"))
				{
					TileController.itemBeingDragged = tileController;
					tileController.tilemap.ShowDroppable(true);
					originalPosition = tileController.transform.position;
					//Set the position to the mouse position
				}
			}
		}
		else if (TileController.itemBeingDragged == tileController)
		{
			if (Input.GetButton("Fire1"))
			{
				this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
										Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
										  0.0f);
				//Set the position to the mouse position

			}
			else
			{
				TileController.itemBeingDragged = null;
				tileController.tilemap.ShowDroppable(false);

				var droppables = tileController.tilemap.GetDroppables();
				TileController lastClosest = null;
				float kDistance = 1;
				for (int i = 0; i < droppables.Count; i++)
				{
					if (Vector3.Distance(tileController.transform.position, droppables[i].transform.position) > kDistance)
						continue;
					
					if (lastClosest != null)
					{
						if (Vector3.Distance(droppables[i].transform.position, lastClosest.transform.position) < Vector3.Distance(tileController.transform.position, droppables[i].transform.position))
							continue;

						if (tileController.GetDefinition.kind != droppableKind)
							continue;

						lastClosest = droppables[i];
						Debug.Log("Replaced lastClosest");
					}
					else
					{
						lastClosest = droppables[i];
					}
				}

				if (lastClosest != null)
				{
					this.transform.position = lastClosest.transform.position;
					Debug.Log("DroppedOn:" + lastClosest.transform.position + lastClosest.name);
					tileController.GetView.spriteRenderer.color = Color.green;
					tileController.definition.SetDraggable(false);
					lastClosest.OnEnter(tileController);										
				}					
				else
					this.transform.position = originalPosition;
			}
		}
	}
}
