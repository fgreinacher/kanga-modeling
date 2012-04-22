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
            if (sequenceDiagram == null) {throw new ArgumentNullException("sequenceDiagram");}
            
            return
                sequenceDiagram
                    .Lifelines
                    .SelectMany(InSignals)
                    .Union(
                        sequenceDiagram
                            .Lifelines
                            .SelectMany(OutSignals));
        }
    }
}