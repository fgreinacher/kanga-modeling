using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    internal class LifelineCollection : KeyedCollection<string, Lifeline>
    {
        public LifelineCollection(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        protected override string GetKeyForItem(Lifeline item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return item.Id;
        }

        public bool TryGetValue(string key, out Lifeline lifeline)
        {
            if (Count == 0)
            {
                lifeline = null;
                return false;
            }
            return Dictionary.TryGetValue(key, out lifeline);
        }
    }
}