using System.Threading.Tasks;

public interface IDbInitializer
{
    void Initialize();
    Task SeedData();

}
