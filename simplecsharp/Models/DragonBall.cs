namespace simplecsharp.Models
{
    public class DragonBall

    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
    }

    public class DragonBallApiResponse
    {
        public Items items { get; set; }
    }
    

    public class Items
    {
        public string name { get; set; }
        public string image { get; set; }
    }

}