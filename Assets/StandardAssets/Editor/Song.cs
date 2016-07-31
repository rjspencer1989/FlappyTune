using System.Collections.Generic;

namespace MusicParser{
    public class Song {
        internal Song() {
            Measures = new List<Measure>();
        }

        public List<Measure> Measures { get; internal set; }
    }
}
