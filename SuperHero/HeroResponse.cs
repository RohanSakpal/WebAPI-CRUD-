namespace SuperHero
{
    public class HeroResponse
    {
        public List<SuperHero> SuperHeroList { get; set; } = new List<SuperHero>();
        public int Pages { get; set; }
        public int CurrentPages { get; set; }

    }
}
