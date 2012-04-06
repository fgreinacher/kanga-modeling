using System.Collections.Generic;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal interface IModelBuilder
    {
        IEnumerable<ModelError> Errors { get; }
        bool HasParticipant(string name);
        void CreateParticipant(Token id, Token name);
        void AddCallSignal(Token source, Token target, Token name);
        void AddReturnSignal(Token source, Token target, Token name);
        void SetTitle(Token title);
        void AddError(Token invalidToken, string message);
        void Activate(Token target);
        void Deactivate(Token target);
        void StartOpt(Token guardExpression);
        void End();
    }
}