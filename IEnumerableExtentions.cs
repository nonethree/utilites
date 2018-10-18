public static class IEnumerableExtensions
{
    public static T RandomElement<T>(this IEnumerable<T> sequence)
    {
        var list = sequence.ToList<T>();

        return list.RandomElement();
    }
    public static T RandomElementByWeight<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
    {
        float totalWeight = sequence.Sum(weightSelector);
        float itemWeightIndex = (float)new Random(Guid.NewGuid().GetHashCode()).NextDouble() * totalWeight;
        float currentWeightIndex = 0;

        foreach (var item in from weightedItem in sequence select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
        {
            currentWeightIndex += item.Weight;

            if (currentWeightIndex >= itemWeightIndex)
                return item.Value;

        }

        return default(T);

    }

}