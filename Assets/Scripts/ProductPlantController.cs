using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    private bool isReadyToPick;
    private Vector3 originalScale;

    [SerializeField] private ProductData _productData;

    private BagController _bagController;
    // Start is called before the first frame update
    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick)
        {
            _bagController = other.GetComponent<BagController>();
            if (_bagController.IsEmptySpace())
            {
                AudioManager.instance.PlayAudio(AudioClipType.grabClip);
                _bagController.AddProductToBag(_productData);
                Debug.Log("Domates Hayırlı Olsun Abe");
                isReadyToPick = false;
                StartCoroutine(ProducdtsPicked()); 
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator ProducdtsPicked()
    {
        Vector3 targetScale = originalScale / 3;
        transform.gameObject.LeanScale(targetScale, 1f);
        yield return new WaitForSeconds(12f);
        transform.gameObject.LeanScale(originalScale, 1f).setEase(LeanTweenType.easeOutBack);
        isReadyToPick = true;
    }


  /*  IEnumerator ProductPicked()
    {
        float duration = 1f;
        float timer = 0;

        Vector3 targetScale = originalScale / 3;

        while (timer < duration)
        {
            float t = timer / duration;
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }
        //fideyi küçültme işlemlerini tamamladık.
        yield return new WaitForSeconds(5f);
        timer = 0f;
        float growBackDuration = 1f;

        while (timer<growBackDuration)
        {
            float t = timer / growBackDuration;
            
            Vector3 newScale = Vector3.Lerp(targetScale,originalScale,t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }
        isReadyToPick = true;
        yield return null;

    }
    
    */
}
