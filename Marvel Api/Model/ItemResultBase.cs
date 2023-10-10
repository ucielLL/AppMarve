
using SQLite;
using SQLiteNetExtensions.Attributes;


namespace Marvel_Api.Model
{
    public class ItemBase 
    {
        [PrimaryKey]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string thumbnailPath { get; set; }
    }
    [Table("TableComics")]
    public class ItemResultBase: ItemBase
    {
        [Ignore]
        public Thumbnail thumbnail { get; set; }
         [ForeignKey(typeof(Characters))]
        public int CharactersId { get; set; }
         [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Characters characters { get; set; }
    }
    [Table("Characters")]
    public class Characters 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [OneToMany (CascadeOperations = CascadeOperation.All)]
        public List<Item> items { get; set; }
    }
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string resourceURI { get; set; }
        public string name { get; set; }
        [ForeignKey(typeof(Characters))]
        public int CharacterId { get; set; }

    }
    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }
    }
}