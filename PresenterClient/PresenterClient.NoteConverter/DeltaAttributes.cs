using System;

namespace PresenterClient.NoteConverter
{
    public class DeltaAttributes : IEquatable<DeltaAttributes>
    {
        // All public properties should be nullable so the Json serializer can omit them
        public bool? Bold { get; set; }
        public bool? Underline { get; set; }
        public bool? Italic { get; set; }
        public int? Indent { get; set; }
        public List? List { get; set; }
        public string Color { get; set; }
        public int? BottomSpacing { get; set; }


        public bool Equals(DeltaAttributes other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Bold == other.Bold && Underline == other.Underline && Italic == other.Italic &&
                   Indent == other.Indent && List == other.List && Color == other.Color;
        }
    }
}