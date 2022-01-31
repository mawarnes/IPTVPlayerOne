using SQLite;

namespace iptvplayer.Models
{
    public class DbEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DbEntity()
        {
        }
    }
}
