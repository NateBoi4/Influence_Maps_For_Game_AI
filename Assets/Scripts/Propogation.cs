using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Copyright (C) 2012 Anomalous Underdog

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

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

	public void SetValue(float v) { _value = v; }
	public void SetGoalPeriod(float period) { changeGoalPeriod = period; }
	public void SetType(string _type) { type = _type; }
	public void SetSquad(int num) { squadNo = num; }

	public void SetPropogationInfo(float v, float period, string _type, int num)
    {
		_value = v;
		changeGoalPeriod = period;
		type = _type;
		squadNo = num;
    }

	public Vector2I GridPosition
	{
		get
		{
			return _server.GetGridPosition(transform.position);
		}
	}

	void Start()
	{
		_server = GameObject.FindGameObjectWithTag("GameController").GetComponent<InfluenceConnection>();
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
