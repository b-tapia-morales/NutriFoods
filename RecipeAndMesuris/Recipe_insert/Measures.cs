using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ScrapySharp.Extensions;

namespace RecipeAndMesuris.Recipe_insert;

public class Measures
{
    private IWebDriver _WebDriver;
    private List<Traslate> _nameTraslate;

    public Measures()
    {
        _nameTraslate = new List<Traslate>();
        InsertTraslate();
        _WebDriver = new ChromeDriver();
    }
    public void GetMeasures()
    {
        var directory = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName;
        var path = Path.Combine(directory, "RecipeAndMesuris", "Recipe_insert", "Ingredient", "measures",
            "ingredient_measures","IngreMissing.csv");
        StreamReader file = 
            new StreamReader("C:/Users/Eduardo/RiderProjects/NutriFoods-Latest/RecipeAndMesuris/Recipe_insert/Ingredient/measures/IngreMissing.csv");
        while (!file.EndOfStream)
        {
            string line = file.ReadLine() ?? throw new InvalidOperationException();
            string [] id = line.Split(";");
            if (!id[2].Equals(""))
            {
                GetMeasuresiD(id[1],id[2]);
            }else Console.WriteLine("error!");
        }
        file.Close();
        
        //GetMeasuresiD("aceite","172357");
    }
    
    private void GetMeasuresiD(string nameIngredient, string id)
    {
        Console.WriteLine($"{nameIngredient} {id}");
        _WebDriver.Navigate().GoToUrl($"https://fdc.nal.usda.gov/fdc-app.html#/food-details/{id}/measures");
        Thread.Sleep(3000);
        StreamWriter fileMeasuires =
            new StreamWriter($"C:/Users/Eduardo/RiderProjects/NutriFoods-Latest/RecipeAndMesuris/Recipe_insert/Ingredient/measures/ingredient_measures/{nameIngredient}.csv");
        var data = _WebDriver.FindElements(By.ClassName("tab-content"));
        foreach (var i in data)
        {
            var cleanText = TraslateName(i.Text.Replace("Amount Unit Measure Description Modifier Average Weight (g) n Footnote", "")).Replace(" ",";").CleanInnerText().
                Replace(" 1 ","\n").Replace(" 2 ","\n").Replace(" 3 ","\n").Replace(" 0.5 ","\n").Replace(" 4 ","\n").Replace(" 6 ","\n").
                Replace(" 5 ","\n").Replace(" 0.25 ","\n").Replace(" 50 ","\n").Replace(" 0.33 ","\n").Replace(" 10 ","\n").Replace("1 ","").Replace("2 ","").
                Replace("0.25 ","").Replace("3 ","").Replace("0.5 ","").Replace("4 ","").Replace("9 ","").
                Replace("5 ","").Replace("10 ","");
            fileMeasuires.WriteLine(cleanText);
        }
        fileMeasuires.Close();
    }

