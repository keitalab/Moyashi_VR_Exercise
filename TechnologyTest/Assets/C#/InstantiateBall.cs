using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBall : MonoBehaviour
{
    [SerializeField] private float instantiateTime;
    [SerializeField] private Vector3 addForceQuantity;
    [SerializeField] private GameObject ball;

    void Start()
    {
        StartCoroutine(InjectionBall());
    }

    private IEnumerator InjectionBall()
    {
        while (true)
        {
            GameObject injectionBall = Instantiate(ball, new Vector3(0, 1, 0), Quaternion.identity);
            injectionBall.GetComponent<Rigidbody>().AddForce(addForceQuantity, ForceMode.Impulse);

            yield return new WaitForSeconds(instantiateTime);
        }
    }
}
