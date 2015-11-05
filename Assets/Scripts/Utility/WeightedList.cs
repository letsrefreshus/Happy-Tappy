using System.Collections.Generic;
using UnityEngine;

//This can be fleshed out more in the future.
public class WeightedList<T>
{
    private List<WeightedItem<T>> _contents;
    private int _totalWeight;

    public WeightedList()
    {
        _contents = new List<WeightedItem<T>>();
    }

    public void addItem(T item, int weight = 1)
    {
        addWeightedItem(new WeightedItem<T>(item, weight));
    }

    public void addWeightedItem(WeightedItem<T> item)
    {
        _contents.Add(item);
        _totalWeight += item.getWeight();
    }

    public T getItem()
    {
        int roll = Random.Range(0, _totalWeight);
        int currentWeight = 0;
        for (int i = 0; i < _contents.Count; i++)
        {
            WeightedItem<T> currentItem = _contents[i];
            currentWeight += currentItem.getWeight();
            if(roll < currentWeight)
            {
                return currentItem.getItem();
            }
        }
        return default(T);
    }
}
