using System;
using System.Collections.Generic;
using System.Linq;

namespace KangaModeling.Compiler.SequenceDiagrams.SimpleModel
{
    internal class MatrixBuilder : IModelBuilder
    {
        private readonly ModelErrorsCollection m_Errors;
        private readonly Stack<CombinedFragment> m_Fragments;
        private readonly Matrix m_Matrix;

        public MatrixBuilder(Matrix matrix, ModelErrorsCollection errors)
        {
            m_Matrix = matrix;
            m_Errors = errors;
            m_Fragments = new Stack<CombinedFragment>();
            m_Fragments.Push(matrix.Root);
            CreateOperand(new Token(0, 0, string.Empty));
        }

        #region IModelBuilder Members

        private bool HasParticipant(string name)
        {
            return m_Matrix.Lifelines.Contains(name);
        }

        public virtual void CreateParticipant(Token id, Token name)
        {
            CreateParticipant(id, name, true);
        }

        public virtual void EnsureParticipant(Token id)
        {
            CreateParticipant(id, id, false);
        }

        private void CreateParticipant(Token id, Token name, bool errorIfExists)
        {
            if (HasParticipant(id.Value))
            {
                if (errorIfExists)
                {
                    AddError(id, "Lifeline with this id already exists.");
                }
                return;
            }
            m_Matrix.CreateLifeline(id.Value, name.Value);
        }

        public void AddCallSignal(Token source, Token target, Token name)
        {
            Signal call = ElementFactory.CreateCall(name);
            AddSignal(source, target, call);
        }

        public void AddReturnSignal(Token source, Token target, Token name)
        {
            Signal returnSignal = ElementFactory.CreateReturn(name);
            AddSignal(source, target, returnSignal);
        }

        public void SetTitle(Token title)
        {
            if (!string.IsNullOrEmpty(m_Matrix.Root.Title))
            {
                AddError(title, "Title is already set. Title can be set only once.");
            }
            m_Matrix.Root.SetTitle(title.Value);
        }

        public void AddError(Token invalidToken, string message)
        {
            m_Errors.Add(invalidToken, message);
        }

        public void Activate(Token targetToken)
        {
            Lifeline target;
            if (!TryGetColumnById(targetToken.Value, out target))
            {
                AddError(targetToken, "No such Lifeline");
                return;
            }

            Row startRow = m_Matrix.LastRow;
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

            var endPin = new OpenPin(target, startPin.Orientation, targetToken);
            target.State.OpenPins.Push(endPin);
            Activity activity = ElementFactory.CreateActivity(level);
            activity.Connect(startPin, endPin);
            CurrentOperand().Add(activity);
        }

        public void Deactivate(Token targetToken)
        {
            Lifeline target;
            if (!TryGetColumnById(targetToken.Value, out target))
            {
                AddError(targetToken, "No such Lifeline");
                return;
            }
            Deactivate(target, targetToken, true);
        }

        private void Deactivate(Lifeline target, Token targetToken, bool errorIfUnexpectedDeactivation)
        {
            

            Row endRow = m_Matrix.LastRow;
            Pin endPin = endRow[target];
            if (target.State.OpenPins.Count == 0)
            {
                AddError(targetToken, "Unexpected deactivation. The lifeline must be activated before.");
                return;
            }

            OpenPin lastOpenPin = target.State.OpenPins.Pop();
            Activity lastOpenActivity = lastOpenPin.GetActivity();

            if (endPin.PinType != PinType.In && endPin.Signal != null)
            {
                ILifeline targetOfReturn = endPin.Signal.End.Lifeline;
                ILifeline sourceOfActivation = lastOpenActivity.Start.Signal.Start.Lifeline;

                if (!sourceOfActivation.Equals(targetOfReturn))
                {
                    AddError(targetToken,
                             string.Format(
                                 "Unexpected deactivation. Return came from '{0}' but activation was initiated by '{1}'",
                                 targetOfReturn.Id,
                                 sourceOfActivation.Id));
                }
            }
            else
            {
                endRow = m_Matrix.CreateRow();
                endPin = endRow[target];
            }

            lastOpenActivity.ReconnectEnd(endPin);
        }

        public void StartOpt(Token keyword, Token guardExpression)
        {
            StartFragment(keyword, OperatorType.Opt);
            CreateOperand(guardExpression);
        }

        public void StartAlt(Token keyword, Token guardExpression)
        {
            StartFragment(keyword, OperatorType.Alt);
            CreateOperand(guardExpression);
        }

        public void StartElse(Token keyword, Token guardExpression)
        {
            if (CurrentOperand().Parent.OperatorType != OperatorType.Alt)
            {
                AddError(keyword, "Unexpected else statement. Else can be used only inside alt statement.");
                return;
            }
            CreateOperand(guardExpression);
        }

        public void StartLoop(Token keyword, Token guardExpression)
        {
            StartFragment(keyword, OperatorType.Loop);
            CreateOperand(guardExpression);
        }

        public void End(Token endToken)
        {
            if (!IsEndExpected())
            {
                AddError(endToken, "Unexpected end statement.");
                return;
            }
            m_Fragments.Pop();
        }

        public void Flush()
        {
            DetectActivitiesWithOpenEnd();
            DetectUnclosedCombinedFragments();
        }

        public void Dispose(Token target)
        {
            
        }

        #endregion

        private void DetectActivitiesWithOpenEnd()
        {
            foreach (Lifeline lifeline in m_Matrix.Lifelines)
            {
                foreach (OpenPin openPin in lifeline.State.OpenPins)
                {
                    AddError(openPin.Token, "Activation has no corresponding deactivation");
                    Pin lastPinInLifeLine = m_Matrix.LastRow[openPin.Lifeline.Index];
                    openPin.Activity.ReconnectEnd(lastPinInLifeLine);
                }
            }
        }

        private bool TryGetColumnById(string name, out Lifeline lifeline)
        {
            return m_Matrix.Lifelines.TryGetValue(name, out lifeline);
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

            Row row = m_Matrix.CreateRow();
            signal.Connect(row[source], row[target]);
            CurrentOperand().Add(signal);
        }

        private CombinedFragment CurrentFragment()
        {
            return m_Fragments.Peek();
        }

        private Operand CurrentOperand()
        {
            return CurrentFragment().LastOperand();
        }

        private void StartFragment(Token keyword, OperatorType operatorType)
        {
            var fragment = new CombinedFragment(CurrentOperand(), operatorType, keyword);
            CurrentOperand().Add(fragment);
            m_Fragments.Push(fragment);
        }


        private void CreateOperand(Token guardExpression)
        {
            CombinedFragment parent = CurrentFragment();
            var operand = new Operand(parent, guardExpression.Value);
            parent.Add(operand);
        }

        private bool IsEndExpected()
        {
            return (CurrentFragment().OperatorType != OperatorType.Root);
        }

        private void DetectUnclosedCombinedFragments()
        {
            while (IsEndExpected())
            {
                CombinedFragment fragment = m_Fragments.Pop();
                AddError(fragment.Token, "Unclosed combined fragment. Corresponding end statement is missing.");
            }
        }
    }
}