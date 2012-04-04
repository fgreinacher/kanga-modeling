using System;
using System.Collections.ObjectModel;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal class ParticipantCollection : KeyedCollection<string, Participant>
    {
        public ParticipantCollection() 
            : base()
        {
        }

        public ParticipantCollection(StringComparer comparer)
            : base(comparer)
        {
        }

        protected override string GetKeyForItem(Participant item)
        {
            if (item == null) {throw new ArgumentNullException("item");}
            return item.Id;
        }

        public bool TryGetValue(string key, out Participant participant)
        {
            if (Count == 0)
            {
                participant = null;
                return false;
            }
            return this.Dictionary.TryGetValue(key,out participant);
        }
    }
}
