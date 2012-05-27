using System.Collections.Generic;
using KangaModeling.Compiler.Toolbox;

namespace KangaModeling.Compiler.ClassDiagrams
{
    /// <summary>
    /// A scanner for class diagrams.
    /// </summary>
    class ClassDiagramScanner : GenericScanner
    {
        private readonly HashSet<string> _keywords;
        private readonly List<ScannerRule> _rules;

        public ClassDiagramScanner()
        {
            _keywords = new HashSet<string>();
            _keywords.AddRange(new[] { "[", "..", "*", ":", "|", "(", ")", ",", "[", "]", "<", ">", "-", "~", "+", "#" });
            _rules = new List<ScannerRule> {ScannerRules.MatchNumbers, ScannerRules.MatchIdentifier};
        }

        protected override ISet<string> Keywords
        {
            get { return _keywords; }
        }

        protected override ICollection<ScannerRule> Rules
        {
            get { return _rules; }
        }
    
    }
}