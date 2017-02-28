using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFighterDrop : Checkpoint {

	public delegate void Enter();
	public delegate void Exit();

	public Enter enter;
	public Exit exit;

	void Start()
	{

	}

	public override void OnEnter(TileController tc)
	{
		Debug.Log("OnEnter");
		if(tc != null)
		{
			if(tc.charBasicController != null)
			{
				tc.charBasicController.isMoving = true;
				tc.charBasicController._axisInputMov = false;
				tc.charBasicController._AestrellaMov = true;
				tc.charAnimationController.IsAnimated = true;
			}
			else
			{
				Debug.Log("CharBasicController = null");
			}
		}
		else
		{
			Debug.Log("TC = null");
		}

		if (enter == null)
			return;

		enter();
		enter = null;
	}

	public override void OnExit(TileController tc)
	{
		if (exit == null)
			return;

		exit();
		exit = null;
	}
}
