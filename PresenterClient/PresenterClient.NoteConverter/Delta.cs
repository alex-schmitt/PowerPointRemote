using System.Collections.Generic;

namespace PresenterClient.NoteConverter
{
    public class Delta
    {
        public readonly List<DeltaOp> Ops = new List<DeltaOp>();

        public DeltaOp PreviousOp => Ops.Count > 0 ? Ops?[Ops.Count - 1] : null;

        public void AddOp(DeltaOp deltaOp)
        {
            Ops.Add(deltaOp);
        }
    }
}