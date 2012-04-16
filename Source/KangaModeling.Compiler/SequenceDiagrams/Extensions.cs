using System;
using System.Collections.Generic;
using System.Linq;
using KangaModeling.Compiler.SequenceDiagrams.SimpleModel;

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

        public static IEnumerable<ILifeline> Lifelines(this IFragment fragment)
        {
            if (fragment == null) throw new ArgumentNullException("fragment");
            return 
                fragment
                    .Signals
                    .SelectMany(Lifelines)
            .Concat(
                fragment
                    .Activities
                    .Select(activity=>activity.Lifeline));
        }

        public static int Left(this IFragment fragment)
        {
            return 
                fragment
                    .Lifelines()
                    .Min(lifeline => lifeline.Index);
        }

        public static int Right(this IFragment fragment)
        {
            return
                fragment
                    .Lifelines()
                    .Max(lifeline => lifeline.Index);
        }

        public static IEnumerable<int> Rows(this IFragment fragment, bool includeActivityEnd)
        {
            if (fragment == null) throw new ArgumentNullException("fragment");
            return
                fragment
                    .Children
                    .SelectMany(child=> child.Rows(includeActivityEnd))
            .Concat(
                fragment
                    .Signals
                    .Select(signal=>signal.RowIndex))
            .Concat(
                fragment
                    .Activities
                    .SelectMany(activity => activity.Rows(includeActivityEnd)));
        }

        public static IEnumerable<int> Rows(this IActivity activity, bool includeActivityEnd)
        {
            yield return activity.StartRowIndex;
            yield return activity.EndRowIndex;
        }

        public static int Top(this IFragment fragment)
        {
            return 
                fragment
                    .Rows(false)
                    .Min();
        }

        public static int Bottom(this IFragment fragment)
        {
            return 
              fragment
                .Bottom(true);
        }

        public static int Bottom(this IFragment fragment, bool includeActivityEnd)
        {
            return 
                fragment
                    .Rows(includeActivityEnd)
                    .Max();
        }
    }
}