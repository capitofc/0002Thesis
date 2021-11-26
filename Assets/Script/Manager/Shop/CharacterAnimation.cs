using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
   // [SerializeField] private Vector3 finalPosition;
    //private Vector3 initialPosition;


    // Start is called before the first frame update
    private void Start()
    {
       // initialPosition = transform.position;

    }

    // Update is called once per frame
    private void Update()
    {
       // transform.position = Vector3.Lerp(transform.position, finalPosition, 0.1f);
    }

    private IEnumerator rotate()
    {
        float rotation = 180f;
        while (true)
        {
            rotation += 1;
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0)); // rotate on y axis
            yield return new WaitForSeconds(0.01f);
        }

    }



    private void OnDisable()
    {
        //transform.position = initialPosition;
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        StartCoroutine(rotate());
    }
}
