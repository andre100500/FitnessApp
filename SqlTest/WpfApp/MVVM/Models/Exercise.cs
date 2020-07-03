namespace WpfApp.Utils
{
    public enum ExercisProgress { Done, UnDone, InProcess}
    public enum ExerciseType { Time, Count}
    public class Exercise
    {
        public string Name { set; get; }
        public int Mass { set; get; }
        public int Count { set; get; }
        public ExerciseType CounterType { set; get; }
        public ExercisProgress CurentProgress { set; get; }
    }
}