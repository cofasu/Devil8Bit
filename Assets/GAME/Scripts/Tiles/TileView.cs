using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileView : MonoBehaviour {
	
	public SpriteRenderer spriteRenderer;
	public Animator anim;	
	private Material material;
	private Sprite sprite;
	private Glow glow;
	private TileController tileController;

	public void ChangeSprite(Sprite sprite)
	{
		this.sprite = sprite;
		spriteRenderer.sprite = sprite;
	}
	/// <summary>
	/// Hace un Resources.Load<Image> y setea el native size.
	/// </summary>
	/// <param name="path"></param>
	public void ChangeSprite(string path)
	{
		sprite = Resources.Load<Sprite>(path);
		spriteRenderer.sprite = sprite;
	}

	public void ChangeMaterial(Material material)
	{
		this.material = material;
		spriteRenderer.material = material;
	}
	/// <summary>
	/// Hace un Resources.Load<Material> 
	/// </summary>
	/// <param name="path"></param>
	public void ChangeMaterial(string path)
	{
		this.material = Resources.Load<Material>(path);
		spriteRenderer.material = material;
	}

	public void SetController(TileController tc)
	{
		tileController = tc;
	}
	
	public void SetGlow(Glow glow)
	{
		if(this.glow != null)
		{
			Destroy(this.glow.gameObject);
			this.glow = null;
		}

		this.glow = tileController.GetComponentInChildren<Glow>();
		this.glow.transform.localPosition = Vector3.zero;
	}

	public Glow GetGlow()
	{
		return glow;
	}
}
