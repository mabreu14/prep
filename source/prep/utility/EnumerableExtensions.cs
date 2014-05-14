using System;
using System.Collections.Generic;
using prep.collections;
using prep.utility.filtering;
using prep.utility.Sorting;

namespace prep.utility
{
  public static class EnumerableExtensions
  {
    public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
    {
      foreach (var item in items) yield return item;
    }

    public static IEnumerable<T> sort_using<T>(this IEnumerable<T> items, IComparer<T> comparer)
    {
      var sorted = new List<T>(items);
      sorted.Sort(comparer);
      return sorted;
    }

    public static SortedEnumerable<T> sort_by<T, AttributeType>(this IEnumerable<T> items,
      IGetAnAttributeValue<T, AttributeType> accessor,
      params AttributeType[] values)
    {
      return new SortedEnumerable<T>(Compare<T>.by(accessor, values),items);
    }

    public static IEnumerable<T> all_items_matching<T>(this IEnumerable<T> items, IMatch<T> match)
    {
      foreach (var item in items)
      {
        if (match(item))
        {
          yield return item;
        }
      }
    }

    public static IEnumerable<T> all_items_matching<T>(this IEnumerable<T> items, IMatchAn<T> criteria)
    {
      return items.all_items_matching(criteria.matches);
    }

    public delegate IMatchAn<ItemToMatch> ExtensionPointConfiguration<ItemToMatch, AttributeType>(
      IProvideAccessToCreateMatchers<ItemToMatch, AttributeType> extension_point_configuration);

    public static IEnumerable<ItemToMatch> where<ItemToMatch, AttributeType>(
      this IEnumerable<ItemToMatch> items, IGetAnAttributeValue<ItemToMatch, AttributeType> accessor,
      ExtensionPointConfiguration<ItemToMatch, AttributeType> configuration)
    {
        var ExtensionPoint = new MatcherCreationExtensionPoint<ItemToMatch, AttributeType>(accessor);
        var criteria = configuration(ExtensionPoint);
        return items.all_items_matching(criteria);
    }
  }
}