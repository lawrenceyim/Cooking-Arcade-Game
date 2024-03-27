public class SceneName 
{
    private SceneName(string name) {
        this.name = name;
    }

    public string name { get; }
    
    public static readonly SceneName INTRO = new SceneName("Intro");
    public static readonly SceneName PIZZA_LETTER = new SceneName("PizzaLetter");
    public static readonly SceneName SALAD_LETTER = new SceneName("SaladLetter");
    public static readonly SceneName MAIN_LEVEL = new SceneName("MainLevel");
    public static readonly SceneName MAIN_MENU = new SceneName("MainMenu");
    public static readonly SceneName TUTORIAL = new SceneName("Tutorial");
    public static readonly SceneName OPTIONS = new SceneName("Options");
    public static readonly SceneName END_LETTER = new SceneName("EndLetter");
    public static readonly SceneName CREDIT_SCENE = new SceneName("Credit");
    public static readonly SceneName RESULT_SCENE = new SceneName("ResultScreen");

}