    private void InsertTraslate()
    {
        _nameTraslate.Add( new Traslate(""," (1/8\" thick)"));
        _nameTraslate.Add( new Traslate(""," (2-1/2\" dia)"));
        _nameTraslate.Add(new Traslate(""," (1/4\" thick)"));
        _nameTraslate.Add(new Traslate(""," (2-1/\" dia)"));
        _nameTraslate.Add(new Traslate(""," (1-1/2\" dia)"));
        _nameTraslate.Add(new Traslate(""," (3-1/4\" dia)"));
        _nameTraslate.Add(new Traslate(""," (2-3/4\" dia)"));
        _nameTraslate.Add(new Traslate(""," (3\" dia)"));
        _nameTraslate.Add(new Traslate(""," (approx 12\" dia)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (2-1/4\" dia)"));
        _nameTraslate.Add(new Traslate("Paquete","packet (0.5 oz)"));
        _nameTraslate.Add(new Traslate("Mediana","artichoke, medium"));
        _nameTraslate.Add(new Traslate("Grande","artichoke, large"));
        _nameTraslate.Add(new Traslate("Largo","large (2-1/4 per pound, approx 3-3/4\" long, 3\" dia.)"));
        _nameTraslate.Add(new Traslate("oz","oz (19 halves)"));
        _nameTraslate.Add(new Traslate("Cucharada","tbsp, drained"));
        _nameTraslate.Add(new Traslate("Anillo","ring (3\" dia., 1/4\" thick)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (approx 2-3/4\" long, 2-1/2 dia.)"));
        _nameTraslate.Add(new Traslate("Taza,rallada","cup grated"));
        _nameTraslate.Add(new Traslate("Grande","pepper, large (3-3/4\" long, 3\" dia)"));
        _nameTraslate.Add(new Traslate("Anillo","ring (3\" dia., 1/4\" thick)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (approx 2-3/4\" long, 2-1/2 dia.)"));
        _nameTraslate.Add(new Traslate("Taza","cup 1/2\" pieces"));
        _nameTraslate.Add(new Traslate("Porotos(10)","beans (4\" long)"));
        _nameTraslate.Add(new Traslate("Servido","serving (5 fl oz)"));
        _nameTraslate.Add(new Traslate("Largo,extra","spear, extra large (8-3/4\" to 10\" long)"));
        _nameTraslate.Add(new Traslate("Aparejo","jigger (1.5 fl oz)"));
        _nameTraslate.Add(new Traslate("Cubo","cube (6 fl oz prepared)"));
        _nameTraslate.Add(new Traslate("Largo,extra","extra large (1-5/8\" dia)"));
        _nameTraslate.Add(new Traslate("Porotos", "beans (4\" long)"));
        _nameTraslate.Add(new Traslate("Largo","spear (about 5\" long)"));
        _nameTraslate.Add(new Traslate("Cucharada","tbsp, whipped"));
        _nameTraslate.Add(new Traslate("Largo","large (2-1/4 per pound, approx 3-3/4\" long, 3\" dia.)"));
        _nameTraslate.Add(new Traslate("Taza","cup 1/2\" pieces"));
        _nameTraslate.Add(new Traslate("Anillo","ring (3\" dia, 1/4\" thick)"));
        _nameTraslate.Add(new Traslate("Largo","large (2-1/4 per lb, approx 3-3/4\" long, 3\" dia)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (approx 2-3/4\" long, 2-1/2\" dia)"));
        _nameTraslate.Add(new Traslate("Rebanada","slice (4-2/3\" dia x 3/4\" thick)"));
        _nameTraslate.Add(new Traslate("Taza,trozos","cup, chunks"));
        _nameTraslate.Add(new Traslate("Larga,extra","extra large (9\" or longer)"));
        _nameTraslate.Add(new Traslate("Larga","large (8\" to 8-7/8\" long)"));
        _nameTraslate.Add(new Traslate("Pequeña","small (6\" to 6-7/8\" long)"));
        _nameTraslate.Add(new Traslate("Pequeña,extra","extra small (less than 6\" long)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (7\" to 7-7/8\" long)"));
        _nameTraslate.Add(new Traslate("Taza,pure","cup, mashed"));
        _nameTraslate.Add(new Traslate("Paquete","package, small (3 oz)"));
        _nameTraslate.Add(new Traslate("Taza,derretido","cup, melted"));
        _nameTraslate.Add(new Traslate("Envase,grande","container (8 oz)"));
        _nameTraslate.Add(new Traslate("Envase,Mediano","container (6 oz)"));
        _nameTraslate.Add(new Traslate("Envase,pequeño","container (4 oz)"));
        _nameTraslate.Add(new Traslate("Paquete","package (6 oz)"));
        _nameTraslate.Add(new Traslate("Cubo","cubic inch"));
        _nameTraslate.Add(new Traslate("Lamina","slice (1 oz)"));
        _nameTraslate.Add(new Traslate("Taza","cup (8 fl oz)"));
        _nameTraslate.Add(new Traslate("Cucharada","tbsp (1/8 cup)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (3/4\" to 1\" dia)"));
        _nameTraslate.Add(new Traslate("Largo","large (1\" to 1-1/4\" dia)"));
        _nameTraslate.Add(new Traslate("Hoja,larga", "leaf, large"));
        _nameTraslate.Add(new Traslate("Cabeza,mediana","head, medium (about 5-3/4\" dia)"));
        _nameTraslate.Add(new Traslate("Cabeza,pequeña","head, small (about 4-1/2\" dia)"));
        _nameTraslate.Add(new Traslate("Cabeza,larga","head, large (about 7\" dia)"));
        _nameTraslate.Add(new Traslate("Hoja,mediana","leaf, medium"));
        _nameTraslate.Add(new Traslate("Taza,rallada","cup, shredded"));
        _nameTraslate.Add(new Traslate("Taza","cup (1\" cubes)"));
        _nameTraslate.Add(new Traslate("Mediano","medium whole (2-3/5\" dia)"));
        _nameTraslate.Add(new Traslate("Rebanada","slice, medium (1/4\" thick)"));
        _nameTraslate.Add(new Traslate("Largo","large whole (3\" dia)"));
        _nameTraslate.Add(new Traslate("Taza,picada","cup, chopped or sliced"));
        _nameTraslate.Add(new Traslate("Taza","cup cherry tomatoes"));
        _nameTraslate.Add(new Traslate("Pequeño","cherry"));
        _nameTraslate.Add(new Traslate("Cuña","wedge (1/4 of medium tomato)"));
        _nameTraslate.Add(new Traslate("Pluma","plum tomato"));
        _nameTraslate.Add(new Traslate("Tomate","Italian tomato"));
        _nameTraslate.Add(new Traslate("Rebanda,delgada","slice, thin/small"));
        _nameTraslate.Add(new Traslate("Rebanada,gruesa","slice, thick/large (1/2\" thick)"));
        _nameTraslate.Add(new Traslate("Mediano","small whole (2-2/5\" dia)"));
        _nameTraslate.Add(new Traslate("Mediano","medium whole (2-3/5\" dia)"));
        _nameTraslate.Add(new Traslate("Zapallo","squash (4 inch dia)"));
        _nameTraslate.Add(new Traslate("Taza","serving 1 cup"));
        _nameTraslate.Add(new Traslate("Mediana,tira","strip medium"));
        _nameTraslate.Add(new Traslate("Taza,rebanadas","cup strips or slices"));
        _nameTraslate.Add(new Traslate("Pequeño","small (5-1/2\" long)"));
        _nameTraslate.Add(new Traslate("Mediana","tortilla medium (approx 6\" dia)"));
        _nameTraslate.Add(new Traslate("Grande","tortilla (approx 7-8\" dia)"));
        _nameTraslate.Add(new Traslate("Grande,extra","tortilla (approx 10\" dia)"));
        _nameTraslate.Add(new Traslate("Largo,ancho","strip large (3\" long)"));
        _nameTraslate.Add(new Traslate("Largo","large (7-1/4\" to 8-/1/2\" long)"));
        _nameTraslate.Add(new Traslate("Pequeña","Potato small (1-3/4\" to 2-1/2\" dia)"));
        _nameTraslate.Add(new Traslate("Taza","cup, diced"));
        _nameTraslate.Add(new Traslate("Larga","Potato large (3\" to 4-1/4\" dia)"));
        _nameTraslate.Add(new Traslate("Mediana","Potato medium (2-1/4\" to 3-1/4\" dia)"));
        _nameTraslate.Add(new Traslate("Florida","avocado, NS as to Florida or California"));
        _nameTraslate.Add(new Traslate("Cucharadita","pat (1\" sq, 1/3\" high)"));
        _nameTraslate.Add(new Traslate("Lata","can (404 x 307)"));
        _nameTraslate.Add(new Traslate("Mediano","medium (3-3/4\" long)"));
        _nameTraslate.Add(new Traslate("Largo","large (4\" long)"));
        _nameTraslate.Add(new Traslate("Pepino","cucumber (8-1/4\")"));
        _nameTraslate.Add(new Traslate("Grande","link (4\" long x 1-1/8\" dia)"));
        _nameTraslate.Add(new Traslate("Mediano","link, little (2\" long x 3/4\" dia)"));
        _nameTraslate.Add(new Traslate("oz","oz 1 pouch"));
        _nameTraslate.Add(new Traslate("Cucharadita","tsp or 1 packet"));
        _nameTraslate.Add(new Traslate("Muslo","thigh with bone and breading"));
        _nameTraslate.Add(new Traslate("Naranja","fruit without seeds"));
        _nameTraslate.Add(new Traslate("Servido","NLEA Serving"));
        _nameTraslate.Add(new Traslate("Taza,1/4","serving 1/4 cup"));
        _nameTraslate.Add(new Traslate("Mediano","medium (2-1/2\" dia)"));
        _nameTraslate.Add(new Traslate("Pequeño","small (2-1/4\" dia)"));
        _nameTraslate.Add(new Traslate("Largo", "large (2-3/4\" dia)"));
        _nameTraslate.Add(new Traslate("Taza","cup (4.86 large eggs)"));
        _nameTraslate.Add(new Traslate("Largo,extra","extra large"));
        _nameTraslate.Add(new Traslate("Pica","wedge yields"));
        _nameTraslate.Add(new Traslate("Lata","can, 15 oz (303 x 406)"));
        _nameTraslate.Add(new Traslate("Limon","lemon yields"));
        _nameTraslate.Add(new Traslate("Lata","can (5.5 oz)"));
        _nameTraslate.Add(new Traslate("Lata","can (13 oz)"));
        _nameTraslate.Add(new Traslate("Taza,rebanada","cup slices"));
        _nameTraslate.Add(new Traslate("Cubo","pat (1\" sq, 1/3\" high)	"));
        _nameTraslate.Add(new Traslate("Hoja,interior","leaf inner"));
        _nameTraslate.Add(new Traslate("Hoja,exterior","leaf outer"));
        _nameTraslate.Add(new Traslate("Taza","cup shredded"));
        _nameTraslate.Add(new Traslate("Taza","cup, sections"));
        _nameTraslate.Add(new Traslate("Caja","drink box (8.45 fl oz)"));
        _nameTraslate.Add(new Traslate("Rebanada","slice (4-1/4\" x 4-1/4\" x 1/16\")"));
        _nameTraslate.Add(new Traslate("Mediana","head medium (5-6\" dia.)"));
        _nameTraslate.Add(new Traslate("Pequeña","head small (4\" dia.)"));
        _nameTraslate.Add(new Traslate("Larga","head large (6-7\" dia.)"));
        _nameTraslate.Add(new Traslate("Taza,picada","cup chopped (1/2\" pieces)"));
        _nameTraslate.Add(new Traslate("Rebanada,grande","slice, large"));
        _nameTraslate.Add(new Traslate("Cucharada","envelope (1 tbsp)"));
        _nameTraslate.Add(new Traslate("Paquete","package (1 oz)"));
        _nameTraslate.Add(new Traslate("Granada","pomegranate (4\" dia)"));
        _nameTraslate.Add(new Traslate("Taza,seed","cup arils (seed/juice sacs)"));
        _nameTraslate.Add(new Traslate("Taza,rodajas","cup, slivered"));
        _nameTraslate.Add(new Traslate("Taza,ramitas","cup sprigs"));
        _nameTraslate.Add(new Traslate("Rebanada,delgada","slice, thin"));
        _nameTraslate.Add(new Traslate("Cucharada,entero","tsp, whole"));
        _nameTraslate.Add(new Traslate("Cucharadita,entero","tbsp, whole"));
        _nameTraslate.Add(new Traslate("cucharada,hoja","tbsp, leaves"));
        _nameTraslate.Add(new Traslate("cucharadita,hoja","tsp, leaves"));
        _nameTraslate.Add(new Traslate("Medio/kilo","pint as purchased, yields"));
        _nameTraslate.Add(new Traslate("Taza,mitades","cup, halves"));
        _nameTraslate.Add(new Traslate("Largo","large (1-3/8\" dia)"));
        _nameTraslate.Add(new Traslate("Taza,molida","cup, pureed"));
        _nameTraslate.Add(new Traslate("Pequeño","small (1\" dia)"));
        _nameTraslate.Add(new Traslate("Mediana","medium (1-1/4\" dia)"));
        _nameTraslate.Add(new Traslate("Largo,extra","extra large (1-5/8\" dia)"));
        _nameTraslate.Add(new Traslate("Pieza","oz 1 NLEA serving"));
        _nameTraslate.Add(new Traslate("Largo,grande","spear, extra large (8-3/4\" to 10\" long)"));
        _nameTraslate.Add(new Traslate("Larga","spear, large (7-1/4\" to 8-1/2\")"));
        _nameTraslate.Add(new Traslate("Punta","spear tip (2\" long or less)"));
        _nameTraslate.Add(new Traslate("Mediana","spear, medium (5-1/4\" to 7\" long)"));
        _nameTraslate.Add(new Traslate("Pequeña","spear, small (5\" long or less)"));
        _nameTraslate.Add(new Traslate("Lata","can (6 oz)"));
        _nameTraslate.Add(new Traslate("flores","floweret"));
        _nameTraslate.Add(new Traslate("Grande","fruit (2-3/8\" dia)"));
        _nameTraslate.Add(new Traslate("Mediana","fruit (2-1/8\" dia)"));
        _nameTraslate.Add(new Traslate("Rodaja","wedge or slice (1/8 of one 2-1/8\" dia lemon)"));
        _nameTraslate.Add(new Traslate("Taza","cup, whipped"));
        _nameTraslate.Add(new Traslate("Paquete","package (10 oz)"));
        _nameTraslate.Add(new Traslate("Picado","date, pitted"));
        _nameTraslate.Add(new Traslate("Taza,mitades","cup, halves"));
        _nameTraslate.Add(new Traslate("Damasco","apricot"));
        _nameTraslate.Add(new Traslate("Taza,liquido","cup, fluid (yields 2 cups whipped)"));
        _nameTraslate.Add(new Traslate("Taza,rodajas","cup, sliced"));
        _nameTraslate.Add(new Traslate("Taza,chips","cup mini chips"));
        _nameTraslate.Add(new Traslate("Taza,chips","cup chips (6 oz package)"));
        _nameTraslate.Add(new Traslate("pieza","oz (approx 60 pcs)"));
        _nameTraslate.Add(new Traslate("Taza,chips,largos","cup large chips"));
        _nameTraslate.Add(new Traslate("chuleta","chop without refuse (Yield from 1 raw chop, with refuse, weighing 259g)"));
        _nameTraslate.Add(new Traslate("Largo","link (4\" long)"));
        _nameTraslate.Add(new Traslate("1/4,Taza","cup pieces"));
        _nameTraslate.Add(new Traslate("Taza,Picada","cup, chopped or diced"));
        _nameTraslate.Add(new Traslate("Taza,rebanadas","cup, pieces or slices"));
        _nameTraslate.Add(new Traslate("Taza","cup, whole"));
        _nameTraslate.Add(new Traslate("Remolacha","beet (2\" dia)"));
        _nameTraslate.Add(new Traslate("Taza,Picada","cup chopped"));
        _nameTraslate.Add(new Traslate("Porcion","serving (3 oz)"));
        _nameTraslate.Add(new Traslate("Taza,Picada","cup, chopped"));
        _nameTraslate.Add(new Traslate("Taza","cup packed"));
        _nameTraslate.Add(new Traslate("Cucharadita","tsp packed"));
        _nameTraslate.Add(new Traslate("Taza,Media","cup unpacked"));
        _nameTraslate.Add(new Traslate("Cucharadita,media","tsp unpacked"));
        _nameTraslate.Add(new Traslate("cucharadita,brownulado","tsp brownulated"));
        _nameTraslate.Add(new Traslate("cucharada,picada","tbsp, chopped"));
        _nameTraslate.Add(new Traslate("cucharada,picada","tbsp chopped"));
        _nameTraslate.Add(new Traslate("cucharadita,picada","tsp chopped"));
        _nameTraslate.Add(new Traslate("Berenjena","eggplant, unpeeled (approx 1-1/4 lb)"));
        _nameTraslate.Add(new Traslate("Berenjena,pelada","eggplant, peeled (yield from 1-1/4 lb)"));
        _nameTraslate.Add(new Traslate("taza,cubos","cup, cubes"));
        _nameTraslate.Add(new Traslate("Taza","cup leaves, whole"));
        _nameTraslate.Add(new Traslate("servido","NLEA serving"));
        _nameTraslate.Add(new Traslate("Hojas","leaves"));
        _nameTraslate.Add(new Traslate("Ramitas","sprigs"));
        _nameTraslate.Add(new Traslate("Fruta","fruit (2-1/8\" dia)"));
        _nameTraslate.Add(new Traslate("Cucharadita", "tsp"));
        _nameTraslate.Add(new Traslate("Cucharada", "tbsp"));
        _nameTraslate.Add(new Traslate("Pizca", "dash"));
        _nameTraslate.Add(new Traslate("Taza", "cup"));
        _nameTraslate.Add(new Traslate("Cucharada", "tablespoon"));
        _nameTraslate.Add(new Traslate("Cucharadita", "teaspoon"));
        _nameTraslate.Add(new Traslate("Largo", "large"));
        _nameTraslate.Add(new Traslate("pequeño", "small"));
        _nameTraslate.Add(new Traslate("Hoja","leaf"));
        _nameTraslate.Add(new Traslate("Clavo","clove"));
        _nameTraslate.Add(new Traslate("Picadas","choppeds"));
        _nameTraslate.Add(new Traslate("Picada","chopped"));
        _nameTraslate.Add(new Traslate("Aceituna","olive"));
        _nameTraslate.Add(new Traslate("Pimiento","pepper"));
        _nameTraslate.Add(new Traslate("Cucharada","tbsp, drained"));
        _nameTraslate.Add(new Traslate("Almendra","almond"));
        _nameTraslate.Add(new Traslate("oz","oz (23 whole kernels)"));
        _nameTraslate.Add(new Traslate("Taza,entera","cup, whole"));
        _nameTraslate.Add(new Traslate("Taza,rodajas","cup, slivered"));
        _nameTraslate.Add(new Traslate("Taza,molido","cup, ground"));
        _nameTraslate.Add(new Traslate("Taza,Rebanada","cup, sliced"));
        _nameTraslate.Add(new Traslate("Tallo,medio","stalk, medium"));
        _nameTraslate.Add(new Traslate("Tallo,pequeño","stalk, small"));
        _nameTraslate.Add(new Traslate("Tallo,medio","stalk, medium (7-1/2\" - 8\" long)"));
        _nameTraslate.Add(new Traslate("Tallo,pequeño","stalk, small (5\" long)"));
        _nameTraslate.Add(new Traslate("banda","strip"));
        _nameTraslate.Add(new Traslate("Tallo,largo","stalk, large (11\"-12\" long)"));
        _nameTraslate.Add(new Traslate("Banda","strip (4\" long)"));
        _nameTraslate.Add(new Traslate("Tallo,grande","stalk, large"));
        _nameTraslate.Add(new Traslate("cubo","serving 1 cube"));
        _nameTraslate.Add(new Traslate("paquete","serving packet"));
        _nameTraslate.Add(new Traslate("Filete","fillet"));
        _nameTraslate.Add(new Traslate("Remolacha","beet"));
        _nameTraslate.Add(new Traslate("Filete","steak"));
        _nameTraslate.Add(new Traslate("Racimo","bunch"));
        _nameTraslate.Add(new Traslate("Tallo","stalk"));
        _nameTraslate.Add(new Traslate("Lanza","spear"));
        _nameTraslate.Add(new Traslate("oz,sinHueso","oz, boneless"));
        _nameTraslate.Add(new Traslate("","(8 fl oz)"));
        _nameTraslate.Add(new Traslate("","(6 fl oz prepared)"));
        _nameTraslate.Add(new Traslate("oz","fl oz"));
        _nameTraslate.Add(new Traslate("Cabeza","head"));
        _nameTraslate.Add(new Traslate("32,oz","carton 32 oz"));
        _nameTraslate.Add(new Traslate("molido"," ground"));
        _nameTraslate.Add(new Traslate("Porcion","serving"));
        _nameTraslate.Add(new Traslate("Rebanada,mediana","slice, medium"));
        _nameTraslate.Add(new Traslate("Mediano","medium"));
        _nameTraslate.Add(new Traslate("anillos,10","rings"));
        _nameTraslate.Add(new Traslate("Paquete","packet"));
        _nameTraslate.Add(new Traslate("Cuarto","quart"));
        _nameTraslate.Add(new Traslate("Palo","stick"));
        _nameTraslate.Add(new Traslate("Rodaja","slice"));
        _nameTraslate.Add(new Traslate("Pieza","Piece"));
        _nameTraslate.Add(new Traslate("Pieza","piece"));
        _nameTraslate.Add(new Traslate("Piezas","pieces"));
        _nameTraslate.Add(new Traslate("Uva","grape"));
        _nameTraslate.Add(new Traslate("Paquete","package"));
        _nameTraslate.Add(new Traslate("Envase","container"));
        _nameTraslate.Add(new Traslate("Puerro","leek"));
        _nameTraslate.Add(new Traslate("Nueces","nuts"));
        _nameTraslate.Add(new Traslate("Fruta","fruit"));
        
    }

    private String TraslateName(String text)
    {
        foreach (var traslate in _nameTraslate)
        {
            if (text.Contains(traslate.GetTraslateName()))
            {
                text = text.Replace(traslate.GetTraslateName(), traslate.GetNameNormal());
            }
        }

        return text;
    }
}