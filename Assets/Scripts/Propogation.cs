using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropagator
{
	Vector2I GridPosition { get; }
	float Value { get; }
}

public class Propogation : MonoBehaviour, IPropagator
{
	public float _value;
	bool _isDead = false;
	public float Value { get { return _value; } }
	public bool IsDead { get { return _isDead; } }

	public InfluenceConnection _server;
	public float changeGoalPeriod = 5f;
	public string type;
	public int squadNo;
	public bool isStaticUnit = false;

	Vector3 _bottomLeft;
	Vector3 _topRight;

	UnityEngine.AI.NavMeshAgent _navAgent;
	//UnitSpecification _properties;

	public Vector2I GridPosition
	{
		get
		{
			return _server.GetGridPosition(transform.position);
		}
	}

	void Start()
	{
		if (!isStaticUnit)
			_navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		//_properties = GetComponentInChildren<Fighter>().unitProperties;
		_server.RegisterPropagator(this, type, squadNo);
		_server.GetMovementLimits(out _bottomLeft, out _topRight);

		if (!isStaticUnit)
			StartCoroutine(ChangeGoalCR());
	}

	void Update()
	{
	}

	public void Dead()
	{
		_isDead = true;
		_server.DeadPropagator(this, type, squadNo);
		StartCoroutine(DestroyCR());
	}

	IEnumerator ChangeGoalCR()
	{
		if (!isStaticUnit)
			while (true)
			{
				Vector3 new_des = PickDestination();
				//_navAgent.speed = _properties.runSpeed;
				_navAgent.SetDestination(new_des);
				yield return new WaitForSeconds(changeGoalPeriod);
			}
	}

	Vector3 PickDestination()
	{
		return new Vector3(
			Random.Range(_bottomLeft.x, _topRight.x),
			Random.Range(_bottomLeft.y, _topRight.y),
			Random.Range(_bottomLeft.z, _topRight.z)
		);
	}

	IEnumerator DestroyCR()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
