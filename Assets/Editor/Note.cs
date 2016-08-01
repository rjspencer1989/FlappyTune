using System;
namespace MusicParser{
	public class Note : IEquatable<Note>
	{
		internal Note()
		{
			Type = string.Empty;
			Duration = -1;
			Voice = -1;
			Staff = -1;
			IsChordTone = false;
		}

		public string Type { get; internal set; }
		
		public int Voice { get; internal set; }

		public int Duration { get; internal set; }
		
		public Pitch Pitch { get; internal set; }

		public int Staff { get; internal set; }

		public bool IsChordTone { get; internal set; }

		public bool IsRest { get; internal set; }

		public bool Equals(Note other){
			if(other == null){
				return false;
			}
			return this.Pitch.Equals(other.Pitch);
		}
	}
}
    