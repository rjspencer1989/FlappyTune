using System.Collections.Generic;

public class PitchComparer : IComparer<Pitch>{
    static Dictionary<char, int> stepOrder = new Dictionary<char, int>();

    public static Dictionary<char, int> getStepOrder(){
        if(stepOrder.Count == 0){
            char[] s = {'C', 'D', 'E', 'F', 'G', 'A', 'B'};
            for (int i = 0; i < s.Length; i++){
                stepOrder.Add(s[i], i + 1);
            }
        }
        return stepOrder;
    }
    public int Compare(Pitch x, Pitch y){
        if(x == null){
            if(y == null){
                return 0;
            } else {
                return -1;
            }
        } else {
            if(y == null){
                return 1;
            } else{
                int retVal = x.Octave.CompareTo(y.Octave);
                if(retVal != 0){
                    return retVal;
                } else{
                    retVal = getStepOrder()[x.Step].CompareTo(getStepOrder()[y.Step]);
                    if(retVal != 0){
                        return retVal;
                    } else {
                        return x.Alter.CompareTo(y.Alter);
                    }
                }
            }
        }
    }
}