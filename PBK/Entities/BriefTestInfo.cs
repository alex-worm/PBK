namespace PBK.Entities
{
    public class BriefTestInfo
    {
        public string Name { get; set; }

        public string TopicName { get; set; }

        public int TotalCorrectAnswers { get; set; }

        public int TotalIncorrectAnswers { get; set; }

        public int PassesNumber { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nPasses number: {PassesNumber}\n" +
                   $"Total correct answers: {TotalCorrectAnswers}\n" +
                   $"Total incorrect answers: {TotalIncorrectAnswers}\n\n";
        }
    }
}