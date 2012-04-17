using System;
using System.Collections;
using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public class ModelErrors : IEnumerable<ModelError>
    {
        private readonly Queue<ModelError> m_Errors;

        public ModelErrors()
        {
            m_Errors = new Queue<ModelError>();
        }
        
        public IEnumerator<ModelError> GetEnumerator()
        {
            return m_Errors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Token invalidToken, string message)
        {
            var error = new ModelError(invalidToken, message);
            Add(error);
        }

        private void Add(ModelError error)
        {
            m_Errors.Enqueue(error);
        }
    }
}
