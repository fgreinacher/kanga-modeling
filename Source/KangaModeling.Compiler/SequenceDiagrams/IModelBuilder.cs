namespace KangaModeling.Compiler.SequenceDiagrams
{
    internal interface IModelBuilder
    {
        void CreateParticipant(Token id, Token name, bool ensureParticipantExists);
        void AddCallSignal(Token source, Token target, Token name);
        void AddReturnSignal(Token source, Token target, Token name);
        void SetTitle(Token title);
        void AddError(Token invalidToken, string message);
        void Activate(Token target);
        void Deactivate(Token target);
        void StartOpt(Token keyword, Token guardExpression);
        void StartAlt(Token keyword, Token guardExpression);
        void StartElse(Token keyword, Token guardExpression);
        void StartLoop(Token keyword, Token guardExpression);
        void End(Token endToken);
        void Flush();
        void Dispose(Token target);
        void AddCreateSignal(Token source, Token target, Token name);
    }
}