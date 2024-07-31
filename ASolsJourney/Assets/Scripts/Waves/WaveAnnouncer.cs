using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveAnnouncer : MonoBehaviour
{
    public Image overlay;
    public TextMeshProUGUI waveLabel;

    private float overlayAlpha = 0.6f;
    private float overlayFadeDuration = 0.5f;
    private float labelFadeDuration = 0.5f;
    private float hideDelay = 1f;

    public static event Action OnWaveAnnounceHidden;

    public void AnnounceWave(int waveNumber)
    {
        Debug.Log("Announce Wave: " + waveNumber);

        // Set correct text
        waveLabel.text = "Trial " + waveNumber;

        // Reset alpha
        overlay.DOFade(0, 0);
        waveLabel.DOFade(0, 0);

        Debug.Log("AH");
        overlay.gameObject.SetActive(true);
        overlay.DOFade(overlayAlpha, overlayFadeDuration)
            .OnComplete(()=> {
                Debug.Log("A1111H");
                
                waveLabel.gameObject.SetActive(true);
                waveLabel.DOFade(1, labelFadeDuration)
                .OnComplete(()=> { 
                    StartCoroutine(HideAnnoucement(hideDelay)); 
                }); 
            });
    }

    private IEnumerator HideAnnoucement(float delay)
    {
        yield return new WaitForSeconds(delay);
        waveLabel.DOFade(0, labelFadeDuration)
            .OnComplete(() => {
                waveLabel.gameObject.SetActive(false);
                overlay.DOFade(0, overlayFadeDuration).OnComplete(OnWaveAnnounceHidden.Invoke);
            });
    }
}
