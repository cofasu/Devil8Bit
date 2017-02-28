using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{

	private SpriteRenderer spriteRenderer;
	private bool isGlowOn;
	private Color alphaZero;
	private Color alphaOne;

	void Awake()
	{
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start()
	{
		alphaOne = alphaZero = Color.white;
		alphaZero.a = 0;
		TurnGlow(isGlowOn);
	}

	public void TurnGlow(bool on)
	{
	
		isGlowOn = on;

		if (on)
			spriteRenderer.color = Color.Lerp(spriteRenderer.color, alphaOne, 1);
		else
			spriteRenderer.color = Color.Lerp(spriteRenderer.color, alphaZero, 1);

	}
}
