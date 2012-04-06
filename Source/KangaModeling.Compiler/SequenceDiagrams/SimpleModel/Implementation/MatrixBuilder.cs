using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class MatrixBuilder : IModelBuilder
    {
        private readonly Queue<ModelError> m_Errors;

        public MatrixBuilder(Matrix matrix)
        {
            Matrix = matrix;
            m_Errors = new Queue<ModelError>();
        }

        public Matrix Matrix { get; private set; }

        public IEnumerable<ModelError> Errors
        {
            get { return m_Errors; }
        }

        public bool HasParticipant(string name)
        {
            return Matrix.Lifelines.Contains(name);
        }

        public virtual void CreateParticipant(Token id, Token name)
        {
            if (HasParticipant(id.Value))
            {
                AddError(id, "Lifeline with this id already exists.");
            }
            else
            {
                Matrix.CreateLifeline(id.Value, name.Value);
            }
        }

        public void AddCallSignal(Token source, Token target, Token name)
        {
            Signal call = new Call(name.Value);
            AddSignal(source, target, call);
        }

        public void AddReturnSignal(Token source, Token target, Token name)
        {
            Signal returnSignal = new Return(name.Value);
            AddSignal(source, target, returnSignal);
        }

        public void SetTitle(Token title)
        {
            if (Matrix.Title != null)
            {
                AddError(title, "Title is already set. Title can be set only once.");
            }
            Matrix.Title = title.Value;
        }

        public void AddError(Token invalidToken, string message)
        {
            var error = new ModelError(invalidToken, message);
            m_Errors.Enqueue(error);
        }

        public void Activate(Token targetToken)
        {
            Lifeline target;
            if (!TryGetColumnById(targetToken.Value, out target))
            {
                AddError(targetToken, "No such Lifeline");
                return;
            }

            Row startRow = Matrix.LastRow;
            Pin startPin = startRow[target];
            if (startPin.PinType != PinType.In)
            {
                AddError(targetToken, "Unexpected activation. Add call [ANY]->[THIS] before activation.");
                return;
            }


            int level;
            IEnumerable<Pin> openPins = target.State.OpenPins;
            if (openPins.Count() == 0)
            {
                level = 0;
            }
            else
            {
                if (startPin.Orientation == Orientation.Left)
                {
                    target.State.LeftLevel++;
                    level = target.State.LeftLevel;
                }
                else
                {
                    target.State.RightLevel++;
                    level = target.State.RightLevel;
                }
            }

            var endPin = new OpenPin(target, startPin.Orientation);
            target.State.OpenPins.Push(endPin);
            var activity = new Activity(level);
            activity.Connect(startPin, endPin);
        }

        public void Deactivate(Token targetToken)
        {
            Lifeline target;
            if (!TryGetColumnById(targetToken.Value, out target))
            {
                AddError(targetToken, "No such Lifeline");
                return;
            }

            Row endRow = Matrix.LastRow;
            Pin endPin = endRow[target];
            OpenPin lastOpenPin = target.State.OpenPins.Pop();
            Activity lastOpenActivity = lastOpenPin.GetActivity();

            if (endPin.PinType != PinType.In &&
                endPin.Signal.IsReturn)
            {
                ILifeline sourceOfReturn = endPin.Signal.Start.Lifeline;
                ILifeline sourceOfActivation = lastOpenActivity.Start.Signal.Start.Lifeline;

                if (!sourceOfActivation.Equals(sourceOfReturn))
                {
                    AddError(targetToken,
                             string.Format(
                                 "Unexpected deactivation. Return came from '{0}' but activation was initiated by '{1}'",
                                 sourceOfReturn.Id,
                                 sourceOfActivation.Id));
                }
            }
            else
            {
                endRow = Matrix.CreateRow();
                endPin = endRow[target];
            }

            lastOpenActivity.ReconnectEnd(endPin);
        }

        public void StartOpt(Token guardExpression)
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        private bool TryGetColumnById(string name, out Lifeline lifeline)
        {
            return Matrix.Lifelines.TryGetValue(name, out lifeline);
        }

        private void AddSignal(Token sourceToken, Token targetToken, Signal signal)
        {
            Lifeline source;
            if (!TryGetColumnById(sourceToken.Value, out source))
            {
                AddError(sourceToken, "No such Lifeline");
                return;
            }

            Lifeline target;
            if (!TryGetColumnById(targetToken.Value, out target))
            {
                AddError(targetToken, "No such Lifeline");
                return;
            }

            Row row = Matrix.CreateRow();
            signal.Connect(row[source], row[target]);
        }
    }
}