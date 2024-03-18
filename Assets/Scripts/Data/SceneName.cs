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

}
