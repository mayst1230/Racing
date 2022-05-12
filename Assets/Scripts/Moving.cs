using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Moving : MonoBehaviour
{

	public Rigidbody rb;
	public GameObject car;

	public GameObject brokenPrefab;
	public GameObject modelHolder;

	public Controls control;

	private float speed = 0.1f;

	private float maxSpeed = 0.5f;
	private float minSpeed = 0.1f;

	private bool isAlive = true;
	private bool isKilled = false;

	public List<GameObject> wheels;

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if(isAlive)
		{
			float newSpeed = speed;
			float sideSpeed = 0f;

			if(control != null)
			{
				newSpeed += control.speed;
				sideSpeed = control.sideSpeed;

				control.scores += 0.1f;
			}

			if(newSpeed > maxSpeed)
			{
				newSpeed = maxSpeed;
			}

			if(newSpeed < minSpeed)
			{
				newSpeed = minSpeed;
			}

			transform.position = new Vector3(transform.position.x + newSpeed, transform.position.y, transform.position.z + 0.1f * sideSpeed);

			if(control != null)
			{
				control.sideSpeed = 0f;
			}

			if(tag == "Car") 
			{
				if(transform.position.y < -50f)
				{
					Destroy(gameObject);
				}
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Car")
		{
			isAlive = false;
	 
			if(car != null)
			{
				if(!isKilled)
				{
					Destroy(car);
	 
					var broken = Instantiate(brokenPrefab, transform.position, Quaternion.Euler(new Vector3(0f, -270f, 0f)));
					broken.transform.SetParent(modelHolder.transform);
	 
					isKilled = true;
					StartCoroutine("Die");
				}
			}
		}
 
		if(other.tag == "Coin")
		{
			if(control != null)
			{
				control.scores += 200f;
				
				other.GetComponent<Coin>().Delete();
			}
		}
	}

	IEnumerator Die()
	{
		string path = "C:/Users/mayst/Desktop/highscore.txt";
			using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				byte [] bytes = new byte[Convert.ToInt32(fs.Length)];
		 
				fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
		 
				string high = Encoding.UTF8.GetString(bytes);
		 
				float highScore = 0f;
		 
				try
				{
					highScore = Convert.ToSingle(high);
				}
				catch(Exception e)
				{
					Debug.Log(e.ToString());
				}
		 
				if(highScore < Math.Floor(control.scores))
				{
					byte[] newScores = Encoding.UTF8.GetBytes(Math.Floor(control.scores).ToString());
		 
					fs.Write(newScores, 0, newScores.Length);
				}
			}
		 
			yield return new WaitForSeconds(2f);
			SceneManager.LoadScene("Menu");
	}
}
