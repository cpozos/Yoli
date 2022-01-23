namespace Yoli.Core.Domain.Entities
{
    public class Score
    {
        public byte Value { get; set; }
        public string Comments { get; set; }
        public IEnumerable<IScoreProve> Scores { get; set; }
    }
}
