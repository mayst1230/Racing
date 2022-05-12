using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	int direction = 1;

	float high = 2.2f;
	float low = 2.2f; 

	void Update()
	{
		transform.Rotate(0f, 1f, 0f);
	}

	public void Delete()
	{
		Destroy(gameObject);
	}
}
