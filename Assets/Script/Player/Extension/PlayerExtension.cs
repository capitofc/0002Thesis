using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtension : MonoBehaviour
{
	Rigidbody rb;
	public float pushForce = 5;
	private bool isStuned = false;
	private bool wasStuned = false;
	private bool canMove = true;
	private bool slide = false;
	public float gravity = 10.0f;
	private Vector3 pushDir;

    private void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

    public void HitPlayer(Vector3 velocityF, float time)
	{
		rb.velocity = velocityF;
		pushForce = velocityF.magnitude;
		pushDir = Vector3.Normalize(velocityF);
		StartCoroutine(Decrease(velocityF.magnitude, time));
	}

	private IEnumerator Decrease(float value, float duration)
	{
		if (isStuned)
			wasStuned = true;
		isStuned = true;
		canMove = false;

		float delta = 0;
		delta = value / duration;

		for (float t = 0; t < duration; t += Time.deltaTime)
		{
			yield return null;
			if (!slide) //Reduce the force if the ground isnt slide
			{
				pushForce = pushForce - Time.deltaTime * delta;
				pushForce = pushForce < 0 ? 0 : pushForce;
				//Debug.Log(pushForce);
			}
			rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
		}

		if (wasStuned)
		{
			wasStuned = false;
		}
		else
		{
			isStuned = false;
			canMove = true;
		}
	}
}
