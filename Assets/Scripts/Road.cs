using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Road : MonoBehaviour
{

	public List<GameObject> blocks;
	public GameObject player;
	public GameObject roadPrefab;
	public GameObject carPrefab;
	public GameObject coinPrefab;

	private System.Random rand = new System.Random();

	void Update()
	{
		float x = player.GetComponent<Moving>().rb.position.x;

		var last = blocks[blocks.Count - 1];

		if(x > last.transform.position.x - 24.69f * 10f)
		{
			var block = Instantiate(roadPrefab, new Vector3(last.transform.position.x + 24.69f, last.transform.position.y, last.transform.position.z), Quaternion.identity); 
			block.transform.SetParent(gameObject.transform);
			blocks.Add(block);

			float side = rand.Next(1, 3) == 1 ? -1f : 1f;

			var car = Instantiate(carPrefab, new Vector3(last.transform.position.x + 24.69f, last.transform.position.y + 0.20f, last.transform.position.z + 1.30f * side), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
			car.transform.SetParent(gameObject.transform);

			if(rand.Next(0, 100) > 50)
			{
				var coin = Instantiate(coinPrefab, new Vector3(last.transform.position.x + 24.69f, last.transform.position.y + 1.20f, last.transform.position.z + 1.50f * side * -1f), Quaternion.identity);
				coin.transform.SetParent(gameObject.transform);
			}
		}

		foreach (GameObject block in blocks) 
		{
			bool fetched = block.GetComponent<RoadBlock>().Fetch(x);

			if(fetched)
			{
				blocks.Remove(block);
				block.GetComponent<RoadBlock>().Delete();
			}
		}
	}
}
