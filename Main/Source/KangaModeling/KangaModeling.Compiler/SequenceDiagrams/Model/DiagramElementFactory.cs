namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
    public class DiagramElementFactory
    {
        public virtual Participant CreateParticipant(Token id, Token name)
        {
            return new Participant(id.Value, name.Value);
        }

        public virtual SignalElement CreateSignal(string signalName, Participant sourceParticipant, Participant targetParticipant, SignalType signalType)
        {
            return new SignalElement(signalName, sourceParticipant, targetParticipant, signalType);
        }

        public virtual LifelineStatusElement CreateLifelineStatusElement(Participant target, ActivationStatus state)
        {
            return new LifelineStatusElement(target, state);
        }
    }
}
