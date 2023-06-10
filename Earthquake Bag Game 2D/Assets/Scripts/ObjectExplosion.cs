using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectExplosion : MonoBehaviour
{
    public GameObject[] objects; // Patlamasý istenen nesnelerin listesi.
   // public GameObject explosionPrefab; // Patlama efekti için prefab.
    public float explosionForce = 10f; // Patlama kuvveti.
    public float explosionRadius = 5f; // Patlama yarýçapý.

    private void Start()
    {
        // Array içindeki her nesne için patlama efekti oluþturulur.
        foreach (GameObject obj in objects)
        {
            //GameObject explosion = Instantiate(explosionPrefab, obj.transform.position, Quaternion.identity);

            // Patlama kuvveti hesaplanýr ve patlamaya etki eden nesneler belirlenir.
            Collider[] colliders = Physics.OverlapSphere(obj.transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, obj.transform.position, explosionRadius, 0.5f, ForceMode.Impulse);
                }
            }

            // Patlama efekti silinir.
            //Destroy(explosion, 5f);
        }
    }
}
