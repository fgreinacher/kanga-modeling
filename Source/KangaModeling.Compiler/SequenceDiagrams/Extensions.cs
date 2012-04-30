using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams
{
    public static class Extensions
    {
        public static IEnumerable<IActivity> Activities(this ILifeline lifeline)
        {
            if (lifeline == null) throw new ArgumentNullException("lifeline");
            return
                lifeline
                    .Pins
                    .Where(IsActivityStart)
                    .Select(pin => pin.Activity);
        }

        public static bool IsActivityStart(this IPin pin)
        {
            if (pin == null) throw new ArgumentNullException("pin");
            return
                pin.Activity != null &&
                pin.Activity.Start.Equals(pin);
        }

        public static bool IsActivityEnd(this IPin pin)
        {
            if (pin == null) throw new ArgumentNullException("pin");
            return
                pin.Activity != null &&
                pin.Activity.End.Equals(pin);
        }

        public static bool IsSignalStart(this IPin pin)
        {
            if (pin == null) throw new ArgumentNullException("pin");
            return
                pin.Signal != null &&
                pin.PinType == PinType.Out;
        }

        public static bool IsSignalEnd(this IPin pin)
        {
            if (pin == null) throw new ArgumentNullException("pin");
            return
                pin.Signal != null &&
                pin.PinType == PinType.In;
        }

        public static bool IsEmpty(this IPin pin)
        {
            if (pin == null) throw new ArgumentNullException("pin");
            return
                pin.Activity == null &&
                pin.Signal == null;
        }

        public static IEnumerable<ISignal> Signals(this ILifeline lifeline)
        {
            if (lifeline == null) throw new ArgumentNullException("lifeline");
            return
                lifeline
                    .Pins
                    .Select(pin => pin.Signal)
                    .Where(signal => signal != null);
        }

        public static IEnumerable<ISignal> InSignals(this ILifeline lifeline)
        {
            if (lifeline == null) throw new ArgumentNullException("lifeline");
            return
                lifeline
                    .Pins
                    .Where(IsSignalEnd)
                    .Select(pin => pin.Signal);
        }

        public static IEnumerable<ISignal> OutSignals(this ILifeline lifeline)
        {
            if (lifeline == null) throw new ArgumentNullException("lifeline");
            return
                lifeline
                    .Pins
                    .Where(IsSignalStart)
                    .Select(pin => pin.Signal);
        }

        public static IEnumerable<ISignal> LeftSignals(this ILifeline lifeline)
        {
            if (lifeline == null) throw new ArgumentNullException("lifeline");
            return
                lifeline
                    .Pins
                    .Where(pin => pin.Orientation == Orientation.Left)
                    .Select(pin => pin.Signal)
                    .Where(signal => signal != null);
        }

        public static IEnumerable<ISignal> RightSignals(this ILifeline lifeline)
        {
            if (lifeline == null) throw new ArgumentNullException("lifeline");
            return
                lifeline
                    .Pins
                    .Where(pin => pin.Orientation != Orientation.Left)
                    .Select(pin => pin.Signal)
                    .Where(signal => signal != null);
        }

        public static IEnumerable<ILifeline> Lifelines(this ISignal signal)
        {
            yield return signal.Start.Lifeline;
            yield return signal.End.Lifeline;
        }

        public static IEnumerable<ISignal> AllSignals(this ISequenceDiagram sequenceDiagram)
        {
            if (sequenceDiagram == null)
            {
                throw new ArgumentNullException("sequenceDiagram");
            }

            return
                sequenceDiagram
                    .Lifelines
                    .SelectMany(InSignals)
                    .Union(
                        sequenceDiagram
                            .Lifelines
                            .SelectMany(OutSignals));
        }

        public static IArea GetArea(this IOperand operand)
        {
            if (operand == null) throw new ArgumentNullException("operand");
            IEnumerable<IArea> signalsAreas = operand.Signals.Select(GetArea);
            IEnumerable<IArea> childrenAreas = operand.Children.Select(GetArea);

            return new AreaContainer(signalsAreas.Concat(childrenAreas), false);
        }


        public static IArea GetArea(this ICombinedFragment fragment)
        {
            if (fragment == null) throw new ArgumentNullException("fragment");
            return new AreaContainer(fragment.Operands.Select(GetArea), true);
        }

        public static IArea GetArea(this ISignal signal)
        {
            if (signal == null) throw new ArgumentNullException("signal");
            return
                new LeafArea(
                    Math.Min(signal.Start.LifelineIndex, signal.End.LifelineIndex),
                    Math.Max(signal.Start.LifelineIndex, signal.End.LifelineIndex),
                    Math.Min(signal.Start.RowIndex, signal.End.RowIndex),
                    Math.Max(signal.Start.RowIndex, signal.End.RowIndex));
        }

        public static int LeftDepth(this IArea area)
        {
            return area.DepthWhile(child => child.Left == area.Left);
        }

        public static int RightDepth(this IArea area)
        {
            return area.DepthWhile(child => child.Right == area.Right);
        }

        public static int TopDepth(this IArea area)
        {
            return area.DepthWhile(child => child.Top == area.Top);
        }

        public static int BottomDepth(this IArea area)
        {
            return area.DepthWhile(child => child.Bottom == area.Bottom);
        }

        internal static int DepthWhile(this IArea area, Func<IArea, bool> condition)
        {
            int result =
                area
                    .Children
                    .Where(condition)
                    .Aggregate(0, (current, child) => Math.Max(current, child.DepthWhile(condition)));

            return !area.HasFrame ? result : result + 1;
        }

        public static bool IsEmpty(this IOperand operand)
        {
            return !operand.Signals.Any() && !operand.Children.Any();
        }
    }
}