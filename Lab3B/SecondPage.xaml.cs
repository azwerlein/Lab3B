
namespace Lab3B;

public partial class SecondPage : ContentPage
{
    int index = 0;

    private Personality resultsOriented = new ();
    private Personality qualityOriented = new ();

    private List<Question> questions;

    public SecondPage()
    {
        InitializeComponent();
        questions = new([
            new Question("Getting the job done is what matters most.", resultsOriented, "images/officeworker.jpg"),
            new Question("I spend more time doing than planning.", resultsOriented, "images/crowd.jpg"),
            new Question("I would pick only perfection over getting the job done fast and simple.", qualityOriented, "images/perfection.jpg"),
            new Question("I strive for maintaining composure regardless of the scenario.", qualityOriented, "images/officeworker.jpg")
        ]);
        WriteQuestion();
    }

    private void WriteQuestion()
    {
        Question nextQuestion = questions[index];
        questionLabel.Text = nextQuestion.Prompt;
        img.Source = nextQuestion.Img;
    }

    private void AnswerQuestion(bool answer)
    {
        if (index >= questions.Count)
        {
            return;
        }
        
        if (answer)
        {
            questions[index].Personality.Value++;
        }

        index++;
        if (index >= questions.Count)
        {
            string type = "results oriented";
            img.Source = "images/trophy.jpg";
            if (qualityOriented > resultsOriented)
            {
                type = "quality oriented";
                img.Source = "images/clipboard.jpg";
            }
            questionLabel.Text = $"Your personality is: {type}";
        }
        else
        {
            WriteQuestion();
        }
    }

    private void OnYes(Object sender, EventArgs e)
    {
        AnswerQuestion(true);
    }

    private void OnNo(Object sender, EventArgs e)
    {
        AnswerQuestion(false);
    }

    class Question
    {
        public string Prompt { get; set; }
        public Personality Personality { get; set; }
        public string Img { get; set; }

        public Question(string prompt, Personality personality, string img)
        {
            this.Prompt = prompt;
            this.Personality = personality;
            this.Img = img;
        }
    }

    class Personality
    {
        public int Value { get; set; }

        public Personality()
        {
            this.Value = 0;
        }

        public static bool operator >(Personality a, Personality b) => a.Value > b.Value;
        public static bool operator <(Personality a, Personality b) => a.Value < b.Value;
    }
}