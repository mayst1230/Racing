using System;
using System.IO;
using UnityEngine;

public class Controls : MonoBehaviour
{

	public float speed = 0f;
	public float maxSpeed = 0.5f;
	public float sideSpeed = 0f;

	public float scores = 0f;
	public float highScore = 0f;

	void Start()
	{
		string high = File.ReadAllText("C:/Users/mayst/Desktop/highscore.txt");

		try
		{
			highScore = Convert.ToSingle(high);
		}
		catch(Exception e) 
		{
			Debug.Log(e.ToString());
		}
		
	}

	void Update()
	{
		float moveSide = Input.GetAxis("Horizontal");
		float moveForward = Input.GetAxis("Vertical");

		if(moveSide != 0)
		{
			sideSpeed = moveSide * -1f;
		}
		
		if(moveForward != 0)
		{
			speed += 0.01f * moveForward;
		}
		else
		{
			if(speed > 0)
			{
				speed -= 0.01f;
			}
			else
			{
				speed += 0.01f;
			}
		}

		if(speed > maxSpeed)
		{
			speed = maxSpeed;
		}

	}
}