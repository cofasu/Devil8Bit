using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharAnimationController))]
public class CharBasicController : MonoBehaviour
{
	public CharAnimationController AnimCtrl { get { return m_animCtrl; } }

	public bool IsVisible
	{
		get { return m_animCtrl.TargetSpriteRenderer.enabled; }
		set { SetVisible(value); }
	}

	public Queue QueueTargets = new Queue();

	public Vector3 AddCharPosition
	{
		get { return Vector3.zero; }
		set { QueueTargets.Enqueue(value); }
	}

	private bool _ReadyToNextPos = true;

	private Vector3 _Target;

	[SerializeField]
	private float _MovSpeed = 3;

	[SerializeField]
	private float _Delay = 0f;

	[SerializeField]
	public bool _axisInputMov = false;

	[SerializeField]
	public bool _AestrellaMov = false;

	protected CharAnimationController m_animCtrl;

	public bool isMoving = false;
	public TileController tileController;

	protected void Start()
	{
		m_animCtrl = GetComponent<CharAnimationController>();
	}

	protected void Update()
	{
		if (_axisInputMov)
		{
			float fAxisX = Input.GetAxis("Horizontal");
			float fAxisY = Input.GetAxis("Vertical");
			UpdateMovementByAxis(fAxisX, fAxisY);
		}
		if (_AestrellaMov)
		{
			UpdateMovementByTargets();
		}
	}

	protected void UpdateMovementByTargets()
	{
		if (_ReadyToNextPos && QueueTargets.Count > 0)
		{
			_Target = (Vector3)QueueTargets.Dequeue();
			_ReadyToNextPos = false;
			StartCoroutine(GOTOPOS_CO());
		}
	}
	IEnumerator GOTOPOS_CO()
	{
		while (true)
		{
			float step = _MovSpeed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, _Target, step);

			float checkDirX = transform.position.x - _Target.x;
			float checkDirY = transform.position.y - _Target.y;

			if (Mathf.Abs(checkDirX) > Mathf.Abs(checkDirY))
			{
				if (checkDirX < 0) m_animCtrl.CurrentDir = CharAnimationController.eDir.RIGHT;
				else m_animCtrl.CurrentDir = CharAnimationController.eDir.LEFT;
			}
			else
			{
				if (checkDirY < 0) m_animCtrl.CurrentDir = CharAnimationController.eDir.UP;
				else m_animCtrl.CurrentDir = CharAnimationController.eDir.DOWN;
			}

			float distance = (_Target - transform.position).magnitude;

			if (distance < 0.001f)
			{

				yield return new WaitForSeconds(_Delay);
				_ReadyToNextPos = true;
				yield break;
			}

			yield return null;
		}

	}
	protected void UpdateMovementByAxis(float fAxisX, float fAxisY)
	{

		if (isMoving)
		{
			var move = new Vector3(fAxisX, fAxisY, 0);

			transform.position += move * _MovSpeed * Time.deltaTime;

			m_animCtrl.IsAnimated = true;

			if (Mathf.Abs(fAxisX) > Mathf.Abs(fAxisY))
			{
				if (fAxisX > 0)
					m_animCtrl.CurrentDir = CharAnimationController.eDir.RIGHT;
				else if (fAxisX < 0)
					m_animCtrl.CurrentDir = CharAnimationController.eDir.LEFT;
			}
			else
			{
				if (fAxisY > 0)
					m_animCtrl.CurrentDir = CharAnimationController.eDir.UP;
				else if (fAxisY < 0)
					m_animCtrl.CurrentDir = CharAnimationController.eDir.DOWN;
			}

		}
		else
		{
			m_animCtrl.IsAnimated = false;
		}
	}
	public virtual void SetVisible(bool value)
	{
		m_animCtrl.TargetSpriteRenderer.enabled = value;
	}
}