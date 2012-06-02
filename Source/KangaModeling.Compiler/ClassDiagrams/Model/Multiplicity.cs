namespace KangaModeling.Compiler.ClassDiagrams.Model
{

    /// <summary>
    /// Kind of a multiplicity.
    /// </summary>
    public enum MultiplicityKind
    {
        None,
        SingleNumber,
        Star
    }

    /// <summary>
    /// Multiplicity of one end of an association.
    /// </summary>
    /// <remarks>
    /// 0..1 -> Multiplicity(SingleNumber, "0", SingleNumber, "1")
    /// 0..* -> Multiplicity(SingleNumber, "0", Star, null)
    /// 3 -> Multiplicity(SingleNumber, "0", None, null)
    /// * -> Multiplicity(Star, null, None, null)
    /// </remarks>
    public class Multiplicity
    {
        public Multiplicity(MultiplicityKind fromKind, string from, MultiplicityKind toKind, string to)
        {
            FromKind = fromKind;
            From = from;
            ToKind = toKind;
            To = to;
        }

        public readonly MultiplicityKind FromKind;
        public readonly string From;
        public readonly MultiplicityKind ToKind;
        public readonly string To;
    }
}