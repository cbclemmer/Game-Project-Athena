using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

	public float speed;
	public string pickUpTag = "PickUp";

	Rigidbody2D rb2d;
	int pickUpsTotal;
	int count;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		pickUpsTotal = GameObject.FindGameObjectsWithTag(pickUpTag).Length;
		count = 0;
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		var movement = new Vector2(moveHorizontal, moveVertical).normalized * speed * Time.deltaTime;
		rb2d.AddForce(movement);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(pickUpTag)) 
		{
			other.gameObject.SetActive(false);
			count += 1;
		}
	}

}