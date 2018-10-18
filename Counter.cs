using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Counter : MonoBehaviour {

    private Text text;

    private Coroutine currentCoroutine;
    private long last;
    private long target;
    private long current=0;
    public int maxTotat = 6;
    public int maxDec = 3;

    public long number {
        set {
            if (null != currentCoroutine) StopCoroutine(currentCoroutine);
            if (Mathf.Abs(current - value) < 10) {
                SetNumber(value);
				last = value;
				target = value;
            } else {
                last = current;
                target = value;
				if (gameObject.activeInHierarchy) {
					currentCoroutine = StartCoroutine (MoveNumbers ());
				}
            }
        }
		get {
			return target;
		}
    }

    void Awake()
    {
        text = GetComponentInChildren<Text>();
        if (maxTotat == 0) {
            maxTotat = 6;
            maxDec = 3;
        }
    }

	private System.Action onComplete = null;
	public void OnComplete(System.Action action) {
		onComplete = action;
	}

    IEnumerator MoveNumbers()
    {
        int framesToChangeCounter = 10;

        for (int step = 1; step <= framesToChangeCounter; step++) {
            long n = (long)(last + (double)step / framesToChangeCounter * (target - last));
            SetNumber(n);
            if (current == target) break;
            yield return null;
        }

		if (onComplete != null) {
			onComplete ();
		}
    }

    public void SetNumber(long number)
    {
        current = number;
		text.text = Mathf.Round(number).ToString("0");
	}

	public void ForceSetNumber(long number)
	{
		current = number;
		target = number;
		text.text = Format(number);
	}

	private string Format(long val) {
		if (formatter != null) {
			return formatter (val);
		} else {
			return Mathf.Round(number).ToString();
		}
	}

	public delegate string Formatter(long v);
	private Formatter formatter = null;

	public void SetFormatter(Formatter formatter) {
		this.formatter = formatter;
	}
}