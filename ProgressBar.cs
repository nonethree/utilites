using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    private Image barImg;
    private Coroutine currentCoroutine;
    public float fillAmount
    {
        get {
            return barImg.fillAmount;
        }
        set {
            if(null != currentCoroutine) StopCoroutine(currentCoroutine);
            currentCoroutine = StartCoroutine(SetProgressBar(value));
        }
    }
	
	void Awake () {
        barImg = GetComponent<Image>();
	}
    
    IEnumerator SetProgressBar(float amount)
    {
        float prevFill = barImg.fillAmount;
        float timeTotal = 0.5f;
        float time = timeTotal;
       
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            barImg.fillAmount = Utils.EaseIn(amount, prevFill, time / timeTotal);
        }
    }
}
