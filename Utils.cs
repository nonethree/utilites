using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class Utils  {

    static public T GetOrAddComponent<T>(this GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null)
        {
            comp = obj.AddComponent<T>();
        }
        return comp;
    }

    static public float EaseOut(float from, float to, float t) 
    {
        return Mathf.Lerp(from, to, - t * (t - 2));
    }

    static public float EaseIn(float from, float to, float t)
    {
        return Mathf.Lerp(from, to, t * t * t);
    }

    static string[] numberNames = { "", "K", "M", "B", "KB", "MB", "BB" };
    static string[] formats = { "0", "0.#", "0.##", "0.###", "0.####", "0.#####" };

    public static string GetNumberRepresentation(long number, int maxDigits = 3, int maxDigitsAfterDot = 3)
    {
        maxDigits = Mathf.Max(3, Mathf.Min(maxDigits, formats.Length));
        float fNumber = number;
        int i = 0;

        while (fNumber > 1000f)
        {
            fNumber /= 1000f;
            ++i;
        }
        int mainDigits = 1;
        while (fNumber / Mathf.Pow(10, mainDigits) > 1f) mainDigits++;
        string result = fNumber.ToString(formats[Mathf.Min(maxDigits - mainDigits, maxDigitsAfterDot)]) + numberNames[i];
        return result;
    }

    public static float TenPower(int power)
    {
        return Mathf.Pow(10, power);
    }

    public static V TryGetOrNew<K, V>(this Dictionary<K, V> dict, K key)
     where V : class, new()
    {
        V val = null;
        if (!dict.TryGetValue(key, out val))
        {
            val = new V();
            dict[key] = val;
        }
        return val;
    }

    public static void DisableAllLayout(this RectTransform root)
    {
        List<Component> layouts = new List<Component>();
        layouts.AddRange(root.GetComponentsInChildren<ContentSizeFitter>());
        layouts.AddRange(root.GetComponentsInChildren<VerticalLayoutGroup>());
        layouts.AddRange(root.GetComponentsInChildren<HorizontalLayoutGroup>());
        layouts.ForEach(l => {
            //if (l.gameObject.name != "TableViewContent")
                Component.Destroy(l);
        });
    }
}
