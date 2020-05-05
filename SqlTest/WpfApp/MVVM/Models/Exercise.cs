namespace WpfApp.Utils
{
    public enum ExercisProgress { Doone, UnDone, InProcess}
    public enum ExerciseType { Time, Count}
    public class Exercise
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public string Count { set; get; }
        public ExerciseType CounterType { set; get; }
        public ExercisProgress CurentProgress { set; get; }
    }
}