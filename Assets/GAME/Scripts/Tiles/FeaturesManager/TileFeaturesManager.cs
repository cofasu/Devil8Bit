using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileFeaturesManager  {

	public static void SetDragAndDroppable(this TileController tc, TileModel.Kind droppableKind, bool draggable = false, bool droppable = true)
	{
		tc.dragAndDroppable = tc.gameObject.AddComponent<DragAndDroppable>();
		tc.dragAndDroppable.droppableKind = droppableKind;
		tc.dragAndDroppable.tileController = tc;
		tc.GetDefinition.SetDraggable(draggable);		
	}

	public static void SetCharacterAnimationController(this TileController tc, bool isAnimated = false, float speed = 5)
	{
		CharAnimationController charAnim = tc.gameObject.AddComponent<CharAnimationController>();
		charAnim.LoadSpriteCharSet("SpriteSets/Char/201-blkmage01");
		charAnim.CreateSpriteFrames();

		charAnim.TargetSpriteRenderer = tc.GetView.spriteRenderer;
		charAnim.IsAnimated = isAnimated;
		charAnim.AnimSpeed = speed;

		tc.charAnimationController = charAnim;
	}

	public static void SetCharBasicController(this TileController tc, bool isMoving = false, bool axisInput = false, bool aStarMove = true)
	{
		tc.charBasicController = tc.gameObject.AddComponent<CharBasicController>();
		tc.charBasicController.isMoving = isMoving;
		tc.charBasicController.tileController = tc;

		tc.charBasicController._axisInputMov = axisInput;
		tc.charBasicController._AestrellaMov = aStarMove;
	}

	public static void AddCheckpointFighter(this TileController tc)
	{
		tc.checkpoints.Add(tc.gameObject.AddComponent<CheckpointFighterDrop>());
	}	

	public static void AddGlow(this TileController tc, Color color)
	{
		tc.GetView.SetGlow(tc.GetComponentInChildren<Glow>());
	}
}
