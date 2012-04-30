using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using KangaModeling.Compiler.SequenceDiagrams;

namespace KangaModeling.Compiler.Test.SequenceDiagrams.EndToEnd.Serailization
{
    public static class SequenceDiagramSerializer
    {
        public static XElement ToXml(this ISequenceDiagram sequenceDiagram)
        {
            return new XElement("SequenceDiagram",
                                new XElement("Lifelines", sequenceDiagram.Lifelines.Select(ToXml)),
                                new XElement("Signals", sequenceDiagram.AllSignals().Select(ToXml)),
                                sequenceDiagram.Root.ToXml(),
                                new XElement("RowCount", sequenceDiagram.RowCount));
        }

        public static XElement ToXml(this ILifeline lifeline)
        {
            return new XElement("Lifeline",
                                new XElement("Id", lifeline.Id),
                                new XElement("Name", lifeline.Name),
                                new XElement("Pins", lifeline.Pins.Select(ToXml)),
                                new XElement("Activities", lifeline.Activities().Select(ToXml)));
        }

        public static XElement ToXml(this IPin pin)
        {
            return new XElement("Pin",
                                new XElement("Level", pin.Level),
                                new XElement("Orientation", pin.Orientation),
                                new XElement("PinType", pin.PinType),
                                new XElement("RowIndex", pin.RowIndex),
                                new XElement("LifelineId", pin.Lifeline.Id));
        }

        public static XElement ToXml(this ISignal signal)
        {
            return new XElement("Signal",
                                new XElement("Name", signal.Name),
                                new XElement("SignalType", signal.SignalType),
                                new XElement("IsSelfSignal", signal.IsSelfSignal),
                                new XElement("Start", signal.Start.ToXml()),
                                new XElement("End", signal.Start.ToXml()));
        }

        public static XElement ToXml(this IActivity activity)
        {
            return new XElement("Activity",
                                new XElement("Level", activity.Level),
                                new XElement("Orientation", activity.Orientation),
                                new XElement("Start", activity.Start),
                                new XElement("End", activity.End));
        }

        public static XElement ToXml(this ICombinedFragment fragment)
        {
            return new XElement("CombinedFragment",
                                new XElement("Level", fragment.Title),
                                new XElement("OperatorType", fragment.OperatorType),
                                new XElement("Operands", fragment.Operands.Select(ToXml)));

        }

        public static XElement ToXml(this IOperand operand)
        {
            return new XElement("Operand",
                                new XElement("GuardExpression", operand.GuardExpression),
                                new XElement("Children", operand.Children.Select(ToXml)),
                                new XElement("Signals", operand.Signals.Select(ToPositionXml)));
        }

        public static XElement ToPositionXml(this ISignal signal)
        {
            return new XElement("SignalPosition",
                                new XElement("Start", signal.Start.ToXml()),
                                new XElement("End", signal.End.ToXml()));
        }

        public static XElement ToXml(this IEnumerable<ModelError> modelErrors)
        {
            return new XElement("ModelErrors", 
                modelErrors.Select(ToXml));
        }

        public static XElement ToXml(this ModelError modelError)
        {
            return new XElement("ModelError",
                                new XElement("Message", modelError.Message),
                                modelError.Token.ToXml());
        }

        public static XElement ToXml(this Token token)
        {
            return new XElement("Token",
                                new XElement("Line", token.Line),
                                new XElement("Start", token.Start),
                                new XElement("End", token.End),
                                new XElement("Value", token.Value));
        }
    }
}