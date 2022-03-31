using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Copyright (C) 2012 Anomalous Underdog

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

public enum InfluenceType { NONE, FRIENDLY_UNIT, ENEMY_UNIT, FRIENDLY_TOWER, ENEMY_TOWER, WALL };

public class Influencer : MonoBehaviour
{
	public Transform _bottomLeft;

	public Transform _upperRight;

	public float _gridSize;

	public float _decay = 0.3f;

	public float _momentum = 0.1f;

	public int _updateFrequency = 3;

	InfluenceMap _influenceMap;

	void CreateMap()
	{
		int width = (int)(Mathf.Abs(_upperRight.position.x - _bottomLeft.position.x) / _gridSize);
		int height = (int)(Mathf.Abs(_upperRight.position.z - _bottomLeft.position.z) / _gridSize);

		_influenceMap = new InfluenceMap(width, height, _decay, _momentum);
	}

	public void RegisterPropagator(Propogation p)
	{
		_influenceMap.RegisterPropagator(p);
	}

	public void DeadUnit(Propogation p)
	{
		_influenceMap.DeletePropagator(p);
	}

	public InfluenceMap IM
	{
		get
		{
			return _influenceMap;
		}
	}

	public void StartWork()
	{
		CreateMap();

		InvokeRepeating("PropagationUpdate", 0.001f, 1.0f / _updateFrequency);
	}

	void PropagationUpdate()
	{
		_influenceMap.Propagate();
	}

	void Update()
	{
		_influenceMap.Decay = Mathf.Clamp(_decay, 0, 1);
		_influenceMap.Momentum = Mathf.Clamp(_momentum, 0, 1);

		//		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		//		RaycastHit mouseHit;
		//		if (Physics.Raycast(mouseRay, out mouseHit) && Input.GetMouseButton(0) || Input.GetMouseButton(1))
		//		{
		//			// is it within the grid
		//			// if so, call SetInfluence in that grid position to 1.0
		//			Vector3 hit = mouseHit.point;
		//			if (hit.x > _bottomLeft.position.x && hit.x < _upperRight.position.x && hit.z > _bottomLeft.position.z && hit.z < _upperRight.position.z)
		//			{
		//				Vector2I gridPos = GetGridPosition(hit);
		//
		//				if (gridPos.x < _influenceMap.Width && gridPos.y < _influenceMap.Height)
		//				{
		//					SetInfluence(gridPos, (Input.GetMouseButton(0) ? 1.0f : -1.0f));
		//				}
		//			}
		//		}
	}
}
