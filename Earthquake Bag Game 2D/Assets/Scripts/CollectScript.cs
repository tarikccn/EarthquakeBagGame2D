using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    [SerializeField] GameObject collectableGameO;
    [SerializeField] GameObject unCollectableGameO;
    public int score = 0;
    private bool isCollectableEnabled = false;
    private bool isUnCollectableEnabled = false;
    [SerializeField] AudioSource correctSound;
    [SerializeField] AudioSource unCorrectSound;
    public int collectItem = 0;

    public GameScreenManager gsm;
    private void Start()
    {
        unCollectableGameO.SetActive(false);
        collectableGameO.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Collectable"))
        {
            isCollectableEnabled = true;
            collectableGameO.SetActive(true);
            //collectableGameO.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            collectableGameO.transform.position = new Vector3(0.5f, 0.5f, -0.5f);
            collision.gameObject.SetActive(false);
            score++;
            collectItem++;
            Debug.Log(score);
            correctSound.Play();
            
            // Ýþlem 1 saniye sonra tekrar çalýþmayacak þekilde ayarlandý
            Invoke("DisableCollectable", 1.5f);
        }
        else if(collision.gameObject.CompareTag("UnCollectable"))
        {
            isUnCollectableEnabled = true;
            unCollectableGameO.SetActive(true);
            //unCollectableGameO.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            unCollectableGameO.transform.position = new Vector3(0.5f, 0.5f, -0.5f);
            collision.gameObject.SetActive(false);
            score--;
            Debug.Log(score);
            unCorrectSound.Play();
            // Ýþlem 1 saniye sonra tekrar çalýþmayacak þekilde ayarlandý
            Invoke("DisableUnCollectable", 1.5f);
        }
    }

    private void DisableCollectable()
    {
        isCollectableEnabled = false;
        collectableGameO.SetActive(false);
        
        CancelInvoke("DisableCollectable");
    }

    private void DisableUnCollectable()
    {
        isUnCollectableEnabled = false;
        
        unCollectableGameO.SetActive(false);
        CancelInvoke("DisableUnCollectable");
    }
}
